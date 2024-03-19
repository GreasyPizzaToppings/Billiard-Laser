
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
            pictureBoxImage = new PictureBox();
            btnFindCueball = new Button();
            btnLaserOn = new Button();
            btnLaserOff = new Button();
            btnLoadImage = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBoxImage).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxImage
            // 
            pictureBoxImage.Image = Properties.Resources.birdEyeShot;
            pictureBoxImage.Location = new Point(106, 26);
            pictureBoxImage.Margin = new Padding(3, 2, 3, 2);
            pictureBoxImage.Name = "pictureBoxImage";
            pictureBoxImage.Size = new Size(1200, 720);
            pictureBoxImage.TabIndex = 0;
            pictureBoxImage.TabStop = false;
            // 
            // btnFindCueball
            // 
            btnFindCueball.Location = new Point(12, 122);
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
            btnLaserOn.Location = new Point(12, 224);
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
            btnLaserOff.Location = new Point(12, 317);
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
            btnLoadImage.Location = new Point(12, 26);
            btnLoadImage.Margin = new Padding(3, 2, 3, 2);
            btnLoadImage.Name = "btnLoadImage";
            btnLoadImage.Size = new Size(88, 39);
            btnLoadImage.TabIndex = 4;
            btnLoadImage.Text = "Load Image ";
            btnLoadImage.UseVisualStyleBackColor = true;
            btnLoadImage.Click += btnLoadImage_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1314, 753);
            Controls.Add(btnLoadImage);
            Controls.Add(btnLaserOff);
            Controls.Add(btnLaserOn);
            Controls.Add(btnFindCueball);
            Controls.Add(pictureBoxImage);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Billiard Laser";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxImage).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBoxImage;
        private Button btnFindCueball;
        private Button btnLaserOn;
        private Button btnLaserOff;
        private Button btnLoadImage;
    }
}