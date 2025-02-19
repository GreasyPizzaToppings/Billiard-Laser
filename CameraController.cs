using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

public class CameraController : IDisposable
{
    private bool disposed = false;
    private FilterInfoCollection filterInfoCollection;
    private VideoCaptureDevice? videoCaptureDevice;
    private ComboBox comboBox;
    private ManualResetEvent frameReceivedEvent = new ManualResetEvent(false);
    private int frameIndex = 0;
    private bool isFlipped = false;
    private bool isMirrored = false;
    private OpenCvSharp.Size outputResolution;

    public event EventHandler<VideoFrame>? ReceivedFrame;
    public event EventHandler? TransformationChanged; //for flip or mirror

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

    public OpenCvSharp.Size OutputResolution
    {
        get => outputResolution;
        set
        {
            if (value.Width <= 0 || value.Height <= 0)
                throw new ArgumentException("Resolution dimensions must be positive");
            outputResolution = value;
        }
    }

    public CameraController(ComboBox comboBox, OpenCvSharp.Size initialResolution)
    {
        this.comboBox = comboBox;
        this.OutputResolution = initialResolution;
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

    /// <summary>
    /// Signal to subscribers that a new frame is here
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="eventArgs"></param>
    private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
    {
        frameReceivedEvent.Set();

        using Bitmap originalFrame = eventArgs.Frame;
        Bitmap? workingFrame = null;

        try
        {
            // determine if we need transformations
            bool needsTransform = isFlipped || isMirrored;
                bool needsResize = originalFrame.Width != OutputResolution.Width || 
                                  originalFrame.Height != OutputResolution.Height;

                workingFrame = originalFrame;

            if (needsTransform)
            {
                    var transformedFrame = TransformFrame(workingFrame);
                    if (workingFrame != originalFrame) workingFrame.Dispose();
                    workingFrame = transformedFrame;
            }

            if (needsResize)
            {
                    var resizedFrame = ResizeFrame(workingFrame);
                    if (workingFrame != originalFrame) workingFrame.Dispose();
                    workingFrame = resizedFrame;
            }
        
            ReceivedFrame?.Invoke(this, new VideoFrame(workingFrame, frameIndex++));
            workingFrame.Dispose();
            workingFrame = null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing camera frame: {ex.Message}");
        }
        finally
        {
            // dispose working frame if we still own it (error case)
            if (workingFrame != null && workingFrame != originalFrame)
                workingFrame.Dispose();
        }
    }

    private Bitmap ResizeFrame(Bitmap original)
    {
        Bitmap resized = new Bitmap(OutputResolution.Width, OutputResolution.Height);

        using (Graphics g = Graphics.FromImage(resized))
        {
            // Configure high quality scaling
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;

            // Draw the resized image
            g.DrawImage(original, 0, 0, OutputResolution.Width, OutputResolution.Height);
        }

        return resized;
    }

    /// <summary>
    /// Flip and or mirror the image if enabled
    /// </summary>
    /// <param name="original"></param>
    /// <returns></returns>
    private Bitmap TransformFrame(Bitmap original)
    {
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

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                StopCameraCapture();
                frameReceivedEvent.Dispose();
                ReceivedFrame = null;
                TransformationChanged = null;
            }
            disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~CameraController()
    {
        Dispose(false);
    }

    public override string ToString() =>
        $"CameraController Settings:\n" +
        $"  Is Flipped: {IsFlipped}\n" +
        $"  Is Mirrored: {IsMirrored}\n" +
        $"  Output Resolution: {OutputResolution.Width}x{OutputResolution.Height}\n" +
        $"  Frame Index: {frameIndex}\n" +
        $"  Camera Device: {videoCaptureDevice?.Source ?? "Not Started"}";
}