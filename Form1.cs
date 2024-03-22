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
using OpenCvSharp;
using System.Net;

namespace billiard_laser
{
    public partial class Form1 : Form
    {
        SerialPort serialPort;
        const string LASER_OFF = "0";
        const string LASER_ON = "1";

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
            detector.FindAndDrawCueBall(pictureBoxImage);
        }

        private void btnGetCameraInput_Click(object sender, EventArgs e)
        {
            // Create a new background worker
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            string cameraUrl = "http://192.168.43.1:8080/video";

            using (WebClient webClient = new WebClient())
            {
                try
                {
                    // Create a stream to receive the video data
                    Stream videoStream = webClient.OpenRead(cameraUrl);

                    // Read the video data in chunks and update the PictureBox
                    byte[] buffer = new byte[1024];
                    int bytesRead;

                    while ((bytesRead = videoStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        // Create a MemoryStream from the received chunk
                        using (MemoryStream memoryStream = new MemoryStream(buffer, 0, bytesRead))
                        {
                            // Create a new Bitmap object from the MemoryStream
                            Bitmap bitmap = new Bitmap(memoryStream);

                            // Update the PictureBox control's Image property on the UI thread
                            pictureBoxImage.Invoke(new Action(() => pictureBoxImage.Image = bitmap));
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that may occur during the video stream processing
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}