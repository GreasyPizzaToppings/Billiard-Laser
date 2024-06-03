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
        public event EventHandler DebugFormClosed;

        public ImageProcessingDebugForm()
        {
            InitializeComponent();
            this.FormClosed += ImageProcessingDebugForm_FormClosed;
            BilliardLaserForm.ProcessedDebugFrame += ShowDebugImages; //subscribe to event where it processes a debug frame
        }


        public void ShowDebugImages(object sender, ImageProcessingResults images) {
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

        private void trackBarMaskRed_ValueChanged(object sender, EventArgs e)
        {
            labelMaskRedValue.Text = trackBarMaskRed.Value.ToString();
        }

        private void trackBarMaskGreen_ValueChanged(object sender, EventArgs e)
        {
            labelMaskGreenValue.Text = trackBarMaskGreen.Value.ToString();
        }

        private void trackBarMaskBlue_ValueChanged(object sender, EventArgs e)
        {
            labelMaskBlueValue.Text = trackBarMaskBlue.Value.ToString();
        }

        private void ImageProcessingDebugForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            DebugFormClosed?.Invoke(this, EventArgs.Empty);
            this.Dispose();
        }
    }
}
