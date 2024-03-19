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
            btnCheckImage = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBoxImage).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxImage
            // 
            pictureBoxImage.Location = new Point(252, 37);
            pictureBoxImage.Name = "pictureBoxImage";
            pictureBoxImage.Size = new Size(511, 341);
            pictureBoxImage.TabIndex = 0;
            pictureBoxImage.TabStop = false;
            // 
            // btnCheckImage
            // 
            btnCheckImage.Location = new Point(65, 202);
            btnCheckImage.Name = "btnCheckImage";
            btnCheckImage.Size = new Size(123, 29);
            btnCheckImage.TabIndex = 1;
            btnCheckImage.Text = "Check Image";
            btnCheckImage.UseVisualStyleBackColor = true;
            btnCheckImage.Click += btnCheckImage_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCheckImage);
            Controls.Add(pictureBoxImage);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxImage).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBoxImage;
        private Button btnCheckImage;
    }
}