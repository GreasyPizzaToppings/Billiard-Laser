using OpenCvSharp;
using OpenCvSharp.Extensions;

public class VideoProcessor
{
    /// <summary>
    /// Load the first maxFrames frames into the queue or until the video ends
    /// </summary>
    /// <param name="videoPath"></param>
    /// <param name="outputResolution"></param>
    /// <param name="framesQueue"></param>
    /// <param name="maxFrames"></param>
    public static void EnqueueVideoFrames(string videoPath, OpenCvSharp.Size outputResolution, Queue<VideoFrame> framesQueue, int maxFrames)
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

            if (framesQueue.Count >= maxFrames)
            {
                framesQueue.Dequeue();
            }
            framesQueue.Enqueue(frame);
            index++;
        }
    }

    public static void DequeueVideoFrames(Queue<VideoFrame> framesQueue)
    {
        foreach (var rawFrame in framesQueue)
        {
            rawFrame.Dispose();
        }
        if (framesQueue.Count > 0) framesQueue.Dequeue();
    }
}