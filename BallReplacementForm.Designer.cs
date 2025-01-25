namespace billiard_laser
{
    partial class BallReplacementForm
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
            pictureBoxTable = new PictureBox();
            labelCameraOpacity = new Label();
            trackBarCameraOpacity = new TrackBar();
            labelCameraOpacityValue = new Label();
            btnLaserLeft = new Button();
            btnLaserRight = new Button();
            btnLaserUp = new Button();
            btnLaserDown = new Button();
            labelLaserStepAmountValue = new Label();
            trackBarLaserStepAmount = new TrackBar();
            labelLaserStepAmount = new Label();
            btnLaserEnableToggle = new Button();
            btnFlipCamera = new Button();
            btnMirrorCamera = new Button();
            btnShowDebugForm = new Button();
            btnCalibrateLaser = new Button();
            tableLayoutPanelMain = new TableLayoutPanel();
            tableLayoutPanelControls = new TableLayoutPanel();
            tableLayoutPanelCameraControls = new TableLayoutPanel();
            tableLayoutPanelCameraOpacity = new TableLayoutPanel();
            tableLayoutPanelTransformCamera = new TableLayoutPanel();
            tableLayoutPanelLaserControls = new TableLayoutPanel();
            tableLayoutPanelLaserOptions = new TableLayoutPanel();
            tableLayoutPanelStepAmount = new TableLayoutPanel();
            tableLayoutPanelLaserManualControls = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)pictureBoxTable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarCameraOpacity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarLaserStepAmount).BeginInit();
            tableLayoutPanelMain.SuspendLayout();
            tableLayoutPanelControls.SuspendLayout();
            tableLayoutPanelCameraControls.SuspendLayout();
            tableLayoutPanelCameraOpacity.SuspendLayout();
            tableLayoutPanelTransformCamera.SuspendLayout();
            tableLayoutPanelLaserControls.SuspendLayout();
            tableLayoutPanelLaserOptions.SuspendLayout();
            tableLayoutPanelStepAmount.SuspendLayout();
            tableLayoutPanelLaserManualControls.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBoxTable
            // 
            pictureBoxTable.BackColor = SystemColors.ControlLight;
            pictureBoxTable.Dock = DockStyle.Fill;
            pictureBoxTable.Image = Properties.Resources.cat;
            pictureBoxTable.Location = new Point(3, 3);
            pictureBoxTable.Name = "pictureBoxTable";
            pictureBoxTable.Size = new Size(978, 442);
            pictureBoxTable.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxTable.TabIndex = 0;
            pictureBoxTable.TabStop = false;
            pictureBoxTable.Click += pictureBoxTable_Click;
            // 
            // labelCameraOpacity
            // 
            labelCameraOpacity.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            labelCameraOpacity.AutoSize = true;
            labelCameraOpacity.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelCameraOpacity.Location = new Point(3, 0);
            labelCameraOpacity.Name = "labelCameraOpacity";
            labelCameraOpacity.Size = new Size(181, 45);
            labelCameraOpacity.TabIndex = 2;
            labelCameraOpacity.Text = "Camera Opacity";
            // 
            // trackBarCameraOpacity
            // 
            trackBarCameraOpacity.Dock = DockStyle.Fill;
            trackBarCameraOpacity.Location = new Point(3, 48);
            trackBarCameraOpacity.Maximum = 100;
            trackBarCameraOpacity.Name = "trackBarCameraOpacity";
            trackBarCameraOpacity.Size = new Size(181, 44);
            trackBarCameraOpacity.TabIndex = 3;
            trackBarCameraOpacity.Value = 65;
            trackBarCameraOpacity.Scroll += trackBarCameraOpacity_Scroll;
            // 
            // labelCameraOpacityValue
            // 
            labelCameraOpacityValue.AutoSize = true;
            labelCameraOpacityValue.Dock = DockStyle.Left;
            labelCameraOpacityValue.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelCameraOpacityValue.Location = new Point(190, 45);
            labelCameraOpacityValue.Name = "labelCameraOpacityValue";
            labelCameraOpacityValue.Size = new Size(30, 50);
            labelCameraOpacityValue.TabIndex = 38;
            labelCameraOpacityValue.Text = "X%";
            // 
            // btnLaserLeft
            // 
            btnLaserLeft.Dock = DockStyle.Fill;
            btnLaserLeft.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnLaserLeft.Location = new Point(3, 49);
            btnLaserLeft.Margin = new Padding(3, 2, 3, 2);
            btnLaserLeft.Name = "btnLaserLeft";
            btnLaserLeft.Size = new Size(43, 44);
            btnLaserLeft.TabIndex = 56;
            btnLaserLeft.Text = "←";
            btnLaserLeft.UseVisualStyleBackColor = true;
            btnLaserLeft.Click += btnLaserLeft_Click;
            // 
            // btnLaserRight
            // 
            btnLaserRight.Dock = DockStyle.Fill;
            btnLaserRight.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnLaserRight.Location = new Point(101, 49);
            btnLaserRight.Margin = new Padding(3, 2, 3, 2);
            btnLaserRight.Name = "btnLaserRight";
            btnLaserRight.Size = new Size(45, 44);
            btnLaserRight.TabIndex = 57;
            btnLaserRight.Text = "→";
            btnLaserRight.UseVisualStyleBackColor = true;
            btnLaserRight.Click += btnLaserRight_Click;
            // 
            // btnLaserUp
            // 
            btnLaserUp.Dock = DockStyle.Fill;
            btnLaserUp.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnLaserUp.Location = new Point(52, 2);
            btnLaserUp.Margin = new Padding(3, 2, 3, 2);
            btnLaserUp.Name = "btnLaserUp";
            btnLaserUp.Size = new Size(43, 43);
            btnLaserUp.TabIndex = 58;
            btnLaserUp.Text = "↑";
            btnLaserUp.UseVisualStyleBackColor = true;
            btnLaserUp.Click += btnLaserUp_Click;
            // 
            // btnLaserDown
            // 
            btnLaserDown.Dock = DockStyle.Fill;
            btnLaserDown.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnLaserDown.Location = new Point(52, 49);
            btnLaserDown.Margin = new Padding(3, 2, 3, 2);
            btnLaserDown.Name = "btnLaserDown";
            btnLaserDown.Size = new Size(43, 44);
            btnLaserDown.TabIndex = 59;
            btnLaserDown.Text = "↓";
            btnLaserDown.UseVisualStyleBackColor = true;
            btnLaserDown.Click += btnLaserDown_Click;
            // 
            // labelLaserStepAmountValue
            // 
            labelLaserStepAmountValue.AutoSize = true;
            labelLaserStepAmountValue.Dock = DockStyle.Left;
            labelLaserStepAmountValue.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelLaserStepAmountValue.Location = new Point(182, 47);
            labelLaserStepAmountValue.Name = "labelLaserStepAmountValue";
            labelLaserStepAmountValue.Size = new Size(18, 48);
            labelLaserStepAmountValue.TabIndex = 62;
            labelLaserStepAmountValue.Text = "X";
            // 
            // trackBarLaserStepAmount
            // 
            trackBarLaserStepAmount.Dock = DockStyle.Fill;
            trackBarLaserStepAmount.Location = new Point(3, 50);
            trackBarLaserStepAmount.Maximum = 400;
            trackBarLaserStepAmount.Minimum = 1;
            trackBarLaserStepAmount.Name = "trackBarLaserStepAmount";
            trackBarLaserStepAmount.Size = new Size(173, 42);
            trackBarLaserStepAmount.TabIndex = 61;
            trackBarLaserStepAmount.Value = 20;
            trackBarLaserStepAmount.ValueChanged += trackBarLaserStepAmount_ValueChanged;
            // 
            // labelLaserStepAmount
            // 
            labelLaserStepAmount.AutoSize = true;
            labelLaserStepAmount.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelLaserStepAmount.Location = new Point(3, 0);
            labelLaserStepAmount.Name = "labelLaserStepAmount";
            labelLaserStepAmount.Size = new Size(134, 20);
            labelLaserStepAmount.TabIndex = 60;
            labelLaserStepAmount.Text = "Laser Step Amount";
            // 
            // btnLaserEnableToggle
            // 
            btnLaserEnableToggle.Dock = DockStyle.Fill;
            btnLaserEnableToggle.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnLaserEnableToggle.Location = new Point(3, 2);
            btnLaserEnableToggle.Margin = new Padding(3, 2, 3, 2);
            btnLaserEnableToggle.Name = "btnLaserEnableToggle";
            btnLaserEnableToggle.Size = new Size(163, 27);
            btnLaserEnableToggle.TabIndex = 63;
            btnLaserEnableToggle.Text = "Toggle Laser";
            btnLaserEnableToggle.UseVisualStyleBackColor = true;
            btnLaserEnableToggle.Click += btnLaserEnableToggle_Click;
            // 
            // btnFlipCamera
            // 
            btnFlipCamera.Dock = DockStyle.Fill;
            btnFlipCamera.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnFlipCamera.Location = new Point(3, 2);
            btnFlipCamera.Margin = new Padding(3, 2, 3, 2);
            btnFlipCamera.Name = "btnFlipCamera";
            btnFlipCamera.Size = new Size(112, 43);
            btnFlipCamera.TabIndex = 64;
            btnFlipCamera.Text = "Flip Camera";
            btnFlipCamera.UseVisualStyleBackColor = true;
            btnFlipCamera.Click += btnFlipCamera_Click;
            // 
            // btnMirrorCamera
            // 
            btnMirrorCamera.Dock = DockStyle.Fill;
            btnMirrorCamera.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnMirrorCamera.Location = new Point(3, 49);
            btnMirrorCamera.Margin = new Padding(3, 2, 3, 2);
            btnMirrorCamera.Name = "btnMirrorCamera";
            btnMirrorCamera.Size = new Size(112, 44);
            btnMirrorCamera.TabIndex = 65;
            btnMirrorCamera.Text = "Mirror Camera";
            btnMirrorCamera.UseVisualStyleBackColor = true;
            btnMirrorCamera.Click += btnMirrorCamera_Click;
            // 
            // btnShowDebugForm
            // 
            btnShowDebugForm.Dock = DockStyle.Fill;
            btnShowDebugForm.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnShowDebugForm.Location = new Point(3, 64);
            btnShowDebugForm.Margin = new Padding(3, 2, 3, 2);
            btnShowDebugForm.Name = "btnShowDebugForm";
            btnShowDebugForm.Size = new Size(163, 29);
            btnShowDebugForm.TabIndex = 66;
            btnShowDebugForm.Text = "Show Debug Form";
            btnShowDebugForm.UseVisualStyleBackColor = true;
            btnShowDebugForm.Click += btnShowDebugForm_Click;
            // 
            // btnCalibrateLaser
            // 
            btnCalibrateLaser.Dock = DockStyle.Fill;
            btnCalibrateLaser.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnCalibrateLaser.Location = new Point(3, 33);
            btnCalibrateLaser.Margin = new Padding(3, 2, 3, 2);
            btnCalibrateLaser.Name = "btnCalibrateLaser";
            btnCalibrateLaser.Size = new Size(163, 27);
            btnCalibrateLaser.TabIndex = 67;
            btnCalibrateLaser.Text = "Calibrate Laser";
            btnCalibrateLaser.UseVisualStyleBackColor = true;
            btnCalibrateLaser.Click += btnCalibrateLaser_Click;
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.ColumnCount = 1;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Controls.Add(pictureBoxTable, 0, 0);
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelControls, 0, 1);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(0, 0);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 2;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 113F));
            tableLayoutPanelMain.Size = new Size(984, 561);
            tableLayoutPanelMain.TabIndex = 68;
            // 
            // tableLayoutPanelControls
            // 
            tableLayoutPanelControls.ColumnCount = 2;
            tableLayoutPanelControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40.26465F));
            tableLayoutPanelControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 59.73535F));
            tableLayoutPanelControls.Controls.Add(tableLayoutPanelCameraControls, 0, 0);
            tableLayoutPanelControls.Controls.Add(tableLayoutPanelLaserControls, 1, 0);
            tableLayoutPanelControls.Dock = DockStyle.Fill;
            tableLayoutPanelControls.Location = new Point(3, 451);
            tableLayoutPanelControls.Name = "tableLayoutPanelControls";
            tableLayoutPanelControls.RowCount = 1;
            tableLayoutPanelControls.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelControls.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelControls.Size = new Size(978, 107);
            tableLayoutPanelControls.TabIndex = 1;
            // 
            // tableLayoutPanelCameraControls
            // 
            tableLayoutPanelCameraControls.BackColor = Color.FromArgb(244, 239, 198);
            tableLayoutPanelCameraControls.ColumnCount = 2;
            tableLayoutPanelCameraControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68.0459747F));
            tableLayoutPanelCameraControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 31.9540234F));
            tableLayoutPanelCameraControls.Controls.Add(tableLayoutPanelCameraOpacity, 0, 0);
            tableLayoutPanelCameraControls.Controls.Add(tableLayoutPanelTransformCamera, 1, 0);
            tableLayoutPanelCameraControls.Dock = DockStyle.Fill;
            tableLayoutPanelCameraControls.Location = new Point(3, 3);
            tableLayoutPanelCameraControls.Name = "tableLayoutPanelCameraControls";
            tableLayoutPanelCameraControls.RowCount = 1;
            tableLayoutPanelCameraControls.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelCameraControls.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelCameraControls.Size = new Size(387, 101);
            tableLayoutPanelCameraControls.TabIndex = 0;
            // 
            // tableLayoutPanelCameraOpacity
            // 
            tableLayoutPanelCameraOpacity.ColumnCount = 2;
            tableLayoutPanelCameraOpacity.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 73.01038F));
            tableLayoutPanelCameraOpacity.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26.98962F));
            tableLayoutPanelCameraOpacity.Controls.Add(labelCameraOpacityValue, 1, 1);
            tableLayoutPanelCameraOpacity.Controls.Add(trackBarCameraOpacity, 0, 1);
            tableLayoutPanelCameraOpacity.Controls.Add(labelCameraOpacity, 0, 0);
            tableLayoutPanelCameraOpacity.Dock = DockStyle.Fill;
            tableLayoutPanelCameraOpacity.Location = new Point(3, 3);
            tableLayoutPanelCameraOpacity.Name = "tableLayoutPanelCameraOpacity";
            tableLayoutPanelCameraOpacity.RowCount = 2;
            tableLayoutPanelCameraOpacity.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanelCameraOpacity.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelCameraOpacity.Size = new Size(257, 95);
            tableLayoutPanelCameraOpacity.TabIndex = 69;
            // 
            // tableLayoutPanelTransformCamera
            // 
            tableLayoutPanelTransformCamera.ColumnCount = 1;
            tableLayoutPanelTransformCamera.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelTransformCamera.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelTransformCamera.Controls.Add(btnMirrorCamera, 0, 1);
            tableLayoutPanelTransformCamera.Controls.Add(btnFlipCamera, 0, 0);
            tableLayoutPanelTransformCamera.Dock = DockStyle.Fill;
            tableLayoutPanelTransformCamera.Location = new Point(266, 3);
            tableLayoutPanelTransformCamera.Name = "tableLayoutPanelTransformCamera";
            tableLayoutPanelTransformCamera.RowCount = 2;
            tableLayoutPanelTransformCamera.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelTransformCamera.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelTransformCamera.Size = new Size(118, 95);
            tableLayoutPanelTransformCamera.TabIndex = 70;
            // 
            // tableLayoutPanelLaserControls
            // 
            tableLayoutPanelLaserControls.BackColor = Color.MistyRose;
            tableLayoutPanelLaserControls.ColumnCount = 3;
            tableLayoutPanelLaserControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 61.6972466F));
            tableLayoutPanelLaserControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38.3027534F));
            tableLayoutPanelLaserControls.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 174F));
            tableLayoutPanelLaserControls.Controls.Add(tableLayoutPanelLaserOptions, 2, 0);
            tableLayoutPanelLaserControls.Controls.Add(tableLayoutPanelStepAmount, 0, 0);
            tableLayoutPanelLaserControls.Controls.Add(tableLayoutPanelLaserManualControls, 1, 0);
            tableLayoutPanelLaserControls.Dock = DockStyle.Fill;
            tableLayoutPanelLaserControls.Location = new Point(396, 3);
            tableLayoutPanelLaserControls.Name = "tableLayoutPanelLaserControls";
            tableLayoutPanelLaserControls.RowCount = 1;
            tableLayoutPanelLaserControls.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelLaserControls.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelLaserControls.Size = new Size(579, 101);
            tableLayoutPanelLaserControls.TabIndex = 1;
            // 
            // tableLayoutPanelLaserOptions
            // 
            tableLayoutPanelLaserOptions.ColumnCount = 1;
            tableLayoutPanelLaserOptions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelLaserOptions.Controls.Add(btnLaserEnableToggle, 0, 0);
            tableLayoutPanelLaserOptions.Controls.Add(btnShowDebugForm, 0, 2);
            tableLayoutPanelLaserOptions.Controls.Add(btnCalibrateLaser, 0, 1);
            tableLayoutPanelLaserOptions.Location = new Point(407, 3);
            tableLayoutPanelLaserOptions.Name = "tableLayoutPanelLaserOptions";
            tableLayoutPanelLaserOptions.RowCount = 3;
            tableLayoutPanelLaserOptions.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanelLaserOptions.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanelLaserOptions.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanelLaserOptions.Size = new Size(169, 95);
            tableLayoutPanelLaserOptions.TabIndex = 0;
            // 
            // tableLayoutPanelStepAmount
            // 
            tableLayoutPanelStepAmount.ColumnCount = 2;
            tableLayoutPanelStepAmount.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 73.66255F));
            tableLayoutPanelStepAmount.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26.3374481F));
            tableLayoutPanelStepAmount.Controls.Add(labelLaserStepAmount, 0, 0);
            tableLayoutPanelStepAmount.Controls.Add(labelLaserStepAmountValue, 1, 1);
            tableLayoutPanelStepAmount.Controls.Add(trackBarLaserStepAmount, 0, 1);
            tableLayoutPanelStepAmount.Dock = DockStyle.Fill;
            tableLayoutPanelStepAmount.Location = new Point(3, 3);
            tableLayoutPanelStepAmount.Name = "tableLayoutPanelStepAmount";
            tableLayoutPanelStepAmount.RowCount = 2;
            tableLayoutPanelStepAmount.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelStepAmount.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelStepAmount.Size = new Size(243, 95);
            tableLayoutPanelStepAmount.TabIndex = 1;
            // 
            // tableLayoutPanelLaserManualControls
            // 
            tableLayoutPanelLaserManualControls.ColumnCount = 3;
            tableLayoutPanelLaserManualControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanelLaserManualControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanelLaserManualControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanelLaserManualControls.Controls.Add(btnLaserUp, 1, 0);
            tableLayoutPanelLaserManualControls.Controls.Add(btnLaserRight, 2, 1);
            tableLayoutPanelLaserManualControls.Controls.Add(btnLaserDown, 1, 1);
            tableLayoutPanelLaserManualControls.Controls.Add(btnLaserLeft, 0, 1);
            tableLayoutPanelLaserManualControls.Dock = DockStyle.Fill;
            tableLayoutPanelLaserManualControls.Location = new Point(252, 3);
            tableLayoutPanelLaserManualControls.Name = "tableLayoutPanelLaserManualControls";
            tableLayoutPanelLaserManualControls.RowCount = 2;
            tableLayoutPanelLaserManualControls.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelLaserManualControls.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelLaserManualControls.Size = new Size(149, 95);
            tableLayoutPanelLaserManualControls.TabIndex = 2;
            // 
            // BallReplacementForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 561);
            Controls.Add(tableLayoutPanelMain);
            Name = "BallReplacementForm";
            Text = "Ball Replacement Helper";
            FormClosed += BallReplacementForm_FormClosed;
            ((System.ComponentModel.ISupportInitialize)pictureBoxTable).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarCameraOpacity).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarLaserStepAmount).EndInit();
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelControls.ResumeLayout(false);
            tableLayoutPanelCameraControls.ResumeLayout(false);
            tableLayoutPanelCameraOpacity.ResumeLayout(false);
            tableLayoutPanelCameraOpacity.PerformLayout();
            tableLayoutPanelTransformCamera.ResumeLayout(false);
            tableLayoutPanelLaserControls.ResumeLayout(false);
            tableLayoutPanelLaserOptions.ResumeLayout(false);
            tableLayoutPanelStepAmount.ResumeLayout(false);
            tableLayoutPanelStepAmount.PerformLayout();
            tableLayoutPanelLaserManualControls.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBoxTable;
        private Label labelCameraOpacity;
        private TrackBar trackBarCameraOpacity;
        private Label labelCameraOpacityValue;
        private Button btnLaserLeft;
        private Button btnLaserRight;
        private Button btnLaserUp;
        private Button btnLaserDown;
        private Label labelLaserStepAmountValue;
        private TrackBar trackBarLaserStepAmount;
        private Label labelLaserStepAmount;
        private Button btnLaserEnableToggle;
        private Button btnFlipCamera;
        private Button btnMirrorCamera;
        private Button btnShowDebugForm;
        private Button btnCalibrateLaser;
        private TableLayoutPanel tableLayoutPanelMain;
        private TableLayoutPanel tableLayoutPanelControls;
        private TableLayoutPanel tableLayoutPanelCameraControls;
        private TableLayoutPanel tableLayoutPanelCameraOpacity;
        private TableLayoutPanel tableLayoutPanelTransformCamera;
        private TableLayoutPanel tableLayoutPanelLaserControls;
        private TableLayoutPanel tableLayoutPanelLaserOptions;
        private TableLayoutPanel tableLayoutPanelStepAmount;
        private TableLayoutPanel tableLayoutPanelLaserManualControls;
    }
}