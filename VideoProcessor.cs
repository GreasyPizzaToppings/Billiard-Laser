using OpenCvSharp;
using OpenCvSharp.Extensions;

public class VideoProcessor
{
    public static void EnqueueVideoFrames(string videoPath, OpenCvSharp.Size outputResolution, Queue<VideoFrame> rawFramesQueue, int maxFrames)
    {
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

}