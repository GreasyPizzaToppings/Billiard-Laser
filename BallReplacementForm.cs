﻿using System;
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

        public Bitmap TargetTableLayout { get => targetTableLayout; 
            set 
            { 
                targetTableLayout = value;
                SetImage(pictureBoxTable, TargetTableLayout);
            }
        }

        public event EventHandler BallReplacementFormClosed;


        public BallReplacementForm(Bitmap targetTableLayout)
        {
            InitializeComponent();
            UpdateOpacityValueLabel();
            this.TargetTableLayout = targetTableLayout;
        }

        /// <summary>
        /// Take in a new camera frame and overlay it on the base table at a lower opacity
        /// </summary>
        /// <param name="image"></param>
        public void UpdateTableOverlay(Bitmap cameraImage)
        {
            if (cameraImage == null || TargetTableLayout == null)
                return;

            try
            {
                // Ensure the camera image is the same size as the target table layout
                using (Bitmap resizedCameraImage = new Bitmap(cameraImage, TargetTableLayout.Size))
                {
                    // Create a new bitmap to combine the images
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

                            using (Graphics graphics = Graphics.FromImage(overlaidImage))
                            {
                                // Draw base table layout
                                graphics.Clear(Color.Transparent);
                                graphics.DrawImage(TargetTableLayout,
                                    new Rectangle(0, 0, TargetTableLayout.Width, TargetTableLayout.Height),
                                    0, 0, TargetTableLayout.Width, TargetTableLayout.Height,
                                    GraphicsUnit.Pixel);

                                // Draw camera image with opacity
                                graphics.DrawImage(resizedCameraImage,
                                    new Rectangle(0, 0, TargetTableLayout.Width, TargetTableLayout.Height),
                                    0, 0, resizedCameraImage.Width, resizedCameraImage.Height,
                                    GraphicsUnit.Pixel,
                                    imageAttributes);
                            }

                            SetImage(pictureBoxTable, overlaidImage);
                        }
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

        private void BallReplacementForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            BallReplacementFormClosed?.Invoke(this, EventArgs.Empty);

            targetTableLayout.Dispose();
            pictureBoxTable.Dispose();
        }
    }
}