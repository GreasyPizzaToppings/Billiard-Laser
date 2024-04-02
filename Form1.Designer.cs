
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
            btnFindCueballInVideo = new Button();
            pictureBoxImage = new PictureBox();
            labelFrameRate = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBoxImage).BeginInit();
            SuspendLayout();
            // 
            // btnFindCueball
            // 
            btnFindCueball.Location = new Point(8, 237);
            btnFindCueball.Margin = new Padding(3, 2, 3, 2);
            btnFindCueball.Name = "btnFindCueball";
            btnFindCueball.Size = new Size(88, 39);
            btnFindCueball.TabIndex = 1;
            btnFindCueball.Text = "Find Cueball";
            btnFindCueball.UseVisualStyleBackColor = true;
            btnFindCueball.Click += btnFindCueball_Click;
            // 
            // btnLaserOn
            // 
            btnLaserOn.Location = new Point(9, 294);
            btnLaserOn.Margin = new Padding(3, 2, 3, 2);
            btnLaserOn.Name = "btnLaserOn";
            btnLaserOn.Size = new Size(88, 32);
            btnLaserOn.TabIndex = 2;
            btnLaserOn.Text = "Laser ON";
            btnLaserOn.UseVisualStyleBackColor = true;
            btnLaserOn.Click += btnLaserOn_Click;
            // 
            // btnLaserOff
            // 
            btnLaserOff.Location = new Point(9, 330);
            btnLaserOff.Margin = new Padding(3, 2, 3, 2);
            btnLaserOff.Name = "btnLaserOff";
            btnLaserOff.Size = new Size(88, 32);
            btnLaserOff.TabIndex = 3;
            btnLaserOff.Text = "Laser OFF";
            btnLaserOff.UseVisualStyleBackColor = true;
            btnLaserOff.Click += btnLaserOff_Click;
            // 
            // btnLoadImage
            // 
            btnLoadImage.Location = new Point(7, 194);
            btnLoadImage.Margin = new Padding(3, 2, 3, 2);
            btnLoadImage.Name = "btnLoadImage";
            btnLoadImage.Size = new Size(88, 39);
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
            btnGetCameraInput.Size = new Size(88, 32);
            btnGetCameraInput.TabIndex = 5;
            btnGetCameraInput.Text = "Camera Input";
            btnGetCameraInput.UseVisualStyleBackColor = true;
            btnGetCameraInput.Click += btnGetCameraInput_Click;
            // 
            // btnLeft
            // 
            btnLeft.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnLeft.Location = new Point(15, 452);
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
            btnRight.Location = new Point(52, 452);
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
            btnUp.Location = new Point(35, 416);
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
            btnDown.Location = new Point(35, 488);
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
            lblServos.Location = new Point(12, 386);
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
            // btnFindCueballInVideo
            // 
            btnFindCueballInVideo.Location = new Point(6, 118);
            btnFindCueballInVideo.Margin = new Padding(3, 2, 3, 2);
            btnFindCueballInVideo.Name = "btnFindCueballInVideo";
            btnFindCueballInVideo.Size = new Size(88, 40);
            btnFindCueballInVideo.TabIndex = 14;
            btnFindCueballInVideo.Text = "Find CB in Video";
            btnFindCueballInVideo.UseVisualStyleBackColor = true;
            btnFindCueballInVideo.Click += btnFindCueballInVideo_Click;
            // 
            // pictureBoxImage
            // 
            pictureBoxImage.Image = Properties.Resources.birdEyeShot;
            pictureBoxImage.Location = new Point(115, 12);
            pictureBoxImage.Margin = new Padding(3, 2, 3, 2);
            pictureBoxImage.Name = "pictureBoxImage";
            pictureBoxImage.Size = new Size(865, 517);
            pictureBoxImage.TabIndex = 15;
            pictureBoxImage.TabStop = false;
            // 
            // labelFrameRate
            // 
            labelFrameRate.AutoSize = true;
            labelFrameRate.Location = new Point(6, 160);
            labelFrameRate.Name = "labelFrameRate";
            labelFrameRate.Size = new Size(32, 15);
            labelFrameRate.TabIndex = 16;
            labelFrameRate.Text = "FPS: ";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(992, 535);
            Controls.Add(labelFrameRate);
            Controls.Add(pictureBoxImage);
            Controls.Add(btnFindCueballInVideo);
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
        private Button btnFindCueballInVideo;
        private PictureBox pictureBoxImage;
        private Label labelFrameRate;
    }
}