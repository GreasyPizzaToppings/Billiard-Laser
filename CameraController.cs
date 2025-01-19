using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

public class CameraController
{
    private FilterInfoCollection filterInfoCollection;
    private VideoCaptureDevice videoCaptureDevice;
    private ComboBox comboBox;
    private ManualResetEvent frameReceivedEvent = new ManualResetEvent(false);
    private int frameIndex = 0;
    private bool isFlipped = false;
    private bool isMirrored = false;
    private OpenCvSharp.Size outputResolution;
    private readonly object lockObject = new object();
    private Bitmap resizedBitmap;

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

    private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
    {
        frameReceivedEvent.Set();

        using (Bitmap originalFrame = (Bitmap)eventArgs.Frame.Clone())
        {
            // First apply any transformations (flip/mirror)
            Bitmap transformedFrame = TransformFrame(originalFrame);

            // Then resize to the desired output resolution
            using (Bitmap resizedFrame = ResizeFrame(transformedFrame))
            {
                VideoFrame frame = new VideoFrame(resizedFrame, frameIndex);
                frameIndex++;
                ReceivedFrame?.Invoke(this, frame);
            }

            // Clean up the intermediate transformed frame
            if (transformedFrame != originalFrame)
            {
                transformedFrame.Dispose();
            }
        }
    }

    private unsafe void FastResize(Bitmap source, Bitmap destination)
    {
        BitmapData sourceData = source.LockBits(new Rectangle(0, 0, source.Width, source.Height),
            ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
        BitmapData destData = destination.LockBits(new Rectangle(0, 0, destination.Width, destination.Height),
            ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

        try
        {
            byte* sourcePtr = (byte*)sourceData.Scan0;
            byte* destPtr = (byte*)destData.Scan0;

            int sourceWidth = source.Width;
            int sourceHeight = source.Height;
            int destWidth = destination.Width;
            int destHeight = destination.Height;

            float xRatio = (float)sourceWidth / destWidth;
            float yRatio = (float)sourceHeight / destHeight;

            // Parallel processing for rows
            Parallel.For(0, destHeight, destY =>
            {
                int srcY = (int)(destY * yRatio);
                byte* destRow = destPtr + destY * destData.Stride;

                for (int destX = 0; destX < destWidth; destX++)
                {
                    int srcX = (int)(destX * xRatio);
                    byte* srcPixel = sourcePtr + srcY * sourceData.Stride + srcX * 3;
                    byte* destPixel = destRow + destX * 3;

                    // Copy RGB values
                    destPixel[0] = srcPixel[0]; // B
                    destPixel[1] = srcPixel[1]; // G
                    destPixel[2] = srcPixel[2]; // R
                }
            });
        }
        finally
        {
            source.UnlockBits(sourceData);
            destination.UnlockBits(destData);
        }
    }

    private Bitmap ResizeFrame(Bitmap original)
    {
        if (original.Width == OutputResolution.Width && original.Height == OutputResolution.Height)
        {
            return (Bitmap)original.Clone();
        }

        lock (lockObject)
        {
            FastResize(original, resizedBitmap);
            return (Bitmap)resizedBitmap.Clone();
        }
    }


    private Bitmap ResizeFrame1(Bitmap original)
    {
        if (original.Width == OutputResolution.Width && original.Height == OutputResolution.Height)
        {
            return (Bitmap)original.Clone();
        }

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