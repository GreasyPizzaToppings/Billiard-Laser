using AForge.Video;
using AForge.Video.DirectShow;

public class CameraController
{
    private FilterInfoCollection filterInfoCollection;
    private VideoCaptureDevice videoCaptureDevice;
    private PictureBox pictureBox;
    private ComboBox comboBox;

    public CameraController(PictureBox pictureBox, ComboBox comboBox)
    {
        this.pictureBox = pictureBox;
        this.comboBox = comboBox;

        PopulateCameraComboBox();
    }

    public void StartCameraCapture() {
        videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[comboBox.SelectedIndex].MonikerString);
        videoCaptureDevice.NewFrame += FinalFrame_NewFrame;
        videoCaptureDevice.Start();
    }

    private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
    {
        pictureBox.Image = (Bitmap)eventArgs.Frame.Clone();
    }

    private void PopulateCameraComboBox()
    {
        filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        foreach (FilterInfo Device in filterInfoCollection)
            comboBox.Items.Add(Device.Name);
        comboBox.SelectedIndex = 0;

        videoCaptureDevice = new VideoCaptureDevice();
    }

    public void StopCameraCapture()
    {
        if (videoCaptureDevice != null && videoCaptureDevice.IsRunning)
        {
            videoCaptureDevice.SignalToStop();
            videoCaptureDevice.NewFrame -= FinalFrame_NewFrame;
            videoCaptureDevice = null;
        }
    }
}