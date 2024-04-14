using System;
using System.Drawing.Drawing2D;
using static VideoProcessor;

public class Shot
{
    private List<PointF> path;
    public List<PointF> Path
    {
        get { return path; }
        set { path = value; }
    }

    private List<VideoProcessor.VideoFrame> frames = new List<VideoProcessor.VideoFrame>();
    public List<VideoProcessor.VideoFrame> ShotFrames { 
        get { return frames; } 
    }

    public double AverageSpeed { get; set; }
    public double PeakSpeed { get; set; }
    public double PeakAcceleration { get; set; }
    public double DistanceTravelled { get; }

    public Shot(List<PointF> path, double averageSpeed, double peakSpeed, double peakAcceleration, double distanceTravelled)
    {
        Path = path;
        AverageSpeed = averageSpeed;
        PeakSpeed = peakSpeed;
        PeakAcceleration = peakAcceleration;
        DistanceTravelled = distanceTravelled;
    }
    public Shot() { }

    public void AddPointToPath(PointF point)
    {
        if (path == null)
        {
            path = new List<PointF>();
        }
        path.Add(point);
    }

    public void AddFrameToShot(VideoProcessor.VideoFrame frame)
    {
        if (frame != null) {
            frames.Add(frame);
        }
    }


    /// <summary>
    /// The String of a Shot is represented by the start and end frame numbers
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        VideoFrame startFrame = ShotFrames.First();
        VideoFrame endFrame = ShotFrames.Last();

        return $"{startFrame} - {endFrame}";
    }
}