namespace billiard_laser
{
    public partial class Form1 : Form
    {
        private ArduinoController arduinoController;
        private CameraController cameraController;
        private VideoProcessor videoProcessor;

        private OpenCvSharp.Size outputVideoResolution = new OpenCvSharp.Size(255, 144); //for testing purposes!! results on baxter pc: native, 1.25fps. 480p: 2.25. 360p: 3.5fps, 180p: 13.8fps, 144p: 21fps, 100p: 44fps

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBoxImage.SizeMode = PictureBoxSizeMode.Zoom;

            arduinoController = new ArduinoController("COM3"); //TODO find better way to find what port to connect to
            cameraController = new CameraController(pictureBoxImage, cboCamera);
            videoProcessor = new VideoProcessor();
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
            HighlightCueball(pictureBoxImage, 150);
        }

        private void HighlightCueball(PictureBox pictureBox, int threshold)
        {
            CueBallDetector detector = new CueBallDetector();
            detector.FindAndDrawCueBall(pictureBox, threshold);
        }

        private void btnGetCameraInput_Click(object sender, EventArgs e)
        {
            cameraController.StartCameraCapture();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            cameraController.StopCameraCapture();
        }

        private void btnFindCueballInVideo_Click(object sender, EventArgs e)
        {
            //show dialog for video
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Video Files (*.mp4;*.avi;*.mkv)|*.mp4;*.avi;*.mkv";
            openFileDialog.Title = "Select a Video File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedVideoPath = openFileDialog.FileName;

                videoProcessor.ProcessVideoAndDetectCueBall(selectedVideoPath, outputVideoResolution, pictureBoxImage, labelFrameRate); 
            }
        }
    }
}