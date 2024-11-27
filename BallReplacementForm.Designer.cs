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
            ((System.ComponentModel.ISupportInitialize)pictureBoxTable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarCameraOpacity).BeginInit();
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
            labelCameraOpacityValue.Location = new Point(196, 670);
            labelCameraOpacityValue.Name = "labelCameraOpacityValue";
            labelCameraOpacityValue.Size = new Size(30, 20);
            labelCameraOpacityValue.TabIndex = 38;
            labelCameraOpacityValue.Text = "X%";
            // 
            // BallReplacementForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 724);
            Controls.Add(labelCameraOpacityValue);
            Controls.Add(trackBarCameraOpacity);
            Controls.Add(labelCameraOpacity);
            Controls.Add(pictureBoxTable);
            Name = "BallReplacementForm";
            Text = "Ball Replacement Helper";
            FormClosed += BallReplacementForm_FormClosed;
            ((System.ComponentModel.ISupportInitialize)pictureBoxTable).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarCameraOpacity).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBoxTable;
        private Label labelCameraOpacity;
        private TrackBar trackBarCameraOpacity;
        private Label labelCameraOpacityValue;
    }
}