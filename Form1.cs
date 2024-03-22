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
using AForge.Video;

namespace billiard_laser
{
    public partial class Form1 : Form
    {
        SerialPort serialPort;
        const string LASER_OFF = "0";
        const string LASER_ON = "1";

        private MJPEGStream mjpegStream = new MJPEGStream(); 
        //private VideoCaptureDevice videoSource;

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
            // Create a MJPEGStream instance with the camera URL
            string cameraUrl = "http://192.168.43.1:8080/video";
            mjpegStream.Source = cameraUrl;

            // Set the NewFrame event handler to update the PictureBox
            mjpegStream.NewFrame += VideoSource_NewFrame;
            mjpegStream.VideoSourceError += MjpegStream_VideoSourceError;

            // Start the video source
            mjpegStream.Start();
        }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            // Update the PictureBox with the new frame
            pictureBoxImage.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void MjpegStream_VideoSourceError(object sender, AForge.Video.VideoSourceErrorEventArgs eventArgs)
        {
            // Handle the error here
            MessageBox.Show("Error occurred during MJPEG stream: " + eventArgs.Description);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            mjpegStream.Stop();
        }

    }
}