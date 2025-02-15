﻿using System.Diagnostics;
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
        private BallDetectionDebugForm ballDetectionDebugForm;
        private BallReplacementForm ballReplacementForm;

        //utility classes
        private CameraController cameraController;
        private ShotDetector shotDetector;
        private CueBallDetector ballDetector;

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
        private FrameQueueManager<VideoFrame> rawFrames;
        private FrameQueueManager<VideoFrame> processedFrames;
        private const int maxFrames = 1000; //testing

        //flags
        private bool replayInProgress = false;
        private bool loadedVideoStarted = false;

        private PlaybackState playbackState;
        private InputType currentInputType;

        //fps
        private Stopwatch stopwatch = new Stopwatch();
        private Stopwatch debugStopwatch = new Stopwatch();
        private const int FPS_WINDOW_SIZE = 30;
        private Queue<double> frameProcessingTimes = new Queue<double>();

        private const string PAUSE_ICON = "⏸";
        private const string PLAY_ICON = "⏵";

        private CancellationTokenSource videoCancellationTokenSource;

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
            btnShowReplaceBallsForm.Enabled = false;

            cameraController = new CameraController(cboCamera, outputVideoResolution);
            shotDetector = new ShotDetector();
            ballDetector = new CueBallDetector();

            shotDetector.ShotFinished += ShotDetector_ShotFinished;
            cameraController.ReceivedFrame += CameraController_ReceivedFrame;

            rawFrames = new FrameQueueManager<VideoFrame>(maxFrames: maxFrames);
            processedFrames = new FrameQueueManager<VideoFrame>(maxFrames: maxFrames);

            listBoxProcessedFrames.DataSource = processedFrames.FrameIndices;
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

            currentInputType = InputType.Camera;
            CurrentPlaybackState = PlaybackState.Loading;
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

            currentInputType = InputType.Video;
            CurrentPlaybackState = PlaybackState.Loading;
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

            currentInputType = InputType.Video;
            CurrentPlaybackState = PlaybackState.Playing;
        }

        private void btnStartCameraInput_Click(object sender, EventArgs e)
        {
            SetStateLoadCamera();
            if (cameraController.StartCameraCapture()) CurrentPlaybackState = PlaybackState.Playing;
        }

        private void CameraController_ReceivedFrame(object? sender, VideoFrame frame)
        {
            try
            {
                if (CurrentPlaybackState == PlaybackState.Paused || currentInputType != InputType.Camera)
                {
                    ballReplacementForm?.UpdateTableOverlay(frame); //send to replacement form even if main form is not processing ball detection
                    frame.Dispose();
                    return;
                }

                rawFrames.Enqueue(frame);
                ProcessFrame(frame);
            }

            catch (Exception ex)
            {
                Console.WriteLine("exception in camera controller received frame: " + ex.Message);
                return;
            }
        }

        /// <summary>
        /// load a video's frames into memory but do not play it
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
                    await VideoFrameLoader.LoadFramesAsync(openFileDialog.FileName, outputVideoResolution, rawFrames, cancellationToken);
                    
                    if (cancellationToken.IsCancellationRequested)
                    {
                        throw new OperationCanceledException(); //break, dont set to ready state
                    }
                    
                    CurrentPlaybackState = PlaybackState.Ready;
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
        /// Start showing the loaded videos frames, detecting balls if enabled
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

            CurrentPlaybackState = PlaybackState.Finished;
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
                var processedFrame = processedFrames.GetFrame(selectedIndex);
                if (processedFrame != null && processedFrame.frame != null)
                {
                    btnShowReplaceBallsForm.Enabled = true; //there are now balls to replace
                    UpdatePictureBoxImage(new Bitmap(processedFrame.frame));

                    var rawFrame = rawFrames.GetFrame(selectedIndex);
                    if (rawFrame != null && rawFrame.frame != null)
                    {
                        try
                        {
                            debugStopwatch.Restart();
                            UpdateDebugForms(rawFrame);
                            debugStopwatch.Stop();   
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("Raw frame was disposed. Not sending to debug form!");
                        }
                    }
                    else Console.WriteLine("Raw frame was null. Not sending to debug form!");
                }
            }
        }

        private void ResetFrameQueuesState(bool clearRawFrames = true)
        {
            if (clearRawFrames) rawFrames.Clear();
            processedFrames.Clear();
            btnShowReplaceBallsForm.Enabled = false;
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
            CueBallDetectionResults results = null;

            try
            {
                using var workingFrame = rawFrame.Clone();

                if (DetectingBalls)
                {
                    try
                    {
                        results = ballDetector.GetCueBallResults(workingFrame);
                        processedFrame = new VideoFrame(new Bitmap(results.CueBallHighlighted), workingFrame.Index, workingFrame.FrameRate);
                        shotDetector.ProcessFrame(results.CueBall, processedFrame);

                        ballDetectionDebugForm?.DisplayDebugImages(results); //use precalced images to avoid recalculating the same images for debug when playing
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error during ball detection: {ex.Message}");
                        processedFrame = workingFrame.Clone();
                    }
                }
                else processedFrame = workingFrame.Clone();

                processedFrames.Enqueue(processedFrame);
                listBoxProcessedFrames.SelectedIndex = listBoxProcessedFrames.Items.Count - 1; //scrolls and updates pic box and debug forms

                Application.DoEvents(); //todo better way?
                UpdateFpsLabel();
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error in ProcessFrame: {ex.Message}");
                processedFrame?.Dispose();
            }

            finally
            {
                results?.Dispose();
                stopwatch.Stop();
                debugStopwatch.Stop();
            }
        }

        /// <summary>
        /// Helper method to update PictureBoxImage safely
        /// </summary>
        /// <param name="newImage"></param>
        private void UpdatePictureBoxImage(Bitmap newImage)
        {
            var oldImage = pictureBoxImage.Image;
            pictureBoxImage.Image = newImage;
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

            // Maintain only the last 30 frame processing times
            if (frameProcessingTimes.Count > FPS_WINDOW_SIZE)
            {
                frameProcessingTimes.Dequeue();
            }

            // Calculate FPS using the current window of frames
            if (frameProcessingTimes.Count > 0)
            {
                double averageProcessingTime = frameProcessingTimes.Sum() / frameProcessingTimes.Count;
                double fps = averageProcessingTime > 0
                    ? 1.0 / averageProcessingTime
                    : 0;

                labelFrameRate.Text = $"FPS: {fps:F2} @ {outputVideoResolution.Width}x{outputVideoResolution.Height}";
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
            videoCancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = videoCancellationTokenSource.Token;

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
            if (currentInputType == InputType.Video && !loadedVideoStarted)
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

            videoCancellationTokenSource?.Cancel();
            videoCancellationTokenSource?.Dispose();

            rawFrames.Dispose();
            processedFrames.Dispose();

            if (pictureBoxImage.Image != null)
            {
                pictureBoxImage.Image.Dispose();
                pictureBoxImage.Image = null;
            }
        }

        private void btnShowDebugForm_Click(object sender, EventArgs e)
        {
            if (ballDetectionDebugForm == null || ballDetectionDebugForm.IsDisposed)
            {
                ballDetectionDebugForm = new BallDetectionDebugForm(ballDetector);

                ballDetectionDebugForm.DebugFormClosed += DebugForm_FormClosed;
                ballDetectionDebugForm.Show();

                //init debug form with current (raw) selected rawFrame
                if (listBoxProcessedFrames.SelectedItem is int selectedIndex)
                {
                    var rawFrame = rawFrames.GetFrame(selectedIndex);
                    if (rawFrame != null) ballDetectionDebugForm.GetAndShowDebugImages(rawFrame);
                    else Console.WriteLine("Raw rawFrame was null. not sending to debug form!");
                }
            }

            else ballDetectionDebugForm.Focus();
        }

        private void DebugForm_FormClosed(object sender, EventArgs e)
        {
            if (ballDetectionDebugForm != null)
            {
                ballDetectionDebugForm.Dispose();
                ballDetectionDebugForm = null;
            }
        }

        //if open, send our raw frame to the ball replacement and image processing debug forms
        private void UpdateDebugForms(VideoFrame originalFrame) {
            if (CurrentPlaybackState != PlaybackState.Playing) ballDetectionDebugForm?.GetAndShowDebugImages(originalFrame);
            ballReplacementForm?.UpdateTableOverlay(originalFrame);
        }

        #region Ball Replacement Form

        private void BallReplacementForm_FormClosed(object sender, EventArgs e)
        {
            if (ballReplacementForm != null)
            {
                ballReplacementForm.Dispose();
                ballReplacementForm = null;
            }
        }

        private void btnShowReplaceBallsForm_Click(object sender, EventArgs e)
        {
            if (listBoxProcessedFrames.SelectedItem is int selectedIndex)
            {
                var rawFrame = rawFrames.GetFrame(selectedIndex);

                if (rawFrame != null)
                {
                    if (ballReplacementForm == null || ballReplacementForm.IsDisposed)
                    {
                        // Create new form if it doesn't exist
                        ballReplacementForm = new BallReplacementForm(rawFrame.frame, cameraController);
                        ballReplacementForm.BallReplacementFormClosed += BallReplacementForm_FormClosed;
                        ballReplacementForm.Show();
                    }
                    else
                    {
                        // Update existing form with current frame
                        ballReplacementForm.TargetTableLayout = rawFrame.frame;
                        ballReplacementForm.Focus();
                    }
                }
                else
                {
                    Console.WriteLine("Raw frame was null. Not sending to debug form!");
                }
            }
        }
        #endregion
    }
}