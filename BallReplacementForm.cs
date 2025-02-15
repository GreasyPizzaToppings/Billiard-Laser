﻿using Microsoft.VisualBasic.Devices;
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
        private LaserDetectionDebugForm laserDetectionDebugForm;
        private bool calibratingLaserPosition = false;

        public CameraController cameraController;

        public Bitmap TargetTableLayout
        {
            get => targetTableLayout;
            set
            {
                targetTableLayout = value;
                SetImage(pictureBoxTable, TargetTableLayout);
            }
        }

        public event EventHandler BallReplacementFormClosed;

        public BallReplacementForm(Bitmap targetTableLayout, CameraController cameraController)
        {
            InitializeComponent();
            UpdateOpacityValueLabel();
            UpdateStepAmountValueLabel();
            TargetTableLayout = new Bitmap(targetTableLayout);
            this.cameraController = cameraController;

            arduinoController = new ArduinoController();

            // Handle the connection asynchronously
            arduinoController.ConnectionTask.ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    // Make sure we're on the UI thread for showing message box
                    this.BeginInvoke(new Action(() =>
                    {
                        MessageBox.Show("Failed to connect to Arduino: " + task.Exception.InnerException.Message,
                            "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }));
                }
            });

            arduinoController.LaserOff();

            laserDetector = new LaserDetector();
        }

        /// <summary>
        /// Take in a new camera frame and overlay it on the base table at a lower opacity
        /// </summary>
        /// <param name="image"></param>

        public void UpdateTableOverlay(VideoFrame cameraFrame)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateTableOverlay(cameraFrame)));
                return;
            }
            if (cameraFrame == null || TargetTableLayout == null)
                return;
            try
            {

                //if laser enabled, try and detect where our laser is on the table and highlight it
                LaserDetectionResults? laserResults = null;
                if (arduinoController.IsLaserOn)
                {
                    laserResults = laserDetector.ProcessLaserDetection(cameraFrame.frame);
                    laserDetectionDebugForm?.ShowDebugImages(laserResults);
                }
                else laserDetectionDebugForm?.GetAndShowDebugImages(cameraFrame.frame); //allow laser detection/debugging anyway if form is open and our laser isnt on for testing purposes

                using (Bitmap overlaidImage = new Bitmap(TargetTableLayout.Width, TargetTableLayout.Height))
                {
                    // Calculate opacity (0-1 range from trackbar percentage)
                    float opacity = trackBarCameraOpacity.Value / 100f;
                    // Create color matrix for blending
                    ColorMatrix colorMatrix = new ColorMatrix
                    {
                        Matrix33 = opacity // Set alpha channel
                    };

                    // Create image attributes for blending
                    using (ImageAttributes imageAttributes = new ImageAttributes())
                    {
                        imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                        Graphics graphics = Graphics.FromImage(overlaidImage);

                        // Draw base table layout
                        graphics.Clear(Color.Transparent);
                        graphics.DrawImage(TargetTableLayout,
                            new Rectangle(0, 0, TargetTableLayout.Width, TargetTableLayout.Height),
                            0, 0, TargetTableLayout.Width, TargetTableLayout.Height,
                            GraphicsUnit.Pixel);

                        // use highlighted laser iamge if detected
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
                            // else draw incoming image with lower opacity
                            graphics.DrawImage(cameraFrame.frame,
                                new Rectangle(0, 0, TargetTableLayout.Width, TargetTableLayout.Height),
                                0, 0, cameraFrame.frame.Width, cameraFrame.frame.Height,
                                GraphicsUnit.Pixel,
                                imageAttributes);
                        }

                        SetImage(pictureBoxTable, overlaidImage);
                    }
                }

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
            if (laserDetectionDebugForm != null)
            {
                laserDetectionDebugForm.Dispose();
                laserDetectionDebugForm = null;
            }

            BallReplacementFormClosed?.Invoke(this, EventArgs.Empty);

            arduinoController.Dispose();
            arduinoController = null;
            targetTableLayout.Dispose();
            pictureBoxTable.Dispose();
        }

        private void btnLaserUp_Click(object sender, EventArgs e)
        {
            arduinoController.MoveUp();
        }

        private void btnLaserLeft_Click(object sender, EventArgs e)
        {
            arduinoController.MoveLeft();
        }

        private void btnLaserDown_Click(object sender, EventArgs e)
        {
            arduinoController.MoveDown();
        }

        private void btnLaserRight_Click(object sender, EventArgs e)
        {
            arduinoController.MoveRight();
        }

        private void btnLaserEnableToggle_Click(object sender, EventArgs e)
        {
            arduinoController.ToggleLaser();
        }

        private void trackBarLaserStepAmount_ValueChanged(object sender, EventArgs e)
        {
            UpdateStepAmountValueLabel();
            arduinoController.SetStepAmount(trackBarLaserStepAmount.Value);
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
                Bitmap mirroredImage = new Bitmap(targetTableLayout.Width, targetTableLayout.Height);
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

                // Initialize debug form with current image if available
                if (targetTableLayout != null)
                {
                    laserDetectionDebugForm.GetAndShowDebugImages(targetTableLayout);
                }
            }
            else
            {
                laserDetectionDebugForm.Focus();
            }
        }

        private void DebugForm_FormClosed(object sender, EventArgs e)
        {
            if (laserDetectionDebugForm != null)
            {
                laserDetectionDebugForm.Dispose();
                laserDetectionDebugForm = null;
            }
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

            // Scale the click position to match the image's actual resolution
            Point scaledPosition = ScalePointToTableResolution(clickPosition, pictureBoxTable);

            // Reset the laser detector's position history and add the calibrated point
            laserDetector.CalibrateLaserPosition(scaledPosition);

            // Reset calibration mode
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
