using AForge.Video;
using AForge.Video.DirectShow;

public class CameraController
{
    private FilterInfoCollection filterInfoCollection;
    private VideoCaptureDevice videoCaptureDevice;
    private ComboBox comboBox;
    private ManualResetEvent frameReceivedEvent = new ManualResetEvent(false);
    private int frameIndex = 0;
    private bool isFlipped = false;
    private bool isMirrored = false;

    public event EventHandler<VideoFrame> ReceivedFrame;
    public event EventHandler TransformationChanged; //for flip or mirror

    public bool IsFlipped
    {
        get => isFlipped;
        set
        {
            isFlipped = value;
            OnTransformationChanged();
        }
    }

    public bool IsMirrored
    {
        get => isMirrored;
        set
        {
            isMirrored = value;
            OnTransformationChanged();
        }
    }

    public CameraController(ComboBox comboBox)
    {
        this.comboBox = comboBox;

        PopulateCameraComboBox();
    }

    private void OnTransformationChanged()
    {
        TransformationChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool StartCameraCapture() {
        try
        {
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[comboBox.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += FinalFrame_NewFrame;
            videoCaptureDevice.Start();

            // wait for the first frame or timeout after 5 seconds
            if (frameReceivedEvent.WaitOne(TimeSpan.FromSeconds(5)))
            {
                return true;
            }
            else
            {
                videoCaptureDevice.SignalToStop();
                MessageBox.Show("Error: Camera did not start capturing frames in time.");
                return false;
            }
        }

        catch (Exception e) {
            MessageBox.Show("Error starting camera!\n" + e.Message);
            return false;
        }
    }

    private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
    {
        frameReceivedEvent.Set();

        // Clone and transform the frame
        using (Bitmap originalFrame = (Bitmap)eventArgs.Frame.Clone())
        {
            Bitmap transformedFrame = TransformFrame(originalFrame);
            VideoFrame frame = new VideoFrame(transformedFrame, frameIndex);
            frameIndex++;

            ReceivedFrame?.Invoke(this, frame);
        }
    }

    /// <summary>
    /// Flip and or mirror the image if enabled
    /// </summary>
    /// <param name="original"></param>
    /// <returns></returns>
    private Bitmap TransformFrame(Bitmap original)
    {
        if (!IsFlipped && !IsMirrored)
            return (Bitmap)original.Clone();

        Bitmap transformed = new Bitmap(original.Width, original.Height);

        using (Graphics g = Graphics.FromImage(transformed))
        {
            if (IsFlipped && IsMirrored)
            {
                g.RotateTransform(180);
                g.TranslateTransform(-original.Width, -original.Height);
            }
            else if (IsFlipped)
            {
                g.TranslateTransform(0, original.Height);
                g.ScaleTransform(1, -1);
            }
            else if (IsMirrored)
            {
                g.TranslateTransform(original.Width, 0);
                g.ScaleTransform(-1, 1);
            }

            g.DrawImage(original, 0, 0);
        }

        return transformed;
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
            try
            {
                videoCaptureDevice.NewFrame -= FinalFrame_NewFrame;
                videoCaptureDevice.SignalToStop();

                // Fallback
                if (videoCaptureDevice.IsRunning)
                {
                    videoCaptureDevice.Stop();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error stopping camera capture: {ex.Message}");
            }
            finally
            {
                videoCaptureDevice = null;
                frameIndex = 0;
            }
        }
    }
}