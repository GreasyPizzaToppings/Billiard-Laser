using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace billiard_laser
{
    public partial class BallReplacementForm : Form
    {
        private Bitmap targetTableLayout;
        private ArduinoController arduinoController;
        private LaserDetector laserDetector;
        private LaserDetectionDebugForm? laserDetectionDebugForm;
        private bool calibratingLaserPosition = false;

        public CameraController cameraController;

        public Bitmap TargetTableLayout
        {
            get => targetTableLayout;
            set
            {
                targetTableLayout = value;
                SetImage(pictureBoxTable, targetTableLayout);
            }
        }

        public event EventHandler? BallReplacementFormClosed;

        public BallReplacementForm(Bitmap targetTableLayout, CameraController cameraController)
        {
            ArgumentNullException.ThrowIfNull(targetTableLayout);

            InitializeComponent();
            UpdateOpacityValueLabel();
            UpdateStepAmountValueLabel();
            
            this.cameraController = cameraController;
            arduinoController = new ArduinoController();
            TargetTableLayout = new Bitmap(targetTableLayout);

            // Handle the connection asynchronously
            arduinoController?.ConnectionTask?.ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    // Only try to show message box if form is available
                    if (!IsDisposed)
                    {
                        try
                        {
                            this.BeginInvoke(new Action(() =>
                            {
                                MessageBox.Show("Failed to connect to Arduino: " + task?.Exception?.InnerException?.Message,
                                    "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }));
                        }
                        catch (InvalidOperationException)
                        {
                            // Form was disposed between our check and the BeginInvoke
                            Console.WriteLine("Could not show Arduino error - form was disposed");
                        }
                    }
                    else
                    {
                        // Log the error since we can't show it to the user
                        Console.WriteLine("Arduino connection failed but form was disposed: " + 
                            task?.Exception?.InnerException?.Message);
                    }
                }
            }, TaskScheduler.Default);

            arduinoController?.LaserOff();
            laserDetector = new LaserDetector();
        }

        /// <summary>
        /// Take in a new camera frame and overlay it on the base table at a lower opacity
        /// </summary>
        /// <param name="image"></param>

        public void UpdateTableOverlay(VideoFrame newFrame)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateTableOverlay(newFrame)));
                return;
            }
            if (newFrame?.frame == null) return;

            try
            {
                using Bitmap overlaidImage = new(TargetTableLayout.Width, TargetTableLayout.Height);
                using var graphics = Graphics.FromImage(overlaidImage);
                
                // Calculate opacity (0-1 range from trackbar percentage)
                float opacity = trackBarCameraOpacity.Value / 100f;
                using var imageAttributes = new ImageAttributes();
                imageAttributes.SetColorMatrix(new ColorMatrix { Matrix33 = opacity }, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                // Draw base table layout
                graphics.Clear(Color.Transparent);
                graphics.DrawImage(TargetTableLayout,
                    new Rectangle(0, 0, TargetTableLayout.Width, TargetTableLayout.Height),
                    0, 0, TargetTableLayout.Width, TargetTableLayout.Height,
                    GraphicsUnit.Pixel);

                // Process laser detection if enabled
                LaserDetectionResults? laserResults = null;
                if (arduinoController?.IsLaserOn ?? false)
                    laserResults = laserDetector.ProcessLaserDetection(newFrame.frame);
                
                // Show debug images if form is open
                laserDetectionDebugForm?.DisplayDebugImages(laserResults);

                // Draw either laser highlight or camera frame
                if (laserResults?.LaserHighlighted != null)
                {
                    graphics.DrawImage(laserResults.LaserHighlighted,
                        new Rectangle(0, 0, TargetTableLayout.Width, TargetTableLayout.Height),
                        0, 0, laserResults.LaserHighlighted.Width, laserResults.LaserHighlighted.Height,
                        GraphicsUnit.Pixel,
                        imageAttributes);
                }
                else
                {
                    graphics.DrawImage(newFrame.frame,
                        new Rectangle(0, 0, TargetTableLayout.Width, TargetTableLayout.Height),
                        0, 0, newFrame.frame.Width, newFrame.frame.Height,
                        GraphicsUnit.Pixel,
                        imageAttributes);
                }

                SetImage(pictureBoxTable, overlaidImage);
                laserResults?.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateTableOverlay: {ex.Message}");
            }
        }

        private static void SetImage(PictureBox pictureBox, Image newImage)
        {
            var oldImage = pictureBox.Image;
            pictureBox.Image = newImage != null ? new Bitmap(newImage) : null;
            oldImage?.Dispose();
        }

        private void trackBarCameraOpacity_Scroll(object sender, EventArgs e)
        {
            UpdateOpacityValueLabel();
        }

        private void UpdateOpacityValueLabel()
        {
            labelCameraOpacityValue.Text = trackBarCameraOpacity.Value.ToString() + "%";
        }

        private void UpdateStepAmountValueLabel()
        {
            labelLaserStepAmountValue.Text = trackBarLaserStepAmount.Value.ToString();
        }

        private void BallReplacementForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            laserDetectionDebugForm?.Dispose();
            arduinoController?.Dispose();
            targetTableLayout.Dispose();
            pictureBoxTable.Dispose();

            BallReplacementFormClosed?.Invoke(this, EventArgs.Empty);
        }

        private void btnLaserUp_Click(object sender, EventArgs e)
        {
            arduinoController?.MoveUp();
        }

        private void btnLaserLeft_Click(object sender, EventArgs e)
        {
            arduinoController?.MoveLeft();
        }

        private void btnLaserDown_Click(object sender, EventArgs e)
        {
            arduinoController?.MoveDown();
        }

        private void btnLaserRight_Click(object sender, EventArgs e)
        {
            arduinoController?.MoveRight();
        }

        private void btnLaserEnableToggle_Click(object sender, EventArgs e)
        {
            arduinoController?.ToggleLaser();
        }

        private void trackBarLaserStepAmount_ValueChanged(object sender, EventArgs e)
        {
            UpdateStepAmountValueLabel();
            arduinoController?.SetStepAmount(trackBarLaserStepAmount.Value);
        }

        private void btnFlipCamera_Click(object sender, EventArgs e)
        {
            cameraController.IsFlipped = !cameraController.IsFlipped;

            // Flip the reference image
            if (targetTableLayout != null)
            {
                Bitmap flippedImage = new Bitmap(targetTableLayout.Width, targetTableLayout.Height);
                using (Graphics g = Graphics.FromImage(flippedImage))
                {
                    g.TranslateTransform(0, targetTableLayout.Height);
                    g.ScaleTransform(1, -1);
                    g.DrawImage(targetTableLayout, 0, 0);
                }

                // Update the property which will handle disposal of old image
                TargetTableLayout = flippedImage;
            }
        }

        private void btnMirrorCamera_Click(object sender, EventArgs e)
        {
            cameraController.IsMirrored = !cameraController.IsMirrored;

            // Mirror the reference image
            if (targetTableLayout != null)
            {
                Bitmap mirroredImage = new(targetTableLayout.Width, targetTableLayout.Height);
                using (Graphics g = Graphics.FromImage(mirroredImage))
                {
                    g.TranslateTransform(targetTableLayout.Width, 0);
                    g.ScaleTransform(-1, 1);
                    g.DrawImage(targetTableLayout, 0, 0);
                }

                // Update the property which will handle disposal of old image
                TargetTableLayout = mirroredImage;
            }
        }

        private void btnShowDebugForm_Click(object sender, EventArgs e)
        {
            if (laserDetectionDebugForm == null || laserDetectionDebugForm.IsDisposed)
            {
                laserDetectionDebugForm = new LaserDetectionDebugForm(laserDetector);
                laserDetectionDebugForm.DebugFormClosed += DebugForm_FormClosed;
                laserDetectionDebugForm.Show();
                laserDetectionDebugForm.GetAndShowDebugImages(targetTableLayout);
            }
            else
            {
                laserDetectionDebugForm.Focus();
            }
        }

        private void DebugForm_FormClosed(object? sender, EventArgs e)
        {
            laserDetectionDebugForm?.Dispose();
            laserDetectionDebugForm = null;
        }

        /// <summary>
        /// Let the user select where the laser pointer currently is and to track from there
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalibrateLaser_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Click on the image at the point where the laser is.", "Laser Calibration Process", MessageBoxButtons.OK);

            calibratingLaserPosition = true;
        }

        private void pictureBoxTable_Click(object sender, EventArgs e)
        {
            if (!calibratingLaserPosition) return;

            // Get mouse position relative to the picturebox
            MouseEventArgs mouseEvent = (MouseEventArgs)e;
            Point clickPosition = mouseEvent.Location;

            Point scaledPosition = ScalePointToTableResolution(clickPosition, pictureBoxTable);
            laserDetector.CalibrateLaserPosition(scaledPosition);

            calibratingLaserPosition = false;

            MessageBox.Show("Laser position calibrated successfully.", "Calibration Complete", MessageBoxButtons.OK);
        }


        private Point ScalePointToTableResolution(Point clickPoint, PictureBox picturebox)
        {
            if (targetTableLayout == null) return clickPoint;

            float scaleX = (float)targetTableLayout.Width / picturebox.Width;
            float scaleY = (float)targetTableLayout.Height / picturebox.Height;

            return new Point(
                (int)(clickPoint.X * scaleX),
                (int)(clickPoint.Y * scaleY)
            );
        }
    }
}
