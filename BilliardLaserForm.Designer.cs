
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
            tableLayoutPanelVideoControls.RowStyles.Add(new RowStyle(SizeType.Absolute, 229F));
            tableLayoutPanelVideoControls.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelVideoControls.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            tableLayoutPanelVideoControls.Size = new Size(203, 561);
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
            panelMediaControls.Location = new Point(3, 464);
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
            panelListBoxes.Size = new Size(197, 229);
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
            listBoxProcessedFrames.Size = new Size(87, 229);
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
            listBoxShots.Size = new Size(87, 229);
            listBoxShots.TabIndex = 41;
            listBoxShots.SelectedIndexChanged += listBoxShots_SelectedIndexChanged;
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
            ClientSize = new Size(1052, 561);
            Controls.Add(tableLayoutPanelVideoControls);
            Controls.Add(pictureBoxImage);
            Margin = new Padding(3, 2, 3, 2);
            Name = "BilliardLaserForm";
            Text = "Billiard Laser";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
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