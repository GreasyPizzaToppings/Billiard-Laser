﻿
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
            btnFindCueball = new Button();
            btnLaserOn = new Button();
            btnLaserOff = new Button();
            btnLoadImage = new Button();
            btnGetCameraInput = new Button();
            btnLeft = new Button();
            btnRight = new Button();
            btnUp = new Button();
            btnDown = new Button();
            lblServos = new Label();
            cboCamera = new ComboBox();
            labelCameras = new Label();
            pictureBoxImage = new PictureBox();
            labelShotInfo = new Label();
            pictureBoxSpeedOverTime = new PictureBox();
            pictureBoxAccelerationOverTime = new PictureBox();
            pictureBoxDistanceOverTime = new PictureBox();
            groupBoxShotInfo = new GroupBox();
            labelAverageAcceleration = new Label();
            labelMaxAcceleration = new Label();
            labelDistanceTravelled = new Label();
            labelAvgSpeed = new Label();
            labelMaxSpeed = new Label();
            Acceleration = new Label();
            labelDistance = new Label();
            labelSpeed = new Label();
            groupBoxVideoControls = new GroupBox();
            radioButtonAllBalls = new RadioButton();
            radioButtonCB = new RadioButton();
            buttonPause = new Button();
            label1 = new Label();
            listBoxShots = new ListBox();
            buttonResume = new Button();
            buttonNextFrame = new Button();
            buttonLastFrame = new Button();
            labelVideoControls = new Label();
            btnLoadVideo = new Button();
            labelFrames = new Label();
            listBoxProcessedFrames = new ListBox();
            labelFrameRate = new Label();
            btnProcessVideo = new Button();
            findColoredBalls = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBoxImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSpeedOverTime).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAccelerationOverTime).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxDistanceOverTime).BeginInit();
            groupBoxShotInfo.SuspendLayout();
            groupBoxVideoControls.SuspendLayout();
            SuspendLayout();
            // 
            // btnFindCueball
            // 
            btnFindCueball.Location = new Point(8, 176);
            btnFindCueball.Margin = new Padding(3, 2, 3, 2);
            btnFindCueball.Name = "btnFindCueball";
            btnFindCueball.Size = new Size(75, 25);
            btnFindCueball.TabIndex = 1;
            btnFindCueball.Text = "Find CB";
            btnFindCueball.UseVisualStyleBackColor = true;
            btnFindCueball.Click += btnFindCueball_Click;
            // 
            // btnLaserOn
            // 
            btnLaserOn.Location = new Point(9, 266);
            btnLaserOn.Margin = new Padding(3, 2, 3, 2);
            btnLaserOn.Name = "btnLaserOn";
            btnLaserOn.Size = new Size(75, 25);
            btnLaserOn.TabIndex = 2;
            btnLaserOn.Text = "Laser ON";
            btnLaserOn.UseVisualStyleBackColor = true;
            btnLaserOn.Click += btnLaserOn_Click;
            // 
            // btnLaserOff
            // 
            btnLaserOff.Location = new Point(8, 295);
            btnLaserOff.Margin = new Padding(3, 2, 3, 2);
            btnLaserOff.Name = "btnLaserOff";
            btnLaserOff.Size = new Size(75, 25);
            btnLaserOff.TabIndex = 3;
            btnLaserOff.Text = "Laser OFF";
            btnLaserOff.UseVisualStyleBackColor = true;
            btnLaserOff.Click += btnLaserOff_Click;
            // 
            // btnLoadImage
            // 
            btnLoadImage.Location = new Point(8, 126);
            btnLoadImage.Margin = new Padding(3, 2, 3, 2);
            btnLoadImage.Name = "btnLoadImage";
            btnLoadImage.Size = new Size(75, 25);
            btnLoadImage.TabIndex = 4;
            btnLoadImage.Text = "Load Img";
            btnLoadImage.UseVisualStyleBackColor = true;
            btnLoadImage.Click += btnLoadImage_Click;
            // 
            // btnGetCameraInput
            // 
            btnGetCameraInput.Location = new Point(8, 71);
            btnGetCameraInput.Margin = new Padding(3, 2, 3, 2);
            btnGetCameraInput.Name = "btnGetCameraInput";
            btnGetCameraInput.Size = new Size(75, 25);
            btnGetCameraInput.TabIndex = 5;
            btnGetCameraInput.Text = "Camera Input";
            btnGetCameraInput.UseVisualStyleBackColor = true;
            btnGetCameraInput.Click += btnGetCameraInput_Click;
            // 
            // btnLeft
            // 
            btnLeft.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnLeft.Location = new Point(9, 407);
            btnLeft.Margin = new Padding(3, 2, 3, 2);
            btnLeft.Name = "btnLeft";
            btnLeft.Size = new Size(31, 32);
            btnLeft.TabIndex = 6;
            btnLeft.Text = "←";
            btnLeft.UseVisualStyleBackColor = true;
            btnLeft.Click += btnLeft_Click;
            // 
            // btnRight
            // 
            btnRight.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnRight.Location = new Point(48, 407);
            btnRight.Margin = new Padding(3, 2, 3, 2);
            btnRight.Name = "btnRight";
            btnRight.Size = new Size(31, 32);
            btnRight.TabIndex = 7;
            btnRight.Text = "→";
            btnRight.UseVisualStyleBackColor = true;
            btnRight.Click += btnRight_Click;
            // 
            // btnUp
            // 
            btnUp.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnUp.Location = new Point(30, 371);
            btnUp.Margin = new Padding(3, 2, 3, 2);
            btnUp.Name = "btnUp";
            btnUp.Size = new Size(31, 32);
            btnUp.TabIndex = 8;
            btnUp.Text = "↑";
            btnUp.UseVisualStyleBackColor = true;
            btnUp.Click += btnUp_Click;
            // 
            // btnDown
            // 
            btnDown.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnDown.Location = new Point(30, 443);
            btnDown.Margin = new Padding(3, 2, 3, 2);
            btnDown.Name = "btnDown";
            btnDown.Size = new Size(31, 32);
            btnDown.TabIndex = 9;
            btnDown.Text = "↓";
            btnDown.UseVisualStyleBackColor = true;
            btnDown.Click += btnDown_Click;
            // 
            // lblServos
            // 
            lblServos.AutoSize = true;
            lblServos.Location = new Point(8, 352);
            lblServos.Name = "lblServos";
            lblServos.Size = new Size(84, 15);
            lblServos.TabIndex = 10;
            lblServos.Text = "Servo Controls";
            // 
            // cboCamera
            // 
            cboCamera.FormattingEnabled = true;
            cboCamera.Location = new Point(8, 37);
            cboCamera.Name = "cboCamera";
            cboCamera.Size = new Size(86, 23);
            cboCamera.TabIndex = 11;
            // 
            // labelCameras
            // 
            labelCameras.AutoSize = true;
            labelCameras.Location = new Point(9, 12);
            labelCameras.Name = "labelCameras";
            labelCameras.Size = new Size(82, 15);
            labelCameras.TabIndex = 12;
            labelCameras.Text = "Select Camera";
            // 
            // pictureBoxImage
            // 
            pictureBoxImage.Image = Properties.Resources.ballGrid2;
            pictureBoxImage.Location = new Point(101, 9);
            pictureBoxImage.Margin = new Padding(3, 2, 3, 2);
            pictureBoxImage.Name = "pictureBoxImage";
            pictureBoxImage.Size = new Size(766, 477);
            pictureBoxImage.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxImage.TabIndex = 15;
            pictureBoxImage.TabStop = false;
            // 
            // labelShotInfo
            // 
            labelShotInfo.AutoSize = true;
            labelShotInfo.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            labelShotInfo.Location = new Point(68, 10);
            labelShotInfo.Name = "labelShotInfo";
            labelShotInfo.Size = new Size(92, 28);
            labelShotInfo.TabIndex = 31;
            labelShotInfo.Text = "Shot Info";
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
            groupBoxShotInfo.Controls.Add(labelAverageAcceleration);
            groupBoxShotInfo.Controls.Add(labelMaxAcceleration);
            groupBoxShotInfo.Controls.Add(labelDistanceTravelled);
            groupBoxShotInfo.Controls.Add(labelAvgSpeed);
            groupBoxShotInfo.Controls.Add(labelMaxSpeed);
            groupBoxShotInfo.Controls.Add(Acceleration);
            groupBoxShotInfo.Controls.Add(labelDistance);
            groupBoxShotInfo.Controls.Add(labelSpeed);
            groupBoxShotInfo.Controls.Add(pictureBoxSpeedOverTime);
            groupBoxShotInfo.Controls.Add(pictureBoxDistanceOverTime);
            groupBoxShotInfo.Controls.Add(labelShotInfo);
            groupBoxShotInfo.Controls.Add(pictureBoxAccelerationOverTime);
            groupBoxShotInfo.Location = new Point(1074, -1);
            groupBoxShotInfo.Margin = new Padding(3, 2, 3, 2);
            groupBoxShotInfo.Name = "groupBoxShotInfo";
            groupBoxShotInfo.Padding = new Padding(3, 2, 3, 2);
            groupBoxShotInfo.Size = new Size(229, 487);
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
            // groupBoxVideoControls
            // 
            groupBoxVideoControls.Controls.Add(radioButtonAllBalls);
            groupBoxVideoControls.Controls.Add(radioButtonCB);
            groupBoxVideoControls.Controls.Add(buttonPause);
            groupBoxVideoControls.Controls.Add(label1);
            groupBoxVideoControls.Controls.Add(listBoxShots);
            groupBoxVideoControls.Controls.Add(buttonResume);
            groupBoxVideoControls.Controls.Add(buttonNextFrame);
            groupBoxVideoControls.Controls.Add(buttonLastFrame);
            groupBoxVideoControls.Controls.Add(labelVideoControls);
            groupBoxVideoControls.Controls.Add(btnLoadVideo);
            groupBoxVideoControls.Controls.Add(labelFrames);
            groupBoxVideoControls.Controls.Add(listBoxProcessedFrames);
            groupBoxVideoControls.Controls.Add(labelFrameRate);
            groupBoxVideoControls.Controls.Add(btnProcessVideo);
            groupBoxVideoControls.Location = new Point(872, -1);
            groupBoxVideoControls.Margin = new Padding(3, 2, 3, 2);
            groupBoxVideoControls.Name = "groupBoxVideoControls";
            groupBoxVideoControls.Padding = new Padding(3, 2, 3, 2);
            groupBoxVideoControls.Size = new Size(196, 487);
            groupBoxVideoControls.TabIndex = 44;
            groupBoxVideoControls.TabStop = false;
            // 
            // radioButtonAllBalls
            // 
            radioButtonAllBalls.AutoSize = true;
            radioButtonAllBalls.Checked = true;
            radioButtonAllBalls.Location = new Point(90, 176);
            radioButtonAllBalls.Name = "radioButtonAllBalls";
            radioButtonAllBalls.Size = new Size(92, 19);
            radioButtonAllBalls.TabIndex = 43;
            radioButtonAllBalls.TabStop = true;
            radioButtonAllBalls.Text = "Find All Balls";
            radioButtonAllBalls.UseVisualStyleBackColor = true;
            // 
            // radioButtonCB
            // 
            radioButtonCB.AutoSize = true;
            radioButtonCB.Location = new Point(18, 176);
            radioButtonCB.Name = "radioButtonCB";
            radioButtonCB.Size = new Size(66, 19);
            radioButtonCB.TabIndex = 42;
            radioButtonCB.Text = "Find CB";
            radioButtonCB.UseVisualStyleBackColor = true;
            // 
            // buttonPause
            // 
            buttonPause.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonPause.Location = new Point(99, 129);
            buttonPause.Margin = new Padding(3, 2, 3, 2);
            buttonPause.Name = "buttonPause";
            buttonPause.Size = new Size(75, 25);
            buttonPause.TabIndex = 41;
            buttonPause.Text = "Pause";
            buttonPause.UseVisualStyleBackColor = true;
            buttonPause.Click += buttonPause_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(100, 207);
            label1.Name = "label1";
            label1.Size = new Size(102, 19);
            label1.TabIndex = 40;
            label1.Text = "Detected Shots";
            // 
            // listBoxShots
            // 
            listBoxShots.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            listBoxShots.FormattingEnabled = true;
            listBoxShots.Location = new Point(103, 227);
            listBoxShots.Margin = new Padding(3, 2, 3, 2);
            listBoxShots.Name = "listBoxShots";
            listBoxShots.Size = new Size(87, 238);
            listBoxShots.TabIndex = 39;
            listBoxShots.SelectedIndexChanged += listBoxShots_SelectedIndexChanged;
            // 
            // buttonResume
            // 
            buttonResume.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonResume.Location = new Point(20, 129);
            buttonResume.Margin = new Padding(3, 2, 3, 2);
            buttonResume.Name = "buttonResume";
            buttonResume.Size = new Size(75, 25);
            buttonResume.TabIndex = 38;
            buttonResume.Text = "Resume";
            buttonResume.UseVisualStyleBackColor = true;
            buttonResume.Click += buttonResume_Click;
            // 
            // buttonNextFrame
            // 
            buttonNextFrame.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonNextFrame.Location = new Point(99, 104);
            buttonNextFrame.Margin = new Padding(3, 2, 3, 2);
            buttonNextFrame.Name = "buttonNextFrame";
            buttonNextFrame.Size = new Size(75, 25);
            buttonNextFrame.TabIndex = 37;
            buttonNextFrame.Text = "Next Frame";
            buttonNextFrame.UseVisualStyleBackColor = true;
            buttonNextFrame.Click += buttonNextFrame_Click;
            // 
            // buttonLastFrame
            // 
            buttonLastFrame.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonLastFrame.Location = new Point(20, 104);
            buttonLastFrame.Margin = new Padding(3, 2, 3, 2);
            buttonLastFrame.Name = "buttonLastFrame";
            buttonLastFrame.Size = new Size(75, 25);
            buttonLastFrame.TabIndex = 36;
            buttonLastFrame.Text = "Last Frame";
            buttonLastFrame.UseVisualStyleBackColor = true;
            buttonLastFrame.Click += buttonLastFrame_Click;
            // 
            // labelVideoControls
            // 
            labelVideoControls.AutoSize = true;
            labelVideoControls.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            labelVideoControls.Location = new Point(34, 10);
            labelVideoControls.Name = "labelVideoControls";
            labelVideoControls.Size = new Size(142, 28);
            labelVideoControls.TabIndex = 35;
            labelVideoControls.Text = "Video Controls";
            // 
            // btnLoadVideo
            // 
            btnLoadVideo.Location = new Point(20, 45);
            btnLoadVideo.Margin = new Padding(3, 2, 3, 2);
            btnLoadVideo.Name = "btnLoadVideo";
            btnLoadVideo.Size = new Size(75, 25);
            btnLoadVideo.TabIndex = 34;
            btnLoadVideo.Text = "Load Video";
            btnLoadVideo.UseVisualStyleBackColor = true;
            btnLoadVideo.Click += btnLoadVideo_Click;
            // 
            // labelFrames
            // 
            labelFrames.AutoSize = true;
            labelFrames.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelFrames.Location = new Point(11, 207);
            labelFrames.Name = "labelFrames";
            labelFrames.Size = new Size(53, 19);
            labelFrames.TabIndex = 33;
            labelFrames.Text = "Frames";
            // 
            // listBoxProcessedFrames
            // 
            listBoxProcessedFrames.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            listBoxProcessedFrames.FormattingEnabled = true;
            listBoxProcessedFrames.Location = new Point(11, 227);
            listBoxProcessedFrames.Margin = new Padding(3, 2, 3, 2);
            listBoxProcessedFrames.Name = "listBoxProcessedFrames";
            listBoxProcessedFrames.Size = new Size(87, 238);
            listBoxProcessedFrames.TabIndex = 32;
            listBoxProcessedFrames.SelectedIndexChanged += listBoxFrames_SelectedIndexChanged;
            // 
            // labelFrameRate
            // 
            labelFrameRate.AutoSize = true;
            labelFrameRate.Location = new Point(102, 72);
            labelFrameRate.Name = "labelFrameRate";
            labelFrameRate.Size = new Size(32, 15);
            labelFrameRate.TabIndex = 31;
            labelFrameRate.Text = "FPS: ";
            // 
            // btnProcessVideo
            // 
            btnProcessVideo.Enabled = false;
            btnProcessVideo.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnProcessVideo.Location = new Point(99, 45);
            btnProcessVideo.Margin = new Padding(3, 2, 3, 2);
            btnProcessVideo.Name = "btnProcessVideo";
            btnProcessVideo.Size = new Size(75, 25);
            btnProcessVideo.TabIndex = 30;
            btnProcessVideo.Text = "Start ";
            btnProcessVideo.UseVisualStyleBackColor = true;
            btnProcessVideo.Click += btnProcessVideo_ClickAsync;
            // 
            // findColoredBalls
            // 
            findColoredBalls.Location = new Point(8, 205);
            findColoredBalls.Margin = new Padding(3, 2, 3, 2);
            findColoredBalls.Name = "findColoredBalls";
            findColoredBalls.Size = new Size(75, 25);
            findColoredBalls.TabIndex = 45;
            findColoredBalls.Text = "Find All";
            findColoredBalls.UseVisualStyleBackColor = true;
            findColoredBalls.Click += findColoredBalls_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1305, 492);
            Controls.Add(findColoredBalls);
            Controls.Add(groupBoxVideoControls);
            Controls.Add(groupBoxShotInfo);
            Controls.Add(pictureBoxImage);
            Controls.Add(labelCameras);
            Controls.Add(cboCamera);
            Controls.Add(lblServos);
            Controls.Add(btnDown);
            Controls.Add(btnUp);
            Controls.Add(btnRight);
            Controls.Add(btnLeft);
            Controls.Add(btnGetCameraInput);
            Controls.Add(btnLoadImage);
            Controls.Add(btnLaserOff);
            Controls.Add(btnLaserOn);
            Controls.Add(btnFindCueball);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Billiard Laser";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSpeedOverTime).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAccelerationOverTime).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxDistanceOverTime).EndInit();
            groupBoxShotInfo.ResumeLayout(false);
            groupBoxShotInfo.PerformLayout();
            groupBoxVideoControls.ResumeLayout(false);
            groupBoxVideoControls.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnFindCueball;
        private Button btnLaserOn;
        private Button btnLaserOff;
        private Button btnLoadImage;
        private Button btnGetCameraInput;
        private Button btnLeft;
        private Button btnRight;
        private Button btnUp;
        private Button btnDown;
        private Label lblServos;
        private ComboBox cboCamera;
        private Label labelCameras;
        private PictureBox pictureBoxImage;
        private Label labelShotInfo;
        private PictureBox pictureBoxSpeedOverTime;
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
        private GroupBox groupBoxVideoControls;
        private Button buttonPause;
        private Label label1;
        private ListBox listBoxShots;
        private Button buttonResume;
        private Button buttonNextFrame;
        private Button buttonLastFrame;
        private Label labelVideoControls;
        private Button btnLoadVideo;
        private Label labelFrames;
        private ListBox listBoxProcessedFrames;
        private Label labelFrameRate;
        private Button btnProcessVideo;
        private Button findColoredBalls;
        private RadioButton radioButtonAllBalls;
        private RadioButton radioButtonCB;
    }
}