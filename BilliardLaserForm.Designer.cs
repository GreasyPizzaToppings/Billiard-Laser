
namespace billiard_laser
{
    partial class BilliardLaserForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BilliardLaserForm));
            pictureBoxImage = new PictureBox();
            labelShotInfo = new Label();
            pictureBoxAccelerationOverTime = new PictureBox();
            pictureBoxDistanceOverTime = new PictureBox();
            groupBoxShotInfo = new GroupBox();
            labelAverageAcceleration = new Label();
            labelSpeed = new Label();
            labelMaxAcceleration = new Label();
            labelDistanceTravelled = new Label();
            labelAvgSpeed = new Label();
            labelMaxSpeed = new Label();
            Acceleration = new Label();
            labelDistance = new Label();
            pictureBoxSpeedOverTime = new PictureBox();
            tableLayoutPanelVideoControls = new TableLayoutPanel();
            panelMediaControls = new Panel();
            labelMediaControls = new Label();
            buttonLastFrame = new Button();
            buttonResume = new Button();
            buttonNextFrame = new Button();
            buttonPause = new Button();
            panelListBoxes = new Panel();
            listBoxProcessedFrames = new ListBox();
            listBoxShots = new ListBox();
            panelVideoControls = new Panel();
            labelShots = new Label();
            labelFrames = new Label();
            buttonShowDebugForm = new Button();
            labelCameras = new Label();
            cboCamera = new ComboBox();
            labelVideoControls = new Label();
            btnLoadVideo = new Button();
            btnGetCameraInput = new Button();
            labelFrameRate = new Label();
            btnDetectBalls = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBoxImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAccelerationOverTime).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxDistanceOverTime).BeginInit();
            groupBoxShotInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSpeedOverTime).BeginInit();
            tableLayoutPanelVideoControls.SuspendLayout();
            panelMediaControls.SuspendLayout();
            panelListBoxes.SuspendLayout();
            panelVideoControls.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBoxImage
            // 
            pictureBoxImage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBoxImage.BackColor = SystemColors.ControlLight;
            pictureBoxImage.Image = (Image)resources.GetObject("pictureBoxImage.Image");
            pictureBoxImage.Location = new Point(209, 10);
            pictureBoxImage.Margin = new Padding(3, 2, 3, 2);
            pictureBoxImage.Name = "pictureBoxImage";
            pictureBoxImage.Size = new Size(656, 670);
            pictureBoxImage.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxImage.TabIndex = 15;
            pictureBoxImage.TabStop = false;
            // 
            // labelShotInfo
            // 
            labelShotInfo.AutoSize = true;
            labelShotInfo.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            labelShotInfo.Location = new Point(37, 9);
            labelShotInfo.Name = "labelShotInfo";
            labelShotInfo.Size = new Size(92, 28);
            labelShotInfo.TabIndex = 31;
            labelShotInfo.Text = "Shot Info";
            // 
            // pictureBoxAccelerationOverTime
            // 
            pictureBoxAccelerationOverTime.Location = new Point(11, 206);
            pictureBoxAccelerationOverTime.Margin = new Padding(3, 2, 3, 2);
            pictureBoxAccelerationOverTime.Name = "pictureBoxAccelerationOverTime";
            pictureBoxAccelerationOverTime.Size = new Size(150, 115);
            pictureBoxAccelerationOverTime.TabIndex = 33;
            pictureBoxAccelerationOverTime.TabStop = false;
            // 
            // pictureBoxDistanceOverTime
            // 
            pictureBoxDistanceOverTime.Location = new Point(11, 353);
            pictureBoxDistanceOverTime.Margin = new Padding(3, 2, 3, 2);
            pictureBoxDistanceOverTime.Name = "pictureBoxDistanceOverTime";
            pictureBoxDistanceOverTime.Size = new Size(150, 115);
            pictureBoxDistanceOverTime.TabIndex = 34;
            pictureBoxDistanceOverTime.TabStop = false;
            // 
            // groupBoxShotInfo
            // 
            groupBoxShotInfo.Controls.Add(labelShotInfo);
            groupBoxShotInfo.Controls.Add(labelAverageAcceleration);
            groupBoxShotInfo.Controls.Add(labelSpeed);
            groupBoxShotInfo.Controls.Add(labelMaxAcceleration);
            groupBoxShotInfo.Controls.Add(labelDistanceTravelled);
            groupBoxShotInfo.Controls.Add(labelAvgSpeed);
            groupBoxShotInfo.Controls.Add(labelMaxSpeed);
            groupBoxShotInfo.Controls.Add(Acceleration);
            groupBoxShotInfo.Controls.Add(labelDistance);
            groupBoxShotInfo.Controls.Add(pictureBoxSpeedOverTime);
            groupBoxShotInfo.Controls.Add(pictureBoxDistanceOverTime);
            groupBoxShotInfo.Controls.Add(pictureBoxAccelerationOverTime);
            groupBoxShotInfo.Dock = DockStyle.Right;
            groupBoxShotInfo.Location = new Point(904, 0);
            groupBoxShotInfo.Margin = new Padding(3, 2, 3, 2);
            groupBoxShotInfo.Name = "groupBoxShotInfo";
            groupBoxShotInfo.Padding = new Padding(3, 2, 3, 2);
            groupBoxShotInfo.Size = new Size(234, 683);
            groupBoxShotInfo.TabIndex = 35;
            groupBoxShotInfo.TabStop = false;
            // 
            // labelAverageAcceleration
            // 
            labelAverageAcceleration.AutoSize = true;
            labelAverageAcceleration.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelAverageAcceleration.Location = new Point(166, 227);
            labelAverageAcceleration.Name = "labelAverageAcceleration";
            labelAverageAcceleration.Size = new Size(36, 19);
            labelAverageAcceleration.TabIndex = 43;
            labelAverageAcceleration.Text = "Avg:";
            // 
            // labelSpeed
            // 
            labelSpeed.AutoSize = true;
            labelSpeed.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelSpeed.Location = new Point(11, 40);
            labelSpeed.Name = "labelSpeed";
            labelSpeed.Size = new Size(46, 19);
            labelSpeed.TabIndex = 36;
            labelSpeed.Text = "Speed";
            // 
            // labelMaxAcceleration
            // 
            labelMaxAcceleration.AutoSize = true;
            labelMaxAcceleration.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelMaxAcceleration.Location = new Point(166, 206);
            labelMaxAcceleration.Name = "labelMaxAcceleration";
            labelMaxAcceleration.Size = new Size(38, 19);
            labelMaxAcceleration.TabIndex = 42;
            labelMaxAcceleration.Text = "Max:";
            // 
            // labelDistanceTravelled
            // 
            labelDistanceTravelled.AutoSize = true;
            labelDistanceTravelled.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelDistanceTravelled.Location = new Point(164, 353);
            labelDistanceTravelled.Name = "labelDistanceTravelled";
            labelDistanceTravelled.Size = new Size(41, 19);
            labelDistanceTravelled.TabIndex = 41;
            labelDistanceTravelled.Text = "Total:";
            // 
            // labelAvgSpeed
            // 
            labelAvgSpeed.AutoSize = true;
            labelAvgSpeed.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelAvgSpeed.Location = new Point(164, 79);
            labelAvgSpeed.Name = "labelAvgSpeed";
            labelAvgSpeed.Size = new Size(36, 19);
            labelAvgSpeed.TabIndex = 40;
            labelAvgSpeed.Text = "Avg:";
            // 
            // labelMaxSpeed
            // 
            labelMaxSpeed.AutoSize = true;
            labelMaxSpeed.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelMaxSpeed.Location = new Point(164, 61);
            labelMaxSpeed.Name = "labelMaxSpeed";
            labelMaxSpeed.Size = new Size(38, 19);
            labelMaxSpeed.TabIndex = 39;
            labelMaxSpeed.Text = "Max:";
            // 
            // Acceleration
            // 
            Acceleration.AutoSize = true;
            Acceleration.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            Acceleration.Location = new Point(6, 185);
            Acceleration.Name = "Acceleration";
            Acceleration.Size = new Size(83, 19);
            Acceleration.TabIndex = 38;
            Acceleration.Text = "Acceleration";
            // 
            // labelDistance
            // 
            labelDistance.AutoSize = true;
            labelDistance.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelDistance.Location = new Point(11, 332);
            labelDistance.Name = "labelDistance";
            labelDistance.Size = new Size(118, 19);
            labelDistance.TabIndex = 37;
            labelDistance.Text = "Distance Travelled";
            // 
            // pictureBoxSpeedOverTime
            // 
            pictureBoxSpeedOverTime.Location = new Point(11, 61);
            pictureBoxSpeedOverTime.Margin = new Padding(3, 2, 3, 2);
            pictureBoxSpeedOverTime.Name = "pictureBoxSpeedOverTime";
            pictureBoxSpeedOverTime.Size = new Size(150, 115);
            pictureBoxSpeedOverTime.TabIndex = 32;
            pictureBoxSpeedOverTime.TabStop = false;
            // 
            // tableLayoutPanelVideoControls
            // 
            tableLayoutPanelVideoControls.ColumnCount = 1;
            tableLayoutPanelVideoControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelVideoControls.Controls.Add(panelMediaControls, 0, 2);
            tableLayoutPanelVideoControls.Controls.Add(panelListBoxes, 0, 1);
            tableLayoutPanelVideoControls.Controls.Add(panelVideoControls, 0, 0);
            tableLayoutPanelVideoControls.Dock = DockStyle.Left;
            tableLayoutPanelVideoControls.Location = new Point(0, 0);
            tableLayoutPanelVideoControls.Name = "tableLayoutPanelVideoControls";
            tableLayoutPanelVideoControls.RowCount = 3;
            tableLayoutPanelVideoControls.RowStyles.Add(new RowStyle(SizeType.Absolute, 229F));
            tableLayoutPanelVideoControls.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelVideoControls.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            tableLayoutPanelVideoControls.Size = new Size(203, 683);
            tableLayoutPanelVideoControls.TabIndex = 47;
            // 
            // panelMediaControls
            // 
            panelMediaControls.Controls.Add(labelMediaControls);
            panelMediaControls.Controls.Add(buttonLastFrame);
            panelMediaControls.Controls.Add(buttonResume);
            panelMediaControls.Controls.Add(buttonNextFrame);
            panelMediaControls.Controls.Add(buttonPause);
            panelMediaControls.Dock = DockStyle.Fill;
            panelMediaControls.Location = new Point(3, 586);
            panelMediaControls.Name = "panelMediaControls";
            panelMediaControls.Size = new Size(197, 94);
            panelMediaControls.TabIndex = 49;
            // 
            // labelMediaControls
            // 
            labelMediaControls.AutoSize = true;
            labelMediaControls.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelMediaControls.Location = new Point(52, 4);
            labelMediaControls.Name = "labelMediaControls";
            labelMediaControls.Size = new Size(103, 19);
            labelMediaControls.TabIndex = 54;
            labelMediaControls.Text = "Media Controls";
            // 
            // buttonLastFrame
            // 
            buttonLastFrame.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonLastFrame.Location = new Point(14, 32);
            buttonLastFrame.Margin = new Padding(3, 2, 3, 2);
            buttonLastFrame.Name = "buttonLastFrame";
            buttonLastFrame.Size = new Size(80, 25);
            buttonLastFrame.TabIndex = 42;
            buttonLastFrame.Text = "Last Frame";
            buttonLastFrame.UseVisualStyleBackColor = true;
            buttonLastFrame.Click += buttonLastFrame_Click;
            // 
            // buttonResume
            // 
            buttonResume.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonResume.Location = new Point(14, 57);
            buttonResume.Margin = new Padding(3, 2, 3, 2);
            buttonResume.Name = "buttonResume";
            buttonResume.Size = new Size(80, 25);
            buttonResume.TabIndex = 44;
            buttonResume.Text = "Resume";
            buttonResume.UseVisualStyleBackColor = true;
            buttonResume.Click += buttonResume_Click;
            // 
            // buttonNextFrame
            // 
            buttonNextFrame.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonNextFrame.Location = new Point(103, 32);
            buttonNextFrame.Margin = new Padding(3, 2, 3, 2);
            buttonNextFrame.Name = "buttonNextFrame";
            buttonNextFrame.Size = new Size(80, 25);
            buttonNextFrame.TabIndex = 43;
            buttonNextFrame.Text = "Next Frame";
            buttonNextFrame.UseVisualStyleBackColor = true;
            buttonNextFrame.Click += buttonNextFrame_Click;
            // 
            // buttonPause
            // 
            buttonPause.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonPause.Location = new Point(103, 57);
            buttonPause.Margin = new Padding(3, 2, 3, 2);
            buttonPause.Name = "buttonPause";
            buttonPause.Size = new Size(80, 25);
            buttonPause.TabIndex = 45;
            buttonPause.Text = "Pause";
            buttonPause.UseVisualStyleBackColor = true;
            buttonPause.Click += buttonPause_Click;
            // 
            // panelListBoxes
            // 
            panelListBoxes.Controls.Add(listBoxProcessedFrames);
            panelListBoxes.Controls.Add(listBoxShots);
            panelListBoxes.Dock = DockStyle.Fill;
            panelListBoxes.Location = new Point(3, 229);
            panelListBoxes.Margin = new Padding(3, 0, 3, 3);
            panelListBoxes.Name = "panelListBoxes";
            panelListBoxes.Size = new Size(197, 351);
            panelListBoxes.TabIndex = 44;
            // 
            // listBoxProcessedFrames
            // 
            listBoxProcessedFrames.Dock = DockStyle.Left;
            listBoxProcessedFrames.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            listBoxProcessedFrames.FormattingEnabled = true;
            listBoxProcessedFrames.Location = new Point(0, 0);
            listBoxProcessedFrames.Margin = new Padding(3, 0, 3, 0);
            listBoxProcessedFrames.Name = "listBoxProcessedFrames";
            listBoxProcessedFrames.Size = new Size(87, 351);
            listBoxProcessedFrames.TabIndex = 40;
            // 
            // listBoxShots
            // 
            listBoxShots.Dock = DockStyle.Right;
            listBoxShots.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            listBoxShots.FormattingEnabled = true;
            listBoxShots.Location = new Point(110, 0);
            listBoxShots.Margin = new Padding(3, 0, 3, 0);
            listBoxShots.Name = "listBoxShots";
            listBoxShots.Size = new Size(87, 351);
            listBoxShots.TabIndex = 41;
            // 
            // panelVideoControls
            // 
            panelVideoControls.Controls.Add(labelShots);
            panelVideoControls.Controls.Add(labelFrames);
            panelVideoControls.Controls.Add(buttonShowDebugForm);
            panelVideoControls.Controls.Add(labelCameras);
            panelVideoControls.Controls.Add(cboCamera);
            panelVideoControls.Controls.Add(labelVideoControls);
            panelVideoControls.Controls.Add(btnLoadVideo);
            panelVideoControls.Controls.Add(btnGetCameraInput);
            panelVideoControls.Controls.Add(labelFrameRate);
            panelVideoControls.Controls.Add(btnDetectBalls);
            panelVideoControls.Dock = DockStyle.Fill;
            panelVideoControls.Location = new Point(3, 3);
            panelVideoControls.Margin = new Padding(3, 3, 3, 0);
            panelVideoControls.Name = "panelVideoControls";
            panelVideoControls.Size = new Size(197, 226);
            panelVideoControls.TabIndex = 48;
            // 
            // labelShots
            // 
            labelShots.AutoSize = true;
            labelShots.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelShots.Location = new Point(112, 203);
            labelShots.Name = "labelShots";
            labelShots.Size = new Size(43, 19);
            labelShots.TabIndex = 53;
            labelShots.Text = "Shots";
            // 
            // labelFrames
            // 
            labelFrames.AutoSize = true;
            labelFrames.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelFrames.Location = new Point(6, 203);
            labelFrames.Name = "labelFrames";
            labelFrames.Size = new Size(53, 19);
            labelFrames.TabIndex = 52;
            labelFrames.Text = "Frames";
            // 
            // buttonShowDebugForm
            // 
            buttonShowDebugForm.Location = new Point(6, 134);
            buttonShowDebugForm.Margin = new Padding(3, 2, 3, 2);
            buttonShowDebugForm.Name = "buttonShowDebugForm";
            buttonShowDebugForm.Size = new Size(181, 25);
            buttonShowDebugForm.TabIndex = 51;
            buttonShowDebugForm.Text = "Show Debug Form";
            buttonShowDebugForm.UseVisualStyleBackColor = true;
            buttonShowDebugForm.Click += buttonShowDebugForm_Click;
            // 
            // labelCameras
            // 
            labelCameras.AutoSize = true;
            labelCameras.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelCameras.Location = new Point(3, 45);
            labelCameras.Name = "labelCameras";
            labelCameras.Size = new Size(62, 19);
            labelCameras.TabIndex = 46;
            labelCameras.Text = "Cameras";
            // 
            // cboCamera
            // 
            cboCamera.FormattingEnabled = true;
            cboCamera.Location = new Point(3, 66);
            cboCamera.Name = "cboCamera";
            cboCamera.Size = new Size(103, 23);
            cboCamera.TabIndex = 45;
            // 
            // labelVideoControls
            // 
            labelVideoControls.AutoSize = true;
            labelVideoControls.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            labelVideoControls.Location = new Point(31, 8);
            labelVideoControls.Name = "labelVideoControls";
            labelVideoControls.Size = new Size(142, 28);
            labelVideoControls.TabIndex = 50;
            labelVideoControls.Text = "Video Controls";
            // 
            // btnLoadVideo
            // 
            btnLoadVideo.Location = new Point(112, 93);
            btnLoadVideo.Margin = new Padding(3, 2, 3, 2);
            btnLoadVideo.Name = "btnLoadVideo";
            btnLoadVideo.Size = new Size(75, 25);
            btnLoadVideo.TabIndex = 49;
            btnLoadVideo.Text = "Load Video";
            btnLoadVideo.UseVisualStyleBackColor = true;
            btnLoadVideo.Click += btnLoadVideo_Click;
            // 
            // btnGetCameraInput
            // 
            btnGetCameraInput.Location = new Point(112, 64);
            btnGetCameraInput.Margin = new Padding(3, 2, 3, 2);
            btnGetCameraInput.Name = "btnGetCameraInput";
            btnGetCameraInput.Size = new Size(75, 25);
            btnGetCameraInput.TabIndex = 44;
            btnGetCameraInput.Text = "Load Cam";
            btnGetCameraInput.UseVisualStyleBackColor = true;
            btnGetCameraInput.Click += btnGetCameraInput_Click;
            // 
            // labelFrameRate
            // 
            labelFrameRate.AutoSize = true;
            labelFrameRate.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelFrameRate.Location = new Point(2, 96);
            labelFrameRate.Name = "labelFrameRate";
            labelFrameRate.Size = new Size(38, 19);
            labelFrameRate.TabIndex = 48;
            labelFrameRate.Text = "FPS: ";
            // 
            // btnDetectBalls
            // 
            btnDetectBalls.Enabled = false;
            btnDetectBalls.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnDetectBalls.Location = new Point(6, 169);
            btnDetectBalls.Margin = new Padding(3, 2, 3, 2);
            btnDetectBalls.Name = "btnDetectBalls";
            btnDetectBalls.Size = new Size(181, 25);
            btnDetectBalls.TabIndex = 47;
            btnDetectBalls.Text = "Start Ball Detection";
            btnDetectBalls.UseVisualStyleBackColor = true;
            btnDetectBalls.Click += btnDetectBalls_Click;
            // 
            // BilliardLaserForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1138, 683);
            Controls.Add(tableLayoutPanelVideoControls);
            Controls.Add(groupBoxShotInfo);
            Controls.Add(pictureBoxImage);
            Margin = new Padding(3, 2, 3, 2);
            Name = "BilliardLaserForm";
            Text = "Billiard Laser";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAccelerationOverTime).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxDistanceOverTime).EndInit();
            groupBoxShotInfo.ResumeLayout(false);
            groupBoxShotInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSpeedOverTime).EndInit();
            tableLayoutPanelVideoControls.ResumeLayout(false);
            panelMediaControls.ResumeLayout(false);
            panelMediaControls.PerformLayout();
            panelListBoxes.ResumeLayout(false);
            panelVideoControls.ResumeLayout(false);
            panelVideoControls.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private PictureBox pictureBoxImage;
        private Label labelShotInfo;
        private PictureBox pictureBoxAccelerationOverTime;
        private PictureBox pictureBoxDistanceOverTime;
        private GroupBox groupBoxShotInfo;
        private Label labelSpeed;
        private Label Acceleration;
        private Label labelDistance;
        private Label labelMaxSpeed;
        private Label labelAvgSpeed;
        private Label labelAverageAcceleration;
        private Label labelMaxAcceleration;
        private Label labelDistanceTravelled;
        private PictureBox pictureBoxSpeedOverTime;
        private TableLayoutPanel tableLayoutPanelVideoControls;
        private GroupBox groupBoxVideoControls;
        private Panel panelVideoControls;
        private Label labelShots;
        private Label labelFrames;
        private Button buttonShowDebugForm;
        private Label labelCameras;
        private ComboBox cboCamera;
        private Label labelVideoControls;
        private Button btnLoadVideo;
        private Button btnGetCameraInput;
        private Label labelFrameRate;
        private Button btnDetectBalls;
        private Panel panelListBoxes;
        private ListBox listBoxProcessedFrames;
        private ListBox listBoxShots;
        private Panel panelMediaControls;
        private Button buttonLastFrame;
        private Button buttonResume;
        private Button buttonNextFrame;
        private Button buttonPause;
        private Label labelMediaControls;
    }
}