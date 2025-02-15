using Emgu.CV.Structure;

namespace billiard_laser
{
    public partial class LaserDetectionDebugForm : Form, IDisposable
    {
        private bool initialisingControls = true;
        private LaserDetector laserDetector;
        private bool disposed = false;
        private Bitmap originalImage;

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

        private void LogLaserInfo(Laser laser)
        {
            if (laser == null) return;
            Console.WriteLine(laser);
        }


        /// <summary>
        /// Shows the debug images from laser detection results in the UI
        /// </summary>
        /// <param name="images">The laser detection results containing debug images</param>
        private void DisplayDebugImages(LaserDetectionResults images)
        {
            if (images == null) return;

            SetImage(filteredCandidatesPicBox, images.FilteredCandidatesHighlighted);
            SetImage(allCandidatesPicBox, images.AllCandidatesHighlighted);
            SetImage(scoredCandidatesPicBox, images.ScoredCandidatesHighlighted);
            SetImage(laserMaskPicBox, images.LaserMask);
            SetImage(laserFoundPicBox, images.LaserHighlighted);
            SetImage(workingImagePicBox, images.WorkingImage);

            LogLaserInfo(images.Laser);
        }

        /// <summary>
        /// Shows precalculated debug images
        /// </summary>
        /// <param name="images">The precalculated laser detection results</param>
        public void ShowDebugImages(LaserDetectionResults images)
        {
            if (originalImage != null) originalImage.Dispose();
            originalImage = new Bitmap(images.OriginalImage);

            DisplayDebugImages(images);
        }

        /// <summary>
        /// Processes a raw image through laser detection and shows the debug images
        /// </summary>
        /// <param name="rawImage">The raw image to process</param>
        public void GetAndShowDebugImages(Bitmap rawImage)
        {
            if (originalImage != null) originalImage.Dispose();
            originalImage = new Bitmap(rawImage);

            LaserDetectionResults images = laserDetector.ProcessLaserDetection(rawImage);
            DisplayDebugImages(images);
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

            LogObjectDetectorSettings();

            //update images upon setting changes
            if (originalImage != null) ShowDebugImages(laserDetector.ProcessLaserDetection(originalImage));
        }

        private void LogObjectDetectorSettings()
        {
            Console.WriteLine(laserDetector);
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
            filteredCandidatesPicBox.Image?.Dispose();
            allCandidatesPicBox.Image?.Dispose();
            scoredCandidatesPicBox.Image?.Dispose();
            laserMaskPicBox.Image?.Dispose();
            laserFoundPicBox.Image?.Dispose();
            workingImagePicBox.Image?.Dispose();
            originalImage?.Dispose();

            laserDetector = null;

            DebugFormClosed?.Invoke(this, EventArgs.Empty);
        }
    }
}
