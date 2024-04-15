using System.Diagnostics;
using static VideoProcessor;

namespace billiard_laser
{
    public partial class Form1 : Form
    {
        private ArduinoController arduinoController;
        private CameraController cameraController;
        private CueBallDetector cueBallDetector;
        ShotDetector shotDetector = new ShotDetector();

        private OpenCvSharp.Size outputVideoResolution = new OpenCvSharp.Size(319, 180); //for testing purposes!! results on baxter pc: native, 1.25fps. 480p: 2.25. 360p: 3.5fps, 180p: 13.8fps, 144p: 21fps, 100p: 44fps

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

        private void btnLaserOn_Click(object sender, EventArgs e)
        {
            arduinoController.LaserOn();
        }

        private void btnLaserOff_Click(object sender, EventArgs e)
        {
            arduinoController.LaserOff();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            arduinoController.MoveUp();
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            arduinoController.MoveLeft();
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            arduinoController.MoveRight();
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            arduinoController.MoveDown();
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
                g.DrawImage(pictureBoxImage.Image, Point.Empty);
                // Draw the ball on the new bitmap
                drawnImage = drawBallOnImage(cueBall, drawnImage);
                pictureBoxImage.Image = drawnImage;
            }
        }

        private Bitmap drawBallOnImage(Ball ball, Bitmap image)
        {
            using (Graphics g = Graphics.FromImage(image))
            using (Pen pen = new Pen(Color.DeepPink, 2f))
            {
                g.DrawEllipse(pen, ball.Centre.X - ball.Radius, ball.Centre.Y - ball.Radius, 2 * ball.Radius, 2 * ball.Radius);
            }
            return image;
        }

        private void btnGetCameraInput_Click(object sender, EventArgs e)
        {
            cameraController.StartCameraCapture();
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

        //lots of debugging stuff in it
        /// <summary>
        /// Find cueball, draw cueball, find shots, record shots
        /// </summary>
        /// <returns></returns>
        private async Task ProcessVideoAsync()
        {
            //DEBUGGING: test cueballs with known starting position

            //Ball cueBall = new Ball(new Point(43, 41), 1); //144p: !!TESTING if we already know starting position. remove later.  testing for 'successfulPot'.
            //Ball cueBall = new Ball(new Point(140, 57), 2); //missedBlack.mp4
            //Ball cueBall = new Ball(new Point(47, 85), 0.5f); //73 break mp4
            //Ball cueBall = new Ball(new Point(109, 40), 0.5f); //successful pot 1 cannon. best: 125. 155 bad. 160 bad. works 50, 25, 15. bad at 5
            //Ball cueBall = new Ball(new Point(20, 110), 1f); // GAME. CROPPED. 3 shots and full video. works nice at 125. 7 Radius search

            //Ball cueBall = new Ball(new Point(110, 120), 1f); //180p: real pool footage: 1 shot 1 miss mantelpiece
            //Ball cueBall = new Ball(new Point(200, 60), 1f); //180p: 3 pots, overhead, light. cropped
            //Ball cueBall = new Ball(new Point(70, 114), 2f); //180p: 2 pots overhead, cropped
            Ball cueBall = new Ball(new Point(181, 105), 1f); //180p: 1 pot side

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
                Point[] searchArea = (Point[])objects[2];

                // detect shots
                shotDetector.ProcessFrame(cueBall, frame);

                // Draw the cue ball on the frame
                var drawnImage = DrawBallOnImage(cueBall, frame.frame);

                // Draw the search area (for debugging)
                drawnImage = DrawPolygon(searchArea, drawnImage);

                // Draw the bright spot (for debugging)
                drawnImage = DrawPoint(brightSpot, drawnImage);

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

        private Bitmap DrawBallOnImage(Ball ball, Bitmap image)
        {
            using (var graphics = Graphics.FromImage(image))
            using (var pen = new Pen(Color.DeepPink, 2f))
            {
                var rect = new Rectangle(
                    (int)(ball.Centre.X - ball.Radius),
                    (int)(ball.Centre.Y - ball.Radius),
                    (int)(2 * ball.Radius),
                    (int)(2 * ball.Radius));
                graphics.DrawEllipse(pen, rect);
            }
            return image;
        }

        private Bitmap DrawPoint(PointF point, Bitmap image)
        {
            using (var graphics = Graphics.FromImage(image))
            using (var brush = new SolidBrush(Color.Black))
            {
                var rect = new RectangleF(point.X - 1, point.Y - 1, 2, 2);
                graphics.FillRectangle(brush, rect);
            }
            return image;
        }

        private Bitmap DrawPolygon(Point[] points, Bitmap image)
        {
            using (var graphics = Graphics.FromImage(image))
            using (var pen = new Pen(Color.LimeGreen, 1f))
            {
                graphics.DrawPolygon(pen, points);
            }
            return image;
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

        private void ShotDetector_ShotFinished(object sender, Shot shot)
        {
            listBoxShots.Items.Add(shot);
        }

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

        private async void ReplayShotWithBallPath(int startFrame, int endFrame, int replayFPS)
        {
            if (replayInProgress)
                return;

            replayInProgress = true;

            Shot selectedShot = shotDetector.Shots[listBoxShots.SelectedIndex];
            int delay = (int)Math.Round(1000d / Math.Abs(replayFPS)); //calculate delay between frames based on given fps

            for (int i = startFrame; i <= endFrame; i++)
            {
                if (i >= 0 && i < listBoxProcessedFrames.Items.Count)
                {
                    listBoxProcessedFrames.SelectedIndex = i;
                    VideoFrame frame = (VideoFrame)listBoxProcessedFrames.SelectedItem;

                    // Draw the path of the selected shot on the current frame
                    using (Graphics g = Graphics.FromImage(frame.frame))
                    {
                        using (Pen pen = new Pen(Color.Red, 2))
                        {
                            if (selectedShot.Path.Count > 1)
                            {
                                g.DrawLines(pen, ScalePoints(selectedShot.Path, new Size(outputVideoResolution.Width, outputVideoResolution.Height), frame.frame.Size).ToArray());
                            }
                            else if (selectedShot.Path.Count == 1)
                            {
                                g.DrawRectangle(pen, selectedShot.Path[0].X - 1, selectedShot.Path[0].Y - 1, 2, 2);
                            }
                        }
                    }

                    pictureBoxImage.Image = frame.frame;
                    pictureBoxImage.Refresh();

                    // Delay to control the replay speed (adjust the delay as needed)
                    await Task.Delay(delay);
                }
            }

            replayInProgress = false;
        }

        private void listBoxShots_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listBoxShots.SelectedIndex >= 0)
            {
                string selectedShotInfo = listBoxShots.SelectedItem.ToString();
                string[] frameIndices = selectedShotInfo.Split('-');

                if (frameIndices.Length == 2 && int.TryParse(frameIndices[0], out int startFrame) && int.TryParse(frameIndices[1], out int endFrame))
                {
                    ReplayShotWithBallPath(startFrame, endFrame, 60);
                }
            }
        }

        private List<PointF> ScalePoints(List<PointF> points, Size original, Size target)
        {

            // Calculate scaling factors
            float scaleX = (float)target.Width / original.Width;
            float scaleY = (float)target.Height / original.Height;

            List<PointF> scaledPoints = new List<PointF>();

            foreach (PointF point in points)
            {
                float scaledX = point.X * scaleX;
                float scaledY = point.Y * scaleY;
                scaledPoints.Add(new PointF(scaledX, scaledY));
            }

            return scaledPoints;
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

            if (listBoxProcessedFrames.SelectedIndex < listBoxProcessedFrames.Items.Count)
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

        private void pictureBoxImage_MouseMove(object sender, MouseEventArgs e)
        {
            //update label with picturebox mouse x,y position
            labelMouseCoordinates.Text = string.Format("Mouse: ({0},{1})", e.X, e.Y);
        }
    }
}