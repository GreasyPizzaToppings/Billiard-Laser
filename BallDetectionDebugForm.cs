using Emgu.CV.Structure;

namespace billiard_laser
{
    public partial class BallDetectionDebugForm : Form, IDisposable
    {
        private bool initialisingControls = true;
        private CueBallDetector? ballDetector;
        private bool disposed = false;
        private VideoFrame? originalFrame; //store the current base table frame to allow for ball detector settings to change and get new results

        public event EventHandler? DebugFormClosed;

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
        public void DisplayDebugImages(CueBallDetectionResults? images)
        {
            if (images == null) return;

            originalFrame?.Dispose();
            originalFrame = images?.OriginalFrame?.Clone();

            SetImage(workingImagePicBox, images.WorkingImage);
            SetImage(tableMaskAppliedPicBox, images.TableMaskApplied);
            SetImage(cueBallMaskPicBox, images.CueBallMask);
            SetImage(cueBallMaskAppliedPicBox, images.CueBallMaskApplied);
            SetImage(allContoursPicBox, images.AllContoursHighlighted);
            SetImage(cueBallCandidatesPicBox, images.CueBallCandidatesHighlighted);
            SetImage(scoredCandidatesPicBox, images.ScoredCandidatesHighlighted);
            SetImage(cueBallFoundPicBox, images.CueBallHighlighted);
        }

        /// <summary>
        /// Processes a raw image through ball detection and shows the debug images
        /// </summary>
        /// <param name="rawImage">The raw image to process</param>
        public void GetAndShowDebugImages(VideoFrame frame) => DisplayDebugImages(ballDetector?.GetCueBallResults(frame));

        private static void SetImage(PictureBox pictureBox, Image newImage)
        {
            var oldImage = pictureBox.Image;
            pictureBox.Image = newImage != null ? new Bitmap(newImage) : null;
            oldImage?.Dispose();
        }

        private void InitCheckBoxes()
        {
            if (ballDetector == null) return;

            checkBoxEnableBlurr.Checked = ballDetector.EnableBlur;
            checkBoxEnableSharpen.Checked = ballDetector.EnableSharpening;
            checkBoxEnableTableBoundary.Checked = ballDetector.EnableTableBoundary;

            // wire up all checkbox events to a single handler
            checkBoxEnableBlurr.CheckedChanged += CheckBox_CheckedChanged;
            checkBoxEnableSharpen.CheckedChanged += CheckBox_CheckedChanged;
            checkBoxEnableTableBoundary.CheckedChanged += CheckBox_CheckedChanged;
        }

        private void CheckBox_CheckedChanged(object? sender, EventArgs e) => SetObjectDetectorSettings();

        private void SetObjectDetectorSettings()
        {
            if (initialisingControls || ballDetector == null)
                return;

            ballDetector.LowerClothMask = new Rgb(trackBarClothMaskRedMin.Value, trackBarClothMaskGreenMin.Value, trackBarClothMaskBlueMin.Value);
            ballDetector.UpperClothMask = new Rgb(trackBarClothMaskRedMax.Value, trackBarClothMaskGreenMax.Value, trackBarClothMaskBlueMax.Value);

            ballDetector.LowerCueBallMask = new Rgb(trackBarCbMaskRedMin.Value, trackBarCbMaskGreenMin.Value, trackBarCbMaskBlueMin.Value);
            ballDetector.UpperCueBallMask = new Rgb(trackBarCbMaskRedMax.Value, trackBarCbMaskGreenMax.Value, trackBarCbMaskBlueMax.Value);

            ballDetector.EnableBlur = checkBoxEnableBlurr.Checked;
            ballDetector.EnableSharpening = checkBoxEnableSharpen.Checked;
            ballDetector.EnableTableBoundary = checkBoxEnableTableBoundary.Checked;

            Console.WriteLine(ballDetector);

            //update images upon setting changes
            if (originalFrame != null) GetAndShowDebugImages(originalFrame);
        }

        private void InitMaskTrackbars()
        {
            if (ballDetector == null) return;

            // define trackbar groups for easier initialization
            var trackbarSettings = new (TrackBar Bar, Label Label, double Value)[]
            {
                // cloth mask min
                (trackBarClothMaskRedMin, labelClothMaskRedMinValue, ballDetector.LowerClothMask.Red),
                (trackBarClothMaskGreenMin, labelClothMaskGreenMinValue, ballDetector.LowerClothMask.Green),
                (trackBarClothMaskBlueMin, labelClothMaskBlueMinValue, ballDetector.LowerClothMask.Blue),
                
                // cloth mask max
                (trackBarClothMaskRedMax, labelClothMaskRedMaxValue, ballDetector.UpperClothMask.Red),
                (trackBarClothMaskGreenMax, labelClothMaskGreenMaxValue, ballDetector.UpperClothMask.Green),
                (trackBarClothMaskBlueMax, labelClothMaskBlueMaxValue, ballDetector.UpperClothMask.Blue),
                
                // cue ball mask min
                (trackBarCbMaskRedMin, labelCbMaskRedMinValue, ballDetector.LowerCueBallMask.Red),
                (trackBarCbMaskGreenMin, labelCbMaskGreenMinValue, ballDetector.LowerCueBallMask.Green),
                (trackBarCbMaskBlueMin, labelCbMaskBlueMinValue, ballDetector.LowerCueBallMask.Blue),
                
                // cue ball mask max
                (trackBarCbMaskRedMax, labelCbMaskRedMaxValue, ballDetector.UpperCueBallMask.Red),
                (trackBarCbMaskGreenMax, labelCbMaskGreenMaxValue, ballDetector.UpperCueBallMask.Green),
                (trackBarCbMaskBlueMax, labelCbMaskBlueMaxValue, ballDetector.UpperCueBallMask.Blue)
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
                // update corresponding label based on trackbar name
                var labelName = trackBar.Name.Replace("trackBar", "label") + "Value";
                if (Controls.Find(labelName, true).FirstOrDefault() is Label label)
                    label.Text = trackBar.Value.ToString();
                
                SetObjectDetectorSettings();
            }
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
