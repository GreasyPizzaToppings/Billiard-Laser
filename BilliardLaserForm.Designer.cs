
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
            tableLayoutParent = new TableLayoutPanel();
            tableLayoutShotInfo = new TableLayoutPanel();
            tableLayoutDistance = new TableLayoutPanel();
            labelDistanceTravelled = new Label();
            pictureBoxDistanceOverTime = new PictureBox();
            labelDistance = new Label();
            tableLayoutAcceleration = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            labelAverageAcceleration = new Label();
            labelMaxAcceleration = new Label();
            Acceleration = new Label();
            pictureBoxAccelerationOverTime = new PictureBox();
            labelShotInfo = new Label();
            tableLayoutSpeed = new TableLayoutPanel();
            labelSpeed = new Label();
            pictureBoxSpeedOverTime = new PictureBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            labelMaxSpeed = new Label();
            labelAvgSpeed = new Label();
            tableLayoutVideoControls = new TableLayoutPanel();
            labelVideoControls = new Label();
            btnLoadVideo = new Button();
            buttonShowDebugForm = new Button();
            btnDetectBalls = new Button();
            labelFrameRate = new Label();
            tableLayoutCameraInput = new TableLayoutPanel();
            labelCameras = new Label();
            cboCamera = new ComboBox();
            btnGetCameraInput = new Button();
            tableLayoutListBoxes = new TableLayoutPanel();
            labelFrames = new Label();
            listBoxShots = new ListBox();
            labelFoundShots = new Label();
            listBoxProcessedFrames = new ListBox();
            tableLayoutMediaControls = new TableLayoutPanel();
            buttonPause = new Button();
            buttonLastFrame = new Button();
            buttonResume = new Button();
            buttonNextFrame = new Button();
            pictureBoxImage = new PictureBox();
            tableLayoutDebugControls = new TableLayoutPanel();
            tableLayoutDebugArduino = new TableLayoutPanel();
            tableLayoutServoControls = new TableLayoutPanel();
            btnLeft = new Button();
            btnRight = new Button();
            tableLayoutServoUpDown = new TableLayoutPanel();
            btnUp = new Button();
            btnDown = new Button();
            lblServos = new Label();
            btnLaserOn = new Button();
            btnLaserOff = new Button();
            tableLayoutDebugButtons = new TableLayoutPanel();
            btnLoadImage = new Button();
            findFindAllBalls = new Button();
            btnFindCueball = new Button();
            tableLayoutParent.SuspendLayout();
            tableLayoutShotInfo.SuspendLayout();
            tableLayoutDistance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxDistanceOverTime).BeginInit();
            tableLayoutAcceleration.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAccelerationOverTime).BeginInit();
            tableLayoutSpeed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSpeedOverTime).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutVideoControls.SuspendLayout();
            tableLayoutCameraInput.SuspendLayout();
            tableLayoutListBoxes.SuspendLayout();
            tableLayoutMediaControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxImage).BeginInit();
            tableLayoutDebugControls.SuspendLayout();
            tableLayoutDebugArduino.SuspendLayout();
            tableLayoutServoControls.SuspendLayout();
            tableLayoutServoUpDown.SuspendLayout();
            tableLayoutDebugButtons.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutParent
            // 
            tableLayoutParent.ColumnCount = 4;
            tableLayoutParent.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            tableLayoutParent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tableLayoutParent.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            tableLayoutParent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutParent.Controls.Add(tableLayoutShotInfo, 3, 0);
            tableLayoutParent.Controls.Add(tableLayoutVideoControls, 2, 0);
            tableLayoutParent.Controls.Add(pictureBoxImage, 1, 0);
            tableLayoutParent.Controls.Add(tableLayoutDebugControls, 0, 0);
            tableLayoutParent.Dock = DockStyle.Fill;
            tableLayoutParent.Location = new Point(0, 0);
            tableLayoutParent.Name = "tableLayoutParent";
            tableLayoutParent.RowCount = 1;
            tableLayoutParent.RowStyles.Add(new RowStyle());
            tableLayoutParent.Size = new Size(1515, 803);
            tableLayoutParent.TabIndex = 47;
            // 
            // tableLayoutShotInfo
            // 
            tableLayoutShotInfo.ColumnCount = 1;
            tableLayoutShotInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutShotInfo.Controls.Add(tableLayoutDistance, 0, 3);
            tableLayoutShotInfo.Controls.Add(tableLayoutAcceleration, 0, 2);
            tableLayoutShotInfo.Controls.Add(labelShotInfo, 0, 0);
            tableLayoutShotInfo.Controls.Add(tableLayoutSpeed, 0, 1);
            tableLayoutShotInfo.Dock = DockStyle.Fill;
            tableLayoutShotInfo.Location = new Point(1168, 3);
            tableLayoutShotInfo.Name = "tableLayoutShotInfo";
            tableLayoutShotInfo.RowCount = 4;
            tableLayoutShotInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutShotInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutShotInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutShotInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutShotInfo.Size = new Size(344, 797);
            tableLayoutShotInfo.TabIndex = 51;
            // 
            // tableLayoutDistance
            // 
            tableLayoutDistance.ColumnCount = 2;
            tableLayoutDistance.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutDistance.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutDistance.Controls.Add(labelDistanceTravelled, 1, 1);
            tableLayoutDistance.Controls.Add(pictureBoxDistanceOverTime, 0, 1);
            tableLayoutDistance.Controls.Add(labelDistance, 0, 0);
            tableLayoutDistance.Dock = DockStyle.Fill;
            tableLayoutDistance.Location = new Point(3, 547);
            tableLayoutDistance.Name = "tableLayoutDistance";
            tableLayoutDistance.RowCount = 2;
            tableLayoutDistance.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutDistance.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutDistance.Size = new Size(338, 247);
            tableLayoutDistance.TabIndex = 34;
            // 
            // labelDistanceTravelled
            // 
            labelDistanceTravelled.AutoSize = true;
            labelDistanceTravelled.Dock = DockStyle.Fill;
            labelDistanceTravelled.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelDistanceTravelled.Location = new Point(241, 30);
            labelDistanceTravelled.Name = "labelDistanceTravelled";
            labelDistanceTravelled.Size = new Size(94, 217);
            labelDistanceTravelled.TabIndex = 41;
            labelDistanceTravelled.Text = "Total:";
            labelDistanceTravelled.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pictureBoxDistanceOverTime
            // 
            pictureBoxDistanceOverTime.Dock = DockStyle.Fill;
            pictureBoxDistanceOverTime.Location = new Point(3, 32);
            pictureBoxDistanceOverTime.Margin = new Padding(3, 2, 3, 2);
            pictureBoxDistanceOverTime.Name = "pictureBoxDistanceOverTime";
            pictureBoxDistanceOverTime.Size = new Size(232, 213);
            pictureBoxDistanceOverTime.TabIndex = 34;
            pictureBoxDistanceOverTime.TabStop = false;
            // 
            // labelDistance
            // 
            labelDistance.AutoSize = true;
            labelDistance.Dock = DockStyle.Fill;
            labelDistance.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelDistance.Location = new Point(3, 0);
            labelDistance.Name = "labelDistance";
            labelDistance.Size = new Size(232, 30);
            labelDistance.TabIndex = 37;
            labelDistance.Text = "Distance Travelled";
            // 
            // tableLayoutAcceleration
            // 
            tableLayoutAcceleration.ColumnCount = 2;
            tableLayoutAcceleration.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutAcceleration.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutAcceleration.Controls.Add(tableLayoutPanel3, 1, 1);
            tableLayoutAcceleration.Controls.Add(Acceleration, 0, 0);
            tableLayoutAcceleration.Controls.Add(pictureBoxAccelerationOverTime, 0, 1);
            tableLayoutAcceleration.Dock = DockStyle.Fill;
            tableLayoutAcceleration.Location = new Point(3, 295);
            tableLayoutAcceleration.Name = "tableLayoutAcceleration";
            tableLayoutAcceleration.RowCount = 2;
            tableLayoutAcceleration.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutAcceleration.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutAcceleration.Size = new Size(338, 246);
            tableLayoutAcceleration.TabIndex = 33;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(labelAverageAcceleration, 0, 1);
            tableLayoutPanel3.Controls.Add(labelMaxAcceleration, 0, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(241, 33);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(94, 210);
            tableLayoutPanel3.TabIndex = 37;
            // 
            // labelAverageAcceleration
            // 
            labelAverageAcceleration.AutoSize = true;
            labelAverageAcceleration.Dock = DockStyle.Fill;
            labelAverageAcceleration.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelAverageAcceleration.Location = new Point(3, 105);
            labelAverageAcceleration.Name = "labelAverageAcceleration";
            labelAverageAcceleration.Size = new Size(88, 105);
            labelAverageAcceleration.TabIndex = 43;
            labelAverageAcceleration.Text = "Avg:";
            labelAverageAcceleration.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelMaxAcceleration
            // 
            labelMaxAcceleration.AutoSize = true;
            labelMaxAcceleration.Dock = DockStyle.Fill;
            labelMaxAcceleration.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelMaxAcceleration.Location = new Point(3, 0);
            labelMaxAcceleration.Name = "labelMaxAcceleration";
            labelMaxAcceleration.Size = new Size(88, 105);
            labelMaxAcceleration.TabIndex = 42;
            labelMaxAcceleration.Text = "Max:";
            labelMaxAcceleration.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Acceleration
            // 
            Acceleration.AutoSize = true;
            Acceleration.Dock = DockStyle.Fill;
            Acceleration.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            Acceleration.Location = new Point(3, 0);
            Acceleration.Name = "Acceleration";
            Acceleration.Size = new Size(232, 30);
            Acceleration.TabIndex = 38;
            Acceleration.Text = "Acceleration";
            // 
            // pictureBoxAccelerationOverTime
            // 
            pictureBoxAccelerationOverTime.Dock = DockStyle.Fill;
            pictureBoxAccelerationOverTime.Location = new Point(3, 32);
            pictureBoxAccelerationOverTime.Margin = new Padding(3, 2, 3, 2);
            pictureBoxAccelerationOverTime.Name = "pictureBoxAccelerationOverTime";
            pictureBoxAccelerationOverTime.Size = new Size(232, 212);
            pictureBoxAccelerationOverTime.TabIndex = 33;
            pictureBoxAccelerationOverTime.TabStop = false;
            // 
            // labelShotInfo
            // 
            labelShotInfo.AutoSize = true;
            labelShotInfo.Dock = DockStyle.Fill;
            labelShotInfo.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            labelShotInfo.Location = new Point(3, 0);
            labelShotInfo.Name = "labelShotInfo";
            labelShotInfo.Size = new Size(338, 40);
            labelShotInfo.TabIndex = 31;
            labelShotInfo.Text = "Shot Info";
            labelShotInfo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutSpeed
            // 
            tableLayoutSpeed.ColumnCount = 2;
            tableLayoutSpeed.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutSpeed.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutSpeed.Controls.Add(labelSpeed, 0, 0);
            tableLayoutSpeed.Controls.Add(pictureBoxSpeedOverTime, 0, 1);
            tableLayoutSpeed.Controls.Add(tableLayoutPanel1, 1, 1);
            tableLayoutSpeed.Dock = DockStyle.Fill;
            tableLayoutSpeed.Location = new Point(3, 43);
            tableLayoutSpeed.Name = "tableLayoutSpeed";
            tableLayoutSpeed.RowCount = 2;
            tableLayoutSpeed.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutSpeed.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutSpeed.Size = new Size(338, 246);
            tableLayoutSpeed.TabIndex = 32;
            // 
            // labelSpeed
            // 
            labelSpeed.AutoSize = true;
            labelSpeed.Dock = DockStyle.Fill;
            labelSpeed.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelSpeed.Location = new Point(3, 0);
            labelSpeed.Name = "labelSpeed";
            labelSpeed.Size = new Size(232, 30);
            labelSpeed.TabIndex = 36;
            labelSpeed.Text = "Speed";
            // 
            // pictureBoxSpeedOverTime
            // 
            pictureBoxSpeedOverTime.Dock = DockStyle.Fill;
            pictureBoxSpeedOverTime.Location = new Point(3, 32);
            pictureBoxSpeedOverTime.Margin = new Padding(3, 2, 3, 2);
            pictureBoxSpeedOverTime.Name = "pictureBoxSpeedOverTime";
            pictureBoxSpeedOverTime.Size = new Size(232, 212);
            pictureBoxSpeedOverTime.TabIndex = 32;
            pictureBoxSpeedOverTime.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(labelMaxSpeed, 0, 0);
            tableLayoutPanel1.Controls.Add(labelAvgSpeed, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(241, 33);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(94, 210);
            tableLayoutPanel1.TabIndex = 37;
            // 
            // labelMaxSpeed
            // 
            labelMaxSpeed.AutoSize = true;
            labelMaxSpeed.Dock = DockStyle.Fill;
            labelMaxSpeed.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelMaxSpeed.Location = new Point(3, 0);
            labelMaxSpeed.Name = "labelMaxSpeed";
            labelMaxSpeed.Size = new Size(88, 105);
            labelMaxSpeed.TabIndex = 39;
            labelMaxSpeed.Text = "Max:";
            labelMaxSpeed.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelAvgSpeed
            // 
            labelAvgSpeed.AutoSize = true;
            labelAvgSpeed.Dock = DockStyle.Fill;
            labelAvgSpeed.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelAvgSpeed.Location = new Point(3, 105);
            labelAvgSpeed.Name = "labelAvgSpeed";
            labelAvgSpeed.Size = new Size(88, 105);
            labelAvgSpeed.TabIndex = 40;
            labelAvgSpeed.Text = "Avg:";
            labelAvgSpeed.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tableLayoutVideoControls
            // 
            tableLayoutVideoControls.ColumnCount = 1;
            tableLayoutVideoControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutVideoControls.Controls.Add(labelVideoControls, 0, 0);
            tableLayoutVideoControls.Controls.Add(btnLoadVideo, 0, 2);
            tableLayoutVideoControls.Controls.Add(buttonShowDebugForm, 0, 3);
            tableLayoutVideoControls.Controls.Add(btnDetectBalls, 0, 4);
            tableLayoutVideoControls.Controls.Add(labelFrameRate, 0, 6);
            tableLayoutVideoControls.Controls.Add(tableLayoutCameraInput, 0, 1);
            tableLayoutVideoControls.Controls.Add(tableLayoutListBoxes, 0, 5);
            tableLayoutVideoControls.Controls.Add(tableLayoutMediaControls, 0, 7);
            tableLayoutVideoControls.Dock = DockStyle.Fill;
            tableLayoutVideoControls.Location = new Point(968, 3);
            tableLayoutVideoControls.Name = "tableLayoutVideoControls";
            tableLayoutVideoControls.RowCount = 8;
            tableLayoutVideoControls.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutVideoControls.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            tableLayoutVideoControls.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutVideoControls.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutVideoControls.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutVideoControls.RowStyles.Add(new RowStyle());
            tableLayoutVideoControls.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutVideoControls.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tableLayoutVideoControls.Size = new Size(194, 797);
            tableLayoutVideoControls.TabIndex = 52;
            // 
            // labelVideoControls
            // 
            labelVideoControls.AutoSize = true;
            labelVideoControls.Dock = DockStyle.Fill;
            labelVideoControls.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            labelVideoControls.Location = new Point(3, 0);
            labelVideoControls.Name = "labelVideoControls";
            labelVideoControls.Size = new Size(188, 40);
            labelVideoControls.TabIndex = 35;
            labelVideoControls.Text = "Video Controls";
            labelVideoControls.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnLoadVideo
            // 
            btnLoadVideo.Dock = DockStyle.Fill;
            btnLoadVideo.Location = new Point(3, 112);
            btnLoadVideo.Margin = new Padding(3, 2, 3, 2);
            btnLoadVideo.Name = "btnLoadVideo";
            btnLoadVideo.Size = new Size(188, 26);
            btnLoadVideo.TabIndex = 34;
            btnLoadVideo.Text = "Load Video";
            btnLoadVideo.UseVisualStyleBackColor = true;
            // 
            // buttonShowDebugForm
            // 
            buttonShowDebugForm.Dock = DockStyle.Fill;
            buttonShowDebugForm.Location = new Point(3, 142);
            buttonShowDebugForm.Margin = new Padding(3, 2, 3, 2);
            buttonShowDebugForm.Name = "buttonShowDebugForm";
            buttonShowDebugForm.Size = new Size(188, 26);
            buttonShowDebugForm.TabIndex = 43;
            buttonShowDebugForm.Text = "Show Debug Form";
            buttonShowDebugForm.UseVisualStyleBackColor = true;
            // 
            // btnDetectBalls
            // 
            btnDetectBalls.Dock = DockStyle.Fill;
            btnDetectBalls.Enabled = false;
            btnDetectBalls.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnDetectBalls.Location = new Point(3, 172);
            btnDetectBalls.Margin = new Padding(3, 2, 3, 2);
            btnDetectBalls.Name = "btnDetectBalls";
            btnDetectBalls.Size = new Size(188, 26);
            btnDetectBalls.TabIndex = 30;
            btnDetectBalls.Text = "Start Ball Detection";
            btnDetectBalls.UseVisualStyleBackColor = true;
            // 
            // labelFrameRate
            // 
            labelFrameRate.AutoSize = true;
            labelFrameRate.Dock = DockStyle.Fill;
            labelFrameRate.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelFrameRate.Location = new Point(3, 683);
            labelFrameRate.Name = "labelFrameRate";
            labelFrameRate.Size = new Size(188, 30);
            labelFrameRate.TabIndex = 31;
            labelFrameRate.Text = "FPS: ";
            // 
            // tableLayoutCameraInput
            // 
            tableLayoutCameraInput.ColumnCount = 2;
            tableLayoutCameraInput.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutCameraInput.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutCameraInput.Controls.Add(labelCameras, 0, 0);
            tableLayoutCameraInput.Controls.Add(cboCamera, 0, 1);
            tableLayoutCameraInput.Controls.Add(btnGetCameraInput, 1, 1);
            tableLayoutCameraInput.Dock = DockStyle.Fill;
            tableLayoutCameraInput.Location = new Point(3, 43);
            tableLayoutCameraInput.Name = "tableLayoutCameraInput";
            tableLayoutCameraInput.RowCount = 2;
            tableLayoutCameraInput.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutCameraInput.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutCameraInput.Size = new Size(188, 64);
            tableLayoutCameraInput.TabIndex = 44;
            // 
            // labelCameras
            // 
            labelCameras.AutoSize = true;
            labelCameras.Dock = DockStyle.Fill;
            labelCameras.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelCameras.Location = new Point(3, 0);
            labelCameras.Name = "labelCameras";
            labelCameras.Size = new Size(88, 30);
            labelCameras.TabIndex = 12;
            labelCameras.Text = "Cameras";
            labelCameras.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cboCamera
            // 
            cboCamera.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            cboCamera.FormattingEnabled = true;
            cboCamera.Location = new Point(3, 33);
            cboCamera.Name = "cboCamera";
            cboCamera.Size = new Size(88, 28);
            cboCamera.TabIndex = 11;
            // 
            // btnGetCameraInput
            // 
            btnGetCameraInput.Dock = DockStyle.Fill;
            btnGetCameraInput.Location = new Point(97, 32);
            btnGetCameraInput.Margin = new Padding(3, 2, 3, 2);
            btnGetCameraInput.Name = "btnGetCameraInput";
            btnGetCameraInput.Size = new Size(88, 30);
            btnGetCameraInput.TabIndex = 5;
            btnGetCameraInput.Text = "Load Cam";
            btnGetCameraInput.UseVisualStyleBackColor = true;
            // 
            // tableLayoutListBoxes
            // 
            tableLayoutListBoxes.ColumnCount = 2;
            tableLayoutListBoxes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutListBoxes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutListBoxes.Controls.Add(labelFrames, 0, 0);
            tableLayoutListBoxes.Controls.Add(listBoxShots, 1, 1);
            tableLayoutListBoxes.Controls.Add(labelFoundShots, 1, 0);
            tableLayoutListBoxes.Controls.Add(listBoxProcessedFrames, 0, 1);
            tableLayoutListBoxes.Dock = DockStyle.Fill;
            tableLayoutListBoxes.Location = new Point(3, 203);
            tableLayoutListBoxes.Name = "tableLayoutListBoxes";
            tableLayoutListBoxes.RowCount = 2;
            tableLayoutListBoxes.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutListBoxes.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutListBoxes.Size = new Size(188, 477);
            tableLayoutListBoxes.TabIndex = 45;
            // 
            // labelFrames
            // 
            labelFrames.AutoSize = true;
            labelFrames.Dock = DockStyle.Fill;
            labelFrames.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelFrames.Location = new Point(3, 0);
            labelFrames.Name = "labelFrames";
            labelFrames.Size = new Size(88, 30);
            labelFrames.TabIndex = 33;
            labelFrames.Text = "Frames";
            labelFrames.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // listBoxShots
            // 
            listBoxShots.Dock = DockStyle.Fill;
            listBoxShots.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            listBoxShots.FormattingEnabled = true;
            listBoxShots.Location = new Point(97, 32);
            listBoxShots.Margin = new Padding(3, 2, 3, 2);
            listBoxShots.Name = "listBoxShots";
            listBoxShots.Size = new Size(88, 443);
            listBoxShots.TabIndex = 39;
            // 
            // labelFoundShots
            // 
            labelFoundShots.AutoSize = true;
            labelFoundShots.Dock = DockStyle.Fill;
            labelFoundShots.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelFoundShots.Location = new Point(97, 0);
            labelFoundShots.Name = "labelFoundShots";
            labelFoundShots.Size = new Size(88, 30);
            labelFoundShots.TabIndex = 40;
            labelFoundShots.Text = "Shots Found";
            labelFoundShots.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // listBoxProcessedFrames
            // 
            listBoxProcessedFrames.Dock = DockStyle.Fill;
            listBoxProcessedFrames.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            listBoxProcessedFrames.FormattingEnabled = true;
            listBoxProcessedFrames.Location = new Point(3, 32);
            listBoxProcessedFrames.Margin = new Padding(3, 2, 3, 2);
            listBoxProcessedFrames.Name = "listBoxProcessedFrames";
            listBoxProcessedFrames.Size = new Size(88, 443);
            listBoxProcessedFrames.TabIndex = 32;
            // 
            // tableLayoutMediaControls
            // 
            tableLayoutMediaControls.ColumnCount = 2;
            tableLayoutMediaControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutMediaControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutMediaControls.Controls.Add(buttonPause, 1, 1);
            tableLayoutMediaControls.Controls.Add(buttonLastFrame, 0, 0);
            tableLayoutMediaControls.Controls.Add(buttonResume, 0, 1);
            tableLayoutMediaControls.Controls.Add(buttonNextFrame, 1, 0);
            tableLayoutMediaControls.Dock = DockStyle.Fill;
            tableLayoutMediaControls.Location = new Point(3, 716);
            tableLayoutMediaControls.Name = "tableLayoutMediaControls";
            tableLayoutMediaControls.RowCount = 2;
            tableLayoutMediaControls.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutMediaControls.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutMediaControls.Size = new Size(188, 78);
            tableLayoutMediaControls.TabIndex = 46;
            // 
            // buttonPause
            // 
            buttonPause.Dock = DockStyle.Fill;
            buttonPause.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonPause.Location = new Point(97, 41);
            buttonPause.Margin = new Padding(3, 2, 3, 2);
            buttonPause.Name = "buttonPause";
            buttonPause.Size = new Size(88, 35);
            buttonPause.TabIndex = 41;
            buttonPause.Text = "Pause";
            buttonPause.UseVisualStyleBackColor = true;
            // 
            // buttonLastFrame
            // 
            buttonLastFrame.Dock = DockStyle.Fill;
            buttonLastFrame.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonLastFrame.Location = new Point(3, 2);
            buttonLastFrame.Margin = new Padding(3, 2, 3, 2);
            buttonLastFrame.Name = "buttonLastFrame";
            buttonLastFrame.Size = new Size(88, 35);
            buttonLastFrame.TabIndex = 36;
            buttonLastFrame.Text = "Last Frame";
            buttonLastFrame.UseVisualStyleBackColor = true;
            // 
            // buttonResume
            // 
            buttonResume.Dock = DockStyle.Fill;
            buttonResume.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonResume.Location = new Point(3, 41);
            buttonResume.Margin = new Padding(3, 2, 3, 2);
            buttonResume.Name = "buttonResume";
            buttonResume.Size = new Size(88, 35);
            buttonResume.TabIndex = 38;
            buttonResume.Text = "Resume";
            buttonResume.UseVisualStyleBackColor = true;
            // 
            // buttonNextFrame
            // 
            buttonNextFrame.Dock = DockStyle.Fill;
            buttonNextFrame.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonNextFrame.Location = new Point(97, 2);
            buttonNextFrame.Margin = new Padding(3, 2, 3, 2);
            buttonNextFrame.Name = "buttonNextFrame";
            buttonNextFrame.Size = new Size(88, 35);
            buttonNextFrame.TabIndex = 37;
            buttonNextFrame.Text = "Next Frame";
            buttonNextFrame.UseVisualStyleBackColor = true;
            // 
            // pictureBoxImage
            // 
            pictureBoxImage.Dock = DockStyle.Fill;
            pictureBoxImage.Image = (Image)resources.GetObject("pictureBoxImage.Image");
            pictureBoxImage.Location = new Point(153, 2);
            pictureBoxImage.Margin = new Padding(3, 2, 3, 2);
            pictureBoxImage.Name = "pictureBoxImage";
            pictureBoxImage.Size = new Size(809, 799);
            pictureBoxImage.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxImage.TabIndex = 53;
            pictureBoxImage.TabStop = false;
            // 
            // tableLayoutDebugControls
            // 
            tableLayoutDebugControls.ColumnCount = 1;
            tableLayoutDebugControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutDebugControls.Controls.Add(tableLayoutDebugArduino, 0, 2);
            tableLayoutDebugControls.Controls.Add(tableLayoutDebugButtons, 0, 0);
            tableLayoutDebugControls.Dock = DockStyle.Fill;
            tableLayoutDebugControls.Location = new Point(3, 3);
            tableLayoutDebugControls.Name = "tableLayoutDebugControls";
            tableLayoutDebugControls.RowCount = 3;
            tableLayoutDebugControls.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutDebugControls.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutDebugControls.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutDebugControls.Size = new Size(144, 797);
            tableLayoutDebugControls.TabIndex = 48;
            // 
            // tableLayoutDebugArduino
            // 
            tableLayoutDebugArduino.ColumnCount = 1;
            tableLayoutDebugArduino.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutDebugArduino.Controls.Add(tableLayoutServoControls, 0, 2);
            tableLayoutDebugArduino.Controls.Add(btnLaserOn, 0, 0);
            tableLayoutDebugArduino.Controls.Add(btnLaserOff, 0, 1);
            tableLayoutDebugArduino.Dock = DockStyle.Fill;
            tableLayoutDebugArduino.ForeColor = Color.CornflowerBlue;
            tableLayoutDebugArduino.Location = new Point(3, 600);
            tableLayoutDebugArduino.Name = "tableLayoutDebugArduino";
            tableLayoutDebugArduino.RowCount = 3;
            tableLayoutDebugArduino.RowStyles.Add(new RowStyle());
            tableLayoutDebugArduino.RowStyles.Add(new RowStyle());
            tableLayoutDebugArduino.RowStyles.Add(new RowStyle());
            tableLayoutDebugArduino.Size = new Size(138, 194);
            tableLayoutDebugArduino.TabIndex = 51;
            // 
            // tableLayoutServoControls
            // 
            tableLayoutServoControls.ColumnCount = 3;
            tableLayoutServoControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutServoControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutServoControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutServoControls.Controls.Add(btnLeft, 0, 1);
            tableLayoutServoControls.Controls.Add(btnRight, 2, 1);
            tableLayoutServoControls.Controls.Add(tableLayoutServoUpDown, 1, 1);
            tableLayoutServoControls.Controls.Add(lblServos, 1, 0);
            tableLayoutServoControls.Dock = DockStyle.Fill;
            tableLayoutServoControls.ForeColor = Color.Black;
            tableLayoutServoControls.Location = new Point(3, 61);
            tableLayoutServoControls.Name = "tableLayoutServoControls";
            tableLayoutServoControls.RowCount = 2;
            tableLayoutServoControls.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableLayoutServoControls.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutServoControls.Size = new Size(132, 130);
            tableLayoutServoControls.TabIndex = 54;
            // 
            // btnLeft
            // 
            btnLeft.Dock = DockStyle.Fill;
            btnLeft.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnLeft.Location = new Point(3, 27);
            btnLeft.Margin = new Padding(3, 2, 3, 2);
            btnLeft.Name = "btnLeft";
            btnLeft.Size = new Size(38, 101);
            btnLeft.TabIndex = 6;
            btnLeft.Text = "←";
            btnLeft.UseVisualStyleBackColor = true;
            // 
            // btnRight
            // 
            btnRight.Dock = DockStyle.Fill;
            btnRight.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnRight.Location = new Point(91, 27);
            btnRight.Margin = new Padding(3, 2, 3, 2);
            btnRight.Name = "btnRight";
            btnRight.Size = new Size(38, 101);
            btnRight.TabIndex = 7;
            btnRight.Text = "→";
            btnRight.UseVisualStyleBackColor = true;
            // 
            // tableLayoutServoUpDown
            // 
            tableLayoutServoUpDown.ColumnCount = 1;
            tableLayoutServoUpDown.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutServoUpDown.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutServoUpDown.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutServoUpDown.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutServoUpDown.Controls.Add(btnUp, 0, 0);
            tableLayoutServoUpDown.Controls.Add(btnDown, 0, 1);
            tableLayoutServoUpDown.Dock = DockStyle.Fill;
            tableLayoutServoUpDown.Location = new Point(47, 28);
            tableLayoutServoUpDown.Name = "tableLayoutServoUpDown";
            tableLayoutServoUpDown.RowCount = 2;
            tableLayoutServoUpDown.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutServoUpDown.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutServoUpDown.Size = new Size(38, 99);
            tableLayoutServoUpDown.TabIndex = 48;
            // 
            // btnUp
            // 
            btnUp.Dock = DockStyle.Fill;
            btnUp.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnUp.Location = new Point(3, 2);
            btnUp.Margin = new Padding(3, 2, 3, 2);
            btnUp.Name = "btnUp";
            btnUp.Size = new Size(32, 45);
            btnUp.TabIndex = 8;
            btnUp.Text = "↑";
            btnUp.UseVisualStyleBackColor = true;
            // 
            // btnDown
            // 
            btnDown.Dock = DockStyle.Fill;
            btnDown.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnDown.Location = new Point(3, 51);
            btnDown.Margin = new Padding(3, 2, 3, 2);
            btnDown.Name = "btnDown";
            btnDown.Size = new Size(32, 46);
            btnDown.TabIndex = 9;
            btnDown.Text = "↓";
            btnDown.UseVisualStyleBackColor = true;
            // 
            // lblServos
            // 
            lblServos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblServos.AutoSize = true;
            lblServos.Location = new Point(47, 0);
            lblServos.Name = "lblServos";
            lblServos.Size = new Size(38, 25);
            lblServos.TabIndex = 10;
            lblServos.Text = "Servos";
            lblServos.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnLaserOn
            // 
            btnLaserOn.Dock = DockStyle.Fill;
            btnLaserOn.ForeColor = Color.Black;
            btnLaserOn.Location = new Point(3, 2);
            btnLaserOn.Margin = new Padding(3, 2, 3, 2);
            btnLaserOn.Name = "btnLaserOn";
            btnLaserOn.Size = new Size(132, 25);
            btnLaserOn.TabIndex = 2;
            btnLaserOn.Text = "Laser ON";
            btnLaserOn.UseVisualStyleBackColor = true;
            // 
            // btnLaserOff
            // 
            btnLaserOff.Dock = DockStyle.Fill;
            btnLaserOff.ForeColor = Color.Black;
            btnLaserOff.Location = new Point(3, 31);
            btnLaserOff.Margin = new Padding(3, 2, 3, 2);
            btnLaserOff.Name = "btnLaserOff";
            btnLaserOff.Size = new Size(132, 25);
            btnLaserOff.TabIndex = 3;
            btnLaserOff.Text = "Laser OFF";
            btnLaserOff.UseVisualStyleBackColor = true;
            // 
            // tableLayoutDebugButtons
            // 
            tableLayoutDebugButtons.ColumnCount = 1;
            tableLayoutDebugButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutDebugButtons.Controls.Add(btnLoadImage, 0, 0);
            tableLayoutDebugButtons.Controls.Add(findFindAllBalls, 0, 2);
            tableLayoutDebugButtons.Controls.Add(btnFindCueball, 0, 1);
            tableLayoutDebugButtons.Dock = DockStyle.Fill;
            tableLayoutDebugButtons.Location = new Point(3, 3);
            tableLayoutDebugButtons.Name = "tableLayoutDebugButtons";
            tableLayoutDebugButtons.RowCount = 3;
            tableLayoutDebugButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutDebugButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutDebugButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutDebugButtons.Size = new Size(138, 193);
            tableLayoutDebugButtons.TabIndex = 48;
            // 
            // btnLoadImage
            // 
            btnLoadImage.Dock = DockStyle.Fill;
            btnLoadImage.Location = new Point(3, 2);
            btnLoadImage.Margin = new Padding(3, 2, 3, 2);
            btnLoadImage.Name = "btnLoadImage";
            btnLoadImage.Size = new Size(132, 26);
            btnLoadImage.TabIndex = 4;
            btnLoadImage.Text = "Load Img";
            btnLoadImage.UseVisualStyleBackColor = true;
            // 
            // findFindAllBalls
            // 
            findFindAllBalls.Dock = DockStyle.Fill;
            findFindAllBalls.Location = new Point(3, 62);
            findFindAllBalls.Margin = new Padding(3, 2, 3, 2);
            findFindAllBalls.Name = "findFindAllBalls";
            findFindAllBalls.Size = new Size(132, 129);
            findFindAllBalls.TabIndex = 45;
            findFindAllBalls.Text = "Find All Balls";
            findFindAllBalls.UseVisualStyleBackColor = true;
            // 
            // btnFindCueball
            // 
            btnFindCueball.Dock = DockStyle.Fill;
            btnFindCueball.Location = new Point(3, 32);
            btnFindCueball.Margin = new Padding(3, 2, 3, 2);
            btnFindCueball.Name = "btnFindCueball";
            btnFindCueball.Size = new Size(132, 26);
            btnFindCueball.TabIndex = 1;
            btnFindCueball.Text = "Find CB";
            btnFindCueball.UseVisualStyleBackColor = true;
            // 
            // BilliardLaserForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1515, 803);
            Controls.Add(tableLayoutParent);
            Margin = new Padding(3, 2, 3, 2);
            Name = "BilliardLaserForm";
            Text = "Billiard Laser";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            tableLayoutParent.ResumeLayout(false);
            tableLayoutShotInfo.ResumeLayout(false);
            tableLayoutShotInfo.PerformLayout();
            tableLayoutDistance.ResumeLayout(false);
            tableLayoutDistance.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxDistanceOverTime).EndInit();
            tableLayoutAcceleration.ResumeLayout(false);
            tableLayoutAcceleration.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAccelerationOverTime).EndInit();
            tableLayoutSpeed.ResumeLayout(false);
            tableLayoutSpeed.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSpeedOverTime).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutVideoControls.ResumeLayout(false);
            tableLayoutVideoControls.PerformLayout();
            tableLayoutCameraInput.ResumeLayout(false);
            tableLayoutCameraInput.PerformLayout();
            tableLayoutListBoxes.ResumeLayout(false);
            tableLayoutListBoxes.PerformLayout();
            tableLayoutMediaControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxImage).EndInit();
            tableLayoutDebugControls.ResumeLayout(false);
            tableLayoutDebugArduino.ResumeLayout(false);
            tableLayoutServoControls.ResumeLayout(false);
            tableLayoutServoControls.PerformLayout();
            tableLayoutServoUpDown.ResumeLayout(false);
            tableLayoutDebugButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private TableLayoutPanel tableLayoutParent;
        private TableLayoutPanel tableLayoutShotInfo;
        private TableLayoutPanel tableLayoutDistance;
        private Label labelDistanceTravelled;
        private PictureBox pictureBoxDistanceOverTime;
        private Label labelDistance;
        private TableLayoutPanel tableLayoutAcceleration;
        private TableLayoutPanel tableLayoutPanel3;
        private Label labelAverageAcceleration;
        private Label labelMaxAcceleration;
        private Label Acceleration;
        private PictureBox pictureBoxAccelerationOverTime;
        private Label labelShotInfo;
        private TableLayoutPanel tableLayoutSpeed;
        private Label labelSpeed;
        private PictureBox pictureBoxSpeedOverTime;
        private TableLayoutPanel tableLayoutPanel1;
        private Label labelMaxSpeed;
        private Label labelAvgSpeed;
        private TableLayoutPanel tableLayoutVideoControls;
        private Label labelVideoControls;
        private Button btnLoadVideo;
        private Button buttonShowDebugForm;
        private Button btnDetectBalls;
        private Label labelFrameRate;
        private TableLayoutPanel tableLayoutCameraInput;
        private Label labelCameras;
        private ComboBox cboCamera;
        private Button btnGetCameraInput;
        private TableLayoutPanel tableLayoutListBoxes;
        private Label labelFrames;
        private ListBox listBoxShots;
        private Label labelFoundShots;
        private ListBox listBoxProcessedFrames;
        private TableLayoutPanel tableLayoutMediaControls;
        private Button buttonPause;
        private Button buttonLastFrame;
        private Button buttonResume;
        private Button buttonNextFrame;
        private PictureBox pictureBoxImage;
        private TableLayoutPanel tableLayoutDebugControls;
        private TableLayoutPanel tableLayoutDebugArduino;
        private TableLayoutPanel tableLayoutServoControls;
        private Button btnLeft;
        private Button btnRight;
        private TableLayoutPanel tableLayoutServoUpDown;
        private Button btnUp;
        private Button btnDown;
        private Label lblServos;
        private Button btnLaserOn;
        private Button btnLaserOff;
        private TableLayoutPanel tableLayoutDebugButtons;
        private Button btnLoadImage;
        private Button findFindAllBalls;
        private Button btnFindCueball;
    }
}