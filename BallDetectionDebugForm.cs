using Emgu.CV.Structure;

namespace billiard_laser
{
    public partial class BallDetectionDebugForm : Form, IDisposable
    {
        private bool initialisingControls = true;
        private CueBallDetector ballDetector;
        private bool disposed = false;
        private VideoFrame originalFrame; //store the current base table frame to allow for ball detector settings to change and get new results

        public event EventHandler DebugFormClosed;

        public BallDetectionDebugForm(CueBallDetector ballDetector)
        {
            this.ballDetector = ballDetector;
            this.FormClosed += ImageProcessingDebugForm_FormClosed;

            InitializeComponent();
            InitMaskTrackbars();
            InitCheckBoxes();

            initialisingControls = false;
        }

        /// <summary>
        /// Shows the debug images from ball detection results in the UI
        /// </summary>
        /// <param name="images">The ball detection results containing debug images</param>
        public void DisplayDebugImages(CueBallDetectionResults images)
        {
            if (images == null) return;

            if (originalFrame != null) originalFrame.Dispose();
            originalFrame = images.OriginalFrame.Clone();

            SetImage(workingImagePicBox, images.WorkingImage);
            SetImage(tableMaskAppliedPicBox, images.TableMaskApplied);
            SetImage(cueBallMaskPicBox, images.CueBallMask);
            SetImage(cueBallMaskAppliedPicBox, images.CueBallMaskApplied);
            SetImage(allContoursPicBox, images.AllContoursHighlighted);
            SetImage(cueBallCandidatesPicBox, images.CueBallCandidatesHighlighted);
            SetImage(scoredCandidatesPicBox, images.ScoredCandidatesHighlighted);
            SetImage(cueBallFoundPicBox, images.CueBallHighlighted);

            //LogInfo(images);
        }

        /// <summary>
        /// Processes a raw image through ball detection and shows the debug images
        /// </summary>
        /// <param name="rawImage">The raw image to process</param>
        public void GetAndShowDebugImages(VideoFrame frame)
        {
            DisplayDebugImages(ballDetector.GetCueBallResults(frame));
        }

        private void LogInfo(CueBallDetectionResults images) {
            Console.WriteLine($"Frame: {images.OriginalFrame.Index}\n");
            if (images.CueBall != null) Console.WriteLine(images.CueBall.ToString());
            else Console.WriteLine($"Cueball not found");
        }

        private static void SetImage(PictureBox pictureBox, Image newImage)
        {
            var oldImage = pictureBox.Image;
            pictureBox.Image = newImage != null ? new Bitmap(newImage) : null;
            oldImage?.Dispose();
        }

        private void InitCheckBoxes()
        {
            checkBoxEnableBlurr.Checked = ballDetector.EnableBlur;
            checkBoxEnableSharpen.Checked = ballDetector.EnableSharpening;
            checkBoxEnableTableBoundary.Checked = ballDetector.EnableTableBoundary;
        }

        private void SetObjectDetectorSettings()
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

            LogObjectDetectorSettings();

            //update images upon setting changes
            if (originalFrame != null) GetAndShowDebugImages(originalFrame);
        }

        private void LogObjectDetectorSettings()
        {
            Console.WriteLine(ballDetector);
        }

        #region Trackbars/Sliders

        private void InitMaskTrackbars()
        {
            //trackbar values
            trackBarClothMaskRedMin.Value = (int)ballDetector.LowerClothMask.Red;
            trackBarClothMaskGreenMin.Value = (int)ballDetector.LowerClothMask.Green;
            trackBarClothMaskBlueMin.Value = (int)ballDetector.LowerClothMask.Blue;

            trackBarClothMaskRedMax.Value = (int)ballDetector.UpperClothMask.Red;
            trackBarClothMaskGreenMax.Value = (int)ballDetector.UpperClothMask.Green;
            trackBarClothMaskBlueMax.Value = (int)ballDetector.UpperClothMask.Blue;

            trackBarCbMaskRedMin.Value = (int)ballDetector.LowerCueBallMask.Red;
            trackBarCbMaskGreenMin.Value = (int)ballDetector.LowerCueBallMask.Green;
            trackBarCbMaskBlueMin.Value = (int)ballDetector.LowerCueBallMask.Blue;

            trackBarCbMaskRedMax.Value = (int)ballDetector.UpperCueBallMask.Red;
            trackBarCbMaskGreenMax.Value = (int)ballDetector.UpperCueBallMask.Green;
            trackBarCbMaskBlueMax.Value = (int)ballDetector.UpperCueBallMask.Blue;

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

        private void trackBarCbMaskRedMin_ValueChanged(object sender, EventArgs e)
        {
            labelCbMaskRedMin.Text = trackBarCbMaskRedMin.Value.ToString();
            SetObjectDetectorSettings();
        }

        private void trackBarCbMaskRedMax_ValueChanged(object sender, EventArgs e)
        {
            labelCbMaskRedMax.Text = trackBarCbMaskRedMax.Value.ToString();
            SetObjectDetectorSettings();
        }

        private void trackBarCbMaskGreenMin_ValueChanged(object sender, EventArgs e)
        {
            labelCbMaskGreenMin.Text = trackBarCbMaskGreenMin.Value.ToString();
            SetObjectDetectorSettings();
        }

        private void trackBarCbMaskGreenMax_ValueChanged(object sender, EventArgs e)
        {
            labelCbMaskGreenMax.Text = trackBarCbMaskGreenMax.Value.ToString();
            SetObjectDetectorSettings();
        }

        private void trackBarCbMaskBlueMin_ValueChanged(object sender, EventArgs e)
        {
            labelCbMaskBlueMin.Text = trackBarCbMaskBlueMin.Value.ToString();
            SetObjectDetectorSettings();
        }

        private void trackBarCbMaskBlueMax_ValueChanged(object sender, EventArgs e)
        {
            labelCbMaskBlueMax.Text = trackBarCbMaskBlueMax.Value.ToString();
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
            workingImagePicBox.Image?.Dispose();
            tableMaskAppliedPicBox.Image?.Dispose();
            cueBallMaskPicBox.Image?.Dispose();
            cueBallMaskAppliedPicBox.Image?.Dispose();
            allContoursPicBox.Image?.Dispose();
            cueBallCandidatesPicBox.Image?.Dispose();
            scoredCandidatesPicBox.Image?.Dispose();
            cueBallFoundPicBox.Image?.Dispose();
            
            originalFrame?.Dispose();
            ballDetector = null;

            DebugFormClosed?.Invoke(this, EventArgs.Empty);
        }
    }
}
