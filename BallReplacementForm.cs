using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace billiard_laser
{
    public partial class BallReplacementForm : Form
    {
        private Bitmap targetTableLayout;
        private ArduinoController arduinoController;
        private LaserDetector laserDetector;
        private LaserDetectionDebugForm? laserDetectionDebugForm;

        private int _opacityPercentage;
        private bool calibratingLaserPosition = false;
        private CancellationTokenSource? cancellationTokenSource;

        public CameraController cameraController;
        public event EventHandler? BallReplacementFormClosed;

        private readonly object updateLock = new object();
        private readonly Queue<Action> updateQueue = new Queue<Action>();
        private readonly SemaphoreSlim updateSemaphore = new SemaphoreSlim(0);
        private readonly CancellationTokenSource updateCancellationTokenSource = new CancellationTokenSource();

        public Bitmap TargetTableLayout
        {
            get => targetTableLayout;
            set
            {
                targetTableLayout = value;
                SetImage(pictureBoxTable, targetTableLayout);
            }
        }

        public int OpacityPercentage
        {
            get => _opacityPercentage;
            set
            {
                _opacityPercentage = value;
                trackBarCameraOpacity.Value = value;

                labelCameraOpacityValue.Text = OpacityPercentage + "%";
            }
        }

        public BallReplacementForm(Bitmap targetTableLayout, CameraController cameraController)
        {
            ArgumentNullException.ThrowIfNull(targetTableLayout);
            ArgumentNullException.ThrowIfNull(cameraController);

            InitializeComponent();

            OpacityPercentage = trackBarCameraOpacity.Value;
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

            cancellationTokenSource = new CancellationTokenSource();
            Task.Run(ProcessUpdateQueue); // process frame updates in the background
        }

        /// <summary>
        /// Take in a new camera frame and overlay it on the base table at a lower opacity
        /// </summary>
        /// <param name="image"></param>
        public void UpdateTableOverlay(VideoFrame newFrame)
        {
            if (updateCancellationTokenSource.Token.IsCancellationRequested) return;

            lock (updateLock)
            {
                // Queue the update operation
                Console.WriteLine("adding new update to queue!");
                updateQueue.Enqueue(() => UpdateTableOverlayInternal(newFrame));
                updateSemaphore.Release();
            }
        }

        private void UpdateTableOverlayInternal(VideoFrame newFrame)
        {
            try
            {
                cancellationTokenSource?.Token.ThrowIfCancellationRequested();
                if (cancellationTokenSource?.Token.IsCancellationRequested == true) throw new OperationCanceledException();

                if (newFrame == null || newFrame.frame == null) throw new InvalidEnumArgumentException("Frame given to update table overlay in ball replacement form should not be null.");

                using Bitmap frameClone = (Bitmap)newFrame.frame.Clone();
                using Bitmap overlaidImage = new(TargetTableLayout.Width, TargetTableLayout.Height);
                using var graphics = Graphics.FromImage(overlaidImage);

                // Calculate opacity in (0-1 range)
                using var imageAttributes = new ImageAttributes();
                imageAttributes.SetColorMatrix(new ColorMatrix { Matrix33 = (OpacityPercentage / 100f) }, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                // Draw base table layout
                graphics.Clear(Color.Transparent);
                graphics.DrawImage(TargetTableLayout,
                    new Rectangle(0, 0, TargetTableLayout.Width, TargetTableLayout.Height),
                    0, 0, TargetTableLayout.Width, TargetTableLayout.Height,
                    GraphicsUnit.Pixel);

                // Process laser detection if enabled
                LaserDetectionResults? laserResults = null;
                if (arduinoController?.IsLaserOn ?? false)
                {
                    laserResults = laserDetector.ProcessLaserDetection(frameClone);
                    laserDetectionDebugForm?.DisplayDebugImages(laserResults);
                }

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
                    graphics.DrawImage(frameClone,
                        new Rectangle(0, 0, TargetTableLayout.Width, TargetTableLayout.Height),
                        0, 0, frameClone.Width, frameClone.Height,
                        GraphicsUnit.Pixel,
                        imageAttributes);
                }

                SetImage(pictureBoxTable, overlaidImage);
                laserResults?.Dispose();
            }
            catch (ObjectDisposedException e)
            {
                Console.WriteLine($"Failed to close gracefully in ball replacement form. {e.Message}");
            }
            catch (OperationCanceledException e) {
                Console.WriteLine($"Table overlay update cancelled! Closing now.");
            }
        }

        private async Task ProcessUpdateQueue()
        {
            try
            {
                while (!updateCancellationTokenSource.Token.IsCancellationRequested)
                {
                    await updateSemaphore.WaitAsync(updateCancellationTokenSource.Token);

                    Action updateAction;
                    lock (updateLock)
                    {
                        if (updateQueue.Count == 0) continue;
                        updateAction = updateQueue.Dequeue();
                    }

                    try
                    {
                        if (InvokeRequired) Invoke(updateAction);
                        //if (InvokeRequired) await Task.Factory.FromAsync(BeginInvoke(updateAction, null), EndInvoke);
                        else updateAction();
                    }
                    catch (ObjectDisposedException e)
                    {
                        Console.WriteLine($"Failed to close gracefully in ball replacement form. {e.Message}");
                        break;
                    }
                }
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine($"Cancel received, stopping processing frame queue. {e.Message}");
            }
        }

        private void SetImage(PictureBox pictureBox, Image newImage)
        {
            cancellationTokenSource?.Token.ThrowIfCancellationRequested();
            if (pictureBox == null || pictureBox.IsDisposed || pictureBox.Disposing) throw new Exception();

            if (pictureBox.InvokeRequired)
            {
                Console.WriteLine($"invoking picturebox update now at {DateTime.Now.Millisecond}");
                pictureBox.Invoke(new Action(() => SetImage(pictureBox, newImage)));
                return;
            }

            Console.WriteLine($"updating picturebox image at {DateTime.Now.Millisecond}");
            var oldImage = pictureBox.Image;
            pictureBox.Image = newImage != null ? new Bitmap(newImage) : null;
            oldImage?.Dispose();
            Console.WriteLine($"finished picturebox update at {DateTime.Now.Millisecond}");
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
            laserDetectionDebugForm = null;
        }

        private async void BallReplacementForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            BallReplacementFormClosed?.Invoke(this, e);
            cancellationTokenSource?.Cancel();

            // give the UI thread a chance to process pending messages
            Console.WriteLine($"In formclosing. {updateQueue.Count} items in queue.");
            Application.DoEvents();

            int maxAttempts = 5;
            for (int i = 0; i < maxAttempts; i++)
            {
                if (updateQueue.Count == 0) break;
                Application.DoEvents();
                Thread.Sleep(25);
            }

            Console.WriteLine($"at end of formclosing. {updateQueue.Count} items in queue.");

            arduinoController?.Dispose();
            laserDetectionDebugForm?.Dispose();
            targetTableLayout.Dispose();
            pictureBoxTable.Dispose();
        }

        private void trackBarCameraOpacity_Scroll(object sender, EventArgs e)
        {
            OpacityPercentage = trackBarCameraOpacity.Value;
        }

        private void UpdateStepAmountValueLabel()
        {
            labelLaserStepAmountValue.Text = trackBarLaserStepAmount.Value.ToString();
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

            Console.WriteLine(cameraController);
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

            Console.WriteLine(cameraController);
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
