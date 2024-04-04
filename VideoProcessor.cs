using OpenCvSharp;
using OpenCvSharp.Extensions;

public class VideoProcessor
{

    private CueBallDetector cueBallDetector;

    public VideoProcessor()
    {
        cueBallDetector = new CueBallDetector();
    }

    public List<Bitmap> GetVideoFrames(string videoPath, OpenCvSharp.Size? resolution = null)
    {
        var frames = new List<Bitmap>();
        var capture = new VideoCapture(videoPath);

        if (resolution.HasValue)
        {
            // Set the desired frame resolution
            capture.Set(VideoCaptureProperties.FrameWidth, resolution.Value.Width);
            capture.Set(VideoCaptureProperties.FrameHeight, resolution.Value.Height);
        }

        while (capture.IsOpened())
        {
            var image = new Mat();

            // Read next frame in video file
            capture.Read(image);

            if (image.Empty())
            {
                break;
            }

            // Resize the frame to the specified resolution
            if (resolution.HasValue)
            {
                Cv2.Resize(image, image, resolution.Value);
            }

            frames.Add(BitmapConverter.ToBitmap(image));
        }

        return frames;
    }


    public void ProcessVideoAndDetectCueBall(string videoPath, OpenCvSharp.Size? resolution, PictureBox pictureBox, Label fpsLabel)
    {
        // Get video frames
        List<Bitmap> frames = GetVideoFrames(videoPath, resolution);

        // Detect cue ball in each frame
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        double totalDetectionTime = 0;

        foreach (var frame in frames)
        {
            pictureBox.Image = frame;

            // Start the stopwatch
            stopwatch.Restart();

            // Detect the cue ball in the current frame
            cueBallDetector.FindAndDrawCueBall(pictureBox, 150);

            // Stop the stopwatch and add the elapsed time to the total
            stopwatch.Stop();
            totalDetectionTime += stopwatch.Elapsed.TotalSeconds;

            // Update the FPS label with the current average FPS
            double averageFps = frames.IndexOf(frame) / totalDetectionTime;
            fpsLabel.Text = $"FPS: {averageFps:F2}";

            // Refresh the UI to show the updated image and FPS
            Application.DoEvents();
        }
    }
}