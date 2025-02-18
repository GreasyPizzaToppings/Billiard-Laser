
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
            tableLayoutPanelVideoControls = new TableLayoutPanel();
            panelMediaControls = new Panel();
            labelMediaControls = new Label();
            btnLastFrame = new Button();
            btnNextFrame = new Button();
            btnPlayPause = new Button();
            labelFrameRate = new Label();
            panelListBoxes = new Panel();
            listBoxProcessedFrames = new ListBox();
            listBoxShots = new ListBox();
            panelVideoControls = new Panel();
            btnShowReplaceBallsForm = new Button();
            checkBoxDetectBalls = new CheckBox();
            labelShots = new Label();
            labelFrames = new Label();
            btnShowDebugForm = new Button();
            labelCameras = new Label();
            cboCamera = new ComboBox();
            labelVideoControls = new Label();
            btnLoadVideo = new Button();
            btnStartCameraInput = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBoxImage).BeginInit();
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
            pictureBoxImage.Location = new Point(208, 0);
            pictureBoxImage.Margin = new Padding(5);
            pictureBoxImage.Name = "pictureBoxImage";
            pictureBoxImage.Size = new Size(837, 558);
            pictureBoxImage.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxImage.TabIndex = 15;
            pictureBoxImage.TabStop = false;
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
            tableLayoutPanelVideoControls.RowStyles.Add(new RowStyle(SizeType.Absolute, 250F));
            tableLayoutPanelVideoControls.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelVideoControls.RowStyles.Add(new RowStyle(SizeType.Absolute, 115F));
            tableLayoutPanelVideoControls.Size = new Size(203, 561);
            tableLayoutPanelVideoControls.TabIndex = 47;
            // 
            // panelMediaControls
            // 
            panelMediaControls.Controls.Add(labelMediaControls);
            panelMediaControls.Controls.Add(btnLastFrame);
            panelMediaControls.Controls.Add(btnNextFrame);
            panelMediaControls.Controls.Add(btnPlayPause);
            panelMediaControls.Controls.Add(labelFrameRate);
            panelMediaControls.Dock = DockStyle.Fill;
            panelMediaControls.Location = new Point(3, 449);
            panelMediaControls.Name = "panelMediaControls";
            panelMediaControls.Size = new Size(197, 109);
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
            // btnLastFrame
            // 
            btnLastFrame.Enabled = false;
            btnLastFrame.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnLastFrame.Location = new Point(8, 33);
            btnLastFrame.Margin = new Padding(3, 2, 3, 2);
            btnLastFrame.Name = "btnLastFrame";
            btnLastFrame.Size = new Size(60, 45);
            btnLastFrame.TabIndex = 42;
            btnLastFrame.Text = "Last Frame";
            btnLastFrame.UseVisualStyleBackColor = true;
            // 
            // btnNextFrame
            // 
            btnNextFrame.Enabled = false;
            btnNextFrame.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnNextFrame.Location = new Point(129, 33);
            btnNextFrame.Margin = new Padding(3, 2, 3, 2);
            btnNextFrame.Name = "btnNextFrame";
            btnNextFrame.Size = new Size(60, 45);
            btnNextFrame.TabIndex = 43;
            btnNextFrame.Text = "Next Frame";
            btnNextFrame.UseVisualStyleBackColor = true;
            // 
            // btnPlayPause
            // 
            btnPlayPause.Enabled = false;
            btnPlayPause.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            btnPlayPause.Location = new Point(69, 33);
            btnPlayPause.Margin = new Padding(3, 2, 3, 2);
            btnPlayPause.Name = "btnPlayPause";
            btnPlayPause.Size = new Size(60, 45);
            btnPlayPause.TabIndex = 45;
            btnPlayPause.Text = "⏵";
            btnPlayPause.UseVisualStyleBackColor = true;
            btnPlayPause.Click += btnPlayPause_Click;
            // 
            // labelFrameRate
            // 
            labelFrameRate.AutoSize = true;
            labelFrameRate.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelFrameRate.Location = new Point(8, 84);
            labelFrameRate.Name = "labelFrameRate";
            labelFrameRate.Size = new Size(38, 19);
            labelFrameRate.TabIndex = 48;
            labelFrameRate.Text = "FPS: ";
            // 
            // panelListBoxes
            // 
            panelListBoxes.Controls.Add(listBoxProcessedFrames);
            panelListBoxes.Controls.Add(listBoxShots);
            panelListBoxes.Dock = DockStyle.Fill;
            panelListBoxes.Location = new Point(3, 250);
            panelListBoxes.Margin = new Padding(3, 0, 3, 3);
            panelListBoxes.Name = "panelListBoxes";
            panelListBoxes.Size = new Size(197, 193);
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
            listBoxProcessedFrames.Size = new Size(87, 193);
            listBoxProcessedFrames.TabIndex = 40;
            listBoxProcessedFrames.SelectedIndexChanged += listBoxFrames_SelectedIndexChanged;
            // 
            // listBoxShots
            // 
            listBoxShots.Dock = DockStyle.Right;
            listBoxShots.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            listBoxShots.FormattingEnabled = true;
            listBoxShots.Location = new Point(110, 0);
            listBoxShots.Margin = new Padding(3, 0, 3, 0);
            listBoxShots.Name = "listBoxShots";
            listBoxShots.Size = new Size(87, 193);
            listBoxShots.TabIndex = 41;
            listBoxShots.SelectedIndexChanged += listBoxShots_SelectedIndexChanged;
            // 
            // panelVideoControls
            // 
            panelVideoControls.Controls.Add(btnShowReplaceBallsForm);
            panelVideoControls.Controls.Add(checkBoxDetectBalls);
            panelVideoControls.Controls.Add(labelShots);
            panelVideoControls.Controls.Add(labelFrames);
            panelVideoControls.Controls.Add(btnShowDebugForm);
            panelVideoControls.Controls.Add(labelCameras);
            panelVideoControls.Controls.Add(cboCamera);
            panelVideoControls.Controls.Add(labelVideoControls);
            panelVideoControls.Controls.Add(btnLoadVideo);
            panelVideoControls.Controls.Add(btnStartCameraInput);
            panelVideoControls.Dock = DockStyle.Fill;
            panelVideoControls.Location = new Point(3, 3);
            panelVideoControls.Margin = new Padding(3, 3, 3, 0);
            panelVideoControls.Name = "panelVideoControls";
            panelVideoControls.Size = new Size(197, 247);
            panelVideoControls.TabIndex = 48;
            // 
            // btnShowReplaceBallsForm
            // 
            btnShowReplaceBallsForm.Location = new Point(8, 192);
            btnShowReplaceBallsForm.Margin = new Padding(3, 2, 3, 2);
            btnShowReplaceBallsForm.Name = "btnShowReplaceBallsForm";
            btnShowReplaceBallsForm.Size = new Size(181, 25);
            btnShowReplaceBallsForm.TabIndex = 55;
            btnShowReplaceBallsForm.Text = "Replace Balls";
            btnShowReplaceBallsForm.UseVisualStyleBackColor = true;
            btnShowReplaceBallsForm.Click += btnShowReplaceBallsForm_Click;
            // 
            // checkBoxDetectBalls
            // 
            checkBoxDetectBalls.AutoSize = true;
            checkBoxDetectBalls.Checked = true;
            checkBoxDetectBalls.CheckState = CheckState.Checked;
            checkBoxDetectBalls.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            checkBoxDetectBalls.Location = new Point(7, 101);
            checkBoxDetectBalls.Name = "checkBoxDetectBalls";
            checkBoxDetectBalls.Size = new Size(112, 23);
            checkBoxDetectBalls.TabIndex = 54;
            checkBoxDetectBalls.Text = "Ball Detection";
            checkBoxDetectBalls.UseVisualStyleBackColor = true;
            // 
            // labelShots
            // 
            labelShots.AutoSize = true;
            labelShots.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelShots.Location = new Point(112, 226);
            labelShots.Name = "labelShots";
            labelShots.Size = new Size(43, 19);
            labelShots.TabIndex = 53;
            labelShots.Text = "Shots";
            // 
            // labelFrames
            // 
            labelFrames.AutoSize = true;
            labelFrames.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelFrames.Location = new Point(6, 226);
            labelFrames.Name = "labelFrames";
            labelFrames.Size = new Size(53, 19);
            labelFrames.TabIndex = 52;
            labelFrames.Text = "Frames";
            // 
            // btnShowDebugForm
            // 
            btnShowDebugForm.Location = new Point(8, 163);
            btnShowDebugForm.Margin = new Padding(3, 2, 3, 2);
            btnShowDebugForm.Name = "btnShowDebugForm";
            btnShowDebugForm.Size = new Size(181, 25);
            btnShowDebugForm.TabIndex = 51;
            btnShowDebugForm.Text = "Show Debug Form";
            btnShowDebugForm.UseVisualStyleBackColor = true;
            btnShowDebugForm.Click += btnShowDebugForm_Click;
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
            cboCamera.Location = new Point(6, 66);
            cboCamera.Name = "cboCamera";
            cboCamera.Size = new Size(100, 23);
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
            btnLoadVideo.Location = new Point(8, 134);
            btnLoadVideo.Margin = new Padding(3, 2, 3, 2);
            btnLoadVideo.Name = "btnLoadVideo";
            btnLoadVideo.Size = new Size(181, 25);
            btnLoadVideo.TabIndex = 49;
            btnLoadVideo.Text = "Load Video";
            btnLoadVideo.UseVisualStyleBackColor = true;
            btnLoadVideo.Click += btnLoadVideo_Click;
            // 
            // btnStartCameraInput
            // 
            btnStartCameraInput.Location = new Point(112, 65);
            btnStartCameraInput.Margin = new Padding(3, 2, 3, 2);
            btnStartCameraInput.Name = "btnStartCameraInput";
            btnStartCameraInput.Size = new Size(75, 25);
            btnStartCameraInput.TabIndex = 44;
            btnStartCameraInput.Text = "Start Cam";
            btnStartCameraInput.UseVisualStyleBackColor = true;
            btnStartCameraInput.Click += btnStartCameraInput_Click;
            // 
            // BilliardLaserForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1052, 561);
            Controls.Add(tableLayoutPanelVideoControls);
            Controls.Add(pictureBoxImage);
            Margin = new Padding(3, 2, 3, 2);
            Name = "BilliardLaserForm";
            Text = "Billiard Laser";
            FormClosed += BilliardLaserForm_FormClosed;
            ((System.ComponentModel.ISupportInitialize)pictureBoxImage).EndInit();
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
        private TableLayoutPanel tableLayoutPanelVideoControls;
        private Panel panelVideoControls;
        private Label labelShots;
        private Label labelFrames;
        private Button btnShowDebugForm;
        private Label labelCameras;
        private ComboBox cboCamera;
        private Label labelVideoControls;
        private Button btnLoadVideo;
        private Button btnStartCameraInput;
        private Label labelFrameRate;
        private Panel panelListBoxes;
        private ListBox listBoxProcessedFrames;
        private ListBox listBoxShots;
        private Panel panelMediaControls;
        private Button btnLastFrame;
        private Button btnNextFrame;
        private Button btnPlayPause;
        private Label labelMediaControls;
        private CheckBox checkBoxDetectBalls;
        private Button btnShowReplaceBallsForm;
    }
}