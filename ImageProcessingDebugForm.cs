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
        //controls
        private List<PictureBox> pictureBoxes = new List<PictureBox>();
        private List<Label> labels = new List<Label>();

        //image processing settings
        public Rgb LowerMaskRgb { get; }
        public Rgb UpperMaskRgb { get; }
        public bool EnableBlur { get; }
        public bool EnableSharpening { get; }

        //events
        public event EventHandler DebugFormClosed;
        public event EventHandler<ImageProcessingSettingsChanged> ImageProcessingSettingsChanged;

        public ImageProcessingDebugForm(Rgb lowerMaskRgb, Rgb upperMaskRgb, bool enableBlur, bool enableSharpening)
        {
            InitializeComponent();
            this.FormClosed += ImageProcessingDebugForm_FormClosed;
            BilliardLaserForm.ProcessedDebugFrame += UpdateDebugImages; //subscribe to event where it processes a debug frame

            GetControlsLists();

            LowerMaskRgb = lowerMaskRgb;
            UpperMaskRgb = upperMaskRgb;
            EnableBlur = enableBlur;
            EnableSharpening = enableSharpening;

            SetMaskTrackbars();
        }

        //signal to subscriber form to change its mask value or blur and sharpen in its image processing
        private void RaiseImageProcessingSettingsChanged()
        {
            Rgb lowerMaskRgb = new Rgb(trackBarMaskRedMin.Value, trackBarMaskGreenMin.Value, trackBarMaskBlueMin.Value);
            Rgb upperMaskRgb = new Rgb(trackBarMaskRedMax.Value, trackBarMaskGreenMax.Value, trackBarMaskBlueMax.Value);

            ImageProcessingSettingsChanged?.Invoke(this, new ImageProcessingSettingsChanged { LowerMaskRgb = lowerMaskRgb, UpperMaskRgb = upperMaskRgb, EnableBlur = checkBoxEnableBlurr.Checked, EnableSharpening = checkBoxEnableSharpen.Checked });
        }

        public void UpdateDebugImages(object sender, ImageProcessingResults images)
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

        #region Trackbars/Sliders

        //initialise to mask value
        private void SetMaskTrackbars()
        {
            // Set trackbar values for the lower mask
            trackBarMaskRedMin.Value = (int)LowerMaskRgb.Red;
            trackBarMaskGreenMin.Value = (int)LowerMaskRgb.Green;
            trackBarMaskBlueMin.Value = (int)LowerMaskRgb.Blue;

            // Set trackbar values for the upper mask
            trackBarMaskRedMax.Value = (int)UpperMaskRgb.Red;
            trackBarMaskGreenMax.Value = (int)UpperMaskRgb.Green;
            trackBarMaskBlueMax.Value = (int)UpperMaskRgb.Blue;

            // Update label text to reflect trackbar values
            labelMaskRedMinValue.Text = trackBarMaskRedMin.Value.ToString();
            labelMaskGreenMinValue.Text = trackBarMaskGreenMin.Value.ToString();
            labelMaskBlueMinValue.Text = trackBarMaskBlueMin.Value.ToString();

            labelMaskRedMaxValue.Text = trackBarMaskRedMax.Value.ToString();
            labelMaskGreenMaxValue.Text = trackBarMaskGreenMax.Value.ToString();
            labelMaskBlueMaxValue.Text = trackBarMaskBlueMax.Value.ToString();
        }
        private void trackBarMaskRedMin_ValueChanged(object sender, EventArgs e)
        {
            labelMaskRedMinValue.Text = trackBarMaskRedMin.Value.ToString();
            RaiseImageProcessingSettingsChanged();
        }

        private void trackBarMaskGreenMin_ValueChanged(object sender, EventArgs e)
        {
            labelMaskGreenMinValue.Text = trackBarMaskGreenMin.Value.ToString();
            RaiseImageProcessingSettingsChanged();
        }

        private void trackBarMaskBlueMin_ValueChanged(object sender, EventArgs e)
        {
            labelMaskBlueMinValue.Text = trackBarMaskBlueMin.Value.ToString();
            RaiseImageProcessingSettingsChanged();
        }

        private void trackBarMaskRedMax_ValueChanged(object sender, EventArgs e)
        {
            labelMaskRedMaxValue.Text = trackBarMaskRedMax.Value.ToString();
            RaiseImageProcessingSettingsChanged();
        }

        private void trackBarMaskGreenMax_ValueChanged(object sender, EventArgs e)
        {
            labelMaskGreenMaxValue.Text = trackBarMaskGreenMax.Value.ToString();
            RaiseImageProcessingSettingsChanged();
        }

        private void trackBarMaskBlueMax_ValueChanged(object sender, EventArgs e)
        {
            labelMaskBlueMaxValue.Text = trackBarMaskBlueMax.Value.ToString();
            RaiseImageProcessingSettingsChanged();
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
            RaiseImageProcessingSettingsChanged();
        }

        private void checkBoxEnableBlurr_CheckedChanged(object sender, EventArgs e)
        {
            RaiseImageProcessingSettingsChanged();
        }
    }

    public class ImageProcessingSettingsChanged : EventArgs
    {
        public Rgb LowerMaskRgb { get; set; }
        public Rgb UpperMaskRgb { get; set; }
        public bool EnableBlur { get; set; }
        public bool EnableSharpening { get; set; }
    }
}
