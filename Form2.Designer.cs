namespace billiard_laser
{
    partial class Form2
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
            loadedImagePicBox = new PictureBox();
            invMaskPicBox = new PictureBox();
            appliedMaskPicBox = new PictureBox();
            blurredImagePicBox = new PictureBox();
            foundBallsPicBox = new PictureBox();
            filteredContoursPicBox = new PictureBox();
            loadImageBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)loadedImagePicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)invMaskPicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)appliedMaskPicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)blurredImagePicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)foundBallsPicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)filteredContoursPicBox).BeginInit();
            SuspendLayout();
            // 
            // loadedImagePicBox
            // 
            loadedImagePicBox.Location = new Point(116, 12);
            loadedImagePicBox.Name = "loadedImagePicBox";
            loadedImagePicBox.Size = new Size(443, 307);
            loadedImagePicBox.TabIndex = 0;
            loadedImagePicBox.TabStop = false;
            // 
            // invMaskPicBox
            // 
            invMaskPicBox.Location = new Point(1023, 12);
            invMaskPicBox.Name = "invMaskPicBox";
            invMaskPicBox.Size = new Size(452, 307);
            invMaskPicBox.TabIndex = 1;
            invMaskPicBox.TabStop = false;
            // 
            // appliedMaskPicBox
            // 
            appliedMaskPicBox.Location = new Point(116, 335);
            appliedMaskPicBox.Name = "appliedMaskPicBox";
            appliedMaskPicBox.Size = new Size(443, 307);
            appliedMaskPicBox.TabIndex = 2;
            appliedMaskPicBox.TabStop = false;
            // 
            // blurredImagePicBox
            // 
            blurredImagePicBox.Location = new Point(565, 12);
            blurredImagePicBox.Name = "blurredImagePicBox";
            blurredImagePicBox.Size = new Size(452, 307);
            blurredImagePicBox.TabIndex = 3;
            blurredImagePicBox.TabStop = false;
            // 
            // foundBallsPicBox
            // 
            foundBallsPicBox.Location = new Point(1023, 335);
            foundBallsPicBox.Name = "foundBallsPicBox";
            foundBallsPicBox.Size = new Size(452, 307);
            foundBallsPicBox.TabIndex = 4;
            foundBallsPicBox.TabStop = false;
            // 
            // filteredContoursPicBox
            // 
            filteredContoursPicBox.Location = new Point(565, 335);
            filteredContoursPicBox.Name = "filteredContoursPicBox";
            filteredContoursPicBox.Size = new Size(452, 307);
            filteredContoursPicBox.TabIndex = 5;
            filteredContoursPicBox.TabStop = false;
            // 
            // loadImageBtn
            // 
            loadImageBtn.Location = new Point(3, 12);
            loadImageBtn.Name = "loadImageBtn";
            loadImageBtn.Size = new Size(107, 29);
            loadImageBtn.TabIndex = 6;
            loadImageBtn.Text = "Load Image";
            loadImageBtn.UseVisualStyleBackColor = true;
            loadImageBtn.Click += loadImageBtn_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1487, 654);
            Controls.Add(loadImageBtn);
            Controls.Add(filteredContoursPicBox);
            Controls.Add(foundBallsPicBox);
            Controls.Add(blurredImagePicBox);
            Controls.Add(appliedMaskPicBox);
            Controls.Add(invMaskPicBox);
            Controls.Add(loadedImagePicBox);
            Name = "Form2";
            Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)loadedImagePicBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)invMaskPicBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)appliedMaskPicBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)blurredImagePicBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)foundBallsPicBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)filteredContoursPicBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox loadedImagePicBox;
        private PictureBox invMaskPicBox;
        private PictureBox appliedMaskPicBox;
        private PictureBox blurredImagePicBox;
        private PictureBox foundBallsPicBox;
        private PictureBox filteredContoursPicBox;
        private Button loadImageBtn;
    }
}