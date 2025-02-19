using Emgu.CV.Structure;

namespace billiard_laser
{
    public partial class LaserDetectionDebugForm : Form, IDisposable
    {
        private bool initialisingControls = true;
        private LaserDetector laserDetector;
        private Bitmap? originalImage;

        public event EventHandler? DebugFormClosed;

        public LaserDetectionDebugForm(LaserDetector laserDetector)
        {
            this.laserDetector = laserDetector;

            InitializeComponent();
            InitMaskTrackbars();
            InitCheckBoxes();

            initialisingControls = false;
        }

        /// <summary>
        /// Shows the debug images from laser detection results in the UI
        /// </summary>
        /// <param name="images">The laser detection results containing debug images</param>
        public void DisplayDebugImages(LaserDetectionResults? images)
        {
            if (images == null) return;

            originalImage?.Dispose();
            originalImage = images.OriginalImage != null ? new Bitmap(images.OriginalImage) : null;

            SetImage(filteredCandidatesPicBox, images.FilteredCandidatesHighlighted);
            SetImage(allCandidatesPicBox, images.AllCandidatesHighlighted);
            SetImage(scoredCandidatesPicBox, images.ScoredCandidatesHighlighted);
            SetImage(laserMaskPicBox, images.LaserMask);
            SetImage(laserFoundPicBox, images.LaserHighlighted);
            SetImage(workingImagePicBox, images.WorkingImage);

            Console.WriteLine(images.Laser);
        }

        /// <summary>
        /// Processes a raw image through laser detection and shows the debug images
        /// </summary>
        /// <param name="rawImage">The raw image to process</param>
        public void GetAndShowDebugImages(Bitmap rawImage) => DisplayDebugImages(laserDetector.ProcessLaserDetection(rawImage));

        private void SetImage(PictureBox pictureBox, Image? newImage)
        {
            var oldImage = pictureBox.Image;
            pictureBox.Image = newImage != null ? new Bitmap(newImage) : null;
            oldImage?.Dispose();
        }

        private void InitCheckBoxes()
        {
            if (laserDetector == null) return;

            checkBoxEnableBlurr.Checked = laserDetector.EnableBlur;
            checkBoxEnableSharpen.Checked = laserDetector.EnableSharpening;
            checkBoxEnableTableBoundary.Checked = laserDetector.EnableTableBoundary;

            // wire up all checkbox events to a single handler
            checkBoxEnableBlurr.CheckedChanged += CheckBox_CheckedChanged;
            checkBoxEnableSharpen.CheckedChanged += CheckBox_CheckedChanged;
            checkBoxEnableTableBoundary.CheckedChanged += CheckBox_CheckedChanged;
        }

        private void CheckBox_CheckedChanged(object? sender, EventArgs e) => SetObjectDetectorSettings();

        private void SetObjectDetectorSettings()
        {
            if (initialisingControls)
                return;

            laserDetector.LowerLaserMask = new Rgb(trackBarLaserMaskRedMin.Value, trackBarLaserMaskGreenMin.Value, trackBarLaserMaskBlueMin.Value);
            laserDetector.UpperLaserMask = new Rgb(trackBarLaserMaskRedMax.Value, trackBarLaserMaskGreenMax.Value, trackBarLaserMaskBlueMax.Value);

            laserDetector.EnableBlur = checkBoxEnableBlurr.Checked;
            laserDetector.EnableSharpening = checkBoxEnableSharpen.Checked;
            laserDetector.EnableTableBoundary = checkBoxEnableTableBoundary.Checked;

            Console.WriteLine(laserDetector);

            //update images upon setting changes
            if (originalImage != null) DisplayDebugImages(laserDetector.ProcessLaserDetection(originalImage));
        }

        private void InitMaskTrackbars()
        {
            if (laserDetector == null) return;

            var trackbarSettings = new (TrackBar Bar, Label Label, double Value)[]
            {
                (trackBarLaserMaskRedMin, labelLaserMaskRedMin, laserDetector.LowerLaserMask.Red),
                (trackBarLaserMaskGreenMin, labelLaserMaskGreenMin, laserDetector.LowerLaserMask.Green),
                (trackBarLaserMaskBlueMin, labelLaserMaskBlueMin, laserDetector.LowerLaserMask.Blue),
                
                (trackBarLaserMaskRedMax, labelLaserMaskRedMax, laserDetector.UpperLaserMask.Red),
                (trackBarLaserMaskGreenMax, labelLaserMaskGreenMax, laserDetector.UpperLaserMask.Green),
                (trackBarLaserMaskBlueMax, labelLaserMaskBlueMax, laserDetector.UpperLaserMask.Blue)
            };

            foreach (var setting in trackbarSettings)
            {
                setting.Bar.Value = (int)setting.Value;
                setting.Label.Text = setting.Value.ToString();
                setting.Bar.ValueChanged += TrackBar_ValueChanged;
            }
        }

        private void TrackBar_ValueChanged(object? sender, EventArgs e)
        {
            if (sender is TrackBar trackBar)
            {
                var labelName = trackBar.Name.Replace("trackBar", "label");
                if (Controls.Find(labelName, true).FirstOrDefault() is Label label)
                    label.Text = trackBar.Value.ToString();
                
                SetObjectDetectorSettings();
            }
        }

        private void ImageProcessingDebugForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            DebugFormClosed?.Invoke(this, EventArgs.Empty);

            if (components != null) components.Dispose();

            filteredCandidatesPicBox.Image?.Dispose();
            allCandidatesPicBox.Image?.Dispose();
            scoredCandidatesPicBox.Image?.Dispose();
            laserMaskPicBox.Image?.Dispose();
            laserFoundPicBox.Image?.Dispose();
            workingImagePicBox.Image?.Dispose();
            originalImage?.Dispose();

            laserDetector?.Dispose();
        }
    }
}
