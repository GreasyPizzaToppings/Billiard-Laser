using System.Diagnostics;
using System.ComponentModel;
using OpenCvSharp;
using System.Windows.Controls;

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
        private static OpenCvSharp.Size p250 = new OpenCvSharp.Size(444, 250);
        private static OpenCvSharp.Size p360 = new OpenCvSharp.Size(640, 360);
        private static OpenCvSharp.Size p480 = new OpenCvSharp.Size(854, 480);
        private static OpenCvSharp.Size p720 = new OpenCvSharp.Size(1280, 720);
        private static OpenCvSharp.Size p1080 = new OpenCvSharp.Size(1920, 1080);

        //testing output
        private OpenCvSharp.Size outputVideoResolution = p360;

        //frames
        private BindingList<int> processedFrameIndices = new BindingList<int>();
        private Queue<VideoFrame> rawFrames = new Queue<VideoFrame>();
        private Queue<VideoFrame> processedFrames = new Queue<VideoFrame>();
        private const int maxFrames = 1000;

        //flags
        private bool detectingBalls = false;
        private bool replayInProgress = false;

        private InputType currentInputType;

        //fps
        private Stopwatch stopwatch = new Stopwatch();
        private double totalProcessingTime = 0;

        public enum InputType
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

        private void btnLaserOn_Click(object sender, EventArgs e) => arduinoController.LaserOn();
        private void btnLaserOff_Click(object sender, EventArgs e) => arduinoController.LaserOff();
        private void btnUp_Click(object sender, EventArgs e) => arduinoController.MoveUp();
        private void btnLeft_Click(object sender, EventArgs e) => arduinoController.MoveLeft();
        private void btnRight_Click(object sender, EventArgs e) => arduinoController.MoveRight();
        private void btnDown_Click(object sender, EventArgs e) => arduinoController.MoveDown();

        private void btnGetCameraInput_Click(object sender, EventArgs e)
        {
            if (cameraController.StartCameraCapture())
            {
                totalProcessingTime = 0;
                btnDetectBalls.Enabled = true;
                currentInputType = InputType.Camera;
            }

            else btnDetectBalls.Enabled = false;
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openfiledialog = new OpenFileDialog())
            {
                openfiledialog.Filter = "image files (*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp|all files (*.*)|*.*";
                openfiledialog.RestoreDirectory = true;

                if (openfiledialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        UpdatePictureBoxImage(new Bitmap(openfiledialog.FileName));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("error loading image: " + ex.Message);
                    }
                }
            }
        }

        private void buttonShowDebugForm_Click(object sender, EventArgs e)
        {
            if (debugForm == null || debugForm.IsDisposed)
            {
                debugForm = new ImageProcessingDebugForm(ballDetector);

                debugForm.DebugFormClosed += DebugForm_DebugFormClosed; //subscribe to event handler letting us know when it closes
                debugForm.Show();

                //init debug form with current (raw) selected rawFrame
                if (listBoxProcessedFrames.SelectedItem is int selectedIndex)
                {
                    var rawFrame = rawFrames.FirstOrDefault(f => f.index == selectedIndex);
                    if (rawFrame != null) debugForm.ShowDebugImages(rawFrame.frame);
                    else Console.WriteLine("Raw rawFrame was null. not sending to debug form!");
                }
            }

            else debugForm.Focus();
        }

        private void btnFindCueball_Click(object sender, EventArgs e)
        {
            using (Bitmap rawImage = new Bitmap(pictureBoxImage.Image))
            using (Mat rawImageMat = OpenCvSharp.Extensions.BitmapConverter.ToMat(rawImage))
            using (Mat resizedImageMat = new Mat())
            {
                Cv2.Resize(rawImageMat, resizedImageMat, outputVideoResolution);
                using (Bitmap resizedImage = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(resizedImageMat))
                {
                    var result = ballDetector.ProcessTableImage(resizedImage);
                    if (result.CueBallHighlighted != null)
                    {
                        UpdatePictureBoxImage((Bitmap)result.CueBallHighlighted.Clone());
                    }
                    else
                    {
                        MessageBox.Show("Cueball not found!");
                    }
                    UpdateDebugForm(resizedImage);
                }
            }
        }

        private void findFindAllBalls_Click(object sender, EventArgs e)
        {
            using (Bitmap rawImage = new Bitmap(pictureBoxImage.Image))
            using (Mat rawImageMat = OpenCvSharp.Extensions.BitmapConverter.ToMat(rawImage))
            using (Mat resizedImageMat = new Mat())
            {
                Cv2.Resize(rawImageMat, resizedImageMat, outputVideoResolution);
                using (Bitmap resizedImage = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(resizedImageMat))
                {
                    var result = ballDetector.ProcessTableImage(resizedImage);
                    if (result.AllBallsHighlighted != null)
                    {
                        UpdatePictureBoxImage((Bitmap)result.AllBallsHighlighted.Clone());
                    }
                    else
                    {
                        MessageBox.Show("No balls found!");
                    }
                    UpdateDebugForm(resizedImage);
                }
            }
        }

        private async void btnDetectBalls_Click(object sender, EventArgs e)
        {
            detectingBalls = true;
            btnDetectBalls.Enabled = false;

            if (currentInputType == InputType.Video)
            {
                buttonResume.Enabled = false;
                buttonNextFrame.Enabled = false;

                DetectBallsInLoadedVideo();

                detectingBalls = false;
                buttonResume.Enabled = false;
                shotDetector.ShotFinished -= ShotDetector_ShotFinished;
                btnDetectBalls.Enabled = true;
            }

            //else its camera input, let it do its thing
        }

        private void btnLoadVideo_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Video Files (*.mp4;*.avi;*.mkv)|*.mp4;*.avi;*.mkv";
            openFileDialog.Title = "Select a Video File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                rawFrames.Clear();
                processedFrames.Clear();
                processedFrameIndices.Clear();
                VideoProcessor.DequeueVideoFrames(rawFrames);
                VideoProcessor.EnqueueVideoFrames(openFileDialog.FileName, outputVideoResolution, rawFrames, maxFrames);
                btnDetectBalls.Enabled = true;
                currentInputType = InputType.Video;
            }
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
            
            using (var workingFrame = new VideoFrame(new Bitmap(rawFrame.frame), rawFrame.index))
            {
                if (detectingBalls)
                {
                    stopwatch.Restart();

                    ImageProcessingResults results = null;
                    try
                    {
                        results = ballDetector.ProcessTableImage(workingFrame.frame);

                        // Create a copy of the processed image to store in the queue
                        using (var processedImage = new Bitmap(results.CueBallHighlighted))
                        {
                            var processedFrame = new VideoFrame(processedImage, workingFrame.index);

                            shotDetector.ProcessFrame(results.CueBall, processedFrame);

                            processedFrames.Enqueue(processedFrame);
                            if (processedFrames.Count > maxFrames) {
                                processedFrames.Dequeue().Dispose();
                            }

                            processedFrameIndices.Add(processedFrame.index);
                            if (processedFrameIndices.Count > maxFrames) processedFrameIndices.RemoveAt(0);

                            //update picturebox by changing listbox index
                            listBoxProcessedFrames.SelectedIndex = listBoxProcessedFrames.Items.Count - 1;

                            stopwatch.Stop();
                            totalProcessingTime += stopwatch.Elapsed.TotalSeconds;

                            UpdateFpsLabel(totalProcessingTime, workingFrame.index);
                            UpdatePictureBoxImage(new Bitmap(processedFrame.frame));
                        }
                    }
                    finally
                    {
                        results?.Dispose();
                    }
                }
                else
                {
                    UpdatePictureBoxImage(new Bitmap(workingFrame.frame));
                }

                Application.DoEvents();
            }
        }

        // Helper method to update PictureBoxImage safely
        private void UpdatePictureBoxImage(Bitmap newImage)
        {
            var oldImage = pictureBoxImage.Image;
            pictureBoxImage.Image = newImage;
            oldImage?.Dispose();
        }

        private void CameraController_ReceivedFrame(object? sender, VideoFrame frame)
        {
            if (rawFrames.Count > maxFrames)
            {
                var oldFrame = rawFrames.Dequeue();
                oldFrame.Dispose();
            }

            rawFrames.Enqueue(frame);
            ProcessFrame(frame);
        }

        /// <summary>
        /// For each rawFrame in the video, perform ball and/or shot tracking
        /// </summary>
        /// <returns></returns>
        private void DetectBallsInLoadedVideo()
        {
            totalProcessingTime = 0;
            foreach (VideoFrame rawFrame in rawFrames) ProcessFrame(rawFrame);
        }

        private void UpdateFpsLabel(double totalTime, int index)
        {
            var fps = index / totalTime;
            labelFrameRate.Text = $"FPS: {fps:F2}";
        }

        //display a selected individual rawFrame of the video and send to debug form if its open
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
                                UpdateDebugForm(tempBitmap);
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

        private void listBoxShots_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxShots.SelectedIndex >= 0)
            {
                Shot selectedShot = (Shot)listBoxShots.SelectedItem;
                ReplayShotWithBallPath(selectedShot, 60);


                // show graphs

                //speed over time
                Bitmap speedImage = new Bitmap(pictureBoxSpeedOverTime.Width, pictureBoxSpeedOverTime.Height);
                pictureBoxSpeedOverTime.Image = DrawingHelper.DrawSpeedOverTimeGraph(speedImage, selectedShot);
                pictureBoxSpeedOverTime.Refresh();

                labelMaxSpeed.Text = $"Max: {Math.Round(selectedShot.MaxSpeed, 2)}";
                labelAvgSpeed.Text = $"Avg: {Math.Round(selectedShot.AverageSpeed, 2)}";

                //cumulative distance over time
                Bitmap distanceImage = new Bitmap(pictureBoxDistanceOverTime.Width, pictureBoxDistanceOverTime.Height);
                pictureBoxDistanceOverTime.Image = DrawingHelper.DrawDistanceTravelledGraph(distanceImage, selectedShot);
                pictureBoxDistanceOverTime.Refresh();

                labelDistanceTravelled.Text = $"Total: {Math.Round(selectedShot.DistanceTravelled, 2)}";

                // acceleration over time
                Bitmap image = new Bitmap(pictureBoxAccelerationOverTime.Width, pictureBoxAccelerationOverTime.Height);
                pictureBoxAccelerationOverTime.Image = DrawingHelper.DrawAccelerationGraph(image, selectedShot);
                pictureBoxAccelerationOverTime.Refresh();

                labelMaxAcceleration.Text = $"Max: {Math.Round(selectedShot.MaxAcceleration, 2)}";
                labelAverageAcceleration.Text = $"Avg: {Math.Round(selectedShot.AverageAcceleration, 2)}";
            }
        }

        private void ShotDetector_ShotFinished(object sender, Shot shot) => listBoxShots.Items.Add(shot);

        private async Task ReplayShotWithBallPath(Shot shot, int replayFPS)
        {
            if (replayInProgress)
                return;

            replayInProgress = true;

            try
            {
                int delay = (int)Math.Round(1000d / Math.Abs(replayFPS));

                for (int i = 0; i < shot.FrameCount; i++)
                {
                    using (Bitmap frameToDrawOn = shot.GetFrameCopy(i).frame)
                    {
                        Bitmap drawnImage = DrawingHelper.DrawBallPath(shot.cueBallPath, new System.Drawing.Size(outputVideoResolution.Width, outputVideoResolution.Height), frameToDrawOn.Size, frameToDrawOn);
                        UpdatePictureBoxImage(drawnImage);
                        await Task.Delay(delay);
                    }
                }
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

        #region Media Contols

        //go back a rawFrame
        private void buttonLastFrame_Click(object sender, EventArgs e)
        {
            //stop the video from playing
            if (detectingBalls)
            {
                detectingBalls = false;
                buttonResume.Enabled = true;
            }

            if (listBoxProcessedFrames.SelectedIndex > 0) listBoxProcessedFrames.SelectedIndex -= 1;
            buttonNextFrame.Enabled = true;
        }

        //go forward a rawFrame
        private void buttonNextFrame_Click(object sender, EventArgs e)
        {
            if (detectingBalls) return; //cant skip rawFrame when at the latest rawFrame

            //stop the video from playing
            detectingBalls = false;

            if (listBoxProcessedFrames.SelectedIndex < (listBoxProcessedFrames.Items.Count - 1)) listBoxProcessedFrames.SelectedIndex += 1;
            buttonResume.Enabled = true;
        }

        //skip to latest
        private void buttonResume_Click(object sender, EventArgs e)
        {
            detectingBalls = true;
            buttonResume.Enabled = false;
            buttonNextFrame.Enabled = false;

            //show latest processed rawFrame from list box in the picturebox
            listBoxProcessedFrames.SelectedIndex = listBoxProcessedFrames.Items.Count - 1;
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            // Stop processing more frames
            detectingBalls = false;

            // Enable the resume button
            buttonResume.Enabled = true;
            buttonNextFrame.Enabled = true;
        }

        #endregion

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            cameraController.StopCameraCapture();

            processedFrameIndices.Clear();

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

        private void DebugForm_DebugFormClosed(object sender, EventArgs e)
        {
            debugForm = null;
        }

        private void UpdateDebugForm(Bitmap rawImage) => debugForm?.ShowDebugImages(rawImage);
    }
}