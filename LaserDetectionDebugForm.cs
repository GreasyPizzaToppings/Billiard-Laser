using Emgu.CV.Structure;

namespace billiard_laser
{
    public partial class LaserDetectionDebugForm : Form, IDisposable
    {
        private bool initialisingControls = true;
        private LaserDetector laserDetector;
        private bool disposed = false;

        public event EventHandler DebugFormClosed;

        public LaserDetectionDebugForm(LaserDetector laserDetector)
        {
            this.laserDetector = laserDetector;
            this.FormClosed += ImageProcessingDebugForm_FormClosed;

            InitializeComponent();
            InitMaskTrackbars();
            InitCheckBoxes();

            initialisingControls = false;
        }

        public void ShowDebugImages(Bitmap rawImage)
        {
            LaserDetectionResults images = laserDetector.ProcessLaserDetection(rawImage);

            SetImage(originalImagePicBox, images.OriginalImage);
            SetImage(filteredCandidatesPicBox, images.FilteredCandidatesHighlighted);
            SetImage(allCandidatesPicBox, images.AllCandidatesHighlighted);
            SetImage(invMaskPicBox, images.TableMask);
            SetImage(appliedMaskPicBox, images.TableWithMaskApplied);
            SetImage(laserMaskPicBox, images.LaserMask);
            SetImage(laserFoundPicBox, images.LaserHighlighted);
            SetImage(transformedImagePicBox, images.TransformedImage);
        }

        private void SetImage(PictureBox pictureBox, Image newImage)
        {
            var oldImage = pictureBox.Image;
            pictureBox.Image = newImage != null ? new Bitmap(newImage) : null;
            oldImage?.Dispose();
        }

        private void InitCheckBoxes()
        {
            checkBoxEnableBlurr.Checked = laserDetector.EnableBlur;
            checkBoxEnableSharpen.Checked = laserDetector.EnableSharpening;
            checkBoxEnableTableBoundary.Checked = laserDetector.EnableTableBoundary;
        }

        private void SetObjectDetectorSettings()
        {
            if (initialisingControls)
                return;

            laserDetector.LowerLaserMask = new Rgb(trackBarLaserMaskRedMin.Value, trackBarLaserMaskGreenMin.Value, trackBarLaserMaskBlueMin.Value);
            laserDetector.UpperLaserMask = new Rgb(trackBarLaserMaskRedMax.Value, trackBarLaserMaskGreenMax.Value, trackBarLaserMaskBlueMax.Value);

            laserDetector.EnableBlur = checkBoxEnableBlurr.Checked;
            laserDetector.EnableSharpening = checkBoxEnableSharpen.Checked;
            laserDetector.EnableTableBoundary = checkBoxEnableTableBoundary.Checked;

            PrintObjectDetectorSettings();

            //update images upon setting changes
            if (originalImagePicBox.Image != null) ShowDebugImages((Bitmap)originalImagePicBox.Image);
        }

        private void PrintObjectDetectorSettings()
        {
            Console.WriteLine(
              $"\nImage processing settings changed! TableObjectDetector Values:" +
              $"\nLower Laser Mask RGB: {laserDetector.LowerLaserMask}" +
              $"\nUpper Laser Mask RGB: {laserDetector.UpperLaserMask}" +
              $"\nEnable Blur: {laserDetector.EnableBlur}" +
              $"\nEnable Sharpening: {laserDetector.EnableSharpening}\n" +
              $"\nEnable Table Boundary: {laserDetector.EnableTableBoundary}\n"
            );
        }

        #region Trackbars/Sliders

        private void InitMaskTrackbars()
        {
            trackBarLaserMaskRedMin.Value = (int)laserDetector.LowerLaserMask.Red;
            trackBarLaserMaskGreenMin.Value = (int)laserDetector.LowerLaserMask.Green;
            trackBarLaserMaskBlueMin.Value = (int)laserDetector.LowerLaserMask.Blue;

            trackBarLaserMaskRedMax.Value = (int)laserDetector.UpperLaserMask.Red;
            trackBarLaserMaskGreenMax.Value = (int)laserDetector.UpperLaserMask.Green;
            trackBarLaserMaskBlueMax.Value = (int)laserDetector.UpperLaserMask.Blue;

            //labels
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
            SetObjectDetectorSettings();
        }

        private void trackBarMaskGreenMin_ValueChanged(object sender, EventArgs e)
        {
            labelClothMaskGreenMinValue.Text = trackBarClothMaskGreenMin.Value.ToString();
            SetObjectDetectorSettings();
        }

        private void trackBarMaskBlueMin_ValueChanged(object sender, EventArgs e)
        {
            labelClothMaskBlueMinValue.Text = trackBarClothMaskBlueMin.Value.ToString();
            SetObjectDetectorSettings();
        }

        private void trackBarMaskRedMax_ValueChanged(object sender, EventArgs e)
        {
            labelClothMaskRedMaxValue.Text = trackBarClothMaskRedMax.Value.ToString();
            SetObjectDetectorSettings();
        }

        private void trackBarMaskGreenMax_ValueChanged(object sender, EventArgs e)
        {
            labelClothMaskGreenMaxValue.Text = trackBarClothMaskGreenMax.Value.ToString();
            SetObjectDetectorSettings();
        }

        private void trackBarMaskBlueMax_ValueChanged(object sender, EventArgs e)
        {
            labelClothMaskBlueMaxValue.Text = trackBarClothMaskBlueMax.Value.ToString();
            SetObjectDetectorSettings();
        }

        private void trackBarLaserMaskRedMin_ValueChanged(object sender, EventArgs e)
        {
            labelLaserMaskRedMin.Text = trackBarLaserMaskRedMin.Value.ToString();
            SetObjectDetectorSettings();
        }

        private void trackBarLaserMaskRedMax_ValueChanged(object sender, EventArgs e)
        {
            labelLaserMaskRedMax.Text = trackBarLaserMaskRedMax.Value.ToString();
            SetObjectDetectorSettings();
        }

        private void trackBarLaserMaskGreenMin_ValueChanged(object sender, EventArgs e)
        {
            labelLaserMaskGreenMin.Text = trackBarLaserMaskGreenMin.Value.ToString();
            SetObjectDetectorSettings();
        }

        private void trackBarLaserMaskGreenMax_ValueChanged(object sender, EventArgs e)
        {
            labelLaserMaskGreenMax.Text = trackBarLaserMaskGreenMax.Value.ToString();
            SetObjectDetectorSettings();
        }

        private void trackBarLaserMaskBlueMin_ValueChanged(object sender, EventArgs e)
        {
            labelLaserMaskBlueMin.Text = trackBarLaserMaskBlueMin.Value.ToString();
            SetObjectDetectorSettings();
        }

        private void trackBarLaserMaskBlueMax_ValueChanged(object sender, EventArgs e)
        {
            labelLaserMaskBlueMax.Text = trackBarLaserMaskBlueMax.Value.ToString();
            SetObjectDetectorSettings();
        }
        #endregion

        private void checkBoxEnableSharpen_CheckedChanged(object sender, EventArgs e)
        {
            SetObjectDetectorSettings();
        }

        private void checkBoxEnableBlurr_CheckedChanged(object sender, EventArgs e)
        {
            SetObjectDetectorSettings();
        }

        private void checkBoxEnableTableBoundary_CheckedChanged(object sender, EventArgs e)
        {
            SetObjectDetectorSettings();
        }

        private void ImageProcessingDebugForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            originalImagePicBox.Image?.Dispose();
            filteredCandidatesPicBox.Image?.Dispose();
            allCandidatesPicBox.Image?.Dispose();
            invMaskPicBox.Image?.Dispose();
            appliedMaskPicBox.Image?.Dispose();
            laserMaskPicBox.Image?.Dispose();
            laserFoundPicBox.Image?.Dispose();
            transformedImagePicBox.Image?.Dispose();

            laserDetector = null;

            DebugFormClosed?.Invoke(this, EventArgs.Empty);
        }
    }
}
