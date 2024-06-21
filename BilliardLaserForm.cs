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
        private const int maxFrames = 2000;

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
                        pictureBoxImage.Image = new Bitmap(openfiledialog.FileName);
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
            Bitmap rawImage = (Bitmap)pictureBoxImage.Image;
            Bitmap cueballHighlighted = ballDetector.FindCueBall(rawImage).Draw(rawImage);

            if (cueballHighlighted != null) pictureBoxImage.Image = cueballHighlighted;
            else MessageBox.Show("Cueball not found!");

            UpdateDebugForm(rawImage);
        }

        private void findFindAllBalls_Click(object sender, EventArgs e)
        {
            Bitmap rawImage = (Bitmap)pictureBoxImage.Image;
            pictureBoxImage.Image = ballDetector.FindAllBalls(rawImage);

            UpdateDebugForm(rawImage);
        }

        private async void btnDetectBalls_Click(object sender, EventArgs e)
        {
            detectingBalls = true;
            btnDetectBalls.Enabled = false;

            if (currentInputType == InputType.Video)
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
            // use the UI thread to process rawFrame
            if (InvokeRequired)
            {
                Invoke(new Action(() => ProcessFrame(rawFrame)));
                return;
            }

            if (detectingBalls)
            {
                stopwatch.Restart();

                Bitmap highlightedBalls = ballDetector.FindAllBalls(rawFrame.frame);
                VideoFrame processedFrame = new VideoFrame(highlightedBalls, rawFrame.index);

                processedFrames.Enqueue(processedFrame);
                processedFrameIndices.Add(processedFrame.index);

                if (processedFrameIndices.Count > maxFrames)
                {
                    processedFrameIndices.RemoveAt(0);
                    processedFrames.Dequeue();
                }

                //calls selectedIndexChanged(). updates list box and picturebox . sussy
                listBoxProcessedFrames.SelectedIndex = listBoxProcessedFrames.Items.Count - 1;

                stopwatch.Stop();
                totalProcessingTime += stopwatch.Elapsed.TotalSeconds;

                UpdateFpsLabel(totalProcessingTime, rawFrame.index);
            }

            else
            {
                pictureBoxImage.Image = rawFrame.frame;
            }

            Application.DoEvents();
        }

        private void CameraController_ReceivedFrame(object? sender, VideoFrame frame)
        {
            if (rawFrames.Count >= maxFrames) rawFrames.Dequeue();
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
                // find the rawFrame in the queue based on the index
                var processedFrame = processedFrames.FirstOrDefault(f => f.index == selectedIndex);

                if (processedFrame != null)
                {
                    pictureBoxImage.Image = processedFrame.frame;

                    var rawFrame = rawFrames.FirstOrDefault(f => f.index == selectedIndex);
                    if (rawFrame != null) UpdateDebugForm(rawFrame.frame);
                    else Console.WriteLine("Raw rawFrame was null. not sending to debug form!");
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

                // Draw the path of the selected shot on the current rawFrame
                Bitmap drawnImage = DrawingHelper.DrawBallPath(shot.Path, new Size(outputVideoResolution.Width, outputVideoResolution.Height), frame.frame.Size, frame.frame);

                pictureBoxImage.Image = drawnImage;
                pictureBoxImage.Refresh();

                // Delay to control the replay speed (adjust the delay as needed)
                await Task.Delay(delay);
            }

            replayInProgress = false;
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

            // Clear the processed frames list box
            processedFrameIndices.Clear();

            // Dispose of the picture box image
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