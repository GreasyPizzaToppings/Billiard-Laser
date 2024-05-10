using OpenCvSharp;
using OpenCvSharp.Extensions;

public class VideoProcessor
{
    public class VideoFrame {

        public Bitmap frame { get; set; }
        public int index { get; set; }


        public VideoFrame(Bitmap frame, int index) { 
            this.frame = frame;
            this.index = index;
        }


        public override string ToString()
        {
            return index.ToString();
        }
    }

    public static List<VideoFrame> GetVideoFrames(string videoPath, OpenCvSharp.Size outputResolution)
    {
        var frames = new List<VideoFrame>();
        var capture = new VideoCapture(videoPath);
        int index = 0;

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

            Bitmap bitmap = BitmapConverter.ToBitmap(image);
            VideoFrame frame = new VideoFrame(bitmap, index);

            frames.Add(frame);
            index++;
        }

        return frames;
    }
}