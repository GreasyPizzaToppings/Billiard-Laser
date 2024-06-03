
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
            trackBarMaskBlueMin = new TrackBar();
            trackBarMaskGreenMin = new TrackBar();
            trackBarMaskRedMin = new TrackBar();
            labelMaskRedMinValue = new Label();
            labelMaskGreenMinValue = new Label();
            labelMaskBlueMinValue = new Label();
            panelImageSettings = new Panel();
            panelMaskValues = new Panel();
            trackBarMaskRedMax = new TrackBar();
            labelMin = new Label();
            labelMaskGreenMaxValue = new Label();
            labelMaskBlueMaxValue = new Label();
            labelMax = new Label();
            trackBarMaskGreenMax = new TrackBar();
            trackBarMaskBlueMax = new TrackBar();
            labelMaskRedMaxValue = new Label();
            ((System.ComponentModel.ISupportInitialize)originalImagePicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)invMaskPicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)appliedMaskPicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)blurredImagePicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)allContoursPicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)filteredContoursPicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sharpenedImagePicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)blurredSharpenedImagePicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMaskBlueMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMaskGreenMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMaskRedMin).BeginInit();
            panelImageSettings.SuspendLayout();
            panelMaskValues.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarMaskRedMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMaskGreenMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMaskBlueMax).BeginInit();
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
            checkBoxEnableBlurr.Dock = DockStyle.Left;
            checkBoxEnableBlurr.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            checkBoxEnableBlurr.Location = new Point(0, 0);
            checkBoxEnableBlurr.Name = "checkBoxEnableBlurr";
            checkBoxEnableBlurr.Size = new Size(103, 93);
            checkBoxEnableBlurr.TabIndex = 16;
            checkBoxEnableBlurr.Text = "Enable Blur";
            checkBoxEnableBlurr.UseVisualStyleBackColor = true;
            // 
            // checkBoxEnableSharpen
            // 
            checkBoxEnableSharpen.AutoSize = true;
            checkBoxEnableSharpen.Dock = DockStyle.Left;
            checkBoxEnableSharpen.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            checkBoxEnableSharpen.Location = new Point(103, 0);
            checkBoxEnableSharpen.Name = "checkBoxEnableSharpen";
            checkBoxEnableSharpen.Size = new Size(152, 93);
            checkBoxEnableSharpen.TabIndex = 17;
            checkBoxEnableSharpen.Text = "Enable Sharpening";
            checkBoxEnableSharpen.UseVisualStyleBackColor = true;
            // 
            // labelMaskGreen
            // 
            labelMaskGreen.AutoSize = true;
            labelMaskGreen.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelMaskGreen.Location = new Point(227, 5);
            labelMaskGreen.Name = "labelMaskGreen";
            labelMaskGreen.Size = new Size(86, 20);
            labelMaskGreen.TabIndex = 21;
            labelMaskGreen.Text = "Mask Green";
            // 
            // labelMaskRed
            // 
            labelMaskRed.AutoSize = true;
            labelMaskRed.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelMaskRed.Location = new Point(64, 4);
            labelMaskRed.Name = "labelMaskRed";
            labelMaskRed.Size = new Size(73, 20);
            labelMaskRed.TabIndex = 22;
            labelMaskRed.Text = "Mask Red";
            // 
            // labelMaskBlue
            // 
            labelMaskBlue.AutoSize = true;
            labelMaskBlue.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelMaskBlue.Location = new Point(390, 5);
            labelMaskBlue.Name = "labelMaskBlue";
            labelMaskBlue.Size = new Size(76, 20);
            labelMaskBlue.TabIndex = 23;
            labelMaskBlue.Text = "Mask Blue";
            // 
            // trackBarMaskBlueMin
            // 
            trackBarMaskBlueMin.Location = new Point(359, 61);
            trackBarMaskBlueMin.Maximum = 255;
            trackBarMaskBlueMin.Name = "trackBarMaskBlueMin";
            trackBarMaskBlueMin.Size = new Size(127, 45);
            trackBarMaskBlueMin.TabIndex = 20;
            trackBarMaskBlueMin.ValueChanged += trackBarMaskBlueMin_ValueChanged;
            // 
            // trackBarMaskGreenMin
            // 
            trackBarMaskGreenMin.Location = new Point(204, 23);
            trackBarMaskGreenMin.Maximum = 255;
            trackBarMaskGreenMin.Name = "trackBarMaskGreenMin";
            trackBarMaskGreenMin.Size = new Size(127, 45);
            trackBarMaskGreenMin.TabIndex = 18;
            trackBarMaskGreenMin.ValueChanged += trackBarMaskGreenMin_ValueChanged;
            // 
            // trackBarMaskRedMin
            // 
            trackBarMaskRedMin.Location = new Point(39, 26);
            trackBarMaskRedMin.Maximum = 255;
            trackBarMaskRedMin.Name = "trackBarMaskRedMin";
            trackBarMaskRedMin.Size = new Size(127, 45);
            trackBarMaskRedMin.TabIndex = 19;
            trackBarMaskRedMin.ValueChanged += trackBarMaskRedMin_ValueChanged;
            // 
            // labelMaskRedMinValue
            // 
            labelMaskRedMinValue.AutoSize = true;
            labelMaskRedMinValue.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelMaskRedMinValue.Location = new Point(167, 29);
            labelMaskRedMinValue.Name = "labelMaskRedMinValue";
            labelMaskRedMinValue.Size = new Size(17, 20);
            labelMaskRedMinValue.TabIndex = 24;
            labelMaskRedMinValue.Text = "0";
            // 
            // labelMaskGreenMinValue
            // 
            labelMaskGreenMinValue.AutoSize = true;
            labelMaskGreenMinValue.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelMaskGreenMinValue.Location = new Point(328, 29);
            labelMaskGreenMinValue.Name = "labelMaskGreenMinValue";
            labelMaskGreenMinValue.Size = new Size(17, 20);
            labelMaskGreenMinValue.TabIndex = 25;
            labelMaskGreenMinValue.Text = "0";
            // 
            // labelMaskBlueMinValue
            // 
            labelMaskBlueMinValue.AutoSize = true;
            labelMaskBlueMinValue.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelMaskBlueMinValue.Location = new Point(485, 31);
            labelMaskBlueMinValue.Name = "labelMaskBlueMinValue";
            labelMaskBlueMinValue.Size = new Size(17, 20);
            labelMaskBlueMinValue.TabIndex = 26;
            labelMaskBlueMinValue.Text = "0";
            // 
            // panelImageSettings
            // 
            panelImageSettings.BackColor = SystemColors.ActiveBorder;
            panelImageSettings.Controls.Add(panelMaskValues);
            panelImageSettings.Controls.Add(checkBoxEnableSharpen);
            panelImageSettings.Controls.Add(checkBoxEnableBlurr);
            panelImageSettings.Dock = DockStyle.Bottom;
            panelImageSettings.Location = new Point(0, 513);
            panelImageSettings.Name = "panelImageSettings";
            panelImageSettings.Size = new Size(1247, 93);
            panelImageSettings.TabIndex = 28;
            // 
            // panelMaskValues
            // 
            panelMaskValues.BackColor = SystemColors.AppWorkspace;
            panelMaskValues.Controls.Add(trackBarMaskRedMax);
            panelMaskValues.Controls.Add(labelMaskGreen);
            panelMaskValues.Controls.Add(labelMin);
            panelMaskValues.Controls.Add(labelMaskGreenMaxValue);
            panelMaskValues.Controls.Add(labelMaskBlue);
            panelMaskValues.Controls.Add(trackBarMaskRedMin);
            panelMaskValues.Controls.Add(labelMaskBlueMaxValue);
            panelMaskValues.Controls.Add(trackBarMaskBlueMin);
            panelMaskValues.Controls.Add(labelMax);
            panelMaskValues.Controls.Add(trackBarMaskGreenMax);
            panelMaskValues.Controls.Add(trackBarMaskBlueMax);
            panelMaskValues.Controls.Add(labelMaskRedMinValue);
            panelMaskValues.Controls.Add(labelMaskGreenMinValue);
            panelMaskValues.Controls.Add(trackBarMaskGreenMin);
            panelMaskValues.Controls.Add(labelMaskRedMaxValue);
            panelMaskValues.Controls.Add(labelMaskBlueMinValue);
            panelMaskValues.Controls.Add(labelMaskRed);
            panelMaskValues.Dock = DockStyle.Right;
            panelMaskValues.Location = new Point(724, 0);
            panelMaskValues.Name = "panelMaskValues";
            panelMaskValues.Size = new Size(523, 93);
            panelMaskValues.TabIndex = 35;
            // 
            // trackBarMaskRedMax
            // 
            trackBarMaskRedMax.Location = new Point(39, 64);
            trackBarMaskRedMax.Maximum = 255;
            trackBarMaskRedMax.Name = "trackBarMaskRedMax";
            trackBarMaskRedMax.Size = new Size(127, 45);
            trackBarMaskRedMax.TabIndex = 27;
            trackBarMaskRedMax.Scroll += trackBarMaskRedMax_Scroll;
            trackBarMaskRedMax.ValueChanged += trackBarMaskRedMax_ValueChanged;
            // 
            // labelMin
            // 
            labelMin.AutoSize = true;
            labelMin.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelMin.Location = new Point(3, 30);
            labelMin.Name = "labelMin";
            labelMin.Size = new Size(34, 20);
            labelMin.TabIndex = 34;
            labelMin.Text = "Min";
            // 
            // labelMaskGreenMaxValue
            // 
            labelMaskGreenMaxValue.AutoSize = true;
            labelMaskGreenMaxValue.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelMaskGreenMaxValue.Location = new Point(328, 61);
            labelMaskGreenMaxValue.Name = "labelMaskGreenMaxValue";
            labelMaskGreenMaxValue.Size = new Size(17, 20);
            labelMaskGreenMaxValue.TabIndex = 32;
            labelMaskGreenMaxValue.Text = "0";
            // 
            // labelMaskBlueMaxValue
            // 
            labelMaskBlueMaxValue.AutoSize = true;
            labelMaskBlueMaxValue.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelMaskBlueMaxValue.Location = new Point(485, 60);
            labelMaskBlueMaxValue.Name = "labelMaskBlueMaxValue";
            labelMaskBlueMaxValue.Size = new Size(17, 20);
            labelMaskBlueMaxValue.TabIndex = 31;
            labelMaskBlueMaxValue.Text = "0";
            // 
            // labelMax
            // 
            labelMax.AutoSize = true;
            labelMax.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelMax.Location = new Point(3, 61);
            labelMax.Name = "labelMax";
            labelMax.Size = new Size(37, 20);
            labelMax.TabIndex = 33;
            labelMax.Text = "Max";
            // 
            // trackBarMaskGreenMax
            // 
            trackBarMaskGreenMax.Location = new Point(204, 61);
            trackBarMaskGreenMax.Maximum = 255;
            trackBarMaskGreenMax.Name = "trackBarMaskGreenMax";
            trackBarMaskGreenMax.Size = new Size(127, 45);
            trackBarMaskGreenMax.TabIndex = 29;
            trackBarMaskGreenMax.ValueChanged += trackBarMaskGreenMax_ValueChanged;
            // 
            // trackBarMaskBlueMax
            // 
            trackBarMaskBlueMax.Location = new Point(359, 26);
            trackBarMaskBlueMax.Maximum = 255;
            trackBarMaskBlueMax.Name = "trackBarMaskBlueMax";
            trackBarMaskBlueMax.Size = new Size(127, 45);
            trackBarMaskBlueMax.TabIndex = 28;
            trackBarMaskBlueMax.ValueChanged += trackBarMaskBlueMax_ValueChanged;
            // 
            // labelMaskRedMaxValue
            // 
            labelMaskRedMaxValue.AutoSize = true;
            labelMaskRedMaxValue.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelMaskRedMaxValue.Location = new Point(167, 60);
            labelMaskRedMaxValue.Name = "labelMaskRedMaxValue";
            labelMaskRedMaxValue.Size = new Size(17, 20);
            labelMaskRedMaxValue.TabIndex = 30;
            labelMaskRedMaxValue.Text = "0";
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
            Load += ImageProcessingDebugForm_Load;
            Resize += ImageProcessingDebugForm_Resize;
            ((System.ComponentModel.ISupportInitialize)originalImagePicBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)invMaskPicBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)appliedMaskPicBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)blurredImagePicBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)allContoursPicBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)filteredContoursPicBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)sharpenedImagePicBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)blurredSharpenedImagePicBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMaskBlueMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMaskGreenMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMaskRedMin).EndInit();
            panelImageSettings.ResumeLayout(false);
            panelImageSettings.PerformLayout();
            panelMaskValues.ResumeLayout(false);
            panelMaskValues.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarMaskRedMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMaskGreenMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMaskBlueMax).EndInit();
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
        private TrackBar trackBarMaskBlueMin;
        private TrackBar trackBarMaskGreenMin;
        private TrackBar trackBarMaskRedMin;
        private Label labelMaskRedMinValue;
        private Label labelMaskGreenMinValue;
        private Label labelMaskBlueMinValue;
        private Panel panelImageSettings;
        private TrackBar trackBarMaskGreenMax;
        private TrackBar trackBarMaskBlueMax;
        private TrackBar trackBarMaskRedMax;
        private Label labelMaskGreenMaxValue;
        private Label labelMaskBlueMaxValue;
        private Label labelMaskRedMaxValue;
        private Label labelMin;
        private Label labelMax;
        private Panel panelMaskValues;
    }
}