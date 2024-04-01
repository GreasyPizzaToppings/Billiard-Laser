using System.IO.Ports;
using AForge.Video.DirectShow;
using AForge.Video;


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
            CueBallDetector detector = new CueBallDetector();
            detector.FindAndDrawCueBall(pictureBoxImage, 150);
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
    }
}