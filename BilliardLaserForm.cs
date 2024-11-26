using System.Diagnostics;
using System.ComponentModel;
using System.Threading;

namespace billiard_laser
{
    public partial class BilliardLaserForm : Form
    {
        //video debugging form
        private ImageProcessingDebugForm debugForm;

        //utility classes
        private ArduinoController arduinoController;
        private CameraController cameraController;
        private ShotDetector shotDetector;
        private BallDetector ballDetector;

        //selection of output resolutions
        private static OpenCvSharp.Size p200 = new OpenCvSharp.Size(355, 200);
        private static OpenCvSharp.Size p270 = new OpenCvSharp.Size(480, 270);
        private static OpenCvSharp.Size p300 = new OpenCvSharp.Size(534, 300);
        private static OpenCvSharp.Size p360 = new OpenCvSharp.Size(640, 360);
        private static OpenCvSharp.Size p480 = new OpenCvSharp.Size(854, 480);
        private static OpenCvSharp.Size p720 = new OpenCvSharp.Size(1280, 720);
        private static OpenCvSharp.Size p1080 = new OpenCvSharp.Size(1920, 1080);

        //testing output
        private OpenCvSharp.Size outputVideoResolution = p300;

        //frames
        private BindingList<int> processedFrameIndices = new BindingList<int>();
        private Queue<VideoFrame> rawFrames = new Queue<VideoFrame>();
        private Queue<VideoFrame> processedFrames = new Queue<VideoFrame>();
        private const int maxFrames = 1000; //testing

        //flags
        private bool replayInProgress = false;
        private bool loadedVideoStarted = false;

        private PlaybackState playbackState;
        private InputType currentInputType;

        //fps
        private Stopwatch stopwatch = new Stopwatch();
        private Stopwatch debugStopwatch = new Stopwatch();
        private double totalProcessingTime = 0;

        private const string PAUSE_ICON = "⏸";
        private const string PLAY_ICON = "⏵";

        private readonly object rawFramesLock = new object();
        private CancellationTokenSource cancellationTokenSource;

        public bool DetectingBalls
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

        private PlaybackState CurrentPlaybackState
        {
            get => playbackState;
            set
            {
                playbackState = value;
                UpdateMediaControlsState();
            }
        }

        private enum PlaybackState
        {
            Loading,
            Ready,
            Playing,
            Paused,
            Finished
        }

        private enum InputType
        {
            Video,
            Camera
        }

        public BilliardLaserForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBoxImage.SizeMode = PictureBoxSizeMode.Zoom;

            arduinoController = new ArduinoController("COM3"); //TODO find better way to find what port to connect to
            cameraController = new CameraController(cboCamera);
            shotDetector = new ShotDetector();
            ballDetector = new BallDetector();

            shotDetector.ShotFinished += ShotDetector_ShotFinished;
            cameraController.ReceivedFrame += CameraController_ReceivedFrame;

            listBoxProcessedFrames.DataSource = processedFrameIndices;
        }

        private void btnLoadCameraInput_Click(object sender, EventArgs e)
        {
            if (cameraController.StartCameraCapture())
            {
                CurrentPlaybackState = PlaybackState.Playing;
                totalProcessingTime = 0;
                currentInputType = InputType.Camera;
            }
        }

        private void CameraController_ReceivedFrame(object? sender, VideoFrame frame)
        {
            if (CurrentPlaybackState == PlaybackState.Paused)
            {
                frame.Dispose();
                return;
            }

            if (rawFrames.Count > maxFrames)
            {
                var oldFrame = rawFrames.Dequeue();
                oldFrame.Dispose();
            }

            rawFrames.Enqueue(frame);
            ProcessFrame(frame);
        }

        //before loading video
        private void SetStateLoadVideo()
        {
            // Cancel any previous video or shot processing task
            cancellationTokenSource?.Cancel();
            cancellationTokenSource?.Dispose();
            cancellationTokenSource = null;

            // Reset the frame queues
            ResetFrameQueuesState();

            // Reset the shot detection
            ResetShotState();

            // Reset other state variables
            loadedVideoStarted = false;
            totalProcessingTime = 0;
            UpdateFpsLabel(0);

            currentInputType = InputType.Video;
            CurrentPlaybackState = PlaybackState.Loading;
        }

        //before playing video
        private void SetStatePlayVideo() {
            // Cancel any previous video or shot processing task
            cancellationTokenSource?.Cancel();
            cancellationTokenSource?.Dispose();
            cancellationTokenSource = null;

            //reset state
            ResetFrameQueuesState(clearRawFrames: false);
            ResetShotState();

            //reset fps
            totalProcessingTime = 0;
            UpdateFpsLabel(0);

            loadedVideoStarted = true;
            currentInputType = InputType.Video;
            CurrentPlaybackState = PlaybackState.Playing;
        }

        /// <summary>
        /// load a video's frames into memory but do not play it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadVideo_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Video Files (*.mp4;*.avi;*.mkv)|*.mp4;*.avi;*.mkv";
            openFileDialog.Title = "Select a Video File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                SetStateLoadVideo();
                VideoProcessor.EnqueueVideoFrames(openFileDialog.FileName, outputVideoResolution, rawFrames, maxFrames);
                
                //loaded
                CurrentPlaybackState = PlaybackState.Ready;
            }
        }

        /// <summary>
        /// Start showing the loaded videos frames, detecting balls if enabled
        /// </summary>
        private async void StartLoadedVideo()
        {
            loadedVideoStarted = true;
            SetStatePlayVideo();

            try
            {
                await ProcessFramesInLoadedVideo();
            }

            catch (OperationCanceledException ex)
            {
                Console.WriteLine("caught OperationCanceledException in startloadedvideo(): " + ex.Message);
                return;
            }

            CurrentPlaybackState = PlaybackState.Finished;
                loadedVideoStarted = false;
        }


        /// <summary>
        /// For each frame in the video, perform ball and/or shot tracking
        /// Pauses processing when playback state is paused
        /// </summary>
        private async Task ProcessFramesInLoadedVideo()
        {
            totalProcessingTime = 0;
            cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            // Create a local copy of the frames to avoid enumeration modification issues
            var framesCopy = rawFrames.ToList();

            foreach (VideoFrame rawFrame in framesCopy)
            {
                while (CurrentPlaybackState == PlaybackState.Paused)
                {
                    await Task.Delay(100, cancellationToken);
                }

                if (cancellationToken.IsCancellationRequested)
                {
                    // The playback has been stopped, break out of the loop
                    throw new OperationCanceledException();
                }

                ProcessFrame(rawFrame);
            }
        }

        //display a selected processed frame of the video and send to debug form if its open
        private void listBoxFrames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxProcessedFrames.SelectedItem is int selectedIndex)
            {
                var processedFrame = processedFrames.FirstOrDefault(f => f.index == selectedIndex);
                if (processedFrame != null && processedFrame.frame != null)
                {

                    UpdatePictureBoxImage(new Bitmap(processedFrame.frame));

                    var rawFrame = rawFrames.FirstOrDefault(f => f.index == selectedIndex);
                    if (rawFrame != null && rawFrame.frame != null)
                    {
                        try
                        {
                            using (var tempBitmap = new Bitmap(rawFrame.frame))
                            {
                                debugStopwatch.Restart();
                                UpdateDebugForm(tempBitmap);
                                debugStopwatch.Stop();
                                totalProcessingTime += debugStopwatch.Elapsed.TotalSeconds;
                            }
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("Raw frame was disposed. Not sending to debug form!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Raw frame was null. Not sending to debug form!");
                    }
                }
            }
        }

        private void ResetFrameQueuesState(bool clearRawFrames = true)
        {
            if (clearRawFrames)
            {
                VideoProcessor.DequeueVideoFrames(rawFrames);
                rawFrames.Clear();
            }

            VideoProcessor.DequeueVideoFrames(processedFrames);
            processedFrames.Clear();
            processedFrameIndices.Clear();
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

            stopwatch.Restart();

            VideoFrame processedFrame = null;
            ImageProcessingResults results = null;

            try
            {
                using var workingFrame = new VideoFrame(new Bitmap(rawFrame.frame), rawFrame.index);

                if (DetectingBalls)
                {
                    try
                    {
                        results = ballDetector.ProcessTableImage(workingFrame.frame);
                        processedFrame = new VideoFrame(new Bitmap(results.CueBallHighlighted), workingFrame.index);
                        shotDetector.ProcessFrame(results.CueBall, processedFrame);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error during ball detection: {ex.Message}");
                        processedFrame = new VideoFrame(new Bitmap(workingFrame.frame), workingFrame.index);
                    }
                }
                else processedFrame = new VideoFrame(new Bitmap(workingFrame.frame), workingFrame.index);

                AddProcessedFrame(processedFrame);

                listBoxProcessedFrames.SelectedIndex = listBoxProcessedFrames.Items.Count - 1; //scroll and update pic box/dbg form

            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error in ProcessFrame: {ex.Message}");
                processedFrame?.Dispose();
            }

            finally
            {
                results?.Dispose();
            }

            Application.DoEvents(); //todo better way?

            // Performance metrics
            stopwatch.Stop();
            totalProcessingTime += stopwatch.Elapsed.TotalSeconds;
            if (processedFrame != null) UpdateFpsLabel(processedFrame.index);
            else Console.WriteLine("No processed frame available for FPS calculation");
        }

        /// <summary>
        /// Handles adding new processed frame and the listbox for it
        /// </summary>
        /// <param name="newFrame"></param>
        private void AddProcessedFrame(VideoFrame newFrame)
        {
            if (newFrame == null) return;

            try
            {
                // Remove oldest frame if at capacity
                if (processedFrames.Count >= maxFrames)
                {
                    var oldFrame = processedFrames.Dequeue();
                    oldFrame?.Dispose();
                    processedFrameIndices.RemoveAt(0);
                }

                // Add new frame
                processedFrames.Enqueue(newFrame);
                processedFrameIndices.Add(newFrame.index);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error managing frame queue: {ex.Message}");
            }
        }

        // Helper method to update PictureBoxImage safely
        private void UpdatePictureBoxImage(Bitmap newImage)
        {
            var oldImage = pictureBoxImage.Image;
            pictureBoxImage.Image = newImage;
            oldImage?.Dispose();
        }

        //display average fps @ resolution
        private void UpdateFpsLabel(int currentFrameIndex)
        {
            var fps = currentFrameIndex / totalProcessingTime;
            labelFrameRate.Text = $"FPS: {fps:F2} @ {outputVideoResolution.Width}x{outputVideoResolution.Height}";
        }

        #region Shots

        /// <summary>
        /// replay a selected shot's cueball trajectory over the shot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxShots_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxShots.SelectedIndex >= 0)
            {
                Shot selectedShot = (Shot)listBoxShots.SelectedItem;
                ReplayShotWithBallPath(selectedShot, 60);
            }
        }

        /// <summary>
        /// dispose of the shot data and the listbox they are referenced in
        /// </summary>
        private void DisposeShots()
        {
            // Dispose each Shot object before clearing the list
            foreach (Shot shot in listBoxShots.Items) shot.Dispose();
            listBoxShots.Items.Clear();
        }

        //clear all shot data and everything associated with it
        private void ResetShotState()
        {
            DisposeShots();
            shotDetector.ResetState();
        }

        private void ShotDetector_ShotFinished(object sender, Shot shot) => listBoxShots.Items.Add(shot);

        private async Task ReplayShotWithBallPath(Shot shot, int replayFPS)
        {
            if (replayInProgress || CurrentPlaybackState == PlaybackState.Playing)
                return;

            replayInProgress = true;
            cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            try
            {
                int delay = (int)Math.Round(1000d / Math.Abs(replayFPS));
                for (int i = 0; i < shot.FrameCount; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    using (Bitmap frameToDrawOn = shot.GetFrameCopy(i).frame)
                    {
                        Bitmap drawnImage = DrawingHelper.DrawBallPath(shot.cueBallPath, new System.Drawing.Size(outputVideoResolution.Width, outputVideoResolution.Height), frameToDrawOn.Size, frameToDrawOn);
                        UpdatePictureBoxImage(drawnImage);
                        await Task.Delay(delay, cancellationToken);
                    }
                }
            }
            catch (OperationCanceledException e)
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

        private void UpdateMediaControlsState()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateMediaControlsState));
                return;
            }

            switch (CurrentPlaybackState)
            {
                case PlaybackState.Loading:
                    btnPlayPause.Text = PLAY_ICON;
                    btnPlayPause.Enabled = false;
                    btnNextFrame.Enabled = false;
                    btnLastFrame.Enabled = false;
                    break;

                case PlaybackState.Ready:
                    btnPlayPause.Text = PLAY_ICON;
                    btnPlayPause.Enabled = true;
                    btnNextFrame.Enabled = false;
                    btnLastFrame.Enabled = false;
                    break;

                case PlaybackState.Playing:
                    btnPlayPause.Text = PAUSE_ICON;
                    btnPlayPause.Enabled = true;
                    btnNextFrame.Enabled = false;
                    btnLastFrame.Enabled = false;
                    break;

                case PlaybackState.Paused:
                    btnPlayPause.Text = PLAY_ICON;
                    btnPlayPause.Enabled = true;
                    btnNextFrame.Enabled = true;
                    btnLastFrame.Enabled = true;
                    break;

                case PlaybackState.Finished:
                    btnPlayPause.Text = PLAY_ICON;
                    btnPlayPause.Enabled = true; //can replay
                    btnNextFrame.Enabled = true;
                    btnLastFrame.Enabled = true;
                    break;
            }
        }

        //go back a rawFrame
        private void btnLastFrame_Click(object sender, EventArgs e)
        {
            if (listBoxProcessedFrames.SelectedIndex > 0) listBoxProcessedFrames.SelectedIndex -= 1;
        }

        //go forward a rawFrame
        private void btnNextFrame_Click(object sender, EventArgs e)
        {
            if (listBoxProcessedFrames.SelectedIndex < (listBoxProcessedFrames.Items.Count - 1)) listBoxProcessedFrames.SelectedIndex += 1;
        }

        /// <summary>
        /// Start to play the loaded video and allow it to be paused/resumed on demand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlayPause_Click(object sender, EventArgs e)
        {
            if (!loadedVideoStarted)
            {
                StartLoadedVideo();
                return;
            }

            // Toggle between Play and Pause states
            CurrentPlaybackState = (CurrentPlaybackState == PlaybackState.Playing)
                ? PlaybackState.Paused
                : PlaybackState.Playing;
        }

        #endregion

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CurrentPlaybackState = PlaybackState.Ready;
            cameraController.StopCameraCapture();

            processedFrameIndices.Clear();

            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();

            // Dispose of all frames in the queues
            while (rawFrames.Count > 0)
            {
                var frame = rawFrames.Dequeue();
                frame.frame.Dispose();
            }

            while (processedFrames.Count > 0)
            {
                var frame = processedFrames.Dequeue();
                frame.frame.Dispose();
            }

            if (pictureBoxImage.Image != null)
            {
                pictureBoxImage.Image.Dispose();
                pictureBoxImage.Image = null;
            }
        }

        private void buttonShowDebugForm_Click(object sender, EventArgs e)
        {
            if (debugForm == null || debugForm.IsDisposed)
            {
                debugForm = new ImageProcessingDebugForm(ballDetector);

                debugForm.DebugFormClosed += DebugForm_DebugFormClosed;
                debugForm.Show();

                //init debug form with current (raw) selected rawFrame
                if (listBoxProcessedFrames.SelectedItem is int selectedIndex)
                {
                    var rawFrame = rawFrames.FirstOrDefault(f => f.index == selectedIndex); //bug: sometimes last frame disposed
                    if (rawFrame != null) debugForm.ShowDebugImages(rawFrame.frame);
                    else Console.WriteLine("Raw rawFrame was null. not sending to debug form!");
                }
            }

            else debugForm.Focus();
        }

        private void DebugForm_DebugFormClosed(object sender, EventArgs e)
        {
            if (debugForm != null)
            {
                debugForm.Dispose();
                debugForm = null;
            }
        }

        private void UpdateDebugForm(Bitmap rawImage) => debugForm?.ShowDebugImages(rawImage);
    }
}