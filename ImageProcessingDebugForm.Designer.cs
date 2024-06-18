
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
            trackBarMaskBlueMin = new TrackBar();
            trackBarMaskGreenMin = new TrackBar();
            labelMaskRedMinValue = new Label();
            labelMaskGreenMinValue = new Label();
            labelMaskBlueMinValue = new Label();
            tableLayoutPanelMain = new TableLayoutPanel();
            tableLayoutPanelCueballMask = new TableLayoutPanel();
            label15 = new Label();
            label16 = new Label();
            label17 = new Label();
            trackBar8 = new TrackBar();
            trackBar9 = new TrackBar();
            trackBar10 = new TrackBar();
            trackBar11 = new TrackBar();
            trackBar12 = new TrackBar();
            trackBar13 = new TrackBar();
            label18 = new Label();
            label19 = new Label();
            label20 = new Label();
            label21 = new Label();
            label22 = new Label();
            label23 = new Label();
            label24 = new Label();
            label25 = new Label();
            tableLayoutPanelCheckboxes = new TableLayoutPanel();
            checkBoxEnableSharpen = new CheckBox();
            checkBoxEnableTableBoundary = new CheckBox();
            checkBoxEnableBlurr = new CheckBox();
            tableLayoutPanelClothMask = new TableLayoutPanel();
            label12 = new Label();
            trackBarMaskRedMax = new TrackBar();
            labelMaskGreenMaxValue = new Label();
            labelMaskBlueMaxValue = new Label();
            label11 = new Label();
            label10 = new Label();
            trackBarMaskRedMin = new TrackBar();
            labelMaskRedMaxValue = new Label();
            trackBarMaskBlueMax = new TrackBar();
            label3 = new Label();
            label2 = new Label();
            trackBarMaskGreenMax = new TrackBar();
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
            ((System.ComponentModel.ISupportInitialize)trackBarMaskBlueMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMaskGreenMin).BeginInit();
            tableLayoutPanelMain.SuspendLayout();
            tableLayoutPanelCueballMask.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar11).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar12).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar13).BeginInit();
            tableLayoutPanelCheckboxes.SuspendLayout();
            tableLayoutPanelClothMask.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarMaskRedMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMaskRedMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMaskBlueMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMaskGreenMax).BeginInit();
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
            // trackBarMaskBlueMin
            // 
            trackBarMaskBlueMin.Dock = DockStyle.Fill;
            trackBarMaskBlueMin.Location = new Point(377, 20);
            trackBarMaskBlueMin.Margin = new Padding(0);
            trackBarMaskBlueMin.Maximum = 255;
            trackBarMaskBlueMin.Name = "trackBarMaskBlueMin";
            trackBarMaskBlueMin.Size = new Size(126, 36);
            trackBarMaskBlueMin.TabIndex = 20;
            trackBarMaskBlueMin.ValueChanged += trackBarMaskBlueMin_ValueChanged;
            // 
            // trackBarMaskGreenMin
            // 
            trackBarMaskGreenMin.Dock = DockStyle.Fill;
            trackBarMaskGreenMin.Location = new Point(211, 20);
            trackBarMaskGreenMin.Margin = new Padding(0);
            trackBarMaskGreenMin.Maximum = 255;
            trackBarMaskGreenMin.Name = "trackBarMaskGreenMin";
            trackBarMaskGreenMin.Size = new Size(126, 36);
            trackBarMaskGreenMin.TabIndex = 18;
            trackBarMaskGreenMin.ValueChanged += trackBarMaskGreenMin_ValueChanged;
            // 
            // labelMaskRedMinValue
            // 
            labelMaskRedMinValue.Anchor = AnchorStyles.None;
            labelMaskRedMinValue.AutoSize = true;
            labelMaskRedMinValue.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelMaskRedMinValue.Location = new Point(182, 28);
            labelMaskRedMinValue.Name = "labelMaskRedMinValue";
            labelMaskRedMinValue.Size = new Size(17, 20);
            labelMaskRedMinValue.TabIndex = 24;
            labelMaskRedMinValue.Text = "0";
            // 
            // labelMaskGreenMinValue
            // 
            labelMaskGreenMinValue.Anchor = AnchorStyles.None;
            labelMaskGreenMinValue.AutoSize = true;
            labelMaskGreenMinValue.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelMaskGreenMinValue.Location = new Point(348, 28);
            labelMaskGreenMinValue.Name = "labelMaskGreenMinValue";
            labelMaskGreenMinValue.Size = new Size(17, 20);
            labelMaskGreenMinValue.TabIndex = 25;
            labelMaskGreenMinValue.Text = "0";
            // 
            // labelMaskBlueMinValue
            // 
            labelMaskBlueMinValue.Anchor = AnchorStyles.None;
            labelMaskBlueMinValue.AutoSize = true;
            labelMaskBlueMinValue.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelMaskBlueMinValue.Location = new Point(515, 28);
            labelMaskBlueMinValue.Name = "labelMaskBlueMinValue";
            labelMaskBlueMinValue.Size = new Size(17, 20);
            labelMaskBlueMinValue.TabIndex = 26;
            labelMaskBlueMinValue.Text = "0";
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
            tableLayoutPanelCueballMask.Controls.Add(label15, 5, 0);
            tableLayoutPanelCueballMask.Controls.Add(label16, 3, 0);
            tableLayoutPanelCueballMask.Controls.Add(label17, 1, 0);
            tableLayoutPanelCueballMask.Controls.Add(trackBar8, 5, 2);
            tableLayoutPanelCueballMask.Controls.Add(trackBar9, 3, 2);
            tableLayoutPanelCueballMask.Controls.Add(trackBar10, 1, 2);
            tableLayoutPanelCueballMask.Controls.Add(trackBar11, 5, 1);
            tableLayoutPanelCueballMask.Controls.Add(trackBar12, 3, 1);
            tableLayoutPanelCueballMask.Controls.Add(trackBar13, 1, 1);
            tableLayoutPanelCueballMask.Controls.Add(label18, 0, 2);
            tableLayoutPanelCueballMask.Controls.Add(label19, 0, 1);
            tableLayoutPanelCueballMask.Controls.Add(label20, 2, 2);
            tableLayoutPanelCueballMask.Controls.Add(label21, 2, 1);
            tableLayoutPanelCueballMask.Controls.Add(label22, 4, 2);
            tableLayoutPanelCueballMask.Controls.Add(label23, 4, 1);
            tableLayoutPanelCueballMask.Controls.Add(label24, 6, 2);
            tableLayoutPanelCueballMask.Controls.Add(label25, 6, 1);
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
            // label15
            // 
            label15.Anchor = AnchorStyles.None;
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label15.Location = new Point(402, 0);
            label15.Name = "label15";
            label15.Size = new Size(76, 20);
            label15.TabIndex = 36;
            label15.Text = "Mask Blue";
            // 
            // label16
            // 
            label16.Anchor = AnchorStyles.None;
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label16.Location = new Point(231, 0);
            label16.Name = "label16";
            label16.Size = new Size(86, 20);
            label16.TabIndex = 36;
            label16.Text = "Mask Green";
            // 
            // label17
            // 
            label17.Anchor = AnchorStyles.None;
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label17.Location = new Point(71, 0);
            label17.Name = "label17";
            label17.Size = new Size(73, 20);
            label17.TabIndex = 36;
            label17.Text = "Mask Red";
            // 
            // trackBar8
            // 
            trackBar8.Dock = DockStyle.Fill;
            trackBar8.Location = new Point(377, 56);
            trackBar8.Margin = new Padding(0);
            trackBar8.Maximum = 255;
            trackBar8.Name = "trackBar8";
            trackBar8.Size = new Size(126, 37);
            trackBar8.TabIndex = 36;
            // 
            // trackBar9
            // 
            trackBar9.Dock = DockStyle.Fill;
            trackBar9.Location = new Point(211, 56);
            trackBar9.Margin = new Padding(0);
            trackBar9.Maximum = 255;
            trackBar9.Name = "trackBar9";
            trackBar9.Size = new Size(126, 37);
            trackBar9.TabIndex = 36;
            // 
            // trackBar10
            // 
            trackBar10.Dock = DockStyle.Fill;
            trackBar10.Location = new Point(45, 56);
            trackBar10.Margin = new Padding(0);
            trackBar10.Maximum = 255;
            trackBar10.Name = "trackBar10";
            trackBar10.Size = new Size(126, 37);
            trackBar10.TabIndex = 36;
            // 
            // trackBar11
            // 
            trackBar11.Dock = DockStyle.Fill;
            trackBar11.Location = new Point(377, 20);
            trackBar11.Margin = new Padding(0);
            trackBar11.Maximum = 255;
            trackBar11.Name = "trackBar11";
            trackBar11.Size = new Size(126, 36);
            trackBar11.TabIndex = 44;
            // 
            // trackBar12
            // 
            trackBar12.Dock = DockStyle.Fill;
            trackBar12.Location = new Point(211, 20);
            trackBar12.Margin = new Padding(0);
            trackBar12.Maximum = 255;
            trackBar12.Name = "trackBar12";
            trackBar12.Size = new Size(126, 36);
            trackBar12.TabIndex = 43;
            // 
            // trackBar13
            // 
            trackBar13.Dock = DockStyle.Fill;
            trackBar13.Location = new Point(45, 20);
            trackBar13.Margin = new Padding(0);
            trackBar13.Maximum = 255;
            trackBar13.Name = "trackBar13";
            trackBar13.Size = new Size(126, 36);
            trackBar13.TabIndex = 35;
            // 
            // label18
            // 
            label18.Anchor = AnchorStyles.None;
            label18.AutoSize = true;
            label18.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label18.Location = new Point(4, 64);
            label18.Name = "label18";
            label18.Size = new Size(37, 20);
            label18.TabIndex = 35;
            label18.Text = "Max";
            // 
            // label19
            // 
            label19.Anchor = AnchorStyles.None;
            label19.AutoSize = true;
            label19.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label19.Location = new Point(5, 28);
            label19.Name = "label19";
            label19.Size = new Size(34, 20);
            label19.TabIndex = 35;
            label19.Text = "Min";
            // 
            // label20
            // 
            label20.Anchor = AnchorStyles.None;
            label20.AutoSize = true;
            label20.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label20.Location = new Point(182, 64);
            label20.Name = "label20";
            label20.Size = new Size(17, 20);
            label20.TabIndex = 36;
            label20.Text = "0";
            // 
            // label21
            // 
            label21.Anchor = AnchorStyles.None;
            label21.AutoSize = true;
            label21.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label21.Location = new Point(182, 28);
            label21.Name = "label21";
            label21.Size = new Size(17, 20);
            label21.TabIndex = 35;
            label21.Text = "0";
            // 
            // label22
            // 
            label22.Anchor = AnchorStyles.None;
            label22.AutoSize = true;
            label22.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label22.Location = new Point(348, 64);
            label22.Name = "label22";
            label22.Size = new Size(17, 20);
            label22.TabIndex = 38;
            label22.Text = "0";
            // 
            // label23
            // 
            label23.Anchor = AnchorStyles.None;
            label23.AutoSize = true;
            label23.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label23.Location = new Point(348, 28);
            label23.Name = "label23";
            label23.Size = new Size(17, 20);
            label23.TabIndex = 37;
            label23.Text = "0";
            // 
            // label24
            // 
            label24.Anchor = AnchorStyles.None;
            label24.AutoSize = true;
            label24.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label24.Location = new Point(514, 64);
            label24.Name = "label24";
            label24.Size = new Size(17, 20);
            label24.TabIndex = 40;
            label24.Text = "0";
            // 
            // label25
            // 
            label25.Anchor = AnchorStyles.None;
            label25.AutoSize = true;
            label25.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label25.Location = new Point(514, 28);
            label25.Name = "label25";
            label25.Size = new Size(17, 20);
            label25.TabIndex = 39;
            label25.Text = "0";
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
            tableLayoutPanelClothMask.Controls.Add(label12, 5, 0);
            tableLayoutPanelClothMask.Controls.Add(trackBarMaskRedMax, 1, 2);
            tableLayoutPanelClothMask.Controls.Add(labelMaskGreenMaxValue, 4, 2);
            tableLayoutPanelClothMask.Controls.Add(labelMaskBlueMaxValue, 6, 2);
            tableLayoutPanelClothMask.Controls.Add(label11, 3, 0);
            tableLayoutPanelClothMask.Controls.Add(label10, 1, 0);
            tableLayoutPanelClothMask.Controls.Add(labelMaskBlueMinValue, 6, 1);
            tableLayoutPanelClothMask.Controls.Add(trackBarMaskRedMin, 1, 1);
            tableLayoutPanelClothMask.Controls.Add(trackBarMaskBlueMin, 5, 1);
            tableLayoutPanelClothMask.Controls.Add(labelMaskGreenMinValue, 4, 1);
            tableLayoutPanelClothMask.Controls.Add(labelMaskRedMinValue, 2, 1);
            tableLayoutPanelClothMask.Controls.Add(labelMaskRedMaxValue, 2, 2);
            tableLayoutPanelClothMask.Controls.Add(trackBarMaskBlueMax, 5, 2);
            tableLayoutPanelClothMask.Controls.Add(label3, 0, 2);
            tableLayoutPanelClothMask.Controls.Add(label2, 0, 1);
            tableLayoutPanelClothMask.Controls.Add(trackBarMaskGreenMax, 3, 2);
            tableLayoutPanelClothMask.Controls.Add(trackBarMaskGreenMin, 3, 1);
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
            // label12
            // 
            label12.Anchor = AnchorStyles.None;
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label12.Location = new Point(402, 0);
            label12.Name = "label12";
            label12.Size = new Size(76, 20);
            label12.TabIndex = 36;
            label12.Text = "Mask Blue";
            // 
            // trackBarMaskRedMax
            // 
            trackBarMaskRedMax.Dock = DockStyle.Fill;
            trackBarMaskRedMax.Location = new Point(45, 56);
            trackBarMaskRedMax.Margin = new Padding(0);
            trackBarMaskRedMax.Maximum = 255;
            trackBarMaskRedMax.Name = "trackBarMaskRedMax";
            trackBarMaskRedMax.Size = new Size(126, 37);
            trackBarMaskRedMax.TabIndex = 27;
            trackBarMaskRedMax.ValueChanged += trackBarMaskRedMax_ValueChanged;
            // 
            // labelMaskGreenMaxValue
            // 
            labelMaskGreenMaxValue.Anchor = AnchorStyles.None;
            labelMaskGreenMaxValue.AutoSize = true;
            labelMaskGreenMaxValue.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelMaskGreenMaxValue.Location = new Point(348, 64);
            labelMaskGreenMaxValue.Name = "labelMaskGreenMaxValue";
            labelMaskGreenMaxValue.Size = new Size(17, 20);
            labelMaskGreenMaxValue.TabIndex = 32;
            labelMaskGreenMaxValue.Text = "0";
            // 
            // labelMaskBlueMaxValue
            // 
            labelMaskBlueMaxValue.Anchor = AnchorStyles.None;
            labelMaskBlueMaxValue.AutoSize = true;
            labelMaskBlueMaxValue.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelMaskBlueMaxValue.Location = new Point(515, 64);
            labelMaskBlueMaxValue.Name = "labelMaskBlueMaxValue";
            labelMaskBlueMaxValue.Size = new Size(17, 20);
            labelMaskBlueMaxValue.TabIndex = 31;
            labelMaskBlueMaxValue.Text = "0";
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.None;
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label11.Location = new Point(231, 0);
            label11.Name = "label11";
            label11.Size = new Size(86, 20);
            label11.TabIndex = 36;
            label11.Text = "Mask Green";
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.None;
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label10.Location = new Point(71, 0);
            label10.Name = "label10";
            label10.Size = new Size(73, 20);
            label10.TabIndex = 36;
            label10.Text = "Mask Red";
            // 
            // trackBarMaskRedMin
            // 
            trackBarMaskRedMin.Dock = DockStyle.Fill;
            trackBarMaskRedMin.Location = new Point(45, 20);
            trackBarMaskRedMin.Margin = new Padding(0);
            trackBarMaskRedMin.Maximum = 255;
            trackBarMaskRedMin.Name = "trackBarMaskRedMin";
            trackBarMaskRedMin.Size = new Size(126, 36);
            trackBarMaskRedMin.TabIndex = 19;
            trackBarMaskRedMin.ValueChanged += trackBarMaskRedMin_ValueChanged;
            // 
            // labelMaskRedMaxValue
            // 
            labelMaskRedMaxValue.Anchor = AnchorStyles.None;
            labelMaskRedMaxValue.AutoSize = true;
            labelMaskRedMaxValue.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelMaskRedMaxValue.Location = new Point(182, 64);
            labelMaskRedMaxValue.Name = "labelMaskRedMaxValue";
            labelMaskRedMaxValue.Size = new Size(17, 20);
            labelMaskRedMaxValue.TabIndex = 30;
            labelMaskRedMaxValue.Text = "0";
            // 
            // trackBarMaskBlueMax
            // 
            trackBarMaskBlueMax.Dock = DockStyle.Fill;
            trackBarMaskBlueMax.Location = new Point(377, 56);
            trackBarMaskBlueMax.Margin = new Padding(0);
            trackBarMaskBlueMax.Maximum = 255;
            trackBarMaskBlueMax.Name = "trackBarMaskBlueMax";
            trackBarMaskBlueMax.Size = new Size(126, 37);
            trackBarMaskBlueMax.TabIndex = 28;
            trackBarMaskBlueMax.ValueChanged += trackBarMaskBlueMax_ValueChanged;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(4, 64);
            label3.Name = "label3";
            label3.Size = new Size(37, 20);
            label3.TabIndex = 35;
            label3.Text = "Max";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(5, 28);
            label2.Name = "label2";
            label2.Size = new Size(34, 20);
            label2.TabIndex = 35;
            label2.Text = "Min";
            // 
            // trackBarMaskGreenMax
            // 
            trackBarMaskGreenMax.Dock = DockStyle.Fill;
            trackBarMaskGreenMax.Location = new Point(211, 56);
            trackBarMaskGreenMax.Margin = new Padding(0);
            trackBarMaskGreenMax.Maximum = 255;
            trackBarMaskGreenMax.Name = "trackBarMaskGreenMax";
            trackBarMaskGreenMax.Size = new Size(126, 37);
            trackBarMaskGreenMax.TabIndex = 29;
            trackBarMaskGreenMax.ValueChanged += trackBarMaskGreenMax_ValueChanged;
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
            ((System.ComponentModel.ISupportInitialize)trackBarMaskBlueMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMaskGreenMin).EndInit();
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelCueballMask.ResumeLayout(false);
            tableLayoutPanelCueballMask.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar8).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar9).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar10).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar11).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar12).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar13).EndInit();
            tableLayoutPanelCheckboxes.ResumeLayout(false);
            tableLayoutPanelCheckboxes.PerformLayout();
            tableLayoutPanelClothMask.ResumeLayout(false);
            tableLayoutPanelClothMask.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarMaskRedMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMaskRedMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMaskBlueMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMaskGreenMax).EndInit();
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
        private TrackBar trackBarMaskBlueMin;
        private TrackBar trackBarMaskGreenMin;
        private Label labelMaskRedMinValue;
        private Label labelMaskGreenMinValue;
        private Label labelMaskBlueMinValue;
        private TrackBar trackBarMaskGreenMax;
        private TrackBar trackBarMaskBlueMax;
        private TrackBar trackBarMaskRedMax;
        private Label labelMaskGreenMaxValue;
        private Label labelMaskBlueMaxValue;
        private Label labelMaskRedMaxValue;
        private TableLayoutPanel tableLayoutPanelMain;
        private CheckBox checkBoxEnableTableBoundary;
        private CheckBox checkBoxEnableSharpen;
        private CheckBox checkBoxEnableBlurr;
        private TableLayoutPanel tableLayoutPanelCheckboxes;
        private TableLayoutPanel tableLayoutPanelClothMask;
        private Label label3;
        private Label label2;
        private Label label12;
        private Label label11;
        private Label label10;
        private TableLayoutPanel tableLayoutPanelCueballMask;
        private Label label15;
        private Label label16;
        private Label label17;
        private TrackBar trackBar8;
        private TrackBar trackBar9;
        private TrackBar trackBar10;
        private TrackBar trackBar11;
        private TrackBar trackBar12;
        private TrackBar trackBar13;
        private Label label18;
        private Label label19;
        private Label label20;
        private Label label21;
        private Label label22;
        private Label label23;
        private Label label24;
        private Label label25;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label13;
        private Label label14;
        private TrackBar trackBarMaskRedMin;
    }
}