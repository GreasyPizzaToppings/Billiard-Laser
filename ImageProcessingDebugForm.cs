using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace billiard_laser
{
    public partial class ImageProcessingDebugForm : Form
    {
        private List<PictureBox> pictureBoxes = new List<PictureBox>();
        private List<Label> labels = new List<Label>();

        private bool initialisingControls = true;
        private BallDetector ballDetector;

        public event EventHandler DebugFormClosed;

        public ImageProcessingDebugForm(BallDetector ballDetector)
        {
            this.ballDetector = ballDetector;
            this.FormClosed += ImageProcessingDebugForm_FormClosed;

            InitializeComponent();
            GetControlsLists();
            InitMaskTrackbars();
            InitCheckBoxes();

            initialisingControls = false;
        }

        public void ShowDebugImages(Bitmap rawImage)
        {
            ImageProcessingResults images = ballDetector.FindAllBallsDebug(rawImage);

            originalImagePicBox.Image = images.OriginalImage;
            filteredContoursPicBox.Image = images.FilteredBallsHighlighted;
            allContoursPicBox.Image = images.AllBallsHighlighted;
            invMaskPicBox.Image = images.ImageMask;
            appliedMaskPicBox.Image = images.ImageWithMaskApplied;

            if (images.BlurredAndSharpenedImage != null) transformedImagePicBox.Image = images.BlurredAndSharpenedImage;
            else if (images.BlurredImage != null) transformedImagePicBox.Image = images.BlurredImage;
            else if (images.SharpenedImage != null) transformedImagePicBox.Image = images.SharpenedImage;
            else transformedImagePicBox.Image = null;
        }

        private void ImageProcessingDebugForm_Load(object sender, EventArgs e)
        {
            ImageProcessingDebugForm_Resize(this, EventArgs.Empty);
        }

        private void InitCheckBoxes()
        {
            checkBoxEnableBlurr.Checked = ballDetector.EnableBlur;
            checkBoxEnableSharpen.Checked = ballDetector.EnableSharpening;
        }

        private void SetBallDetectorSettings()
        {
            if (initialisingControls)
                return;

            ballDetector.LowerClothMask = new Rgb(trackBarClothMaskRedMin.Value, trackBarClothMaskGreenMin.Value, trackBarClothMaskBlueMin.Value);
            ballDetector.UpperClothMask = new Rgb(trackBarClothMaskRedMax.Value, trackBarClothMaskGreenMax.Value, trackBarClothMaskBlueMax.Value);

            ballDetector.LowerCueBallMask = new Rgb(trackBarCbMaskRedMin.Value, trackBarCbMaskGreenMin.Value, trackBarCbMaskBlueMin.Value);
            ballDetector.UpperCueBallMask = new Rgb(trackBarCbMaskRedMax.Value, trackBarCbMaskGreenMax.Value, trackBarCbMaskBlueMax.Value);

            ballDetector.EnableBlur = checkBoxEnableBlurr.Checked;
            ballDetector.EnableSharpening = checkBoxEnableSharpen.Checked;
            ballDetector.EnableTableBoundary = checkBoxEnableTableBoundary.Checked;

            PrintBallDetectorSettings();

            //update images upon setting changes
            if (originalImagePicBox.Image != null) ShowDebugImages((Bitmap)originalImagePicBox.Image);
        }

        private void PrintBallDetectorSettings()
        {
            Console.WriteLine(
              $"\nImage processing settings changed! BallDetector Values:" +
              $"\nLower Cloth Mask RGB: {ballDetector.LowerClothMask}" +
              $"\nUpper Cloth Mask RGB: {ballDetector.UpperClothMask}" +
              $"\nLower Cb Mask RGB: {ballDetector.LowerCueBallMask}" +
              $"\nUpper Cb Mask RGB: {ballDetector.UpperCueBallMask}" +
              $"\nEnable Blur: {ballDetector.EnableBlur}" +
              $"\nEnable Sharpening: {ballDetector.EnableSharpening}\n"
            );
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

        #region Trackbars/Sliders

        //initialise to mask value
        private void InitMaskTrackbars()
        {
            // Set trackbar values for the lower mask
            trackBarClothMaskRedMin.Value = (int)ballDetector.LowerClothMask.Red;
            trackBarClothMaskGreenMin.Value = (int)ballDetector.LowerClothMask.Green;
            trackBarClothMaskBlueMin.Value = (int)ballDetector.LowerClothMask.Blue;

            // Set trackbar values for the upper mask
            trackBarClothMaskRedMax.Value = (int)ballDetector.UpperClothMask.Red;
            trackBarClothMaskGreenMax.Value = (int)ballDetector.UpperClothMask.Green;
            trackBarClothMaskBlueMax.Value = (int)ballDetector.UpperClothMask.Blue;

            // set trackbar values for the lower cb mask
            trackBarCbMaskRedMin.Value = (int)ballDetector.LowerCueBallMask.Red;
            trackBarCbMaskGreenMin.Value = (int)ballDetector.LowerCueBallMask.Green;
            trackBarCbMaskBlueMin.Value = (int)ballDetector.LowerCueBallMask.Blue;

            // set trackbar values for the upper cb mask
            trackBarCbMaskRedMax.Value = (int)ballDetector.UpperCueBallMask.Red;
            trackBarCbMaskGreenMax.Value = (int)ballDetector.UpperCueBallMask.Green;
            trackBarCbMaskBlueMax.Value = (int)ballDetector.UpperCueBallMask.Blue;

            // Update label text to reflect trackbar values
            labelClothMaskRedMinValue.Text = trackBarClothMaskRedMin.Value.ToString();
            labelClothMaskGreenMinValue.Text = trackBarClothMaskGreenMin.Value.ToString();
            labelClothMaskBlueMinValue.Text = trackBarClothMaskBlueMin.Value.ToString();

            labelClothMaskRedMaxValue.Text = trackBarClothMaskRedMax.Value.ToString();
            labelClothMaskGreenMaxValue.Text = trackBarClothMaskGreenMax.Value.ToString();
            labelClothMaskBlueMaxValue.Text = trackBarClothMaskBlueMax.Value.ToString();
        }
        private void trackBarMaskRedMin_ValueChanged(object sender, EventArgs e)
        {
            labelClothMaskRedMinValue.Text = trackBarClothMaskRedMin.Value.ToString();
            SetBallDetectorSettings();
        }

        private void trackBarMaskGreenMin_ValueChanged(object sender, EventArgs e)
        {
            labelClothMaskGreenMinValue.Text = trackBarClothMaskGreenMin.Value.ToString();
            SetBallDetectorSettings();
        }

        private void trackBarMaskBlueMin_ValueChanged(object sender, EventArgs e)
        {
            labelClothMaskBlueMinValue.Text = trackBarClothMaskBlueMin.Value.ToString();
            SetBallDetectorSettings();
        }

        private void trackBarMaskRedMax_ValueChanged(object sender, EventArgs e)
        {
            labelClothMaskRedMaxValue.Text = trackBarClothMaskRedMax.Value.ToString();
            SetBallDetectorSettings();
        }

        private void trackBarMaskGreenMax_ValueChanged(object sender, EventArgs e)
        {
            labelClothMaskGreenMaxValue.Text = trackBarClothMaskGreenMax.Value.ToString();
            SetBallDetectorSettings();
        }

        private void trackBarMaskBlueMax_ValueChanged(object sender, EventArgs e)
        {
            labelClothMaskBlueMaxValue.Text = trackBarClothMaskBlueMax.Value.ToString();
            SetBallDetectorSettings();
        }

        private void trackBarCbMaskRedMin_ValueChanged(object sender, EventArgs e)
        {
            labelCbMaskRedMin.Text = trackBarCbMaskRedMin.Value.ToString();
            SetBallDetectorSettings();
        }

        private void trackBarCbMaskRedMax_ValueChanged(object sender, EventArgs e)
        {
            labelCbMaskRedMax.Text = trackBarCbMaskRedMax.Value.ToString();
            SetBallDetectorSettings();
        }

        private void trackBarCbMaskGreenMin_ValueChanged(object sender, EventArgs e)
        {
            labelCbMaskGreenMin.Text = trackBarCbMaskGreenMin.Value.ToString();
            SetBallDetectorSettings();
        }

        private void trackBarCbMaskGreenMax_ValueChanged(object sender, EventArgs e)
        {
            labelCbMaskGreenMax.Text = trackBarCbMaskGreenMax.Value.ToString();
            SetBallDetectorSettings();
        }

        private void trackBarCbMaskBlueMin_ValueChanged(object sender, EventArgs e)
        {
            labelCbMaskBlueMin.Text = trackBarCbMaskBlueMin.Value.ToString();
            SetBallDetectorSettings();
        }

        private void trackBarCbMaskBlueMax_ValueChanged(object sender, EventArgs e)
        {
            labelCbMaskBlueMax.Text = trackBarCbMaskBlueMax.Value.ToString();
            SetBallDetectorSettings();
        }
        #endregion

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

        private void checkBoxEnableSharpen_CheckedChanged(object sender, EventArgs e)
        {
            SetBallDetectorSettings();
        }

        private void checkBoxEnableBlurr_CheckedChanged(object sender, EventArgs e)
        {
            SetBallDetectorSettings();
        }

        private void checkBoxEnableTableBoundary_CheckedChanged(object sender, EventArgs e)
        {
            SetBallDetectorSettings();
        }
    }
}
