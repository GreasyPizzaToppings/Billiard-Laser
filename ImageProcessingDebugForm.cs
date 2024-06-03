using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace billiard_laser
{
    public partial class ImageProcessingDebugForm : Form
    {
        //controls
        private List<PictureBox> pictureBoxes = new List<PictureBox>();
        private List<Label> labels = new List<Label>();

        public event EventHandler DebugFormClosed;

        public ImageProcessingDebugForm()
        {
            InitializeComponent();
            this.FormClosed += ImageProcessingDebugForm_FormClosed;
            BilliardLaserForm.ProcessedDebugFrame += ShowDebugImages; //subscribe to event where it processes a debug frame

            GetControlsLists();
        }

        private void ImageProcessingDebugForm_Load(object sender, EventArgs e)
        {
            ImageProcessingDebugForm_Resize(this, EventArgs.Empty);
        }

        private void GetControlsLists()
        {
            // Filter and sort PictureBox controls by their position
            pictureBoxes = this.Controls.OfType<PictureBox>()
                                          .OrderBy(p => p.Top)
                                          .ThenBy(p => p.Left)
                                          .ToList();

            // Filter and sort Label controls by their position
            labels = this.Controls.OfType<Label>()
                                   .OrderBy(l => l.Top)
                                   .ThenBy(l => l.Left)
                                   .ToList();
        }

        public void ShowDebugImages(object sender, ImageProcessingResults images)
        {
            //set pictureboxes with all the images

            originalImagePicBox.Image = images.OriginalImage;
            blurredImagePicBox.Image = images.BlurredImage;
            sharpenedImagePicBox.Image = images.SharpenedImage;
            blurredSharpenedImagePicBox.Image = images.BlurredAndSharpenedImage;
            filteredContoursPicBox.Image = images.FilteredBallsFound;
            allContoursPicBox.Image = images.AllBallsFound;
            invMaskPicBox.Image = images.ImageMask;
            appliedMaskPicBox.Image = images.ImageWithMaskApplied;
        }

        private void trackBarMaskRedMin_ValueChanged(object sender, EventArgs e)
        {
            labelMaskRedMinValue.Text = trackBarMaskRedMin.Value.ToString();
        }

        private void trackBarMaskGreenMin_ValueChanged(object sender, EventArgs e)
        {
            labelMaskGreenMinValue.Text = trackBarMaskGreenMin.Value.ToString();
        }

        private void trackBarMaskBlueMin_ValueChanged(object sender, EventArgs e)
        {
            labelMaskBlueMinValue.Text = trackBarMaskBlueMin.Value.ToString();
        }

        private void trackBarMaskRedMax_Scroll(object sender, EventArgs e)
        {
            labelMaskRedMaxValue.Text = trackBarMaskRedMax.Value.ToString();
        }

        private void trackBarMaskRedMax_ValueChanged(object sender, EventArgs e)
        {
            labelMaskRedMaxValue.Text = trackBarMaskRedMax.Value.ToString();
        }

        private void trackBarMaskGreenMax_ValueChanged(object sender, EventArgs e)
        {
            labelMaskGreenMaxValue.Text = trackBarMaskGreenMax.Value.ToString();
        }

        private void trackBarMaskBlueMax_ValueChanged(object sender, EventArgs e)
        {
            labelMaskBlueMaxValue.Text = trackBarMaskBlueMax.Value.ToString();
        }

        private void trackBarMaskRed_ValueChanged(object sender, EventArgs e)
        {
            labelMaskRedMinValue.Text = trackBarMaskRedMin.Value.ToString();
        }

        private void trackBarMaskGreen_ValueChanged(object sender, EventArgs e)
        {
            labelMaskGreenMinValue.Text = trackBarMaskGreenMin.Value.ToString();
        }

        private void trackBarMaskBlue_ValueChanged(object sender, EventArgs e)
        {
            labelMaskBlueMinValue.Text = trackBarMaskBlueMin.Value.ToString();
        }

        private void ImageProcessingDebugForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            DebugFormClosed?.Invoke(this, EventArgs.Empty);
            this.Dispose();
        }

        private void ImageProcessingDebugForm_Resize(object sender, EventArgs e)
        {
            ResizePictureBoxes(10, 4, 2);
            RepositionPictureBoxes(10);

            RepositionLabels();
        }

        private void RepositionLabels()
        {
            //picturebox labels
            for (int i = 0; i < pictureBoxes.Count; i++)
            {
                PictureBox pictureBox = pictureBoxes[i];
                Label label = labels[i];

                // Calculate the new position of the label
                int labelX = pictureBox.Left + (pictureBox.Width - label.Width) / 2;
                int labelY = pictureBox.Top;

                // Set the new position of the label
                label.Location = new Point(labelX, labelY);
            }
        }

        private void ResizePictureBoxes(int padding, int columns, int rows)
        {
            // Calculate the size of each PictureBox based on the size of the form
            int pictureBoxWidth = (this.ClientSize.Width - (padding * (columns + 1))) / columns;
            int pictureBoxHeight = (this.ClientSize.Height - (padding * (rows + 1))) / (rows); //to make room for the controls at the bottom

            // Set the size of each PictureBox
            foreach (PictureBox pictureBox in pictureBoxes)
            {
                pictureBox.Size = new Size(pictureBoxWidth, pictureBoxHeight);
            }
        }

        private void RepositionPictureBoxes(int padding)
        {
            // Calculate the position of each PictureBox
            int x = padding;
            int y = padding;
            foreach (PictureBox pictureBox in pictureBoxes)
            {
                pictureBox.Location = new Point(x, y);
                x += pictureBox.Width + padding;

                //move down when no room
                if (x + pictureBox.Width > this.ClientSize.Width)
                {
                    x = padding;
                    y += pictureBox.Height + padding;
                }
            }
        }


    }
}
