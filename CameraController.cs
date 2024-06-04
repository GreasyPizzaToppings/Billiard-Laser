using AForge.Video;
using AForge.Video.DirectShow;

public class CameraController
{
    private FilterInfoCollection filterInfoCollection;
    private VideoCaptureDevice videoCaptureDevice;
    private ComboBox comboBox;
    private int frameIndex = 0;

    public event EventHandler<VideoFrame> ReceivedFrame;

    public CameraController(ComboBox comboBox)
    {
        this.comboBox = comboBox;

        PopulateCameraComboBox();
    }

    public bool StartCameraCapture() {
        try
        {
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[comboBox.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += FinalFrame_NewFrame;
            videoCaptureDevice.Start();
            return true;
        }

        catch (Exception e) {
            MessageBox.Show("Error starting camera!\n" + e.Message);
            return false;
        }
    }

    private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
    {
        VideoFrame frame = new VideoFrame((Bitmap)eventArgs.Frame.Clone(), frameIndex);
        frameIndex++;

        //alert followers we got a frame for them
        ReceivedFrame?.Invoke(this, frame);
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