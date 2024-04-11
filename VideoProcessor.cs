using OpenCvSharp;
using OpenCvSharp.Extensions;

public class VideoProcessor
{

    private CueBallDetector cueBallDetector;
    private string videoPath;
    private OpenCvSharp.Size outputResolution;

    public VideoProcessor(string videoPath, OpenCvSharp.Size outputResolution)
    {
        cueBallDetector = new CueBallDetector();
        this.videoPath = videoPath;
        this.outputResolution = outputResolution;
    }


    public List<Bitmap> GetCueballDetectionFrames()
    {
        List<Bitmap> frames = GetVideoFrames();

        foreach (Bitmap frame in frames) { 
            
        }

        return frames;
    }

    private List<Bitmap> GetVideoFrames()
    {
        var frames = new List<Bitmap>();
        var capture = new VideoCapture(videoPath);

        while (capture.IsOpened())
        {
            var image = new Mat();

            // Read next frame in video file
            capture.Read(image);

            if (image.Empty())
            {
                break;
            }

            Cv2.Resize(image, image, outputResolution);
            
            frames.Add(BitmapConverter.ToBitmap(image));
        }

        return frames;
    }

    public void ProcessVideoAndDetectCueBall(PictureBox pictureBox, Label fpsLabel)
    {
        // Get video frames
        List<Bitmap> frames = GetVideoFrames();

        // Detect cue ball in each frame
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        double totalDetectionTime = 0;

        foreach (var frame in frames)
        {
            pictureBox.Image = frame;

            // Start the stopwatch
            stopwatch.Restart();

            // Detect the cue ball in the current frame
            Bitmap highlightedImage = cueBallDetector.HighlightCueBall(pictureBox.Image, 150);
            pictureBox.Image = highlightedImage;

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