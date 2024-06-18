
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
            trackBarClothMaskBlueMin = new TrackBar();
            trackBarClothMaskGreenMin = new TrackBar();
            labelClothMaskRedMinValue = new Label();
            labelClothMaskGreenMinValue = new Label();
            labelClothMaskBlueMinValue = new Label();
            tableLayoutPanelMain = new TableLayoutPanel();
            tableLayoutPanelCueballMask = new TableLayoutPanel();
            labelCbMaskBlue = new Label();
            labelCbMaskGreen = new Label();
            labelCbMaskRed = new Label();
            trackBarCbMaskBlueMax = new TrackBar();
            trackBarCbMaskGreenMax = new TrackBar();
            trackBarCbMaskRedMax = new TrackBar();
            trackBarCbMaskBlueMin = new TrackBar();
            trackBarCbMaskGreenMin = new TrackBar();
            trackBarCbMaskRedMin = new TrackBar();
            labelCbMaskMax = new Label();
            labelCbMaskMin = new Label();
            labelCbMaskRedMax = new Label();
            labelCbMaskRedMin = new Label();
            labelCbMaskGreenMax = new Label();
            labelCbMaskGreenMin = new Label();
            labelCbMaskBlueMax = new Label();
            labelCbMaskBlueMin = new Label();
            tableLayoutPanelCheckboxes = new TableLayoutPanel();
            checkBoxEnableSharpen = new CheckBox();
            checkBoxEnableTableBoundary = new CheckBox();
            checkBoxEnableBlurr = new CheckBox();
            tableLayoutPanelClothMask = new TableLayoutPanel();
            labelClothMaskBlue = new Label();
            trackBarClothMaskRedMax = new TrackBar();
            labelClothMaskGreenMaxValue = new Label();
            labelClothMaskBlueMaxValue = new Label();
            labelClothMaskGreen = new Label();
            labelClothMaskRed = new Label();
            trackBarClothMaskRedMin = new TrackBar();
            labelClothMaskRedMaxValue = new Label();
            trackBarClothMaskBlueMax = new TrackBar();
            labelClothMaskMax = new Label();
            labelClothMaskMin = new Label();
            trackBarClothMaskGreenMax = new TrackBar();
            tableLayoutPanel1 = new TableLayoutPanel();
            label13 = new Label();
            label14 = new Label();
            ((System.ComponentModel.ISupportInitialize)originalImagePicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)invMaskPicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)appliedMaskPicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)blurredImagePicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)allContoursPicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)filteredContoursPicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sharpenedImagePicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)blurredSharpenedImagePicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarClothMaskBlueMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarClothMaskGreenMin).BeginInit();
            tableLayoutPanelMain.SuspendLayout();
            tableLayoutPanelCueballMask.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarCbMaskBlueMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarCbMaskGreenMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarCbMaskRedMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarCbMaskBlueMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarCbMaskGreenMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarCbMaskRedMin).BeginInit();
            tableLayoutPanelCheckboxes.SuspendLayout();
            tableLayoutPanelClothMask.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarClothMaskRedMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarClothMaskRedMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarClothMaskBlueMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarClothMaskGreenMax).BeginInit();
            tableLayoutPanel1.SuspendLayout();
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
            // trackBarClothMaskBlueMin
            // 
            trackBarClothMaskBlueMin.Dock = DockStyle.Fill;
            trackBarClothMaskBlueMin.Location = new Point(377, 20);
            trackBarClothMaskBlueMin.Margin = new Padding(0);
            trackBarClothMaskBlueMin.Maximum = 255;
            trackBarClothMaskBlueMin.Name = "trackBarClothMaskBlueMin";
            trackBarClothMaskBlueMin.Size = new Size(126, 36);
            trackBarClothMaskBlueMin.TabIndex = 20;
            trackBarClothMaskBlueMin.ValueChanged += trackBarMaskBlueMin_ValueChanged;
            // 
            // trackBarClothMaskGreenMin
            // 
            trackBarClothMaskGreenMin.Dock = DockStyle.Fill;
            trackBarClothMaskGreenMin.Location = new Point(211, 20);
            trackBarClothMaskGreenMin.Margin = new Padding(0);
            trackBarClothMaskGreenMin.Maximum = 255;
            trackBarClothMaskGreenMin.Name = "trackBarClothMaskGreenMin";
            trackBarClothMaskGreenMin.Size = new Size(126, 36);
            trackBarClothMaskGreenMin.TabIndex = 18;
            trackBarClothMaskGreenMin.ValueChanged += trackBarMaskGreenMin_ValueChanged;
            // 
            // labelClothMaskRedMinValue
            // 
            labelClothMaskRedMinValue.Anchor = AnchorStyles.None;
            labelClothMaskRedMinValue.AutoSize = true;
            labelClothMaskRedMinValue.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelClothMaskRedMinValue.Location = new Point(182, 28);
            labelClothMaskRedMinValue.Name = "labelClothMaskRedMinValue";
            labelClothMaskRedMinValue.Size = new Size(17, 20);
            labelClothMaskRedMinValue.TabIndex = 24;
            labelClothMaskRedMinValue.Text = "0";
            // 
            // labelClothMaskGreenMinValue
            // 
            labelClothMaskGreenMinValue.Anchor = AnchorStyles.None;
            labelClothMaskGreenMinValue.AutoSize = true;
            labelClothMaskGreenMinValue.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelClothMaskGreenMinValue.Location = new Point(348, 28);
            labelClothMaskGreenMinValue.Name = "labelClothMaskGreenMinValue";
            labelClothMaskGreenMinValue.Size = new Size(17, 20);
            labelClothMaskGreenMinValue.TabIndex = 25;
            labelClothMaskGreenMinValue.Text = "0";
            // 
            // labelClothMaskBlueMinValue
            // 
            labelClothMaskBlueMinValue.Anchor = AnchorStyles.None;
            labelClothMaskBlueMinValue.AutoSize = true;
            labelClothMaskBlueMinValue.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelClothMaskBlueMinValue.Location = new Point(515, 28);
            labelClothMaskBlueMinValue.Name = "labelClothMaskBlueMinValue";
            labelClothMaskBlueMinValue.Size = new Size(17, 20);
            labelClothMaskBlueMinValue.TabIndex = 26;
            labelClothMaskBlueMinValue.Text = "0";
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.BackColor = SystemColors.Info;
            tableLayoutPanelMain.ColumnCount = 3;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelCueballMask, 0, 0);
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelCheckboxes, 0, 0);
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelClothMask, 1, 0);
            tableLayoutPanelMain.Dock = DockStyle.Bottom;
            tableLayoutPanelMain.ImeMode = ImeMode.NoControl;
            tableLayoutPanelMain.Location = new Point(0, 513);
            tableLayoutPanelMain.Margin = new Padding(0);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 1;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Size = new Size(1247, 93);
            tableLayoutPanelMain.TabIndex = 29;
            // 
            // tableLayoutPanelCueballMask
            // 
            tableLayoutPanelCueballMask.BackColor = Color.FromArgb(244, 239, 198);
            tableLayoutPanelCueballMask.ColumnCount = 7;
            tableLayoutPanelCueballMask.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 45F));
            tableLayoutPanelCueballMask.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanelCueballMask.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            tableLayoutPanelCueballMask.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanelCueballMask.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            tableLayoutPanelCueballMask.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanelCueballMask.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            tableLayoutPanelCueballMask.Controls.Add(labelCbMaskBlue, 5, 0);
            tableLayoutPanelCueballMask.Controls.Add(labelCbMaskGreen, 3, 0);
            tableLayoutPanelCueballMask.Controls.Add(labelCbMaskRed, 1, 0);
            tableLayoutPanelCueballMask.Controls.Add(trackBarCbMaskBlueMax, 5, 2);
            tableLayoutPanelCueballMask.Controls.Add(trackBarCbMaskGreenMax, 3, 2);
            tableLayoutPanelCueballMask.Controls.Add(trackBarCbMaskRedMax, 1, 2);
            tableLayoutPanelCueballMask.Controls.Add(trackBarCbMaskBlueMin, 5, 1);
            tableLayoutPanelCueballMask.Controls.Add(trackBarCbMaskGreenMin, 3, 1);
            tableLayoutPanelCueballMask.Controls.Add(trackBarCbMaskRedMin, 1, 1);
            tableLayoutPanelCueballMask.Controls.Add(labelCbMaskMax, 0, 2);
            tableLayoutPanelCueballMask.Controls.Add(labelCbMaskMin, 0, 1);
            tableLayoutPanelCueballMask.Controls.Add(labelCbMaskRedMax, 2, 2);
            tableLayoutPanelCueballMask.Controls.Add(labelCbMaskRedMin, 2, 1);
            tableLayoutPanelCueballMask.Controls.Add(labelCbMaskGreenMax, 4, 2);
            tableLayoutPanelCueballMask.Controls.Add(labelCbMaskGreenMin, 4, 1);
            tableLayoutPanelCueballMask.Controls.Add(labelCbMaskBlueMax, 6, 2);
            tableLayoutPanelCueballMask.Controls.Add(labelCbMaskBlueMin, 6, 1);
            tableLayoutPanelCueballMask.Dock = DockStyle.Fill;
            tableLayoutPanelCueballMask.Location = new Point(160, 0);
            tableLayoutPanelCueballMask.Margin = new Padding(0);
            tableLayoutPanelCueballMask.Name = "tableLayoutPanelCueballMask";
            tableLayoutPanelCueballMask.RowCount = 3;
            tableLayoutPanelCueballMask.RowStyles.Add(new RowStyle());
            tableLayoutPanelCueballMask.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelCueballMask.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelCueballMask.Size = new Size(543, 93);
            tableLayoutPanelCueballMask.TabIndex = 42;
            // 
            // labelCbMaskBlue
            // 
            labelCbMaskBlue.Anchor = AnchorStyles.None;
            labelCbMaskBlue.AutoSize = true;
            labelCbMaskBlue.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelCbMaskBlue.Location = new Point(402, 0);
            labelCbMaskBlue.Name = "labelCbMaskBlue";
            labelCbMaskBlue.Size = new Size(76, 20);
            labelCbMaskBlue.TabIndex = 36;
            labelCbMaskBlue.Text = "Mask Blue";
            // 
            // labelCbMaskGreen
            // 
            labelCbMaskGreen.Anchor = AnchorStyles.None;
            labelCbMaskGreen.AutoSize = true;
            labelCbMaskGreen.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelCbMaskGreen.Location = new Point(231, 0);
            labelCbMaskGreen.Name = "labelCbMaskGreen";
            labelCbMaskGreen.Size = new Size(86, 20);
            labelCbMaskGreen.TabIndex = 36;
            labelCbMaskGreen.Text = "Mask Green";
            // 
            // labelCbMaskRed
            // 
            labelCbMaskRed.Anchor = AnchorStyles.None;
            labelCbMaskRed.AutoSize = true;
            labelCbMaskRed.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelCbMaskRed.Location = new Point(71, 0);
            labelCbMaskRed.Name = "labelCbMaskRed";
            labelCbMaskRed.Size = new Size(73, 20);
            labelCbMaskRed.TabIndex = 36;
            labelCbMaskRed.Text = "Mask Red";
            // 
            // trackBarCbMaskBlueMax
            // 
            trackBarCbMaskBlueMax.Dock = DockStyle.Fill;
            trackBarCbMaskBlueMax.Location = new Point(377, 56);
            trackBarCbMaskBlueMax.Margin = new Padding(0);
            trackBarCbMaskBlueMax.Maximum = 255;
            trackBarCbMaskBlueMax.Name = "trackBarCbMaskBlueMax";
            trackBarCbMaskBlueMax.Size = new Size(126, 37);
            trackBarCbMaskBlueMax.TabIndex = 36;
            trackBarCbMaskBlueMax.ValueChanged += trackBarCbMaskBlueMax_ValueChanged;
            // 
            // trackBarCbMaskGreenMax
            // 
            trackBarCbMaskGreenMax.Dock = DockStyle.Fill;
            trackBarCbMaskGreenMax.Location = new Point(211, 56);
            trackBarCbMaskGreenMax.Margin = new Padding(0);
            trackBarCbMaskGreenMax.Maximum = 255;
            trackBarCbMaskGreenMax.Name = "trackBarCbMaskGreenMax";
            trackBarCbMaskGreenMax.Size = new Size(126, 37);
            trackBarCbMaskGreenMax.TabIndex = 36;
            trackBarCbMaskGreenMax.ValueChanged += trackBarCbMaskGreenMax_ValueChanged;
            // 
            // trackBarCbMaskRedMax
            // 
            trackBarCbMaskRedMax.Dock = DockStyle.Fill;
            trackBarCbMaskRedMax.Location = new Point(45, 56);
            trackBarCbMaskRedMax.Margin = new Padding(0);
            trackBarCbMaskRedMax.Maximum = 255;
            trackBarCbMaskRedMax.Name = "trackBarCbMaskRedMax";
            trackBarCbMaskRedMax.Size = new Size(126, 37);
            trackBarCbMaskRedMax.TabIndex = 36;
            trackBarCbMaskRedMax.ValueChanged += trackBarCbMaskRedMax_ValueChanged;
            // 
            // trackBarCbMaskBlueMin
            // 
            trackBarCbMaskBlueMin.Dock = DockStyle.Fill;
            trackBarCbMaskBlueMin.Location = new Point(377, 20);
            trackBarCbMaskBlueMin.Margin = new Padding(0);
            trackBarCbMaskBlueMin.Maximum = 255;
            trackBarCbMaskBlueMin.Name = "trackBarCbMaskBlueMin";
            trackBarCbMaskBlueMin.Size = new Size(126, 36);
            trackBarCbMaskBlueMin.TabIndex = 44;
            trackBarCbMaskBlueMin.ValueChanged += trackBarCbMaskBlueMin_ValueChanged;
            // 
            // trackBarCbMaskGreenMin
            // 
            trackBarCbMaskGreenMin.Dock = DockStyle.Fill;
            trackBarCbMaskGreenMin.Location = new Point(211, 20);
            trackBarCbMaskGreenMin.Margin = new Padding(0);
            trackBarCbMaskGreenMin.Maximum = 255;
            trackBarCbMaskGreenMin.Name = "trackBarCbMaskGreenMin";
            trackBarCbMaskGreenMin.Size = new Size(126, 36);
            trackBarCbMaskGreenMin.TabIndex = 43;
            trackBarCbMaskGreenMin.ValueChanged += trackBarCbMaskGreenMin_ValueChanged;
            // 
            // trackBarCbMaskRedMin
            // 
            trackBarCbMaskRedMin.Dock = DockStyle.Fill;
            trackBarCbMaskRedMin.Location = new Point(45, 20);
            trackBarCbMaskRedMin.Margin = new Padding(0);
            trackBarCbMaskRedMin.Maximum = 255;
            trackBarCbMaskRedMin.Name = "trackBarCbMaskRedMin";
            trackBarCbMaskRedMin.Size = new Size(126, 36);
            trackBarCbMaskRedMin.TabIndex = 35;
            trackBarCbMaskRedMin.ValueChanged += trackBarCbMaskRedMin_ValueChanged;
            // 
            // labelCbMaskMax
            // 
            labelCbMaskMax.Anchor = AnchorStyles.None;
            labelCbMaskMax.AutoSize = true;
            labelCbMaskMax.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelCbMaskMax.Location = new Point(4, 64);
            labelCbMaskMax.Name = "labelCbMaskMax";
            labelCbMaskMax.Size = new Size(37, 20);
            labelCbMaskMax.TabIndex = 35;
            labelCbMaskMax.Text = "Max";
            // 
            // labelCbMaskMin
            // 
            labelCbMaskMin.Anchor = AnchorStyles.None;
            labelCbMaskMin.AutoSize = true;
            labelCbMaskMin.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelCbMaskMin.Location = new Point(5, 28);
            labelCbMaskMin.Name = "labelCbMaskMin";
            labelCbMaskMin.Size = new Size(34, 20);
            labelCbMaskMin.TabIndex = 35;
            labelCbMaskMin.Text = "Min";
            // 
            // labelCbMaskRedMax
            // 
            labelCbMaskRedMax.Anchor = AnchorStyles.None;
            labelCbMaskRedMax.AutoSize = true;
            labelCbMaskRedMax.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelCbMaskRedMax.Location = new Point(182, 64);
            labelCbMaskRedMax.Name = "labelCbMaskRedMax";
            labelCbMaskRedMax.Size = new Size(17, 20);
            labelCbMaskRedMax.TabIndex = 36;
            labelCbMaskRedMax.Text = "0";
            // 
            // labelCbMaskRedMin
            // 
            labelCbMaskRedMin.Anchor = AnchorStyles.None;
            labelCbMaskRedMin.AutoSize = true;
            labelCbMaskRedMin.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelCbMaskRedMin.Location = new Point(182, 28);
            labelCbMaskRedMin.Name = "labelCbMaskRedMin";
            labelCbMaskRedMin.Size = new Size(17, 20);
            labelCbMaskRedMin.TabIndex = 35;
            labelCbMaskRedMin.Text = "0";
            // 
            // labelCbMaskGreenMax
            // 
            labelCbMaskGreenMax.Anchor = AnchorStyles.None;
            labelCbMaskGreenMax.AutoSize = true;
            labelCbMaskGreenMax.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelCbMaskGreenMax.Location = new Point(348, 64);
            labelCbMaskGreenMax.Name = "labelCbMaskGreenMax";
            labelCbMaskGreenMax.Size = new Size(17, 20);
            labelCbMaskGreenMax.TabIndex = 38;
            labelCbMaskGreenMax.Text = "0";
            // 
            // labelCbMaskGreenMin
            // 
            labelCbMaskGreenMin.Anchor = AnchorStyles.None;
            labelCbMaskGreenMin.AutoSize = true;
            labelCbMaskGreenMin.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelCbMaskGreenMin.Location = new Point(348, 28);
            labelCbMaskGreenMin.Name = "labelCbMaskGreenMin";
            labelCbMaskGreenMin.Size = new Size(17, 20);
            labelCbMaskGreenMin.TabIndex = 37;
            labelCbMaskGreenMin.Text = "0";
            // 
            // labelCbMaskBlueMax
            // 
            labelCbMaskBlueMax.Anchor = AnchorStyles.None;
            labelCbMaskBlueMax.AutoSize = true;
            labelCbMaskBlueMax.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelCbMaskBlueMax.Location = new Point(514, 64);
            labelCbMaskBlueMax.Name = "labelCbMaskBlueMax";
            labelCbMaskBlueMax.Size = new Size(17, 20);
            labelCbMaskBlueMax.TabIndex = 40;
            labelCbMaskBlueMax.Text = "0";
            // 
            // labelCbMaskBlueMin
            // 
            labelCbMaskBlueMin.Anchor = AnchorStyles.None;
            labelCbMaskBlueMin.AutoSize = true;
            labelCbMaskBlueMin.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelCbMaskBlueMin.Location = new Point(514, 28);
            labelCbMaskBlueMin.Name = "labelCbMaskBlueMin";
            labelCbMaskBlueMin.Size = new Size(17, 20);
            labelCbMaskBlueMin.TabIndex = 39;
            labelCbMaskBlueMin.Text = "0";
            // 
            // tableLayoutPanelCheckboxes
            // 
            tableLayoutPanelCheckboxes.BackColor = SystemColors.ScrollBar;
            tableLayoutPanelCheckboxes.ColumnCount = 1;
            tableLayoutPanelCheckboxes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelCheckboxes.Controls.Add(checkBoxEnableSharpen, 0, 0);
            tableLayoutPanelCheckboxes.Controls.Add(checkBoxEnableTableBoundary, 0, 2);
            tableLayoutPanelCheckboxes.Controls.Add(checkBoxEnableBlurr, 0, 1);
            tableLayoutPanelCheckboxes.Dock = DockStyle.Fill;
            tableLayoutPanelCheckboxes.Location = new Point(0, 0);
            tableLayoutPanelCheckboxes.Margin = new Padding(0);
            tableLayoutPanelCheckboxes.Name = "tableLayoutPanelCheckboxes";
            tableLayoutPanelCheckboxes.RowCount = 3;
            tableLayoutPanelCheckboxes.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanelCheckboxes.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanelCheckboxes.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanelCheckboxes.Size = new Size(160, 93);
            tableLayoutPanelCheckboxes.TabIndex = 40;
            // 
            // checkBoxEnableSharpen
            // 
            checkBoxEnableSharpen.AutoSize = true;
            checkBoxEnableSharpen.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            checkBoxEnableSharpen.Location = new Point(3, 3);
            checkBoxEnableSharpen.Name = "checkBoxEnableSharpen";
            checkBoxEnableSharpen.Size = new Size(124, 23);
            checkBoxEnableSharpen.TabIndex = 39;
            checkBoxEnableSharpen.Text = "Use Sharpening";
            checkBoxEnableSharpen.UseVisualStyleBackColor = true;
            // 
            // checkBoxEnableTableBoundary
            // 
            checkBoxEnableTableBoundary.AutoSize = true;
            checkBoxEnableTableBoundary.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            checkBoxEnableTableBoundary.Location = new Point(3, 65);
            checkBoxEnableTableBoundary.Name = "checkBoxEnableTableBoundary";
            checkBoxEnableTableBoundary.Size = new Size(148, 23);
            checkBoxEnableTableBoundary.TabIndex = 39;
            checkBoxEnableTableBoundary.Text = "Use Table Boundary";
            checkBoxEnableTableBoundary.UseVisualStyleBackColor = true;
            // 
            // checkBoxEnableBlurr
            // 
            checkBoxEnableBlurr.AutoSize = true;
            checkBoxEnableBlurr.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            checkBoxEnableBlurr.Location = new Point(3, 34);
            checkBoxEnableBlurr.Name = "checkBoxEnableBlurr";
            checkBoxEnableBlurr.Size = new Size(79, 23);
            checkBoxEnableBlurr.TabIndex = 38;
            checkBoxEnableBlurr.Text = "Use Blur";
            checkBoxEnableBlurr.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelClothMask
            // 
            tableLayoutPanelClothMask.BackColor = Color.FromArgb(181, 244, 205);
            tableLayoutPanelClothMask.ColumnCount = 7;
            tableLayoutPanelClothMask.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 45F));
            tableLayoutPanelClothMask.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanelClothMask.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            tableLayoutPanelClothMask.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanelClothMask.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            tableLayoutPanelClothMask.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanelClothMask.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            tableLayoutPanelClothMask.Controls.Add(labelClothMaskBlue, 5, 0);
            tableLayoutPanelClothMask.Controls.Add(trackBarClothMaskRedMax, 1, 2);
            tableLayoutPanelClothMask.Controls.Add(labelClothMaskGreenMaxValue, 4, 2);
            tableLayoutPanelClothMask.Controls.Add(labelClothMaskBlueMaxValue, 6, 2);
            tableLayoutPanelClothMask.Controls.Add(labelClothMaskGreen, 3, 0);
            tableLayoutPanelClothMask.Controls.Add(labelClothMaskRed, 1, 0);
            tableLayoutPanelClothMask.Controls.Add(labelClothMaskBlueMinValue, 6, 1);
            tableLayoutPanelClothMask.Controls.Add(trackBarClothMaskRedMin, 1, 1);
            tableLayoutPanelClothMask.Controls.Add(trackBarClothMaskBlueMin, 5, 1);
            tableLayoutPanelClothMask.Controls.Add(labelClothMaskGreenMinValue, 4, 1);
            tableLayoutPanelClothMask.Controls.Add(labelClothMaskRedMinValue, 2, 1);
            tableLayoutPanelClothMask.Controls.Add(labelClothMaskRedMaxValue, 2, 2);
            tableLayoutPanelClothMask.Controls.Add(trackBarClothMaskBlueMax, 5, 2);
            tableLayoutPanelClothMask.Controls.Add(labelClothMaskMax, 0, 2);
            tableLayoutPanelClothMask.Controls.Add(labelClothMaskMin, 0, 1);
            tableLayoutPanelClothMask.Controls.Add(trackBarClothMaskGreenMax, 3, 2);
            tableLayoutPanelClothMask.Controls.Add(trackBarClothMaskGreenMin, 3, 1);
            tableLayoutPanelClothMask.Dock = DockStyle.Fill;
            tableLayoutPanelClothMask.Location = new Point(703, 0);
            tableLayoutPanelClothMask.Margin = new Padding(0);
            tableLayoutPanelClothMask.Name = "tableLayoutPanelClothMask";
            tableLayoutPanelClothMask.RowCount = 3;
            tableLayoutPanelClothMask.RowStyles.Add(new RowStyle());
            tableLayoutPanelClothMask.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelClothMask.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelClothMask.Size = new Size(544, 93);
            tableLayoutPanelClothMask.TabIndex = 41;
            // 
            // labelClothMaskBlue
            // 
            labelClothMaskBlue.Anchor = AnchorStyles.None;
            labelClothMaskBlue.AutoSize = true;
            labelClothMaskBlue.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelClothMaskBlue.Location = new Point(402, 0);
            labelClothMaskBlue.Name = "labelClothMaskBlue";
            labelClothMaskBlue.Size = new Size(76, 20);
            labelClothMaskBlue.TabIndex = 36;
            labelClothMaskBlue.Text = "Mask Blue";
            // 
            // trackBarClothMaskRedMax
            // 
            trackBarClothMaskRedMax.Dock = DockStyle.Fill;
            trackBarClothMaskRedMax.Location = new Point(45, 56);
            trackBarClothMaskRedMax.Margin = new Padding(0);
            trackBarClothMaskRedMax.Maximum = 255;
            trackBarClothMaskRedMax.Name = "trackBarClothMaskRedMax";
            trackBarClothMaskRedMax.Size = new Size(126, 37);
            trackBarClothMaskRedMax.TabIndex = 27;
            trackBarClothMaskRedMax.ValueChanged += trackBarMaskRedMax_ValueChanged;
            // 
            // labelClothMaskGreenMaxValue
            // 
            labelClothMaskGreenMaxValue.Anchor = AnchorStyles.None;
            labelClothMaskGreenMaxValue.AutoSize = true;
            labelClothMaskGreenMaxValue.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelClothMaskGreenMaxValue.Location = new Point(348, 64);
            labelClothMaskGreenMaxValue.Name = "labelClothMaskGreenMaxValue";
            labelClothMaskGreenMaxValue.Size = new Size(17, 20);
            labelClothMaskGreenMaxValue.TabIndex = 32;
            labelClothMaskGreenMaxValue.Text = "0";
            // 
            // labelClothMaskBlueMaxValue
            // 
            labelClothMaskBlueMaxValue.Anchor = AnchorStyles.None;
            labelClothMaskBlueMaxValue.AutoSize = true;
            labelClothMaskBlueMaxValue.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelClothMaskBlueMaxValue.Location = new Point(515, 64);
            labelClothMaskBlueMaxValue.Name = "labelClothMaskBlueMaxValue";
            labelClothMaskBlueMaxValue.Size = new Size(17, 20);
            labelClothMaskBlueMaxValue.TabIndex = 31;
            labelClothMaskBlueMaxValue.Text = "0";
            // 
            // labelClothMaskGreen
            // 
            labelClothMaskGreen.Anchor = AnchorStyles.None;
            labelClothMaskGreen.AutoSize = true;
            labelClothMaskGreen.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelClothMaskGreen.Location = new Point(231, 0);
            labelClothMaskGreen.Name = "labelClothMaskGreen";
            labelClothMaskGreen.Size = new Size(86, 20);
            labelClothMaskGreen.TabIndex = 36;
            labelClothMaskGreen.Text = "Mask Green";
            // 
            // labelClothMaskRed
            // 
            labelClothMaskRed.Anchor = AnchorStyles.None;
            labelClothMaskRed.AutoSize = true;
            labelClothMaskRed.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelClothMaskRed.Location = new Point(71, 0);
            labelClothMaskRed.Name = "labelClothMaskRed";
            labelClothMaskRed.Size = new Size(73, 20);
            labelClothMaskRed.TabIndex = 36;
            labelClothMaskRed.Text = "Mask Red";
            // 
            // trackBarClothMaskRedMin
            // 
            trackBarClothMaskRedMin.Dock = DockStyle.Fill;
            trackBarClothMaskRedMin.Location = new Point(45, 20);
            trackBarClothMaskRedMin.Margin = new Padding(0);
            trackBarClothMaskRedMin.Maximum = 255;
            trackBarClothMaskRedMin.Name = "trackBarClothMaskRedMin";
            trackBarClothMaskRedMin.Size = new Size(126, 36);
            trackBarClothMaskRedMin.TabIndex = 19;
            trackBarClothMaskRedMin.ValueChanged += trackBarMaskRedMin_ValueChanged;
            // 
            // labelClothMaskRedMaxValue
            // 
            labelClothMaskRedMaxValue.Anchor = AnchorStyles.None;
            labelClothMaskRedMaxValue.AutoSize = true;
            labelClothMaskRedMaxValue.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelClothMaskRedMaxValue.Location = new Point(182, 64);
            labelClothMaskRedMaxValue.Name = "labelClothMaskRedMaxValue";
            labelClothMaskRedMaxValue.Size = new Size(17, 20);
            labelClothMaskRedMaxValue.TabIndex = 30;
            labelClothMaskRedMaxValue.Text = "0";
            // 
            // trackBarClothMaskBlueMax
            // 
            trackBarClothMaskBlueMax.Dock = DockStyle.Fill;
            trackBarClothMaskBlueMax.Location = new Point(377, 56);
            trackBarClothMaskBlueMax.Margin = new Padding(0);
            trackBarClothMaskBlueMax.Maximum = 255;
            trackBarClothMaskBlueMax.Name = "trackBarClothMaskBlueMax";
            trackBarClothMaskBlueMax.Size = new Size(126, 37);
            trackBarClothMaskBlueMax.TabIndex = 28;
            trackBarClothMaskBlueMax.ValueChanged += trackBarMaskBlueMax_ValueChanged;
            // 
            // labelClothMaskMax
            // 
            labelClothMaskMax.Anchor = AnchorStyles.None;
            labelClothMaskMax.AutoSize = true;
            labelClothMaskMax.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelClothMaskMax.Location = new Point(4, 64);
            labelClothMaskMax.Name = "labelClothMaskMax";
            labelClothMaskMax.Size = new Size(37, 20);
            labelClothMaskMax.TabIndex = 35;
            labelClothMaskMax.Text = "Max";
            // 
            // labelClothMaskMin
            // 
            labelClothMaskMin.Anchor = AnchorStyles.None;
            labelClothMaskMin.AutoSize = true;
            labelClothMaskMin.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelClothMaskMin.Location = new Point(5, 28);
            labelClothMaskMin.Name = "labelClothMaskMin";
            labelClothMaskMin.Size = new Size(34, 20);
            labelClothMaskMin.TabIndex = 35;
            labelClothMaskMin.Text = "Min";
            // 
            // trackBarClothMaskGreenMax
            // 
            trackBarClothMaskGreenMax.Dock = DockStyle.Fill;
            trackBarClothMaskGreenMax.Location = new Point(211, 56);
            trackBarClothMaskGreenMax.Margin = new Padding(0);
            trackBarClothMaskGreenMax.Maximum = 255;
            trackBarClothMaskGreenMax.Name = "trackBarClothMaskGreenMax";
            trackBarClothMaskGreenMax.Size = new Size(126, 37);
            trackBarClothMaskGreenMax.TabIndex = 29;
            trackBarClothMaskGreenMax.ValueChanged += trackBarMaskGreenMax_ValueChanged;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.PeachPuff;
            tableLayoutPanel1.ColumnCount = 7;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Controls.Add(label13, 5, 0);
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(200, 100);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label13
            // 
            label13.Anchor = AnchorStyles.None;
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label13.Location = new Point(141, 30);
            label13.Name = "label13";
            label13.Size = new Size(47, 40);
            label13.TabIndex = 36;
            label13.Text = "Mask Blue";
            // 
            // label14
            // 
            label14.Anchor = AnchorStyles.None;
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label14.Location = new Point(75, 30);
            label14.Name = "label14";
            label14.Size = new Size(48, 40);
            label14.TabIndex = 36;
            label14.Text = "Mask Green";
            // 
            // ImageProcessingDebugForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1247, 606);
            Controls.Add(tableLayoutPanelMain);
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
            ((System.ComponentModel.ISupportInitialize)trackBarClothMaskBlueMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarClothMaskGreenMin).EndInit();
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelCueballMask.ResumeLayout(false);
            tableLayoutPanelCueballMask.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarCbMaskBlueMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarCbMaskGreenMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarCbMaskRedMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarCbMaskBlueMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarCbMaskGreenMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarCbMaskRedMin).EndInit();
            tableLayoutPanelCheckboxes.ResumeLayout(false);
            tableLayoutPanelCheckboxes.PerformLayout();
            tableLayoutPanelClothMask.ResumeLayout(false);
            tableLayoutPanelClothMask.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarClothMaskRedMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarClothMaskRedMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarClothMaskBlueMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarClothMaskGreenMax).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
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
        private TrackBar trackBarClothMaskBlueMin;
        private TrackBar trackBarClothMaskGreenMin;
        private Label labelClothMaskRedMinValue;
        private Label labelClothMaskGreenMinValue;
        private Label labelClothMaskBlueMinValue;
        private TrackBar trackBarClothMaskGreenMax;
        private TrackBar trackBarClothMaskBlueMax;
        private TrackBar trackBarClothMaskRedMax;
        private Label labelClothMaskGreenMaxValue;
        private Label labelClothMaskBlueMaxValue;
        private Label labelClothMaskRedMaxValue;
        private TableLayoutPanel tableLayoutPanelMain;
        private CheckBox checkBoxEnableTableBoundary;
        private CheckBox checkBoxEnableSharpen;
        private CheckBox checkBoxEnableBlurr;
        private TableLayoutPanel tableLayoutPanelCheckboxes;
        private TableLayoutPanel tableLayoutPanelClothMask;
        private Label labelClothMaskMax;
        private Label labelClothMaskMin;
        private Label labelClothMaskBlue;
        private Label labelClothMaskGreen;
        private Label labelClothMaskRed;
        private TableLayoutPanel tableLayoutPanelCueballMask;
        private Label labelCbMaskBlue;
        private Label labelCbMaskGreen;
        private Label labelCbMaskRed;
        private TrackBar trackBarCbMaskBlueMax;
        private TrackBar trackBarCbMaskGreenMax;
        private TrackBar trackBarCbMaskRedMax;
        private TrackBar trackBarCbMaskBlueMin;
        private TrackBar trackBarCbMaskGreenMin;
        private TrackBar trackBarCbMaskRedMin;
        private Label labelCbMaskMax;
        private Label labelCbMaskMin;
        private Label labelCbMaskRedMax;
        private Label labelCbMaskRedMin;
        private Label labelCbMaskGreenMax;
        private Label labelCbMaskGreenMin;
        private Label labelCbMaskBlueMax;
        private Label labelCbMaskBlueMin;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label13;
        private Label label14;
        private TrackBar trackBarClothMaskRedMin;
    }
}