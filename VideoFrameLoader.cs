using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

public static class VideoFrameLoader
{
    /// <summary>
    /// Loads frames into the queue asynchronously, maintaining the maximum frame limit
    /// </summary>
    /// <param name="videoPath">Path to the video file</param>
    /// <param name="outputResolution">Desired output resolution for the frames</param>
    /// <param name="frameManager">The frame queue manager to store frames</param>
    /// <param name="cancellationToken">Token to cancel the loading operation</param>
    /// <returns>The number of frames that were successfully loaded</returns>
    public static async Task<int> LoadFramesAsync(
        string videoPath,
        OpenCvSharp.Size outputResolution,
        FrameQueueManager<VideoFrame> frameManager,
        CancellationToken cancellationToken = default)
    {
        using var capture = new VideoCapture(videoPath);
        if (!capture.IsOpened())
            throw new InvalidOperationException("Failed to open video file.");

        int frameIndex = 0;
        try
        {
            while (capture.IsOpened())
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    throw new OperationCanceledException();
                }

                if (frameManager.Count >= frameManager.MaxFrames)
                    break;

                // Process frame on background thread
                var frame = await Task.Run(() =>
                {
                    using var image = new Mat();
                    if (!capture.Read(image) || image.Empty())
                        return null;

                    Cv2.Resize(image, image, outputResolution);
                    var bitmap = BitmapConverter.ToBitmap(image);
                    return new VideoFrame(bitmap, frameIndex);
                }, cancellationToken);

                if (frame == null)
                    break;

                frameManager.Enqueue(frame);
                frameIndex++;

                // Allow UI thread to process
                await Task.Yield();
            }

            return frameIndex;
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Cancellation requested in videoframeloader");

            // Clean up any partially loaded frame
            if (capture.IsOpened())
            {
                using var image = new Mat();
                capture.Read(image);
            }

            frameManager.Clear();
            return frameIndex;
        }
        catch (Exception)
        {
            throw;
        }
    }
}