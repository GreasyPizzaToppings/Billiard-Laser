
namespace billiard_laser
{
    partial class Form1
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
            btnProcessVideo = new Button();
            pictureBoxImage = new PictureBox();
            labelFrameRate = new Label();
            listBoxProcessedFrames = new ListBox();
            labelFrames = new Label();
            buttonLoadVideo = new Button();
            labelVideoControls = new Label();
            buttonLastFrame = new Button();
            buttonNextFrame = new Button();
            buttonResume = new Button();
            listBoxShots = new ListBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBoxImage).BeginInit();
            SuspendLayout();
            // 
            // btnFindCueball
            // 
            btnFindCueball.Location = new Point(7, 168);
            btnFindCueball.Margin = new Padding(3, 2, 3, 2);
            btnFindCueball.Name = "btnFindCueball";
            btnFindCueball.Size = new Size(84, 26);
            btnFindCueball.TabIndex = 1;
            btnFindCueball.Text = "Find Cueball";
            btnFindCueball.UseVisualStyleBackColor = true;
            btnFindCueball.Click += btnFindCueball_Click;
            // 
            // btnLaserOn
            // 
            btnLaserOn.Location = new Point(9, 245);
            btnLaserOn.Margin = new Padding(3, 2, 3, 2);
            btnLaserOn.Name = "btnLaserOn";
            btnLaserOn.Size = new Size(84, 26);
            btnLaserOn.TabIndex = 2;
            btnLaserOn.Text = "Laser ON";
            btnLaserOn.UseVisualStyleBackColor = true;
            btnLaserOn.Click += btnLaserOn_Click;
            // 
            // btnLaserOff
            // 
            btnLaserOff.Location = new Point(9, 275);
            btnLaserOff.Margin = new Padding(3, 2, 3, 2);
            btnLaserOff.Name = "btnLaserOff";
            btnLaserOff.Size = new Size(84, 26);
            btnLaserOff.TabIndex = 3;
            btnLaserOff.Text = "Laser OFF";
            btnLaserOff.UseVisualStyleBackColor = true;
            btnLaserOff.Click += btnLaserOff_Click;
            // 
            // btnLoadImage
            // 
            btnLoadImage.Location = new Point(7, 138);
            btnLoadImage.Margin = new Padding(3, 2, 3, 2);
            btnLoadImage.Name = "btnLoadImage";
            btnLoadImage.Size = new Size(84, 26);
            btnLoadImage.TabIndex = 4;
            btnLoadImage.Text = "Load Image ";
            btnLoadImage.UseVisualStyleBackColor = true;
            btnLoadImage.Click += btnLoadImage_Click;
            // 
            // btnGetCameraInput
            // 
            btnGetCameraInput.Location = new Point(8, 65);
            btnGetCameraInput.Margin = new Padding(3, 2, 3, 2);
            btnGetCameraInput.Name = "btnGetCameraInput";
            btnGetCameraInput.Size = new Size(84, 26);
            btnGetCameraInput.TabIndex = 5;
            btnGetCameraInput.Text = "Camera Input";
            btnGetCameraInput.UseVisualStyleBackColor = true;
            btnGetCameraInput.Click += btnGetCameraInput_Click;
            // 
            // btnLeft
            // 
            btnLeft.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnLeft.Location = new Point(12, 419);
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
            btnRight.Location = new Point(49, 419);
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
            btnUp.Location = new Point(32, 383);
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
            btnDown.Location = new Point(32, 455);
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
            lblServos.Location = new Point(9, 356);
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
            // btnProcessVideo
            // 
            btnProcessVideo.Enabled = false;
            btnProcessVideo.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnProcessVideo.Location = new Point(1104, 77);
            btnProcessVideo.Margin = new Padding(3, 2, 3, 2);
            btnProcessVideo.Name = "btnProcessVideo";
            btnProcessVideo.Size = new Size(84, 26);
            btnProcessVideo.TabIndex = 14;
            btnProcessVideo.Text = "Start ";
            btnProcessVideo.UseVisualStyleBackColor = true;
            btnProcessVideo.Click += btnProcessVideo_Click;
            // 
            // pictureBoxImage
            // 
            pictureBoxImage.Image = Properties.Resources.birdEyeShot;
            pictureBoxImage.Location = new Point(115, 12);
            pictureBoxImage.Margin = new Padding(3, 2, 3, 2);
            pictureBoxImage.Name = "pictureBoxImage";
            pictureBoxImage.Size = new Size(876, 517);
            pictureBoxImage.TabIndex = 15;
            pictureBoxImage.TabStop = false;
            // 
            // labelFrameRate
            // 
            labelFrameRate.AutoSize = true;
            labelFrameRate.Location = new Point(1104, 105);
            labelFrameRate.Name = "labelFrameRate";
            labelFrameRate.Size = new Size(32, 15);
            labelFrameRate.TabIndex = 16;
            labelFrameRate.Text = "FPS: ";
            // 
            // listBoxProcessedFrames
            // 
            listBoxProcessedFrames.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            listBoxProcessedFrames.FormattingEnabled = true;
            listBoxProcessedFrames.Location = new Point(999, 182);
            listBoxProcessedFrames.Name = "listBoxProcessedFrames";
            listBoxProcessedFrames.Size = new Size(99, 264);
            listBoxProcessedFrames.TabIndex = 17;
            listBoxProcessedFrames.SelectedIndexChanged += listBoxFrames_SelectedIndexChanged;
            // 
            // labelFrames
            // 
            labelFrames.AutoSize = true;
            labelFrames.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelFrames.Location = new Point(1014, 138);
            labelFrames.Name = "labelFrames";
            labelFrames.Size = new Size(70, 38);
            labelFrames.TabIndex = 19;
            labelFrames.Text = "Processed\r\nFrames";
            // 
            // buttonLoadVideo
            // 
            buttonLoadVideo.Location = new Point(1014, 77);
            buttonLoadVideo.Margin = new Padding(3, 2, 3, 2);
            buttonLoadVideo.Name = "buttonLoadVideo";
            buttonLoadVideo.Size = new Size(84, 26);
            buttonLoadVideo.TabIndex = 20;
            buttonLoadVideo.Text = "Load Video";
            buttonLoadVideo.UseVisualStyleBackColor = true;
            buttonLoadVideo.Click += buttonLoadVideo_Click;
            // 
            // labelVideoControls
            // 
            labelVideoControls.AutoSize = true;
            labelVideoControls.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            labelVideoControls.Location = new Point(1028, 29);
            labelVideoControls.Name = "labelVideoControls";
            labelVideoControls.Size = new Size(142, 28);
            labelVideoControls.TabIndex = 21;
            labelVideoControls.Text = "Video Controls";
            // 
            // buttonLastFrame
            // 
            buttonLastFrame.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonLastFrame.Location = new Point(1014, 464);
            buttonLastFrame.Margin = new Padding(3, 2, 3, 2);
            buttonLastFrame.Name = "buttonLastFrame";
            buttonLastFrame.Size = new Size(84, 26);
            buttonLastFrame.TabIndex = 22;
            buttonLastFrame.Text = "Last Frame";
            buttonLastFrame.UseVisualStyleBackColor = true;
            buttonLastFrame.Click += buttonLastFrame_Click;
            // 
            // buttonNextFrame
            // 
            buttonNextFrame.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonNextFrame.Location = new Point(1104, 464);
            buttonNextFrame.Margin = new Padding(3, 2, 3, 2);
            buttonNextFrame.Name = "buttonNextFrame";
            buttonNextFrame.Size = new Size(84, 26);
            buttonNextFrame.TabIndex = 23;
            buttonNextFrame.Text = "Next Frame";
            buttonNextFrame.UseVisualStyleBackColor = true;
            buttonNextFrame.Click += buttonNextFrame_Click;
            // 
            // buttonResume
            // 
            buttonResume.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonResume.Location = new Point(1059, 494);
            buttonResume.Margin = new Padding(3, 2, 3, 2);
            buttonResume.Name = "buttonResume";
            buttonResume.Size = new Size(84, 26);
            buttonResume.TabIndex = 24;
            buttonResume.Text = "Resume";
            buttonResume.UseVisualStyleBackColor = true;
            buttonResume.Click += buttonResume_Click;
            // 
            // listBoxShots
            // 
            listBoxShots.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            listBoxShots.FormattingEnabled = true;
            listBoxShots.Location = new Point(1104, 182);
            listBoxShots.Name = "listBoxShots";
            listBoxShots.Size = new Size(99, 264);
            listBoxShots.TabIndex = 25;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(1104, 156);
            label1.Name = "label1";
            label1.Size = new Size(102, 19);
            label1.TabIndex = 26;
            label1.Text = "Detected Shots";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1215, 537);
            Controls.Add(label1);
            Controls.Add(listBoxShots);
            Controls.Add(buttonResume);
            Controls.Add(buttonNextFrame);
            Controls.Add(buttonLastFrame);
            Controls.Add(labelVideoControls);
            Controls.Add(buttonLoadVideo);
            Controls.Add(labelFrames);
            Controls.Add(listBoxProcessedFrames);
            Controls.Add(labelFrameRate);
            Controls.Add(pictureBoxImage);
            Controls.Add(btnProcessVideo);
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
        private Button btnProcessVideo;
        private PictureBox pictureBoxImage;
        private Label labelFrameRate;
        private ListBox listBoxProcessedFrames;
        private Label labelFrames;
        private Button buttonLoadVideo;
        private Label labelVideoControls;
        private Button buttonLastFrame;
        private Button buttonNextFrame;
        private Button buttonResume;
        private ListBox listBoxShots;
        private Label label1;
    }
}