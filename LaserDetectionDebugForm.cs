using Emgu.CV.Structure;

namespace billiard_laser
{
    public partial class LaserDetectionDebugForm : Form, IDisposable
    {
        private bool initialisingControls = true;
        private TableObjectDetector objectDetector;
        private bool disposed = false;

        public event EventHandler DebugFormClosed;

        public LaserDetectionDebugForm(TableObjectDetector objectDetector)
        {
            this.objectDetector = objectDetector;
            this.FormClosed += ImageProcessingDebugForm_FormClosed;

            InitializeComponent();
            InitMaskTrackbars();
            InitCheckBoxes();

            initialisingControls = false;
        }

        public void ShowDebugImages(Bitmap rawImage)
        {
            LaserDetectionResults images = objectDetector.ProcessLaserDetection(rawImage);

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
            checkBoxEnableBlurr.Checked = objectDetector.EnableBlur;
            checkBoxEnableSharpen.Checked = objectDetector.EnableSharpening;
            checkBoxEnableTableBoundary.Checked = objectDetector.EnableTableBoundary;
        }

        private void SetObjectDetectorSettings()
        {
            if (initialisingControls)
                return;

            objectDetector.LowerClothMask = new Rgb(trackBarClothMaskRedMin.Value, trackBarClothMaskGreenMin.Value, trackBarClothMaskBlueMin.Value);
            objectDetector.UpperClothMask = new Rgb(trackBarClothMaskRedMax.Value, trackBarClothMaskGreenMax.Value, trackBarClothMaskBlueMax.Value);

            objectDetector.LowerLaserMask = new Rgb(trackBarLaserMaskRedMin.Value, trackBarLaserMaskGreenMin.Value, trackBarLaserMaskBlueMin.Value);
            objectDetector.UpperLaserMask = new Rgb(trackBarLaserMaskRedMax.Value, trackBarLaserMaskGreenMax.Value, trackBarLaserMaskBlueMax.Value);

            objectDetector.EnableBlur = checkBoxEnableBlurr.Checked;
            objectDetector.EnableSharpening = checkBoxEnableSharpen.Checked;
            objectDetector.EnableTableBoundary = checkBoxEnableTableBoundary.Checked;

            PrintObjectDetectorSettings();

            //update images upon setting changes
            if (originalImagePicBox.Image != null) ShowDebugImages((Bitmap)originalImagePicBox.Image);
        }

        private void PrintObjectDetectorSettings()
        {
            Console.WriteLine(
              $"\nImage processing settings changed! TableObjectDetector Values:" +
              $"\nLower Cloth Mask RGB: {objectDetector.LowerClothMask}" +
              $"\nUpper Cloth Mask RGB: {objectDetector.UpperClothMask}" +
              $"\nLower Laser Mask RGB: {objectDetector.LowerLaserMask}" +
              $"\nUpper Laser Mask RGB: {objectDetector.UpperLaserMask}" +
              $"\nEnable Blur: {objectDetector.EnableBlur}" +
              $"\nEnable Sharpening: {objectDetector.EnableSharpening}\n" +
              $"\nEnable Table Boundary: {objectDetector.EnableTableBoundary}\n"
            );
        }

        #region Trackbars/Sliders

        private void InitMaskTrackbars()
        {
            //trackbar values
            trackBarClothMaskRedMin.Value = (int)objectDetector.LowerClothMask.Red;
            trackBarClothMaskGreenMin.Value = (int)objectDetector.LowerClothMask.Green;
            trackBarClothMaskBlueMin.Value = (int)objectDetector.LowerClothMask.Blue;

            trackBarClothMaskRedMax.Value = (int)objectDetector.UpperClothMask.Red;
            trackBarClothMaskGreenMax.Value = (int)objectDetector.UpperClothMask.Green;
            trackBarClothMaskBlueMax.Value = (int)objectDetector.UpperClothMask.Blue;

            trackBarLaserMaskRedMin.Value = (int)objectDetector.LowerLaserMask.Red;
            trackBarLaserMaskGreenMin.Value = (int)objectDetector.LowerLaserMask.Green;
            trackBarLaserMaskBlueMin.Value = (int)objectDetector.LowerLaserMask.Blue;

            trackBarLaserMaskRedMax.Value = (int)objectDetector.UpperLaserMask.Red;
            trackBarLaserMaskGreenMax.Value = (int)objectDetector.UpperLaserMask.Green;
            trackBarLaserMaskBlueMax.Value = (int)objectDetector.UpperLaserMask.Blue;

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

            objectDetector = null;

            DebugFormClosed?.Invoke(this, EventArgs.Empty);
        }
    }
}
