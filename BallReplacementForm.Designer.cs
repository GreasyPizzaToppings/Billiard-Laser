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
            ((System.ComponentModel.ISupportInitialize)pictureBoxTable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarCameraOpacity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarLaserStepAmount).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxTable
            // 
            pictureBoxTable.Image = Properties.Resources.cat;
            pictureBoxTable.Location = new Point(12, 12);
            pictureBoxTable.Name = "pictureBoxTable";
            pictureBoxTable.Size = new Size(1068, 600);
            pictureBoxTable.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxTable.TabIndex = 0;
            pictureBoxTable.TabStop = false;
            // 
            // labelCameraOpacity
            // 
            labelCameraOpacity.AutoSize = true;
            labelCameraOpacity.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelCameraOpacity.Location = new Point(21, 643);
            labelCameraOpacity.Name = "labelCameraOpacity";
            labelCameraOpacity.Size = new Size(115, 20);
            labelCameraOpacity.TabIndex = 2;
            labelCameraOpacity.Text = "Camera Opacity";
            // 
            // trackBarCameraOpacity
            // 
            trackBarCameraOpacity.Location = new Point(12, 670);
            trackBarCameraOpacity.Maximum = 100;
            trackBarCameraOpacity.Name = "trackBarCameraOpacity";
            trackBarCameraOpacity.Size = new Size(178, 45);
            trackBarCameraOpacity.TabIndex = 3;
            trackBarCameraOpacity.Value = 65;
            trackBarCameraOpacity.Scroll += trackBarCameraOpacity_Scroll;
            // 
            // labelCameraOpacityValue
            // 
            labelCameraOpacityValue.Anchor = AnchorStyles.None;
            labelCameraOpacityValue.AutoSize = true;
            labelCameraOpacityValue.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelCameraOpacityValue.Location = new Point(189, 676);
            labelCameraOpacityValue.Name = "labelCameraOpacityValue";
            labelCameraOpacityValue.Size = new Size(30, 20);
            labelCameraOpacityValue.TabIndex = 38;
            labelCameraOpacityValue.Text = "X%";
            // 
            // btnLaserLeft
            // 
            btnLaserLeft.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnLaserLeft.Location = new Point(799, 668);
            btnLaserLeft.Margin = new Padding(3, 2, 3, 2);
            btnLaserLeft.Name = "btnLaserLeft";
            btnLaserLeft.Size = new Size(35, 33);
            btnLaserLeft.TabIndex = 56;
            btnLaserLeft.Text = "←";
            btnLaserLeft.UseVisualStyleBackColor = true;
            btnLaserLeft.Click += btnLaserLeft_Click;
            // 
            // btnLaserRight
            // 
            btnLaserRight.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnLaserRight.Location = new Point(881, 668);
            btnLaserRight.Margin = new Padding(3, 2, 3, 2);
            btnLaserRight.Name = "btnLaserRight";
            btnLaserRight.Size = new Size(35, 33);
            btnLaserRight.TabIndex = 57;
            btnLaserRight.Text = "→";
            btnLaserRight.UseVisualStyleBackColor = true;
            btnLaserRight.Click += btnLaserRight_Click;
            // 
            // btnLaserUp
            // 
            btnLaserUp.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnLaserUp.Location = new Point(840, 631);
            btnLaserUp.Margin = new Padding(3, 2, 3, 2);
            btnLaserUp.Name = "btnLaserUp";
            btnLaserUp.Size = new Size(35, 33);
            btnLaserUp.TabIndex = 58;
            btnLaserUp.Text = "↑";
            btnLaserUp.UseVisualStyleBackColor = true;
            btnLaserUp.Click += btnLaserUp_Click;
            // 
            // btnLaserDown
            // 
            btnLaserDown.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnLaserDown.Location = new Point(840, 668);
            btnLaserDown.Margin = new Padding(3, 2, 3, 2);
            btnLaserDown.Name = "btnLaserDown";
            btnLaserDown.Size = new Size(35, 33);
            btnLaserDown.TabIndex = 59;
            btnLaserDown.Text = "↓";
            btnLaserDown.UseVisualStyleBackColor = true;
            btnLaserDown.Click += btnLaserDown_Click;
            // 
            // labelLaserStepAmountValue
            // 
            labelLaserStepAmountValue.Anchor = AnchorStyles.None;
            labelLaserStepAmountValue.AutoSize = true;
            labelLaserStepAmountValue.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelLaserStepAmountValue.Location = new Point(730, 673);
            labelLaserStepAmountValue.Name = "labelLaserStepAmountValue";
            labelLaserStepAmountValue.Size = new Size(18, 20);
            labelLaserStepAmountValue.TabIndex = 62;
            labelLaserStepAmountValue.Text = "X";
            // 
            // trackBarLaserStepAmount
            // 
            trackBarLaserStepAmount.Location = new Point(546, 668);
            trackBarLaserStepAmount.Maximum = 500;
            trackBarLaserStepAmount.Minimum = 1;
            trackBarLaserStepAmount.Name = "trackBarLaserStepAmount";
            trackBarLaserStepAmount.Size = new Size(178, 45);
            trackBarLaserStepAmount.TabIndex = 61;
            trackBarLaserStepAmount.Value = 65;
            trackBarLaserStepAmount.ValueChanged += trackBarLaserStepAmount_ValueChanged;
            // 
            // labelLaserStepAmount
            // 
            labelLaserStepAmount.AutoSize = true;
            labelLaserStepAmount.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelLaserStepAmount.Location = new Point(546, 637);
            labelLaserStepAmount.Name = "labelLaserStepAmount";
            labelLaserStepAmount.Size = new Size(96, 20);
            labelLaserStepAmount.TabIndex = 60;
            labelLaserStepAmount.Text = "Step Amount";
            // 
            // btnLaserEnableToggle
            // 
            btnLaserEnableToggle.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnLaserEnableToggle.Location = new Point(400, 671);
            btnLaserEnableToggle.Margin = new Padding(3, 2, 3, 2);
            btnLaserEnableToggle.Name = "btnLaserEnableToggle";
            btnLaserEnableToggle.Size = new Size(101, 33);
            btnLaserEnableToggle.TabIndex = 63;
            btnLaserEnableToggle.Text = "Toggle Laser";
            btnLaserEnableToggle.UseVisualStyleBackColor = true;
            btnLaserEnableToggle.Click += btnLaserEnableToggle_Click;
            // 
            // btnFlipCamera
            // 
            btnFlipCamera.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnFlipCamera.Location = new Point(280, 631);
            btnFlipCamera.Margin = new Padding(3, 2, 3, 2);
            btnFlipCamera.Name = "btnFlipCamera";
            btnFlipCamera.Size = new Size(101, 33);
            btnFlipCamera.TabIndex = 64;
            btnFlipCamera.Text = "Flip Camera";
            btnFlipCamera.UseVisualStyleBackColor = true;
            btnFlipCamera.Click += btnFlipCamera_Click;
            // 
            // btnMirrorCamera
            // 
            btnMirrorCamera.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnMirrorCamera.Location = new Point(280, 671);
            btnMirrorCamera.Margin = new Padding(3, 2, 3, 2);
            btnMirrorCamera.Name = "btnMirrorCamera";
            btnMirrorCamera.Size = new Size(101, 33);
            btnMirrorCamera.TabIndex = 65;
            btnMirrorCamera.Text = "Mirror Camera";
            btnMirrorCamera.UseVisualStyleBackColor = true;
            btnMirrorCamera.Click += btnMirrorCamera_Click;
            // 
            // btnShowDebugForm
            // 
            btnShowDebugForm.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnShowDebugForm.Location = new Point(958, 668);
            btnShowDebugForm.Margin = new Padding(3, 2, 3, 2);
            btnShowDebugForm.Name = "btnShowDebugForm";
            btnShowDebugForm.Size = new Size(116, 33);
            btnShowDebugForm.TabIndex = 66;
            btnShowDebugForm.Text = "Show Debug Form";
            btnShowDebugForm.UseVisualStyleBackColor = true;
            btnShowDebugForm.Click += btnShowDebugForm_Click;
            // 
            // BallReplacementForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1105, 712);
            Controls.Add(btnShowDebugForm);
            Controls.Add(btnMirrorCamera);
            Controls.Add(btnFlipCamera);
            Controls.Add(btnLaserEnableToggle);
            Controls.Add(labelLaserStepAmountValue);
            Controls.Add(trackBarLaserStepAmount);
            Controls.Add(labelLaserStepAmount);
            Controls.Add(btnLaserDown);
            Controls.Add(btnLaserUp);
            Controls.Add(btnLaserRight);
            Controls.Add(btnLaserLeft);
            Controls.Add(labelCameraOpacityValue);
            Controls.Add(trackBarCameraOpacity);
            Controls.Add(labelCameraOpacity);
            Controls.Add(pictureBoxTable);
            Name = "BallReplacementForm";
            Text = "Ball Replacement Helper";
            FormClosed += BallReplacementForm_FormClosed;
            ((System.ComponentModel.ISupportInitialize)pictureBoxTable).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarCameraOpacity).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarLaserStepAmount).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
    }
}