
namespace billiard_laser
{
    partial class LaserDetectionDebugForm
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
            scoredCandidatesPicBox = new PictureBox();
            allCandidatesPicBox = new PictureBox();
            labelWorkingImage = new Label();
            labelScoredCandidates = new Label();
            labelFilteredCandidates = new Label();
            labelAllCandidates = new Label();
            tableLayoutPanelMain = new TableLayoutPanel();
            tableLayoutPanelCueballMask = new TableLayoutPanel();
            labelLaserMaskArea = new Label();
            labelLaserMaskBlue = new Label();
            labelLaserMaskGreen = new Label();
            labelLaserMaskRed = new Label();
            trackBarLaserMaskBlueMax = new TrackBar();
            trackBarLaserMaskGreenMax = new TrackBar();
            trackBarLaserMaskRedMax = new TrackBar();
            trackBarLaserMaskBlueMin = new TrackBar();
            trackBarLaserMaskGreenMin = new TrackBar();
            trackBarLaserMaskRedMin = new TrackBar();
            labelLaserMaskMax = new Label();
            labelLaserMaskMin = new Label();
            labelLaserMaskRedMax = new Label();
            labelLaserMaskRedMin = new Label();
            labelLaserMaskGreenMax = new Label();
            labelLaserMaskGreenMin = new Label();
            labelLaserMaskBlueMax = new Label();
            labelLaserMaskBlueMin = new Label();
            tableLayoutPanelCheckboxes = new TableLayoutPanel();
            checkBoxEnableSharpen = new CheckBox();
            checkBoxEnableTableBoundary = new CheckBox();
            checkBoxEnableBlurr = new CheckBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            label13 = new Label();
            label14 = new Label();
            tableLayoutPanelDebugImages = new TableLayoutPanel();
            filteredCandidatesPicBox = new PictureBox();
            label2 = new Label();
            workingImagePicBox = new PictureBox();
            laserMaskPicBox = new PictureBox();
            labelLaserFound = new Label();
            laserFoundPicBox = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)scoredCandidatesPicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)allCandidatesPicBox).BeginInit();
            tableLayoutPanelMain.SuspendLayout();
            tableLayoutPanelCueballMask.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarLaserMaskBlueMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarLaserMaskGreenMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarLaserMaskRedMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarLaserMaskBlueMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarLaserMaskGreenMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarLaserMaskRedMin).BeginInit();
            tableLayoutPanelCheckboxes.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanelDebugImages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)filteredCandidatesPicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)workingImagePicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)laserMaskPicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)laserFoundPicBox).BeginInit();
            SuspendLayout();
            // 
            // scoredCandidatesPicBox
            // 
            scoredCandidatesPicBox.Dock = DockStyle.Fill;
            scoredCandidatesPicBox.Location = new Point(415, 286);
            scoredCandidatesPicBox.Margin = new Padding(0);
            scoredCandidatesPicBox.Name = "scoredCandidatesPicBox";
            scoredCandidatesPicBox.Size = new Size(415, 227);
            scoredCandidatesPicBox.SizeMode = PictureBoxSizeMode.Zoom;
            scoredCandidatesPicBox.TabIndex = 1;
            scoredCandidatesPicBox.TabStop = false;
            // 
            // allCandidatesPicBox
            // 
            allCandidatesPicBox.Dock = DockStyle.Fill;
            allCandidatesPicBox.Location = new Point(830, 30);
            allCandidatesPicBox.Margin = new Padding(0);
            allCandidatesPicBox.Name = "allCandidatesPicBox";
            allCandidatesPicBox.Size = new Size(417, 226);
            allCandidatesPicBox.SizeMode = PictureBoxSizeMode.Zoom;
            allCandidatesPicBox.TabIndex = 4;
            allCandidatesPicBox.TabStop = false;
            // 
            // labelWorkingImage
            // 
            labelWorkingImage.Anchor = AnchorStyles.None;
            labelWorkingImage.AutoSize = true;
            labelWorkingImage.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelWorkingImage.Location = new Point(152, 5);
            labelWorkingImage.Name = "labelWorkingImage";
            labelWorkingImage.Size = new Size(110, 20);
            labelWorkingImage.TabIndex = 7;
            labelWorkingImage.Text = "Working Image";
            // 
            // labelScoredCandidates
            // 
            labelScoredCandidates.Anchor = AnchorStyles.None;
            labelScoredCandidates.AutoSize = true;
            labelScoredCandidates.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelScoredCandidates.Location = new Point(537, 261);
            labelScoredCandidates.Name = "labelScoredCandidates";
            labelScoredCandidates.Size = new Size(171, 20);
            labelScoredCandidates.TabIndex = 8;
            labelScoredCandidates.Text = "Scored Laser Candidates";
            // 
            // labelFilteredCandidates
            // 
            labelFilteredCandidates.Anchor = AnchorStyles.None;
            labelFilteredCandidates.AutoSize = true;
            labelFilteredCandidates.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelFilteredCandidates.Location = new Point(120, 261);
            labelFilteredCandidates.Name = "labelFilteredCandidates";
            labelFilteredCandidates.Size = new Size(175, 20);
            labelFilteredCandidates.TabIndex = 10;
            labelFilteredCandidates.Text = "Filtered Laser Candidates";
            // 
            // labelAllCandidates
            // 
            labelAllCandidates.Anchor = AnchorStyles.None;
            labelAllCandidates.AutoSize = true;
            labelAllCandidates.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelAllCandidates.Location = new Point(967, 5);
            labelAllCandidates.Name = "labelAllCandidates";
            labelAllCandidates.Size = new Size(143, 20);
            labelAllCandidates.TabIndex = 11;
            labelAllCandidates.Text = "All Laser Candidates";
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.BackColor = SystemColors.Info;
            tableLayoutPanelMain.ColumnCount = 2;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelCueballMask, 0, 0);
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelCheckboxes, 0, 0);
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
            tableLayoutPanelCueballMask.Controls.Add(labelLaserMaskArea, 0, 0);
            tableLayoutPanelCueballMask.Controls.Add(labelLaserMaskBlue, 5, 0);
            tableLayoutPanelCueballMask.Controls.Add(labelLaserMaskGreen, 3, 0);
            tableLayoutPanelCueballMask.Controls.Add(labelLaserMaskRed, 1, 0);
            tableLayoutPanelCueballMask.Controls.Add(trackBarLaserMaskBlueMax, 5, 2);
            tableLayoutPanelCueballMask.Controls.Add(trackBarLaserMaskGreenMax, 3, 2);
            tableLayoutPanelCueballMask.Controls.Add(trackBarLaserMaskRedMax, 1, 2);
            tableLayoutPanelCueballMask.Controls.Add(trackBarLaserMaskBlueMin, 5, 1);
            tableLayoutPanelCueballMask.Controls.Add(trackBarLaserMaskGreenMin, 3, 1);
            tableLayoutPanelCueballMask.Controls.Add(trackBarLaserMaskRedMin, 1, 1);
            tableLayoutPanelCueballMask.Controls.Add(labelLaserMaskMax, 0, 2);
            tableLayoutPanelCueballMask.Controls.Add(labelLaserMaskMin, 0, 1);
            tableLayoutPanelCueballMask.Controls.Add(labelLaserMaskRedMax, 2, 2);
            tableLayoutPanelCueballMask.Controls.Add(labelLaserMaskRedMin, 2, 1);
            tableLayoutPanelCueballMask.Controls.Add(labelLaserMaskGreenMax, 4, 2);
            tableLayoutPanelCueballMask.Controls.Add(labelLaserMaskGreenMin, 4, 1);
            tableLayoutPanelCueballMask.Controls.Add(labelLaserMaskBlueMax, 6, 2);
            tableLayoutPanelCueballMask.Controls.Add(labelLaserMaskBlueMin, 6, 1);
            tableLayoutPanelCueballMask.Dock = DockStyle.Fill;
            tableLayoutPanelCueballMask.Location = new Point(160, 0);
            tableLayoutPanelCueballMask.Margin = new Padding(0);
            tableLayoutPanelCueballMask.Name = "tableLayoutPanelCueballMask";
            tableLayoutPanelCueballMask.RowCount = 3;
            tableLayoutPanelCueballMask.RowStyles.Add(new RowStyle());
            tableLayoutPanelCueballMask.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelCueballMask.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelCueballMask.Size = new Size(1087, 93);
            tableLayoutPanelCueballMask.TabIndex = 42;
            // 
            // labelLaserMaskArea
            // 
            labelLaserMaskArea.Anchor = AnchorStyles.None;
            labelLaserMaskArea.AutoSize = true;
            labelLaserMaskArea.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelLaserMaskArea.Location = new Point(4, 2);
            labelLaserMaskArea.Name = "labelLaserMaskArea";
            labelLaserMaskArea.Size = new Size(36, 15);
            labelLaserMaskArea.TabIndex = 40;
            labelLaserMaskArea.Text = "Laser";
            // 
            // labelLaserMaskBlue
            // 
            labelLaserMaskBlue.Anchor = AnchorStyles.None;
            labelLaserMaskBlue.AutoSize = true;
            labelLaserMaskBlue.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelLaserMaskBlue.Location = new Point(854, 0);
            labelLaserMaskBlue.Name = "labelLaserMaskBlue";
            labelLaserMaskBlue.Size = new Size(76, 20);
            labelLaserMaskBlue.TabIndex = 36;
            labelLaserMaskBlue.Text = "Mask Blue";
            // 
            // labelLaserMaskGreen
            // 
            labelLaserMaskGreen.Anchor = AnchorStyles.None;
            labelLaserMaskGreen.AutoSize = true;
            labelLaserMaskGreen.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelLaserMaskGreen.Location = new Point(502, 0);
            labelLaserMaskGreen.Name = "labelLaserMaskGreen";
            labelLaserMaskGreen.Size = new Size(86, 20);
            labelLaserMaskGreen.TabIndex = 36;
            labelLaserMaskGreen.Text = "Mask Green";
            // 
            // labelLaserMaskRed
            // 
            labelLaserMaskRed.Anchor = AnchorStyles.None;
            labelLaserMaskRed.AutoSize = true;
            labelLaserMaskRed.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelLaserMaskRed.Location = new Point(162, 0);
            labelLaserMaskRed.Name = "labelLaserMaskRed";
            labelLaserMaskRed.Size = new Size(73, 20);
            labelLaserMaskRed.TabIndex = 36;
            labelLaserMaskRed.Text = "Mask Red";
            // 
            // trackBarLaserMaskBlueMax
            // 
            trackBarLaserMaskBlueMax.Dock = DockStyle.Fill;
            trackBarLaserMaskBlueMax.Location = new Point(739, 56);
            trackBarLaserMaskBlueMax.Margin = new Padding(0);
            trackBarLaserMaskBlueMax.Maximum = 255;
            trackBarLaserMaskBlueMax.Name = "trackBarLaserMaskBlueMax";
            trackBarLaserMaskBlueMax.Size = new Size(307, 37);
            trackBarLaserMaskBlueMax.TabIndex = 36;
            trackBarLaserMaskBlueMax.ValueChanged += trackBarLaserMaskBlueMax_ValueChanged;
            // 
            // trackBarLaserMaskGreenMax
            // 
            trackBarLaserMaskGreenMax.Dock = DockStyle.Fill;
            trackBarLaserMaskGreenMax.Location = new Point(392, 56);
            trackBarLaserMaskGreenMax.Margin = new Padding(0);
            trackBarLaserMaskGreenMax.Maximum = 255;
            trackBarLaserMaskGreenMax.Name = "trackBarLaserMaskGreenMax";
            trackBarLaserMaskGreenMax.Size = new Size(307, 37);
            trackBarLaserMaskGreenMax.TabIndex = 36;
            trackBarLaserMaskGreenMax.ValueChanged += trackBarLaserMaskGreenMax_ValueChanged;
            // 
            // trackBarLaserMaskRedMax
            // 
            trackBarLaserMaskRedMax.Dock = DockStyle.Fill;
            trackBarLaserMaskRedMax.Location = new Point(45, 56);
            trackBarLaserMaskRedMax.Margin = new Padding(0);
            trackBarLaserMaskRedMax.Maximum = 255;
            trackBarLaserMaskRedMax.Name = "trackBarLaserMaskRedMax";
            trackBarLaserMaskRedMax.Size = new Size(307, 37);
            trackBarLaserMaskRedMax.TabIndex = 36;
            trackBarLaserMaskRedMax.ValueChanged += trackBarLaserMaskRedMax_ValueChanged;
            // 
            // trackBarLaserMaskBlueMin
            // 
            trackBarLaserMaskBlueMin.Dock = DockStyle.Fill;
            trackBarLaserMaskBlueMin.Location = new Point(739, 20);
            trackBarLaserMaskBlueMin.Margin = new Padding(0);
            trackBarLaserMaskBlueMin.Maximum = 255;
            trackBarLaserMaskBlueMin.Name = "trackBarLaserMaskBlueMin";
            trackBarLaserMaskBlueMin.Size = new Size(307, 36);
            trackBarLaserMaskBlueMin.TabIndex = 44;
            trackBarLaserMaskBlueMin.ValueChanged += trackBarLaserMaskBlueMin_ValueChanged;
            // 
            // trackBarLaserMaskGreenMin
            // 
            trackBarLaserMaskGreenMin.Dock = DockStyle.Fill;
            trackBarLaserMaskGreenMin.Location = new Point(392, 20);
            trackBarLaserMaskGreenMin.Margin = new Padding(0);
            trackBarLaserMaskGreenMin.Maximum = 255;
            trackBarLaserMaskGreenMin.Name = "trackBarLaserMaskGreenMin";
            trackBarLaserMaskGreenMin.Size = new Size(307, 36);
            trackBarLaserMaskGreenMin.TabIndex = 43;
            trackBarLaserMaskGreenMin.Value = 3;
            trackBarLaserMaskGreenMin.ValueChanged += trackBarLaserMaskGreenMin_ValueChanged;
            // 
            // trackBarLaserMaskRedMin
            // 
            trackBarLaserMaskRedMin.Dock = DockStyle.Fill;
            trackBarLaserMaskRedMin.Location = new Point(45, 20);
            trackBarLaserMaskRedMin.Margin = new Padding(0);
            trackBarLaserMaskRedMin.Maximum = 255;
            trackBarLaserMaskRedMin.Name = "trackBarLaserMaskRedMin";
            trackBarLaserMaskRedMin.Size = new Size(307, 36);
            trackBarLaserMaskRedMin.TabIndex = 35;
            trackBarLaserMaskRedMin.ValueChanged += trackBarLaserMaskRedMin_ValueChanged;
            // 
            // labelLaserMaskMax
            // 
            labelLaserMaskMax.Anchor = AnchorStyles.None;
            labelLaserMaskMax.AutoSize = true;
            labelLaserMaskMax.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelLaserMaskMax.Location = new Point(4, 64);
            labelLaserMaskMax.Name = "labelLaserMaskMax";
            labelLaserMaskMax.Size = new Size(37, 20);
            labelLaserMaskMax.TabIndex = 35;
            labelLaserMaskMax.Text = "Max";
            // 
            // labelLaserMaskMin
            // 
            labelLaserMaskMin.Anchor = AnchorStyles.None;
            labelLaserMaskMin.AutoSize = true;
            labelLaserMaskMin.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelLaserMaskMin.Location = new Point(5, 28);
            labelLaserMaskMin.Name = "labelLaserMaskMin";
            labelLaserMaskMin.Size = new Size(34, 20);
            labelLaserMaskMin.TabIndex = 35;
            labelLaserMaskMin.Text = "Min";
            // 
            // labelLaserMaskRedMax
            // 
            labelLaserMaskRedMax.Anchor = AnchorStyles.None;
            labelLaserMaskRedMax.AutoSize = true;
            labelLaserMaskRedMax.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelLaserMaskRedMax.Location = new Point(363, 64);
            labelLaserMaskRedMax.Name = "labelLaserMaskRedMax";
            labelLaserMaskRedMax.Size = new Size(17, 20);
            labelLaserMaskRedMax.TabIndex = 36;
            labelLaserMaskRedMax.Text = "0";
            // 
            // labelLaserMaskRedMin
            // 
            labelLaserMaskRedMin.Anchor = AnchorStyles.None;
            labelLaserMaskRedMin.AutoSize = true;
            labelLaserMaskRedMin.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelLaserMaskRedMin.Location = new Point(363, 28);
            labelLaserMaskRedMin.Name = "labelLaserMaskRedMin";
            labelLaserMaskRedMin.Size = new Size(17, 20);
            labelLaserMaskRedMin.TabIndex = 35;
            labelLaserMaskRedMin.Text = "0";
            // 
            // labelLaserMaskGreenMax
            // 
            labelLaserMaskGreenMax.Anchor = AnchorStyles.None;
            labelLaserMaskGreenMax.AutoSize = true;
            labelLaserMaskGreenMax.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelLaserMaskGreenMax.Location = new Point(710, 64);
            labelLaserMaskGreenMax.Name = "labelLaserMaskGreenMax";
            labelLaserMaskGreenMax.Size = new Size(17, 20);
            labelLaserMaskGreenMax.TabIndex = 38;
            labelLaserMaskGreenMax.Text = "0";
            // 
            // labelLaserMaskGreenMin
            // 
            labelLaserMaskGreenMin.Anchor = AnchorStyles.None;
            labelLaserMaskGreenMin.AutoSize = true;
            labelLaserMaskGreenMin.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelLaserMaskGreenMin.Location = new Point(710, 28);
            labelLaserMaskGreenMin.Name = "labelLaserMaskGreenMin";
            labelLaserMaskGreenMin.Size = new Size(17, 20);
            labelLaserMaskGreenMin.TabIndex = 37;
            labelLaserMaskGreenMin.Text = "0";
            // 
            // labelLaserMaskBlueMax
            // 
            labelLaserMaskBlueMax.Anchor = AnchorStyles.None;
            labelLaserMaskBlueMax.AutoSize = true;
            labelLaserMaskBlueMax.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelLaserMaskBlueMax.Location = new Point(1058, 64);
            labelLaserMaskBlueMax.Name = "labelLaserMaskBlueMax";
            labelLaserMaskBlueMax.Size = new Size(17, 20);
            labelLaserMaskBlueMax.TabIndex = 40;
            labelLaserMaskBlueMax.Text = "0";
            // 
            // labelLaserMaskBlueMin
            // 
            labelLaserMaskBlueMin.Anchor = AnchorStyles.None;
            labelLaserMaskBlueMin.AutoSize = true;
            labelLaserMaskBlueMin.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelLaserMaskBlueMin.Location = new Point(1058, 28);
            labelLaserMaskBlueMin.Name = "labelLaserMaskBlueMin";
            labelLaserMaskBlueMin.Size = new Size(17, 20);
            labelLaserMaskBlueMin.TabIndex = 39;
            labelLaserMaskBlueMin.Text = "0";
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
            checkBoxEnableSharpen.CheckedChanged += checkBoxEnableSharpen_CheckedChanged;
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
            checkBoxEnableTableBoundary.CheckedChanged += checkBoxEnableTableBoundary_CheckedChanged;
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
            checkBoxEnableBlurr.CheckedChanged += checkBoxEnableBlurr_CheckedChanged;
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
            // tableLayoutPanelDebugImages
            // 
            tableLayoutPanelDebugImages.BackColor = Color.MistyRose;
            tableLayoutPanelDebugImages.ColumnCount = 3;
            tableLayoutPanelDebugImages.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanelDebugImages.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanelDebugImages.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanelDebugImages.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanelDebugImages.Controls.Add(filteredCandidatesPicBox, 0, 3);
            tableLayoutPanelDebugImages.Controls.Add(labelFilteredCandidates, 0, 2);
            tableLayoutPanelDebugImages.Controls.Add(labelWorkingImage, 0, 0);
            tableLayoutPanelDebugImages.Controls.Add(label2, 1, 0);
            tableLayoutPanelDebugImages.Controls.Add(workingImagePicBox, 0, 1);
            tableLayoutPanelDebugImages.Controls.Add(laserMaskPicBox, 1, 1);
            tableLayoutPanelDebugImages.Controls.Add(labelAllCandidates, 2, 0);
            tableLayoutPanelDebugImages.Controls.Add(allCandidatesPicBox, 2, 1);
            tableLayoutPanelDebugImages.Controls.Add(labelScoredCandidates, 1, 2);
            tableLayoutPanelDebugImages.Controls.Add(scoredCandidatesPicBox, 1, 3);
            tableLayoutPanelDebugImages.Controls.Add(labelLaserFound, 2, 2);
            tableLayoutPanelDebugImages.Controls.Add(laserFoundPicBox, 2, 3);
            tableLayoutPanelDebugImages.Dock = DockStyle.Fill;
            tableLayoutPanelDebugImages.Location = new Point(0, 0);
            tableLayoutPanelDebugImages.Name = "tableLayoutPanelDebugImages";
            tableLayoutPanelDebugImages.RowCount = 4;
            tableLayoutPanelDebugImages.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanelDebugImages.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelDebugImages.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanelDebugImages.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelDebugImages.Size = new Size(1247, 513);
            tableLayoutPanelDebugImages.TabIndex = 30;
            // 
            // filteredCandidatesPicBox
            // 
            filteredCandidatesPicBox.Dock = DockStyle.Fill;
            filteredCandidatesPicBox.Location = new Point(0, 286);
            filteredCandidatesPicBox.Margin = new Padding(0);
            filteredCandidatesPicBox.Name = "filteredCandidatesPicBox";
            filteredCandidatesPicBox.Size = new Size(415, 227);
            filteredCandidatesPicBox.SizeMode = PictureBoxSizeMode.Zoom;
            filteredCandidatesPicBox.TabIndex = 39;
            filteredCandidatesPicBox.TabStop = false;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(582, 5);
            label2.Name = "label2";
            label2.Size = new Size(81, 20);
            label2.TabIndex = 32;
            label2.Text = "Laser Mask";
            // 
            // workingImagePicBox
            // 
            workingImagePicBox.Dock = DockStyle.Fill;
            workingImagePicBox.Location = new Point(0, 30);
            workingImagePicBox.Margin = new Padding(0);
            workingImagePicBox.Name = "workingImagePicBox";
            workingImagePicBox.Size = new Size(415, 226);
            workingImagePicBox.SizeMode = PictureBoxSizeMode.Zoom;
            workingImagePicBox.TabIndex = 36;
            workingImagePicBox.TabStop = false;
            // 
            // laserMaskPicBox
            // 
            laserMaskPicBox.Dock = DockStyle.Fill;
            laserMaskPicBox.Location = new Point(415, 30);
            laserMaskPicBox.Margin = new Padding(0);
            laserMaskPicBox.Name = "laserMaskPicBox";
            laserMaskPicBox.Size = new Size(415, 226);
            laserMaskPicBox.SizeMode = PictureBoxSizeMode.Zoom;
            laserMaskPicBox.TabIndex = 37;
            laserMaskPicBox.TabStop = false;
            // 
            // labelLaserFound
            // 
            labelLaserFound.Anchor = AnchorStyles.None;
            labelLaserFound.AutoSize = true;
            labelLaserFound.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelLaserFound.Location = new Point(994, 261);
            labelLaserFound.Name = "labelLaserFound";
            labelLaserFound.Size = new Size(88, 20);
            labelLaserFound.TabIndex = 34;
            labelLaserFound.Text = "Laser Found";
            // 
            // laserFoundPicBox
            // 
            laserFoundPicBox.Dock = DockStyle.Fill;
            laserFoundPicBox.Location = new Point(830, 286);
            laserFoundPicBox.Margin = new Padding(0);
            laserFoundPicBox.Name = "laserFoundPicBox";
            laserFoundPicBox.Size = new Size(417, 227);
            laserFoundPicBox.SizeMode = PictureBoxSizeMode.Zoom;
            laserFoundPicBox.TabIndex = 38;
            laserFoundPicBox.TabStop = false;
            // 
            // LaserDetectionDebugForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1247, 606);
            Controls.Add(tableLayoutPanelDebugImages);
            Controls.Add(tableLayoutPanelMain);
            Margin = new Padding(3, 2, 3, 2);
            Name = "LaserDetectionDebugForm";
            Text = "Laser Detection Image Debugging";
            FormClosed += ImageProcessingDebugForm_FormClosed;
            ((System.ComponentModel.ISupportInitialize)scoredCandidatesPicBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)allCandidatesPicBox).EndInit();
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelCueballMask.ResumeLayout(false);
            tableLayoutPanelCueballMask.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarLaserMaskBlueMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarLaserMaskGreenMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarLaserMaskRedMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarLaserMaskBlueMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarLaserMaskGreenMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarLaserMaskRedMin).EndInit();
            tableLayoutPanelCheckboxes.ResumeLayout(false);
            tableLayoutPanelCheckboxes.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanelDebugImages.ResumeLayout(false);
            tableLayoutPanelDebugImages.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)filteredCandidatesPicBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)workingImagePicBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)laserMaskPicBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)laserFoundPicBox).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private PictureBox scoredCandidatesPicBox;
        private PictureBox allCandidatesPicBox;
        private Label labelOriginalImage;
        private Label labelWorkingImage;
        private Label labelScoredCandidates;
        private Label labelMaskApplied;
        private Label labelFilteredCandidates;
        private Label labelAllCandidates;
        private TableLayoutPanel tableLayoutPanelMain;
        private CheckBox checkBoxEnableTableBoundary;
        private CheckBox checkBoxEnableSharpen;
        private CheckBox checkBoxEnableBlurr;
        private TableLayoutPanel tableLayoutPanelCheckboxes;
        private TableLayoutPanel tableLayoutPanelCueballMask;
        private Label labelLaserMaskBlue;
        private Label labelLaserMaskGreen;
        private Label labelLaserMaskRed;
        private TrackBar trackBarLaserMaskBlueMax;
        private TrackBar trackBarLaserMaskGreenMax;
        private TrackBar trackBarLaserMaskRedMax;
        private TrackBar trackBarLaserMaskBlueMin;
        private TrackBar trackBarLaserMaskGreenMin;
        private TrackBar trackBarLaserMaskRedMin;
        private Label labelLaserMaskMax;
        private Label labelLaserMaskMin;
        private Label labelLaserMaskRedMax;
        private Label labelLaserMaskRedMin;
        private Label labelLaserMaskGreenMax;
        private Label labelLaserMaskGreenMin;
        private Label labelLaserMaskBlueMax;
        private Label labelLaserMaskBlueMin;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label13;
        private Label label14;
        private TableLayoutPanel tableLayoutPanelDebugImages;
        private Label label2;
        private Label labelLaserFound;
        private PictureBox laserFoundPicBox;
        private PictureBox laserMaskPicBox;
        private PictureBox filteredCandidatesPicBox;
        private Label labelLaserMaskArea;
        private PictureBox workingImagePicBox;
    }
}