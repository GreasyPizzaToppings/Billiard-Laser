using Emgu.CV.Structure;

namespace billiard_laser
{
    public partial class BallDetectionDebugForm : Form, IDisposable
    {
        private bool initialisingControls = true;
        private TableObjectDetector objectDetector;
        private bool disposed = false;

        public event EventHandler DebugFormClosed;

        public BallDetectionDebugForm(TableObjectDetector objectDetector)
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
            BallDetectionResults images = objectDetector.ProcessBallDetection(rawImage);
                        
            
            SetImage(originalImagePicBox, images.OriginalImage);
            SetImage(filteredContoursPicBox, images.FilteredBallsHighlighted);
            SetImage(allContoursPicBox, images.AllBallsHighlighted);
            SetImage(invMaskPicBox, images.TableMask);
            SetImage(appliedMaskPicBox, images.TableWithMaskApplied);
            SetImage(cueBallMaskPicBox, images.CueBallMask);
            SetImage(cueBallFoundPicBox, images.CueBallHighlighted);
            SetImage(transformedImagePicBox, images.TransformedImage);   
        }

        private static void SetImage(PictureBox pictureBox, Image newImage)
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

            objectDetector.LowerCueBallMask = new Rgb(trackBarCbMaskRedMin.Value, trackBarCbMaskGreenMin.Value, trackBarCbMaskBlueMin.Value);
            objectDetector.UpperCueBallMask = new Rgb(trackBarCbMaskRedMax.Value, trackBarCbMaskGreenMax.Value, trackBarCbMaskBlueMax.Value);

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
              $"\nLower Cb Mask RGB: {objectDetector.LowerCueBallMask}" +
              $"\nUpper Cb Mask RGB: {objectDetector.UpperCueBallMask}" +
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

            trackBarCbMaskRedMin.Value = (int)objectDetector.LowerCueBallMask.Red;
            trackBarCbMaskGreenMin.Value = (int)objectDetector.LowerCueBallMask.Green;
            trackBarCbMaskBlueMin.Value = (int)objectDetector.LowerCueBallMask.Blue;

            trackBarCbMaskRedMax.Value = (int)objectDetector.UpperCueBallMask.Red;
            trackBarCbMaskGreenMax.Value = (int)objectDetector.UpperCueBallMask.Green;
            trackBarCbMaskBlueMax.Value = (int)objectDetector.UpperCueBallMask.Blue;

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
            originalImagePicBox.Image?.Dispose();
            filteredContoursPicBox.Image?.Dispose();
            allContoursPicBox.Image?.Dispose();
            invMaskPicBox.Image?.Dispose();
            appliedMaskPicBox.Image?.Dispose();
            cueBallMaskPicBox.Image?.Dispose();
            cueBallFoundPicBox.Image?.Dispose();
            transformedImagePicBox.Image?.Dispose();

            objectDetector = null;

            DebugFormClosed?.Invoke(this, EventArgs.Empty);
        }
    }
}
