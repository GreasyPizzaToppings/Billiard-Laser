using Accord;
using System.Diagnostics;
using static VideoProcessor;
using System.IO.Ports;
using AForge.Video.DirectShow;
using AForge.Video;
using System.Windows.Media;

namespace billiard_laser
{
    public partial class Form1 : Form
    {
        private ArduinoController arduinoController;
        private CameraController cameraController;
        private CueBallDetector cueBallDetector;
        ShotDetector shotDetector = new ShotDetector();


        //selection of output resolutions
        private static OpenCvSharp.Size p200 = new OpenCvSharp.Size(200, 200 / 1.77);
        private static OpenCvSharp.Size p250 = new OpenCvSharp.Size(250, 250 / 1.77);
        private static OpenCvSharp.Size p360 = new OpenCvSharp.Size(360, 203);
        private static OpenCvSharp.Size p480 = new OpenCvSharp.Size(480, 271);
        private static OpenCvSharp.Size p720 = new OpenCvSharp.Size(720, 407);


        private OpenCvSharp.Size outputVideoResolution = p360; //for testing purposes!! results on baxter pc: native, 1.25fps. 480p: 2.25. 360p: 3.5fps, 180p: 13.8fps, 144p: 21fps, 100p: 44fps

        private List<VideoProcessor.VideoFrame> videoFrames;

        private Boolean playingVideo = false;
        private bool replayInProgress = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBoxImage.SizeMode = PictureBoxSizeMode.Zoom;

            arduinoController = new ArduinoController("COM3"); //TODO find better way to find what port to connect to
            cameraController = new CameraController(pictureBoxImage, cboCamera);
            cueBallDetector = new CueBallDetector();

            shotDetector.ShotFinished += ShotDetector_ShotFinished;
        }

        private void btnLaserOn_Click(object sender, EventArgs e) => arduinoController.LaserOn();
        private void btnLaserOff_Click(object sender, EventArgs e) => arduinoController.LaserOff();
        private void btnUp_Click(object sender, EventArgs e) => arduinoController.MoveUp();
        private void btnLeft_Click(object sender, EventArgs e) => arduinoController.MoveLeft();
        private void btnRight_Click(object sender, EventArgs e) => arduinoController.MoveRight();
        private void btnDown_Click(object sender, EventArgs e) => arduinoController.MoveDown();

        private void btnGetCameraInput_Click(object sender, EventArgs e) => cameraController.StartCameraCapture();

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
                        if (pictureBoxImage.Image != null) pictureBoxImage.Image.Dispose(); //dispose of old image
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

        //todo make async?
        private void LoadVideo(string videoPath)
        {
            videoFrames = VideoProcessor.GetVideoFrames(videoPath, outputVideoResolution);
            btnProcessVideo.Enabled = true;
        }

        private void findColoredBalls_Click(object sender, EventArgs e)
        {
            ColoredBallDetection colored = new ColoredBallDetection();
            pictureBoxImage.Image = colored.BallDetection((Bitmap)pictureBoxImage.Image);
        }

        /// <summary>
        /// For each frame in the video, perform ball and/or shot tracking
        /// </summary>
        /// <returns></returns>
        private async Task ProcessVideoAsync()
        {
            //Check which processing method to use

            //Track just cueball (using bright spot) and detect shots
            if (radioButtonCB.Checked)
            {
                await PerformCueBallShotDetection();
            }

            //Highlight All balls using image curve library
            else if (radioButtonAllBalls.Checked)
            {
                ColoredBallDetection colored = new ColoredBallDetection();
                Stopwatch stopwatch = new Stopwatch();
                double totalProcessingTime = 0;
                var processedFrames = new List<VideoFrame>();

                foreach (VideoFrame frame in videoFrames)
                {
                    //time how long it takes to process frame
                    stopwatch.Restart();

                    Bitmap highlightedBalls = colored.BallDetection(frame.frame);

                    // Create a new processed frame
                    var processedFrame = new VideoFrame(highlightedBalls, frame.index);

                    processedFrames.Add(processedFrame);
                    listBoxProcessedFrames.Items.Add(processedFrame);

                    stopwatch.Stop();

                    totalProcessingTime += stopwatch.Elapsed.TotalSeconds;

                    UpdateFpsLabel(totalProcessingTime, frame.index);

                    if (playingVideo)
                    {
                        buttonNextFrame.Enabled = false;
                        listBoxProcessedFrames.SelectedIndex = listBoxProcessedFrames.Items.Count - 1;
                        pictureBoxImage.Image = highlightedBalls;
                    }

                    else
                    {
                        buttonNextFrame.Enabled = true;
                    }

                    Application.DoEvents();
                }
            }
        }

        /// <summary>
        /// Track just the cue ball using bright spot method and detect shots using old method
        /// </summary>
        /// <returns></returns>
        private async Task PerformCueBallShotDetection()
        {
            //DEBUGGING: test cueballs with known starting position

            //Ball cueBall = new Ball(new Point(43, 41), 1); //144p: !!TESTING if we already know starting position. remove later.  testing for 'successfulPot'.
            //Ball cueBall = new Ball(new Point(140, 57), 2); //missedBlack.mp4
            //Ball cueBall = new Ball(new Point(47, 85), 0.5f); //73 break mp4

            //Ball cueBall = new Ball(new System.Drawing.Point((int)(0.43f * outputVideoResolution.Width), (int)(0.24f * outputVideoResolution.Height)), 1f); //successful pot 1 cannon. best: 125. 155 bad. 160 bad. works 50, 25, 15. bad at 5
            //Ball cueBall = new Ball(new System.Drawing.Point((int)(0.06f * outputVideoResolution.Width), (int)(0.61f * outputVideoResolution.Height)), 1f); // GAME. CROPPED. 3 shots and full video. works nice at 125. 7 Radius search

            //ronnie miss. object ball hits cue ball
            Ball cueBall = new Ball(new System.Drawing.Point((int)(0.40f * outputVideoResolution.Width), (int)(0.05f * outputVideoResolution.Height)), 1f); // ronnie miss


            //footage from my house
            //Ball cueBall = new Ball(new Point(110, 120), 1f); //180p: real pool footage: 1 shot 1 miss mantelpiece
            //Ball cueBall = new Ball(new Point(200, 60), 1f); //180p: 3 pots, overhead, light. cropped
            //Ball cueBall = new Ball(new Point(70, 114), 2f); //180p: 2 pots overhead, cropped
            //Ball cueBall = new Ball(new Point(181, 105), 1f); //180p: 1 pot side

            var processedFrames = new List<VideoFrame>();
            Stopwatch stopwatch = new Stopwatch();
            double totalProcessingTime = 0;

            foreach (VideoFrame frame in videoFrames)
            {
                //time how long it takes to process frame
                stopwatch.Restart();

                object[] objects = await Task.Run(() => cueBallDetector.FindCueBallDebug(cueBall, frame.frame, 125));

                totalProcessingTime += stopwatch.Elapsed.TotalSeconds;

                cueBall = (Ball)objects[0];

                Console.WriteLine("Frame {0}\n CB: ({1},{2}) R:{3}", frame.index, cueBall.Centre.X, cueBall.Centre.Y, cueBall.Radius);
                Console.WriteLine("Delta: X{0},Y{1}\n", cueBall.DeltaX, cueBall.DeltaY);

                PointF brightSpot = (PointF)objects[1];
                System.Drawing.Point[] searchArea = (System.Drawing.Point[])objects[2];

                // detect shots
                shotDetector.ProcessFrame(cueBall, frame);

                // Draw the cue ball on the frame
                var drawnImage = DrawingHelper.DrawBallOnImage(cueBall, frame.frame);

                // Draw the search area (for debugging)
                drawnImage = DrawingHelper.DrawPolygon(searchArea, drawnImage);

                // Draw the bright spot (for debugging)
                drawnImage = DrawingHelper.DrawPoint(brightSpot, drawnImage);

                // Create a new processed frame
                var processedFrame = new VideoFrame(drawnImage, frame.index);

                processedFrames.Add(processedFrame);
                listBoxProcessedFrames.Items.Add(processedFrame);

                stopwatch.Stop();
                UpdateFpsLabel(totalProcessingTime, frame.index);

                if (playingVideo)
                {
                    buttonNextFrame.Enabled = false;
                    listBoxProcessedFrames.SelectedIndex = listBoxProcessedFrames.Items.Count - 1;
                    pictureBoxImage.Image = processedFrame.frame;
                }
                else
                {
                    buttonNextFrame.Enabled = true;
                }

                Application.DoEvents();
            }
        }

        private void UpdateFpsLabel(double totalTime, int index)
        {
            var fps = index / totalTime;
            labelFrameRate.Text = $"FPS: {fps:F2}";
        }

        private async void btnProcessVideo_ClickAsync(object sender, EventArgs e)
        {
            listBoxProcessedFrames.Items.Clear();

            playingVideo = true;
            buttonResume.Enabled = false;
            buttonNextFrame.Enabled = false;

            await ProcessVideoAsync();

            playingVideo = false;
            buttonResume.Enabled = false;
            shotDetector.ShotFinished -= ShotDetector_ShotFinished;
        }

        private void ShotDetector_ShotFinished(object sender, Shot shot) => listBoxShots.Items.Add(shot);

        //display a selected individual frame of the video
        private void listBoxFrames_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if any item is selected
            if (listBoxProcessedFrames.SelectedItem != null)
            {
                // Get the selected frame from the list
                VideoProcessor.VideoFrame selectedFrame = (VideoProcessor.VideoFrame)listBoxProcessedFrames.SelectedItem;

                // Display the selected frame in the PictureBox
                pictureBoxImage.Image = selectedFrame.frame;
            }
        }

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
            if (playingVideo)
            {
                playingVideo = false;
                buttonResume.Enabled = true;
            }

            buttonNextFrame.Enabled = true;

            //show prev image in the picturebox
            if (listBoxProcessedFrames.SelectedIndex > 0)
            {
                listBoxProcessedFrames.SelectedIndex -= 1;
                var frame = (VideoProcessor.VideoFrame)listBoxProcessedFrames.SelectedItem;
                pictureBoxImage.Image = frame.frame;
            }
        }

        //go forward a frame
        private void buttonNextFrame_Click(object sender, EventArgs e)
        {
            if (playingVideo) return; //cant skip frame when at the latest frame

            //stop the video from playing
            playingVideo = false;

            if (listBoxProcessedFrames.SelectedIndex < (listBoxProcessedFrames.Items.Count - 1))
            {
                listBoxProcessedFrames.SelectedIndex += 1;

                //show that image in the picturebox
                var frame = (VideoProcessor.VideoFrame)listBoxProcessedFrames.SelectedItem;
                pictureBoxImage.Image = frame.frame;
            }

            buttonResume.Enabled = true;
        }

        //skip to latest
        private void buttonResume_Click(object sender, EventArgs e)
        {
            playingVideo = true;
            buttonResume.Enabled = false;
            buttonNextFrame.Enabled = false;

            //show latest processed frame from list box in the picturebox
            listBoxProcessedFrames.SelectedIndex = listBoxProcessedFrames.Items.Count - 1;
            VideoFrame frame = (VideoFrame)listBoxProcessedFrames.SelectedItem;
            pictureBoxImage.Image = frame.frame;
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            // Stop processing more frames
            playingVideo = false;

            // Enable the resume button
            buttonResume.Enabled = true;
            buttonNextFrame.Enabled = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            cameraController.StopCameraCapture();

            // Clear and dispose of the video frames
            if (videoFrames != null)
            {
                videoFrames.Clear();
                videoFrames = null;
            }

            // Clear the processed frames list box
            listBoxProcessedFrames.Items.Clear();

            // Dispose of the picture box image
            if (pictureBoxImage.Image != null)
            {
                pictureBoxImage.Image.Dispose();
                pictureBoxImage.Image = null;
            }
        }

    }
}