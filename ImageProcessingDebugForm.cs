using Emgu.CV.Structure;

namespace billiard_laser
{
    public partial class ImageProcessingDebugForm : Form, IDisposable
    {
        private bool initialisingControls = true;
        private BallDetector ballDetector;
        private bool disposed = false;

        public event EventHandler DebugFormClosed;

        public ImageProcessingDebugForm(BallDetector ballDetector)
        {
            this.ballDetector = ballDetector;
            this.FormClosed += ImageProcessingDebugForm_FormClosed;

            InitializeComponent();
            InitMaskTrackbars();
            InitCheckBoxes();

            initialisingControls = false;
        }

        public void ShowDebugImages(Bitmap rawImage)
        {
            ImageProcessingResults images = ballDetector.ProcessTableImage(rawImage);
            
            SetImage(originalImagePicBox, images.OriginalImage);
            SetImage(filteredContoursPicBox, images.FilteredBallsHighlighted);
            SetImage(allContoursPicBox, images.AllBallsHighlighted);
            SetImage(invMaskPicBox, images.TableMask);
            SetImage(appliedMaskPicBox, images.TableWithMaskApplied);
            SetImage(cueBallMaskPicBox, images.CueBallMask);
            SetImage(cueBallFoundPicBox, images.CueBallHighlighted);
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
            checkBoxEnableBlurr.Checked = ballDetector.EnableBlur;
            checkBoxEnableSharpen.Checked = ballDetector.EnableSharpening;
            checkBoxEnableTableBoundary.Checked = ballDetector.EnableTableBoundary;
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
              $"\nEnable Sharpening: {ballDetector.EnableSharpening}\n" +
              $"\nEnable Table Boundary: {ballDetector.EnableTableBoundary}\n"
            );
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

        private void ImageProcessingDebugForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            originalImagePicBox.Image?.Dispose();
            filteredContoursPicBox.Image?.Dispose();
            allContoursPicBox.Image?.Dispose();
            invMaskPicBox.Image?.Dispose();
            appliedMaskPicBox.Image?.Dispose();
            cueBallMaskPicBox.Image?.Dispose();
            cueBallFoundPicBox.Image?.Dispose();
            transformedImagePicBox.Image?.Dispose();

            ballDetector = null;

            DebugFormClosed?.Invoke(this, EventArgs.Empty);
        }
    }
}
