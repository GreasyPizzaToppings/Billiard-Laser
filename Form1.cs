using static VideoProcessor;

namespace billiard_laser
{
    public partial class Form1 : Form
    {
        private ArduinoController arduinoController;
        private CameraController cameraController;
        private CueBallDetector cueBallDetector;
        ShotDetector shotDetector = new ShotDetector();

        private OpenCvSharp.Size outputVideoResolution = new OpenCvSharp.Size(255, 144); //for testing purposes!! results on baxter pc: native, 1.25fps. 480p: 2.25. 360p: 3.5fps, 180p: 13.8fps, 144p: 21fps, 100p: 44fps

        private List<VideoProcessor.VideoFrame> videoFrames;

        private Boolean playingVideo = false;

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
                g.DrawEllipse(pen, ball.centre.X - ball.radius, ball.centre.Y - ball.radius, 2 * ball.radius, 2 * ball.radius);
            }
            return image;
        }

        //debug
        private Bitmap drawPoint(PointF point, Bitmap image)
        {

            using (Graphics g = Graphics.FromImage(image))
            using (Brush brush = new SolidBrush(Color.Black))
            {
                RectangleF dotRect = new RectangleF(point.X - 1, point.Y - 1, 1, 1);
                g.FillRectangle(brush, dotRect);
            }

            return image;
        }

        //debug
        private Bitmap drawPolygon(Point[] points, Bitmap image)
        {

            using (Graphics g = Graphics.FromImage(image))
            using (Pen pen = new Pen(Color.LimeGreen, 1f))
            {
                g.DrawPolygon(pen, points);
            }

            return image;
        }

        private void btnGetCameraInput_Click(object sender, EventArgs e)
        {
            cameraController.StartCameraCapture();
        }

        private void buttonLoadVideo_Click(object sender, EventArgs e)
        {
            //show dialog for video
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Video Files (*.mp4;*.avi;*.mkv)|*.mp4;*.avi;*.mkv";
            openFileDialog.Title = "Select a Video File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedVideoPath = openFileDialog.FileName;

                videoFrames = new List<VideoFrame>();

                videoFrames = VideoProcessor.GetVideoFrames(selectedVideoPath, outputVideoResolution);
            }

            //enable the start button
            btnProcessVideo.Enabled = true;
        }

        /// <summary>
        /// for each frame in the video, highlight the cue ball and show processed frames in the picturebox and listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProcessVideo_Click(object sender, EventArgs e)
        {
            //clear out list box
            listBoxProcessedFrames.Items.Clear();

            //media controls
            playingVideo = true;
            buttonResume.Enabled = false;
            buttonNextFrame.Enabled = false;

            // setup timer for fps
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            double totalDetectionTime = 0;

            //DEBUGGING: test cueballs with known starting position

            //Ball cueBall = new Ball(new Point(43, 41), 1); //144p: !!TESTING if we already know starting position. remove later.  testing for 'successfulPot'.
            //Ball cueBall = new Ball(new Point(140, 57), 2); //missedBlack.mp4
            //Ball cueBall = new Ball(new Point(47, 85), 0.5f); //73 break mp4
            //Ball cueBall = new Ball(new Point(109, 40), 0.5f); //successful pot 1 cannon. best: 125. 155 bad. 160 bad. works 50, 25, 15. bad at 5
            Ball cueBall = new Ball(new Point(16, 87), 0.5f); // GAME. CROPPED. 3 shots and full video. works nice at 125. 7 radius search

            List<VideoFrame> processedFrames = new List<VideoFrame>();


            for (int i = 0; i < videoFrames.Count; i++)
            {
                // Start the stopwatch
                stopwatch.Restart();




                // Detect the cue ball in the current frame
                //cueBall = cueBallDetector.FindCueBall(cueBall, videoFrames[i].frame, 125);
                object[] objects = cueBallDetector.FindCueBallDebug(cueBall, videoFrames[i].frame, 125); //TESTING. todo remove

                cueBall = (Ball)objects[0];
                PointF brightSpot = (PointF)objects[1];
                Point[] searchArea = (Point[])objects[2];

                // Process the frame to detect shots
                shotDetector.ProcessFrame(cueBall, i);



                //DEBUGGING: print info
                Console.WriteLine("Frame {0}\n CB: ({1},{2}) R:{3}", i, cueBall.centre.X, cueBall.centre.Y, cueBall.radius);
                Console.WriteLine("Delta: X{0},Y{1}\n", cueBall.deltaX, cueBall.deltaY);



                // Stop the stopwatch and add the elapsed time to the total
                stopwatch.Stop();
                totalDetectionTime += stopwatch.Elapsed.TotalSeconds;

                //draw the cue ball on the frame
                Bitmap rawImage = videoFrames[i].frame;
                Bitmap drawnImage = drawBallOnImage(cueBall, rawImage);


                //DEBUGGING: draw search area
                drawnImage = drawPolygon(searchArea, drawnImage);

                //DEBUGGING: draw middle and search area
                drawnImage = drawPoint(brightSpot, drawnImage);


                VideoFrame processedFrame = new VideoFrame(drawnImage, i);




                //add to processed frames list
                processedFrames.Add(processedFrame);

                //add frame to list box
                listBoxProcessedFrames.Items.Add(processedFrame);

                //show in pic box, only if in 'playing' mode
                if (playingVideo)
                {
                    buttonNextFrame.Enabled = false;
                    listBoxProcessedFrames.SelectedIndex = listBoxProcessedFrames.Items.Count - 1;

                    pictureBoxImage.Image = processedFrame.frame;
                }
                else buttonNextFrame.Enabled = true;

                // Update the FPS label with the current average FPS
                double averageFps = i / totalDetectionTime;
                labelFrameRate.Text = $"FPS: {averageFps:F2}";

                // Refresh the UI to show the updated image and FPS
                Application.DoEvents();
            }

            playingVideo = false;
            buttonResume.Enabled = false;
            shotDetector.ShotFinished -= ShotDetector_ShotFinished;
        }

        private void ShotDetector_ShotFinished(object sender, List<PointF> shot)
        {
            int startFrame = shotDetector.shotStartFrame;
            int endFrame = shotDetector.shotEndFrame;

            string shotInfo = $"{startFrame} - {endFrame}";
            listBoxShots.Items.Add(shotInfo);
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

        private void listBoxShots_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxShots.SelectedIndex >= 0)
            {
                int selectedIndex = listBoxShots.SelectedIndex;

                List<PointF> selectedShot = shotDetector.Shots[selectedIndex];

                // Draw the path of the selected shot in the PictureBox
                using (Graphics g = pictureBoxImage.CreateGraphics())
                {
                    using (Pen pen = new Pen(Color.Red, 2))
                    {
                        if (selectedShot.Count > 1)
                        {
                            g.DrawLines(pen, ScalePoints(selectedShot, new Size(outputVideoResolution.Width, outputVideoResolution.Height), pictureBoxImage.Size).ToArray());
                        }
                        else if (selectedShot.Count == 1)
                        {
                            g.DrawRectangle(pen, selectedShot[0].X - 1, selectedShot[0].Y - 1, 2, 2);
                        }
                    }
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
    }
}