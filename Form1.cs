using System.Windows.Forms;
using static VideoProcessor;

namespace billiard_laser
{
    public partial class Form1 : Form
    {
        private ArduinoController arduinoController;
        private CameraController cameraController;
        private CueBallDetector cueBallDetector;

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
            //new cueball
            pictureBoxImage.Image = drawBallOnImage(cueBallDetector.FindCueBall(null, pictureBoxImage.Image), (Bitmap)pictureBoxImage.Image);
        }

        private Bitmap drawBallOnImage(Ball ball, Bitmap image) {
            //draw
            Graphics g = Graphics.FromImage(image);
            Pen pen = new Pen(Color.DeepPink, 2f);
            g.DrawEllipse(pen, ball.CurrentPosition.X - ball.Radius, ball.CurrentPosition.Y - ball.Radius, 2 * ball.Radius, 2 * ball.Radius);

            return image;
        }

        private void btnGetCameraInput_Click(object sender, EventArgs e)
        {
            cameraController.StartCameraCapture();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            cameraController.StopCameraCapture();
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

            Ball cueBall = new Ball(new Point(43,41), -1); //144p: !!TESTING if we already know starting position. remove later.  testing for 'successfulPot'.

            List<VideoFrame> processedFrames = new List<VideoFrame>();

            for (int i = 0; i < videoFrames.Count; i++)
            {
                // Start the stopwatch
                stopwatch.Restart();

                // Detect the cue ball in the current frame
                Ball newCueBall = cueBallDetector.FindCueBall(cueBall, videoFrames[i].frame);

                float deltaTimeMs = 1000 / 24; //24 fps
                cueBall.UpdateBallPositionAndVelocity(newCueBall.CurrentPosition, deltaTimeMs);

                // Stop the stopwatch and add the elapsed time to the total
                stopwatch.Stop();
                totalDetectionTime += stopwatch.Elapsed.TotalSeconds;

                //draw the cue ball on the frame
                Bitmap rawImage = videoFrames[i].frame;
                Bitmap drawnImage = drawBallOnImage(cueBall, rawImage);
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
    }
}