using System.Diagnostics;
using System.ComponentModel;
using System.Threading;
using System.Windows.Controls;
using System.Xml.Linq;
using Accord.IO;

namespace billiard_laser
{
    public partial class BilliardLaserForm : Form
    {
        //video debugging form
        private BallDetectionDebugForm? ballDetectionDebugForm;
        private BallReplacementForm? ballReplacementForm;

        //utility classes
        private readonly CameraController cameraController;
        private readonly ShotDetector shotDetector;
        private readonly CueBallDetector ballDetector;

        //items
        private readonly QueueManager<VideoFrame> rawFrames;
        private readonly QueueManager<VideoFrame> processedFrames;
        private const int maxFrames = 1000; //testing

        private readonly QueueManager<Shot> shots;
        private const int maxShots = 15;

        //flags
        private bool replayInProgress = false;
        private bool loadedVideoStarted = false;

        //fps
        private Stopwatch stopwatch = new Stopwatch();
        private Stopwatch debugStopwatch = new Stopwatch();
        private const int FPS_WINDOW_SIZE = 50;
        private Queue<double> frameProcessingTimes = new Queue<double>();

        private CancellationTokenSource? videoCancellationTokenSource;

        private MediaInputType currentInputType;

        private readonly PlaybackController playbackController;

        public OpenCvSharp.Size OutputVideoResolution { get; private set; }

        private bool DetectingBalls
        {
            get => checkBoxDetectBalls.Checked;
            set
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() => DetectingBalls = value));
                    return;
                }
                checkBoxDetectBalls.Checked = value;
            }
        }

        private PlaybackController.PlaybackState CurrentPlaybackState
        {
            get => playbackController.State;
            set => playbackController.State = value;
        }

        private enum MediaInputType
        {
            Video,
            Camera
        }

        public BilliardLaserForm()
        {
            InitializeComponent();

            playbackController = new PlaybackController(btnPlayPause, btnNextFrame, btnLastFrame);
            playbackController.NextFrameRequested += (s, e) => ShowNextFrame();
            playbackController.LastFrameRequested += (s, e) => ShowPreviousFrame();

            // set output resolution. todo: add user-configurable way
            OutputVideoResolution = ResolutionHelper.GetSize(VideoResolution.P300);

            // initialise core components
            cameraController = new CameraController(cboCamera, OutputVideoResolution);
            shotDetector = new ShotDetector();
            ballDetector = new CueBallDetector();
            rawFrames = new QueueManager<VideoFrame>(maxSize: maxFrames);
            processedFrames = new QueueManager<VideoFrame>(maxSize: maxFrames);
            shots = new QueueManager<Shot>(maxSize: maxShots);

            // wire up events
            shotDetector.ShotFinished += ShotDetector_ShotFinished;
            cameraController.ReceivedFrame += CameraController_ReceivedFrame;

            // configure UI
            pictureBoxImage.SizeMode = PictureBoxSizeMode.Zoom;
            btnShowReplaceBallsForm.Enabled = false;
            listBoxProcessedFrames.DataSource = processedFrames.Indices;
            listBoxShots.DataSource = shots.Indices;
        }

        //before loading camera
        private void SetStateLoadCamera()
        {
            cameraController.StopCameraCapture(); //stop previous camera

            //get rid of current video
            videoCancellationTokenSource?.Cancel();
            videoCancellationTokenSource?.Dispose();
            videoCancellationTokenSource = null;

            ResetFrameQueuesState();
            ResetShotState();

            loadedVideoStarted = false;
            UpdateFpsLabel();

            currentInputType = MediaInputType.Camera;
            CurrentPlaybackState = PlaybackController.PlaybackState.Loading;
        }

        //before loading video
        private void SetStateLoadVideo()
        {
            cameraController.StopCameraCapture();

            // Cancel any previous video or shot processing task
            videoCancellationTokenSource?.Cancel();
            videoCancellationTokenSource?.Dispose();
            videoCancellationTokenSource = null;

            ResetFrameQueuesState();
            ResetShotState();

            loadedVideoStarted = false;
            UpdateFpsLabel();

            currentInputType = MediaInputType.Video;
            CurrentPlaybackState = PlaybackController.PlaybackState.Loading;
        }

        //before playing video
        private void SetStateStartVideo()
        {
            // Cancel any previous video or shot processing task
            videoCancellationTokenSource?.Cancel();
            videoCancellationTokenSource?.Dispose();
            videoCancellationTokenSource = null;

            ResetFrameQueuesState(clearRawFrames: false);
            ResetShotState();

            loadedVideoStarted = true;
            UpdateFpsLabel();

            currentInputType = MediaInputType.Video;
            CurrentPlaybackState = PlaybackController.PlaybackState.Playing;
        }

        private void btnStartCameraInput_Click(object sender, EventArgs e)
        {
            SetStateLoadCamera();
            stopwatch.Restart();
            if (cameraController.StartCameraCapture()) CurrentPlaybackState = PlaybackController.PlaybackState.Playing;
        }

        private void CameraController_ReceivedFrame(object? sender, VideoFrame frame)
        {
            if (currentInputType != MediaInputType.Camera) throw new InvalidOperationException("Camera controller frame received despite not being input media type.");
            try
            {
                
                if (CurrentPlaybackState == PlaybackController.PlaybackState.Playing)
                {
                    rawFrames.Enqueue(frame.Clone());
                    ProcessFrame(frame);
                }
                else {
                    Invoke(new Action(() => ballReplacementForm?.UpdateTableOverlay(frame)));
                }
            }
            finally
            {
                frame.Dispose();
                stopwatch.Restart();
            }
        }

        /// <summary>
        /// load a video's items into memory but do not play it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnLoadVideo_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Video Files (*.mp4;*.avi;*.mkv)|*.mp4;*.avi;*.mkv";
            openFileDialog.Title = "Select a Video File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                SetStateLoadVideo();

                try
                {
                    videoCancellationTokenSource = new CancellationTokenSource();
                    CancellationToken cancellationToken = videoCancellationTokenSource.Token;
                    await VideoFrameLoader.LoadFramesAsync(openFileDialog.FileName, OutputVideoResolution, rawFrames, cancellationToken);

                    if (cancellationToken.IsCancellationRequested)
                    {
                        throw new OperationCanceledException(); //break, dont set to ready state
                    }

                    CurrentPlaybackState = PlaybackController.PlaybackState.Ready;
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Video loading was cancelled");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading video: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Start showing the loaded videos items, detecting balls if enabled
        /// </summary>
        private async void StartLoadedVideo()
        {
            SetStateStartVideo();

            try
            {
                loadedVideoStarted = true;
                await ProcessFramesInLoadedVideo();
            }

            catch (OperationCanceledException ex)
            {
                Console.WriteLine("caught OperationCanceledException in startloadedvideo(): " + ex.Message);
                return;
            }

            CurrentPlaybackState = PlaybackController.PlaybackState.Finished;
            loadedVideoStarted = false;
        }

        /// <summary>
        /// For each frame in the video, perform ball and/or shot tracking
        /// Pauses processing when playback state is paused
        /// </summary>
        private async Task ProcessFramesInLoadedVideo()
        {
            videoCancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = videoCancellationTokenSource.Token;

            var indices = rawFrames.Indices.Cast<int>().ToList(); // create a snapshot of indices to avoid collection being modified
            foreach (int index in indices)
            {
                while (CurrentPlaybackState == PlaybackController.PlaybackState.Paused) await Task.Delay(100, cancellationToken);
                if (cancellationToken.IsCancellationRequested) throw new OperationCanceledException();

                stopwatch.Restart();
                ProcessFrame(rawFrames.GetItem(index));
            }
        }

        //display a selected processed frame of the video and send to debug form if its open
        private void listBoxFrames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxProcessedFrames.SelectedItem is not int selectedIndex) return;

            if (processedFrames.GetItem(selectedIndex) is VideoFrame processedFrame &&
                processedFrame.frame is Bitmap processedBitmap)
            {
                btnShowReplaceBallsForm.Enabled = true;
                UpdatePictureBoxImage(processedBitmap);
                UpdateDebugForms(selectedIndex);
            }
        }

        private void ResetFrameQueuesState(bool clearRawFrames = true)
        {
            if (clearRawFrames) rawFrames.Clear();
            processedFrames.Clear();
            btnShowReplaceBallsForm.Enabled = false;
        }

        private void ShowNextFrame()
        {
            if (listBoxProcessedFrames.SelectedIndex < listBoxProcessedFrames.Items.Count - 1)
                listBoxProcessedFrames.SelectedIndex++;
        }

        private void ShowPreviousFrame()
        {
            if (listBoxProcessedFrames.SelectedIndex > 0)
                listBoxProcessedFrames.SelectedIndex--;
        }

        /// <summary>
        /// Processing involves: finding and showing the balls in the rawFrame and listbox and showing the fps
        /// </summary>
        /// <param name="rawFrame"></param>
        private void ProcessFrame(VideoFrame rawFrame)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ProcessFrame(rawFrame)));
                return;
            }

            if (rawFrame?.frame == null)
            {
                Console.WriteLine("Received null frame or frame data");
                return;
            }

            try
            {
                VideoFrame processedFrame;

                if (DetectingBalls)
                {
                    try
                    {
                        using var results = ballDetector.GetCueBallResults(rawFrame);
                        processedFrame = new VideoFrame(new Bitmap(results?.CueBallHighlighted ?? rawFrame.frame), rawFrame.Index, rawFrame.FrameRate);
                        shotDetector.ProcessFrame(results?.CueBall, processedFrame);

                        ballDetectionDebugForm?.DisplayDebugImages(results); // use precalced images to avoid recalculating the same images for debug when playing
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error during ball detection: {ex.Message}");
                        processedFrame = rawFrame.Clone();
                    }
                }
                else processedFrame = rawFrame.Clone();
              
                processedFrames.Enqueue(processedFrame);
                listBoxProcessedFrames.SelectedIndex = listBoxProcessedFrames.Items.Count - 1; // scrolls and updates pic box and debug forms

                Application.DoEvents(); // todo better way?
                UpdateFpsLabel();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ProcessFrame: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates the PictureBox image, using a clone of the given image.
        /// </summary>
        private void UpdatePictureBoxImage(Bitmap newImage)
        {
            var oldImage = pictureBoxImage.Image;
            pictureBoxImage.Image = new Bitmap(newImage);
            oldImage?.Dispose();
        }

        /// <summary>
        /// display average fps @ resolution. reads currently running stopwatches
        /// </summary>
        private void UpdateFpsLabel()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateFpsLabel));
                return;
            }

            double frameProcessingTime = stopwatch.Elapsed.TotalSeconds + debugStopwatch.Elapsed.TotalSeconds;
            frameProcessingTimes.Enqueue(frameProcessingTime);

            while (frameProcessingTimes.Count > FPS_WINDOW_SIZE) frameProcessingTimes.Dequeue();

            // Calculate FPS using the current window of items
            if (frameProcessingTimes.Count > 0)
            {
                double averageProcessingTime = frameProcessingTimes.Sum() / frameProcessingTimes.Count;
                double fps = averageProcessingTime > 0
                    ? 1.0 / averageProcessingTime
                    : 0;

                labelFrameRate.Text = $"FPS: {fps:F2} @ {OutputVideoResolution.Width}x{OutputVideoResolution.Height}";
            }
        }

        #region Shots

        /// <summary>
        /// replay a selected shot's cueball trajectory over the shot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxShots_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxShots.SelectedItem is int frameIndex && frameIndex >= 0)
                ReplayShotWithBallPath(frameIndex, 60);
        }

        /// <summary>
        /// dispose of the shot data and the listbox they are referenced in
        /// </summary>
        private void DisposeShots()
        {
            shots.Dispose();
        }

        //clear all shot data and everything associated with it
        private void ResetShotState()
        {
            DisposeShots();
            shotDetector.ResetState();
        }

        private void ShotDetector_ShotFinished(object? sender, Shot shot) {
            if (InvokeRequired)
            {
                Invoke(new Action(() => { ShotDetector_ShotFinished(sender, shot); }));
                return;
            }
            shots.Enqueue(shot);
        }

        private async Task ReplayShotWithBallPath(int shotIndex, int replayFPS)
        {
            if (replayInProgress || CurrentPlaybackState == PlaybackController.PlaybackState.Playing)
                return;

            replayInProgress = true;
            videoCancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = videoCancellationTokenSource.Token;

            try
            {
                int delay = (int)Math.Round(1000d / Math.Abs(replayFPS));
                using Shot shot = shots.GetItem(shotIndex).Clone();
                
                for (int i = 0; i < shot.FrameCount; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    using Bitmap frameToDrawOn = shot.GetFrameCopy(i).frame;
                    using Bitmap drawnImage = DrawingHelper.DrawBallPath(shot.cueBallPath, new System.Drawing.Size(OutputVideoResolution.Width, OutputVideoResolution.Height), frameToDrawOn.Size, frameToDrawOn);
                    UpdatePictureBoxImage(drawnImage);

                    await Task.Delay(delay, cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Shot replay ball path cancelled.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during shot replay: {ex.Message}", "Replay Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                replayInProgress = false;
            }
        }

        #endregion

        #region Media Contols

        private void btnPlayPause_Click(object sender, EventArgs e)
        {
            if (currentInputType == MediaInputType.Video && !loadedVideoStarted)
            {
                StartLoadedVideo();
                return;
            }
            playbackController.TogglePlayPause();
        }

        #endregion

        private void btnShowDebugForm_Click(object sender, EventArgs e)
        {
            if (ballDetectionDebugForm?.IsDisposed != false)
            {
                ballDetectionDebugForm = new BallDetectionDebugForm(ballDetector);
                ballDetectionDebugForm.DebugFormClosed += DebugForm_FormClosed;
                ballDetectionDebugForm.Show();

                // init debug form with current frame if one is selected
                if (listBoxProcessedFrames.SelectedItem is int selectedIndex &&
                    rawFrames.GetItem(selectedIndex) is VideoFrame rawFrame)
                {
                    ballDetectionDebugForm.GetAndShowDebugImages(rawFrame);
                }
            }
            else ballDetectionDebugForm.Focus();
        }

        private void UpdateDebugForms(int frameIndex)
        {
            try
            {
                debugStopwatch.Restart();

                if (rawFrames.GetItem(frameIndex) is VideoFrame rawFrame)
                {
                    if (CurrentPlaybackState != PlaybackController.PlaybackState.Playing)
                        ballDetectionDebugForm?.GetAndShowDebugImages(rawFrame);
                    ballReplacementForm?.UpdateTableOverlay(rawFrame);

                    debugStopwatch.Stop();
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Raw frame was disposed. Not sending to debug form!");
            }
        }

        private void btnShowReplaceBallsForm_Click(object sender, EventArgs e)
        {
            if (listBoxProcessedFrames.SelectedItem is int selectedIndex &&
                rawFrames.GetItem(selectedIndex) is VideoFrame rawFrame)
            {
                // new form
                if (ballReplacementForm?.IsDisposed != false)
                {
                    ballReplacementForm = new BallReplacementForm(rawFrame.frame, cameraController);
                    ballReplacementForm.BallReplacementFormClosed += BallReplacementForm_FormClosed;
                    ballReplacementForm.Show();
                }
                // update existing form
                else
                {
                    ballReplacementForm.TargetTableLayout = rawFrame.frame;
                    ballReplacementForm.Focus();
                }
            }
        }

        private void BilliardLaserForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // unwire events first
            shotDetector.ShotFinished -= ShotDetector_ShotFinished;
            cameraController.ReceivedFrame -= CameraController_ReceivedFrame;
            if (ballReplacementForm != null) ballReplacementForm.BallReplacementFormClosed -= BallReplacementForm_FormClosed;
            if (ballDetectionDebugForm != null) ballDetectionDebugForm.DebugFormClosed -= DebugForm_FormClosed;

            // dispose managed resources
            cameraController.Dispose();
            shotDetector.Dispose();
            ballDetector.Dispose();
            rawFrames.Dispose();
            processedFrames.Dispose();

            ballReplacementForm?.Dispose();
            ballDetectionDebugForm?.Dispose();
        }

        private void DebugForm_FormClosed(object? sender, EventArgs e)
        {
            ballDetectionDebugForm = null;
        }

        private void BallReplacementForm_FormClosed(object? sender, EventArgs e)
        {
            ballReplacementForm = null;
        }
    }
}