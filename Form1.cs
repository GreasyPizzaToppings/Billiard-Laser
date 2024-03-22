using System;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.IO.Ports;
using System.CodeDom;
using System.Net;
using Emgu.CV;


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

        bool streamVideo = false;
        VideoCapture capture = new VideoCapture(0);


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
            streamVideo = true;

            capture.ImageGrabbed += Capture_ImageGrabbed;
            capture.Start();

            //StreamVideo();

        }


        private void Capture_ImageGrabbed(object sender, EventArgs e)
        {
            var frameSize = new System.Drawing.Size(640, 480);

            if (streamVideo)
            {
                Mat frame = new Mat();
                capture.Retrieve(frame);
                //Mat frame = capture.QueryFrame(); //what is this

                CvInvoke.Resize(frame, frame, frameSize);
                pictureBoxImage.Image = frame.ToBitmap();

                frame.Dispose();
            }
        }

        private async void StreamVideo()
        {
            while (streamVideo)
            {

                Mat frame = new Mat();

                try
                {
                    capture.Read(frame);

                    if (frame.IsEmpty)
                    {
                        Console.Write("gay");
                    }

                    MessageBox.Show(frame.Size.ToString());

                    double frameWidth = capture.Get(Emgu.CV.CvEnum.CapProp.FrameWidth);
                    double frameHeight = capture.Get(Emgu.CV.CvEnum.CapProp.FrameHeight);
                    double fps = capture.Get(Emgu.CV.CvEnum.CapProp.Fps);

                    // Check if the properties have valid values
                    if (frameWidth > 0 && frameHeight > 0 && fps > 0)
                    {
                        // Video source properties are valid, proceed with frame capture
                        // ...
                    }
                    else
                    {
                        // Video source properties are invalid, handle the error
                        MessageBox.Show("Invalid video source properties.");
                    }

                    //capture.Retrieve(frame);



                    var frameSize = new System.Drawing.Size(640, 480);
                    CvInvoke.Resize(frame, frame, frameSize);

                    var img = frame.ToBitmap();
                    pictureBoxImage.Image = img;

                    await Task.Delay(16);
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

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
    }
}