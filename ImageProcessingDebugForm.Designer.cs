
namespace billiard_laser
{
    partial class ImageProcessingDebugForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            originalImagePicBox = new PictureBox();
            invMaskPicBox = new PictureBox();
            appliedMaskPicBox = new PictureBox();
            blurredImagePicBox = new PictureBox();
            allContoursPicBox = new PictureBox();
            filteredContoursPicBox = new PictureBox();
            labelOriginalImage = new Label();
            labelBlurredImage = new Label();
            labelMask = new Label();
            labelMaskApplied = new Label();
            labelFilteredContours = new Label();
            label1 = new Label();
            sharpenedImagePicBox = new PictureBox();
            blurredSharpenedImagePicBox = new PictureBox();
            labelSharpenedImage = new Label();
            labelBlurredSharpened = new Label();
            checkBoxEnableBlurr = new CheckBox();
            checkBoxEnableSharpen = new CheckBox();
            labelMaskGreen = new Label();
            labelMaskRed = new Label();
            labelMaskBlue = new Label();
            trackBarMaskBlue = new TrackBar();
            trackBarMaskGreen = new TrackBar();
            trackBarMaskRed = new TrackBar();
            labelMaskRedValue = new Label();
            labelMaskGreenValue = new Label();
            labelMaskBlueValue = new Label();
            panelImageSettings = new Panel();
            ((System.ComponentModel.ISupportInitialize)originalImagePicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)invMaskPicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)appliedMaskPicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)blurredImagePicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)allContoursPicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)filteredContoursPicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sharpenedImagePicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)blurredSharpenedImagePicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMaskBlue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMaskGreen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMaskRed).BeginInit();
            panelImageSettings.SuspendLayout();
            SuspendLayout();
            // 
            // originalImagePicBox
            // 
            originalImagePicBox.Location = new Point(12, 39);
            originalImagePicBox.Margin = new Padding(3, 2, 3, 2);
            originalImagePicBox.Name = "originalImagePicBox";
            originalImagePicBox.Size = new Size(300, 188);
            originalImagePicBox.SizeMode = PictureBoxSizeMode.Zoom;
            originalImagePicBox.TabIndex = 0;
            originalImagePicBox.TabStop = false;
            // 
            // invMaskPicBox
            // 
            invMaskPicBox.Location = new Point(624, 259);
            invMaskPicBox.Margin = new Padding(3, 2, 3, 2);
            invMaskPicBox.Name = "invMaskPicBox";
            invMaskPicBox.Size = new Size(300, 188);
            invMaskPicBox.SizeMode = PictureBoxSizeMode.Zoom;
            invMaskPicBox.TabIndex = 1;
            invMaskPicBox.TabStop = false;
            // 
            // appliedMaskPicBox
            // 
            appliedMaskPicBox.Location = new Point(930, 259);
            appliedMaskPicBox.Margin = new Padding(3, 2, 3, 2);
            appliedMaskPicBox.Name = "appliedMaskPicBox";
            appliedMaskPicBox.Size = new Size(300, 188);
            appliedMaskPicBox.SizeMode = PictureBoxSizeMode.Zoom;
            appliedMaskPicBox.TabIndex = 2;
            appliedMaskPicBox.TabStop = false;
            // 
            // blurredImagePicBox
            // 
            blurredImagePicBox.Location = new Point(318, 39);
            blurredImagePicBox.Margin = new Padding(3, 2, 3, 2);
            blurredImagePicBox.Name = "blurredImagePicBox";
            blurredImagePicBox.Size = new Size(300, 188);
            blurredImagePicBox.SizeMode = PictureBoxSizeMode.Zoom;
            blurredImagePicBox.TabIndex = 3;
            blurredImagePicBox.TabStop = false;
            // 
            // allContoursPicBox
            // 
            allContoursPicBox.Location = new Point(318, 259);
            allContoursPicBox.Margin = new Padding(3, 2, 3, 2);
            allContoursPicBox.Name = "allContoursPicBox";
            allContoursPicBox.Size = new Size(300, 188);
            allContoursPicBox.SizeMode = PictureBoxSizeMode.Zoom;
            allContoursPicBox.TabIndex = 4;
            allContoursPicBox.TabStop = false;
            // 
            // filteredContoursPicBox
            // 
            filteredContoursPicBox.Location = new Point(12, 259);
            filteredContoursPicBox.Margin = new Padding(3, 2, 3, 2);
            filteredContoursPicBox.Name = "filteredContoursPicBox";
            filteredContoursPicBox.Size = new Size(300, 188);
            filteredContoursPicBox.SizeMode = PictureBoxSizeMode.Zoom;
            filteredContoursPicBox.TabIndex = 5;
            filteredContoursPicBox.TabStop = false;
            // 
            // labelOriginalImage
            // 
            labelOriginalImage.AutoSize = true;
            labelOriginalImage.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelOriginalImage.Location = new Point(12, 15);
            labelOriginalImage.Name = "labelOriginalImage";
            labelOriginalImage.Size = new Size(108, 20);
            labelOriginalImage.TabIndex = 6;
            labelOriginalImage.Text = "Original Image";
            // 
            // labelBlurredImage
            // 
            labelBlurredImage.AutoSize = true;
            labelBlurredImage.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelBlurredImage.Location = new Point(318, 15);
            labelBlurredImage.Name = "labelBlurredImage";
            labelBlurredImage.Size = new Size(103, 20);
            labelBlurredImage.TabIndex = 7;
            labelBlurredImage.Text = "Blurred Image";
            // 
            // labelMask
            // 
            labelMask.AutoSize = true;
            labelMask.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelMask.Location = new Point(624, 235);
            labelMask.Name = "labelMask";
            labelMask.Size = new Size(43, 20);
            labelMask.TabIndex = 8;
            labelMask.Text = "Mask";
            // 
            // labelMaskApplied
            // 
            labelMaskApplied.AutoSize = true;
            labelMaskApplied.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelMaskApplied.Location = new Point(929, 235);
            labelMaskApplied.Name = "labelMaskApplied";
            labelMaskApplied.Size = new Size(100, 20);
            labelMaskApplied.TabIndex = 9;
            labelMaskApplied.Text = "Mask Applied";
            // 
            // labelFilteredContours
            // 
            labelFilteredContours.AutoSize = true;
            labelFilteredContours.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelFilteredContours.Location = new Point(11, 235);
            labelFilteredContours.Name = "labelFilteredContours";
            labelFilteredContours.Size = new Size(122, 20);
            labelFilteredContours.TabIndex = 10;
            labelFilteredContours.Text = "Contours Filtered";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(318, 235);
            label1.Name = "label1";
            label1.Size = new Size(90, 20);
            label1.TabIndex = 11;
            label1.Text = "All Contours";
            // 
            // sharpenedImagePicBox
            // 
            sharpenedImagePicBox.Location = new Point(624, 39);
            sharpenedImagePicBox.Margin = new Padding(3, 2, 3, 2);
            sharpenedImagePicBox.Name = "sharpenedImagePicBox";
            sharpenedImagePicBox.Size = new Size(300, 188);
            sharpenedImagePicBox.SizeMode = PictureBoxSizeMode.Zoom;
            sharpenedImagePicBox.TabIndex = 12;
            sharpenedImagePicBox.TabStop = false;
            // 
            // blurredSharpenedImagePicBox
            // 
            blurredSharpenedImagePicBox.Location = new Point(930, 39);
            blurredSharpenedImagePicBox.Margin = new Padding(3, 2, 3, 2);
            blurredSharpenedImagePicBox.Name = "blurredSharpenedImagePicBox";
            blurredSharpenedImagePicBox.Size = new Size(300, 188);
            blurredSharpenedImagePicBox.SizeMode = PictureBoxSizeMode.Zoom;
            blurredSharpenedImagePicBox.TabIndex = 13;
            blurredSharpenedImagePicBox.TabStop = false;
            // 
            // labelSharpenedImage
            // 
            labelSharpenedImage.AutoSize = true;
            labelSharpenedImage.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelSharpenedImage.Location = new Point(624, 15);
            labelSharpenedImage.Name = "labelSharpenedImage";
            labelSharpenedImage.Size = new Size(126, 20);
            labelSharpenedImage.TabIndex = 14;
            labelSharpenedImage.Text = "Sharpened Image";
            // 
            // labelBlurredSharpened
            // 
            labelBlurredSharpened.AutoSize = true;
            labelBlurredSharpened.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelBlurredSharpened.Location = new Point(930, 15);
            labelBlurredSharpened.Name = "labelBlurredSharpened";
            labelBlurredSharpened.Size = new Size(194, 20);
            labelBlurredSharpened.TabIndex = 15;
            labelBlurredSharpened.Text = "Blurred && Sharpened Image";
            // 
            // checkBoxEnableBlurr
            // 
            checkBoxEnableBlurr.AutoSize = true;
            checkBoxEnableBlurr.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            checkBoxEnableBlurr.Location = new Point(11, 32);
            checkBoxEnableBlurr.Name = "checkBoxEnableBlurr";
            checkBoxEnableBlurr.Size = new Size(108, 24);
            checkBoxEnableBlurr.TabIndex = 16;
            checkBoxEnableBlurr.Text = "Enable Blurr";
            checkBoxEnableBlurr.UseVisualStyleBackColor = true;
            // 
            // checkBoxEnableSharpen
            // 
            checkBoxEnableSharpen.AutoSize = true;
            checkBoxEnableSharpen.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            checkBoxEnableSharpen.Location = new Point(132, 32);
            checkBoxEnableSharpen.Name = "checkBoxEnableSharpen";
            checkBoxEnableSharpen.Size = new Size(152, 24);
            checkBoxEnableSharpen.TabIndex = 17;
            checkBoxEnableSharpen.Text = "Enable Sharpening";
            checkBoxEnableSharpen.UseVisualStyleBackColor = true;
            // 
            // labelMaskGreen
            // 
            labelMaskGreen.AutoSize = true;
            labelMaskGreen.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelMaskGreen.Location = new Point(948, 2);
            labelMaskGreen.Name = "labelMaskGreen";
            labelMaskGreen.Size = new Size(86, 20);
            labelMaskGreen.TabIndex = 21;
            labelMaskGreen.Text = "Mask Green";
            // 
            // labelMaskRed
            // 
            labelMaskRed.AutoSize = true;
            labelMaskRed.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelMaskRed.Location = new Point(807, 2);
            labelMaskRed.Name = "labelMaskRed";
            labelMaskRed.Size = new Size(73, 20);
            labelMaskRed.TabIndex = 22;
            labelMaskRed.Text = "Mask Red";
            // 
            // labelMaskBlue
            // 
            labelMaskBlue.AutoSize = true;
            labelMaskBlue.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelMaskBlue.Location = new Point(1091, 2);
            labelMaskBlue.Name = "labelMaskBlue";
            labelMaskBlue.Size = new Size(76, 20);
            labelMaskBlue.TabIndex = 23;
            labelMaskBlue.Text = "Mask Blue";
            // 
            // trackBarMaskBlue
            // 
            trackBarMaskBlue.Location = new Point(1066, 25);
            trackBarMaskBlue.Maximum = 255;
            trackBarMaskBlue.Name = "trackBarMaskBlue";
            trackBarMaskBlue.Size = new Size(127, 45);
            trackBarMaskBlue.TabIndex = 20;
            trackBarMaskBlue.ValueChanged += trackBarMaskBlue_ValueChanged;
            // 
            // trackBarMaskGreen
            // 
            trackBarMaskGreen.Location = new Point(933, 25);
            trackBarMaskGreen.Maximum = 255;
            trackBarMaskGreen.Name = "trackBarMaskGreen";
            trackBarMaskGreen.Size = new Size(127, 45);
            trackBarMaskGreen.TabIndex = 18;
            trackBarMaskGreen.ValueChanged += trackBarMaskGreen_ValueChanged;
            // 
            // trackBarMaskRed
            // 
            trackBarMaskRed.Location = new Point(768, 25);
            trackBarMaskRed.Maximum = 255;
            trackBarMaskRed.Name = "trackBarMaskRed";
            trackBarMaskRed.Size = new Size(127, 45);
            trackBarMaskRed.TabIndex = 19;
            trackBarMaskRed.ValueChanged += trackBarMaskRed_ValueChanged;
            // 
            // labelMaskRedValue
            // 
            labelMaskRedValue.AutoSize = true;
            labelMaskRedValue.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelMaskRedValue.Location = new Point(901, 29);
            labelMaskRedValue.Name = "labelMaskRedValue";
            labelMaskRedValue.Size = new Size(17, 20);
            labelMaskRedValue.TabIndex = 24;
            labelMaskRedValue.Text = "0";
            // 
            // labelMaskGreenValue
            // 
            labelMaskGreenValue.AutoSize = true;
            labelMaskGreenValue.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelMaskGreenValue.Location = new Point(1062, 26);
            labelMaskGreenValue.Name = "labelMaskGreenValue";
            labelMaskGreenValue.Size = new Size(17, 20);
            labelMaskGreenValue.TabIndex = 25;
            labelMaskGreenValue.Text = "0";
            // 
            // labelMaskBlueValue
            // 
            labelMaskBlueValue.AutoSize = true;
            labelMaskBlueValue.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelMaskBlueValue.Location = new Point(1199, 29);
            labelMaskBlueValue.Name = "labelMaskBlueValue";
            labelMaskBlueValue.Size = new Size(17, 20);
            labelMaskBlueValue.TabIndex = 26;
            labelMaskBlueValue.Text = "0";
            // 
            // panelImageSettings
            // 
            panelImageSettings.BackColor = SystemColors.ActiveBorder;
            panelImageSettings.Controls.Add(checkBoxEnableSharpen);
            panelImageSettings.Controls.Add(checkBoxEnableBlurr);
            panelImageSettings.Controls.Add(labelMaskBlueValue);
            panelImageSettings.Controls.Add(trackBarMaskGreen);
            panelImageSettings.Controls.Add(labelMaskGreenValue);
            panelImageSettings.Controls.Add(trackBarMaskRed);
            panelImageSettings.Controls.Add(labelMaskRedValue);
            panelImageSettings.Controls.Add(trackBarMaskBlue);
            panelImageSettings.Controls.Add(labelMaskBlue);
            panelImageSettings.Controls.Add(labelMaskGreen);
            panelImageSettings.Controls.Add(labelMaskRed);
            panelImageSettings.Dock = DockStyle.Bottom;
            panelImageSettings.Location = new Point(0, 536);
            panelImageSettings.Name = "panelImageSettings";
            panelImageSettings.Size = new Size(1247, 70);
            panelImageSettings.TabIndex = 28;
            // 
            // ImageProcessingDebugForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1247, 606);
            Controls.Add(panelImageSettings);
            Controls.Add(labelBlurredSharpened);
            Controls.Add(labelSharpenedImage);
            Controls.Add(blurredSharpenedImagePicBox);
            Controls.Add(sharpenedImagePicBox);
            Controls.Add(label1);
            Controls.Add(labelFilteredContours);
            Controls.Add(labelMaskApplied);
            Controls.Add(labelMask);
            Controls.Add(labelBlurredImage);
            Controls.Add(labelOriginalImage);
            Controls.Add(filteredContoursPicBox);
            Controls.Add(allContoursPicBox);
            Controls.Add(blurredImagePicBox);
            Controls.Add(appliedMaskPicBox);
            Controls.Add(invMaskPicBox);
            Controls.Add(originalImagePicBox);
            Margin = new Padding(3, 2, 3, 2);
            Name = "ImageProcessingDebugForm";
            Text = "Image Processing Debugging";
            FormClosed += ImageProcessingDebugForm_FormClosed;
            Resize += ImageProcessingDebugForm_Resize;
            ((System.ComponentModel.ISupportInitialize)originalImagePicBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)invMaskPicBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)appliedMaskPicBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)blurredImagePicBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)allContoursPicBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)filteredContoursPicBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)sharpenedImagePicBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)blurredSharpenedImagePicBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMaskBlue).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMaskGreen).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMaskRed).EndInit();
            panelImageSettings.ResumeLayout(false);
            panelImageSettings.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion

        private PictureBox originalImagePicBox;
        private PictureBox invMaskPicBox;
        private PictureBox appliedMaskPicBox;
        private PictureBox blurredImagePicBox;
        private PictureBox allContoursPicBox;
        private PictureBox filteredContoursPicBox;
        private Label labelOriginalImage;
        private Label labelBlurredImage;
        private Label labelMask;
        private Label labelMaskApplied;
        private Label labelFilteredContours;
        private Label label1;
        private PictureBox sharpenedImagePicBox;
        private PictureBox blurredSharpenedImagePicBox;
        private Label labelSharpenedImage;
        private Label labelBlurredSharpened;
        private CheckBox checkBoxEnableBlurr;
        private CheckBox checkBoxEnableSharpen;
        private Label labelMaskGreen;
        private Label labelMaskRed;
        private Label labelMaskBlue;
        private TrackBar trackBarMaskBlue;
        private TrackBar trackBarMaskGreen;
        private TrackBar trackBarMaskRed;
        private Label labelMaskRedValue;
        private Label labelMaskGreenValue;
        private Label labelMaskBlueValue;
        private Panel panelImageSettings;
    }
}