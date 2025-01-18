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

        private void LogLaserInfo(Laser laser)
        {
            if (laser == null) return;
            Console.WriteLine(
                $"Laser Position: ({laser.Location.X}, {laser.Location.Y})\n" +
                $"Intensity: {laser.Intensity:F2}\n" +
                $"Area: {laser.Area:F2}\n" +
                $"Confidence: {laser.Confidence:F2}\n"
            );
        }


        /// <summary>
        /// show precalculated debug images
        /// </summary>
        /// <param name="images"></param>
        public void ShowDebugImages(LaserDetectionResults images)
        {
            if (images == null) return;

            SetImage(originalImagePicBox, images.OriginalImage);
            SetImage(filteredCandidatesPicBox, images.FilteredCandidatesHighlighted);
            SetImage(allCandidatesPicBox, images.AllCandidatesHighlighted);
            SetImage(scoredCandidatesPicBox, images.ScoredCandidatesHighlighted);
            SetImage(appliedMaskPicBox, images.LaserMaskApplied);
            SetImage(laserMaskPicBox, images.LaserMask);
            SetImage(laserFoundPicBox, images.LaserHighlighted);
            SetImage(transformedImagePicBox, images.TransformedImage);

            LogLaserInfo(images.Laser);
        }

        /// <summary>
        /// calculate and show debug images
        /// </summary>
        /// <param name="rawImage"></param>
        public void GetAndShowDebugImages(Bitmap rawImage)
        {
            LaserDetectionResults images = laserDetector.ProcessLaserDetection(rawImage);
            if (images == null) return;

            SetImage(originalImagePicBox, images.OriginalImage);
            SetImage(filteredCandidatesPicBox, images.FilteredCandidatesHighlighted);
            SetImage(allCandidatesPicBox, images.AllCandidatesHighlighted);
            SetImage(scoredCandidatesPicBox, images.ScoredCandidatesHighlighted);
            SetImage(appliedMaskPicBox, images.LaserMaskApplied);
            SetImage(laserMaskPicBox, images.LaserMask);
            SetImage(laserFoundPicBox, images.LaserHighlighted);
            SetImage(transformedImagePicBox, images.TransformedImage);

            LogLaserInfo(images.Laser);
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
            if (originalImagePicBox.Image != null) GetAndShowDebugImages((Bitmap)originalImagePicBox.Image);
        }

        private void LogObjectDetectorSettings()
        {
            Console.WriteLine(
              $"\nImage processing settings changed! LaserDetector Values:" +
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
            scoredCandidatesPicBox.Image?.Dispose();
            appliedMaskPicBox.Image?.Dispose();
            laserMaskPicBox.Image?.Dispose();
            laserFoundPicBox.Image?.Dispose();
            transformedImagePicBox.Image?.Dispose();

            laserDetector = null;

            DebugFormClosed?.Invoke(this, EventArgs.Empty);
        }
    }
}
