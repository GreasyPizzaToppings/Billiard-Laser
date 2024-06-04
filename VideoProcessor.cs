using OpenCvSharp;
using OpenCvSharp.Extensions;

public class VideoProcessor
{
    public static List<VideoFrame> GetVideoFrames(string videoPath, OpenCvSharp.Size outputResolution)
    {
        var frames = new List<VideoFrame>();
        var capture = new VideoCapture(videoPath);
        int index = 0;

        while (capture.IsOpened())
        {
            Mat image = new Mat();

            // Read next frame in video file
            capture.Read(image);

            if (image.Empty())
            {
                break;
            }

            Cv2.Resize(image, image, outputResolution);
            Bitmap bitmap = BitmapConverter.ToBitmap(image); // Corrected line
            VideoFrame frame = new VideoFrame(bitmap, index);

            frames.Add(frame);
            index++;
        }
        return frames;
    }
}