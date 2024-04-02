using System.IO.Ports;
using AForge.Video.DirectShow;
using AForge.Video;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Drawing;
using Emgu.CV.Reg;
using System.Windows.Forms;

namespace billiard_laser
{
    public partial class Form1 : Form
    {
        SerialPort serialPort;
        const string LASER_OFF = "0";
        const string LASER_ON = "1";
        const string LEFT = "l";
        const string RIGHT = "r";
        const string UP = "u";
        const string DOWN = "d";

        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBoxImage.SizeMode = PictureBoxSizeMode.Zoom;

            // try connect to arduino. close serial monitor in arduino ide if not working
            try
            {
                serialPort = new SerialPort("COM3", 9600);
                serialPort.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
            }

            // fill combo box with camera options
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Device in filterInfoCollection)
                cboCamera.Items.Add(Device.Name);
            cboCamera.SelectedIndex = 0;

            videoCaptureDevice = new VideoCaptureDevice();
        }


        private void btnLaserOn_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort.WriteLine(LASER_ON); // Send "on" command to Arduino to turn on the laser
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
            }
        }

        private void btnLaserOff_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort.WriteLine(LASER_OFF); // Send "off" command to Arduino to turn off the laser
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
            }
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

        private void HighlightCueball(PictureBox pictureBox, int threshold) {
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

        private void btnUp_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort.WriteLine(UP);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort.WriteLine(LEFT);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
            }
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort.WriteLine(RIGHT);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort.WriteLine(DOWN);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
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

        public static List<Bitmap> GetVideoFrames(string videoPath)
        {

            var frames = new List<Bitmap>();
            var capture = new VideoCapture(videoPath);

            while (capture.IsOpened())
            {
                var image = new Mat();

                // Read next frame in video file
                capture.Read(image);
                if (image.Empty())
                {
                    break;
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
                List<Bitmap> images = GetVideoFrames(selectedVideoPath);

                // detect cue ball in each frame
                System.Timers.Timer cbTimer = new System.Timers.Timer();
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