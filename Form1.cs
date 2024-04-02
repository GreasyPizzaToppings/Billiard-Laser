using System.IO.Ports;
using AForge.Video;
using AForge.Video.DirectShow;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace billiard_laser
{
    public partial class Form1 : Form
    {
        ArduinoController arduinoController;
        private FilterInfoCollection filterInfoCollection;
        private VideoCaptureDevice videoCaptureDevice;
        private OpenCvSharp.Size size = new OpenCvSharp.Size(255, 144); //for testing purposes!! results on baxter pc: native, 1.25fps. 480p: 2.25. 360p: 3.5fps, 180p: 13.8fps, 144p: 21fps, 100p: 44fps

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBoxImage.SizeMode = PictureBoxSizeMode.Zoom;
            arduinoController = new ArduinoController("COM3"); //TODO find better way to find what port to connect to
            PopulateCameraComboBox();
        }


        private void PopulateCameraComboBox()
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Device in filterInfoCollection)
                cboCamera.Items.Add(Device.Name);
            cboCamera.SelectedIndex = 0;

            videoCaptureDevice = new VideoCaptureDevice();
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
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[cboCamera.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += FinalFrame_NewFrame;
            videoCaptureDevice.Start();
        }

        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBoxImage.Image = (Bitmap)eventArgs.Frame.Clone();
        }



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopVideoCaptureDevice();
        }

        private void StopVideoCaptureDevice()
        {
            if (videoCaptureDevice != null)
            {
                videoCaptureDevice.SignalToStop();

                // wait ~ 3 seconds
                for (int i = 0; i < 30; i++)
                {
                    if (!videoCaptureDevice.IsRunning)
                    {
                        break;
                    }
                    System.Threading.Thread.Sleep(100);
                }

                if (videoCaptureDevice.IsRunning)
                {
                    videoCaptureDevice.Stop();
                }

                videoCaptureDevice.Stop();

                videoCaptureDevice = null;
            }
        }

        public static List<Bitmap> GetVideoFrames(string videoPath, OpenCvSharp.Size? resolution = null)
        {
            var frames = new List<Bitmap>();
            var capture = new VideoCapture(videoPath);

            if (resolution.HasValue)
            {
                // Set the desired frame resolution
                capture.Set(VideoCaptureProperties.FrameWidth, resolution.Value.Width);
                capture.Set(VideoCaptureProperties.FrameHeight, resolution.Value.Height);
            }

            while (capture.IsOpened())
            {
                var image = new Mat();

                // Read next frame in video file
                capture.Read(image);

                if (image.Empty())
                {
                    break;
                }

                // Resize the frame to the specified resolution
                if (resolution.HasValue)
                {
                    Cv2.Resize(image, image, resolution.Value);
                }

                frames.Add(BitmapConverter.ToBitmap(image));
            }

            return frames;
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

                // get video frames
                List<Bitmap> images = GetVideoFrames(selectedVideoPath, size);

                // detect cue ball in each frame
                System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
                double totalDetectionTime = 0;

                foreach (var image in images)
                {
                    pictureBoxImage.Image = image;

                    // start the stopwatch
                    stopwatch.Restart();

                    // detect the cue ball in the current frame
                    HighlightCueball(pictureBoxImage, 150);

                    // stop the stopwatch and add the elapsed time to the total
                    stopwatch.Stop();
                    totalDetectionTime += stopwatch.Elapsed.TotalSeconds;

                    // update the labelFPS with the current average FPS
                    double averageFps = images.IndexOf(image) / totalDetectionTime;
                    labelFrameRate.Text = $"FPS: {averageFps:F2}";

                    // refresh the UI to show the updated image and FPS
                    Application.DoEvents();
                }
            }
        }
    }
}