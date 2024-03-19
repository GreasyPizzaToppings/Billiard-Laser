using System;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
namespace billiard_laser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnCheckImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                switch (Path.GetExtension(ofd.FileName).ToUpper())
                {
                    case ".BMP":
                        pictureBoxImage.Image.Save(ofd.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case ".JPG":
                        pictureBoxImage.Image.Save(ofd.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case ".PNG":
                        pictureBoxImage.Save(ofd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}