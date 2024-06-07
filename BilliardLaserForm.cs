using Accord;
using System.Diagnostics;
using static VideoProcessor;
using System.IO.Ports;
using AForge.Video.DirectShow;
using AForge.Video;
using System.Windows.Media;
using System.ComponentModel;

namespace billiard_laser
{
    public partial class BilliardLaserForm : Form
    {
        //video debugging form
        private ImageProcessingDebugForm debugForm;

        //utility classes
        private ArduinoController arduinoController;
        private CameraController cameraController;
        private CueBallDetector cueBallDetector;
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
        private OpenCvSharp.Size outputVideoResolution = p480;

        //frames
        private BindingList<int> processedFrameIndices = new BindingList<int>();
        private Queue<VideoFrame> rawFrames = new Queue<VideoFrame>();
        private Queue<VideoFrame> processedFrames = new Queue<VideoFrame>();
        private const int maxFrames = 2000;

        //flags
        public Boolean detectingBalls = false;
        private bool replayInProgress = false;

        public static event EventHandler<ImageProcessingResults> ProcessedDebugFrame;

        public BilliardLaserForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBoxImage.SizeMode = PictureBoxSizeMode.Zoom;

            //utility classes
            arduinoController = new ArduinoController("COM3"); //TODO find better way to find what port to connect to
            cameraController = new CameraController(cboCamera);
            cueBallDetector = new CueBallDetector(); //todo remove later?
            shotDetector = new ShotDetector();
            ballDetector = new BallDetector();

            //event handler methods
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
            if (cameraController.StartCameraCapture()) btnDetectBalls.Enabled = true;
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
                        pictureBoxImage.Image = new Bitmap(openfiledialog.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("error loading image: " + ex.Message);
                    }
                }
            }
        }

        private void btnFindCueball_Click(object sender, EventArgs e)
        {
            // Get a new cueball
            Ball cueBall = cueBallDetector.FindCueBall(null, pictureBoxImage.Image);

            // Create a new bitmap to draw the ball on
            Bitmap drawnImage = new Bitmap(pictureBoxImage.Image.Width, pictureBoxImage.Image.Height);

            using (Graphics g = Graphics.FromImage(drawnImage))
            {
                g.DrawImage(pictureBoxImage.Image, System.Drawing.Point.Empty);

                // Draw the ball on the new bitmap
                drawnImage = DrawingHelper.DrawBallOnImage(cueBall, drawnImage);
                pictureBoxImage.Image = drawnImage;
            }
        }

        private void btnLoadVideo_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Video Files (*.mp4;*.avi;*.mkv)|*.mp4;*.avi;*.mkv";
            openFileDialog.Title = "Select a Video File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedVideoPath = openFileDialog.FileName;
                LoadVideo(selectedVideoPath);
            }
        }

        private void LoadVideo(string videoPath)
        {
            rawFrames.Clear();
            processedFrames.Clear();
            VideoProcessor.EnqueueVideoFrames(videoPath, outputVideoResolution, rawFrames, maxFrames);
            btnDetectBalls.Enabled = true;
        }

        private void findFindAllBalls_Click(object sender, EventArgs e)
        {
            Bitmap unprocessedImage = (Bitmap)pictureBoxImage.Image;
            pictureBoxImage.Image = ballDetector.FindAllBalls(unprocessedImage);

            //find all in debug form if open
            if (debugForm != null && !debugForm.IsDisposed)
            {
                debugForm.ProcessRawFrame(new VideoFrame(unprocessedImage, listBoxProcessedFrames.SelectedIndex));
            }
        }

        private void CameraController_ReceivedFrame(object? sender, VideoFrame frame)
        {
            //remove too-old frames
            if (rawFrames.Count >= maxFrames) rawFrames.Dequeue();
            rawFrames.Enqueue(frame);

            if (detectingBalls)
            {
                Bitmap highlightedBalls = ballDetector.FindAllBalls(frame.frame);
                pictureBoxImage.Image = highlightedBalls;

                VideoFrame processedFrame = new VideoFrame(highlightedBalls, frame.index);

                //remove too-old processed frames
                if (processedFrames.Count >= maxFrames) processedFrames.Dequeue();
                processedFrames.Enqueue(processedFrame);

                Application.DoEvents();
            }
            else
            {
                pictureBoxImage.Image = frame.frame;
            }
        }

        /// <summary>
        /// For each frame in the video, perform ball and/or shot tracking
        /// </summary>
        /// <returns></returns>
        private void DetectBallsInLoadedVideo()
        {
            Stopwatch stopwatch = new Stopwatch();
            double totalProcessingTime = 0;

            foreach (VideoFrame rawFrame in rawFrames)
            {
                stopwatch.Restart();

                var processedFrame = new VideoFrame(ballDetector.FindAllBalls(rawFrame.frame), rawFrame.index);

                processedFrames.Enqueue(processedFrame);
                processedFrameIndices.Add(processedFrame.index);

                // remove the oldest frame if the limit is reached
                if (processedFrameIndices.Count > maxFrames)
                {
                    processedFrameIndices.RemoveAt(0);
                    processedFrames.Dequeue();
                }

                if (detectingBalls)
                {
                    buttonNextFrame.Enabled = false;
                    listBoxProcessedFrames.SelectedIndex = listBoxProcessedFrames.Items.Count - 1;
                }
                else
                {
                    buttonNextFrame.Enabled = true;
                }

                stopwatch.Stop();
                totalProcessingTime += stopwatch.Elapsed.TotalSeconds;

                UpdateFpsLabel(totalProcessingTime, rawFrame.index);

                Application.DoEvents();
            }
        }

        private void UpdateFpsLabel(double totalTime, int index)
        {
            var fps = index / totalTime;
            labelFrameRate.Text = $"FPS: {fps:F2}";
        }

        private async void btnDetectBalls_Click(object sender, EventArgs e)
        {
            //set flag that ball detection is enabled
            detectingBalls = true;
            btnDetectBalls.Enabled = false;

            //if we loaded a video, process that
            if (rawFrames != null)
            {
                // refresh the listbox
                processedFrameIndices.Clear();

                buttonResume.Enabled = false;
                buttonNextFrame.Enabled = false;

                DetectBallsInLoadedVideo();

                detectingBalls = false;
                buttonResume.Enabled = false;
                shotDetector.ShotFinished -= ShotDetector_ShotFinished;
                btnDetectBalls.Enabled = true;
            }

            //else if its camera input, let it do its thing

        }

        private void HandleImageProcessingSettingsChanged(object sender, ImageProcessingSettingsChanged e)
        {
            //update ball detector mask values
            ballDetector.LowerMaskRgb = e.LowerMaskRgb;
            ballDetector.UpperMaskRgb = e.UpperMaskRgb;

            //update blur and sharpen status
            ballDetector.enableBlur = e.EnableBlur;
            ballDetector.enableSharpening = e.EnableSharpening;

            Console.WriteLine($"Image processing settings changed! Editing ballDetector values:" +
                  $"\nLower Mask RGB: {e.LowerMaskRgb}" +
                  $"\nUpper Mask RGB: {e.UpperMaskRgb}" +
                  $"\nEnable Blur: {e.EnableBlur}" +
                  $"\nEnable Sharpening: {e.EnableSharpening}\n");
        }

        private void ShotDetector_ShotFinished(object sender, Shot shot) => listBoxShots.Items.Add(shot);


        //display a selected individual frame of the video and send to debug form if its open
        private void listBoxFrames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxProcessedFrames.SelectedItem is int selectedIndex)
            {
                // find the frame in the queue based on the index
                var processedFrame = processedFrames.FirstOrDefault(f => f.index == selectedIndex);

                if (processedFrame != null)
                {
                    pictureBoxImage.Image = processedFrame.frame;

                    // send raw frame to debug form if open
                    if (debugForm != null && !debugForm.IsDisposed)
                    {
                        var rawFrame = rawFrames.FirstOrDefault(f => f.index == selectedIndex);
                        if (rawFrame != null) debugForm.ProcessRawFrame(rawFrame);
                        else Console.WriteLine("Raw frame was null. not sending to debug form!");
                    }
                }
            }
        }

        private void buttonShowDebugForm_Click(object sender, EventArgs e)
        {
            if (debugForm == null || debugForm.IsDisposed)
            {
                debugForm = new ImageProcessingDebugForm(ballDetector.LowerMaskRgb, ballDetector.UpperMaskRgb, ballDetector.enableBlur, ballDetector.enableSharpening);

                debugForm.DebugFormClosed += DebugForm_DebugFormClosed; //subscribe to event handler letting us know when it closes
                debugForm.ImageProcessingSettingsChanged += HandleImageProcessingSettingsChanged; // subscribe to mask rgb value change updates
                debugForm.Show();

                //init debug form with current (raw) selected frame
                if (listBoxProcessedFrames.SelectedItem is int selectedIndex)
                {
                    var rawFrame = rawFrames.FirstOrDefault(f => f.index == selectedIndex);
                    if (rawFrame != null) debugForm.ProcessRawFrame(rawFrame);
                    else Console.WriteLine("Raw frame was null. not sending to debug form!");
                }
            }

            else
            {
                debugForm.Focus();
            }
        }

        //sussy. dont put in form? also updates picturebox twice. 
        private async void ReplayShotWithBallPath(Shot shot, int replayFPS)
        {
            if (replayInProgress)
                return;

            replayInProgress = true;

            int delay = (int)Math.Round(1000d / Math.Abs(replayFPS)); //calculate delay between frames based on given fps

            foreach (VideoFrame frame in shot.ShotFrames)
            {
                listBoxProcessedFrames.SelectedIndex = frame.index;

                // Draw the path of the selected shot on the current frame
                Bitmap drawnImage = DrawingHelper.DrawBallPath(shot.Path, new Size(outputVideoResolution.Width, outputVideoResolution.Height), frame.frame.Size, frame.frame);

                pictureBoxImage.Image = drawnImage;
                pictureBoxImage.Refresh();

                // Delay to control the replay speed (adjust the delay as needed)
                await Task.Delay(delay);
            }

            replayInProgress = false;
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

        //go back a frame
        private void buttonLastFrame_Click(object sender, EventArgs e)
        {
            //stop the video from playing
            if (detectingBalls)
            {
                detectingBalls = false;
                buttonResume.Enabled = true;
            }

            buttonNextFrame.Enabled = true;

            //show prev image in the picturebox
            if (listBoxProcessedFrames.SelectedIndex > 0)
            {
                listBoxProcessedFrames.SelectedIndex -= 1;
                var frame = (VideoFrame)listBoxProcessedFrames.SelectedItem;
                pictureBoxImage.Image = frame.frame;
            }
        }

        //go forward a frame
        private void buttonNextFrame_Click(object sender, EventArgs e)
        {
            if (detectingBalls) return; //cant skip frame when at the latest frame

            //stop the video from playing
            detectingBalls = false;

            if (listBoxProcessedFrames.SelectedIndex < (listBoxProcessedFrames.Items.Count - 1))
            {
                listBoxProcessedFrames.SelectedIndex += 1;

                //show that image in the picturebox
                var frame = (VideoFrame)listBoxProcessedFrames.SelectedItem;
                pictureBoxImage.Image = frame.frame;
            }

            buttonResume.Enabled = true;
        }

        //skip to latest
        private void buttonResume_Click(object sender, EventArgs e)
        {
            detectingBalls = true;
            buttonResume.Enabled = false;
            buttonNextFrame.Enabled = false;

            //show latest processed frame from list box in the picturebox
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

        //sussy
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            cameraController.StopCameraCapture();

            // Clear the processed frames list box
            processedFrameIndices.Clear();

            // Dispose of the picture box image
            if (pictureBoxImage.Image != null)
            {
                pictureBoxImage.Image.Dispose();
                pictureBoxImage.Image = null;
            }
        }

        //set our debug form object to null when it closes
        private void DebugForm_DebugFormClosed(object sender, EventArgs e)
        {
            debugForm = null;
        }
    }
}