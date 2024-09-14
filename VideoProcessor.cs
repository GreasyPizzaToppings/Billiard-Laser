using OpenCvSharp;
using OpenCvSharp.Extensions;

public class VideoProcessor
{
    /// <summary>
    /// Load the first maxFrames frames into the queue or until the video ends
    /// </summary>
    /// <param name="videoPath"></param>
    /// <param name="outputResolution"></param>
    /// <param name="rawFramesQueue"></param>
    /// <param name="maxFrames"></param>
    public static void EnqueueVideoFrames(string videoPath, OpenCvSharp.Size outputResolution, Queue<VideoFrame> rawFramesQueue, int maxFrames)
    {
        var capture = new VideoCapture(videoPath);
        int index = 0;

        while (capture.IsOpened() && index < maxFrames)
        {
            Mat image = new Mat();

            // Read next frame in video file
            capture.Read(image);

            if (image.Empty())
            {
                break;
            }

            Cv2.Resize(image, image, outputResolution);
            Bitmap bitmap = BitmapConverter.ToBitmap(image);
            VideoFrame frame = new VideoFrame(bitmap, index);

            

            if (rawFramesQueue.Count >= maxFrames)
            {
                rawFramesQueue.Dequeue();
            }
            rawFramesQueue.Enqueue(frame);
            index++;
        }
    }
    public static void DequeueVideoFrames(Queue<VideoFrame> rawFramesQueue)
    {
        foreach (var rawFrame in rawFramesQueue)
        {
            rawFrame.Dispose();
        }
        if (rawFramesQueue.Count > 0)rawFramesQueue.Dequeue();
    }
}