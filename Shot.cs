using static VideoProcessor;

public class Shot
{
    private List<PointF> path;
    public List<PointF> Path
    {
        get { return path; }
        set { path = value; }
    }

    private List<VideoFrame> frames = new List<VideoFrame>();
    public List<VideoFrame> ShotFrames => frames;
    public int ShotFrameCount => ShotFrames.Count;

    /// <summary>
    /// distance travelled per frame
    /// </summary>
    public List<double> FrameDistances => Path.Zip(Path.Skip(1), CalculateDistance).ToList();
    
    /// <summary>
    /// sum total of distance travelled over the frames of its path
    /// </summary>
    public List<double> DistanceTravelledOverTime => FrameDistances.Aggregate(new List<double>(), (cumulativeDistances, distance) =>
    {
        cumulativeDistances.Add(cumulativeDistances.LastOrDefault() + distance);
        return cumulativeDistances;
    });

    public double AverageDistance => ShotFrameCount > 0 ? DistanceTravelled / ShotFrameCount : 0;
    public double DistanceTravelled =>  FrameDistances.Sum();

    public double PeakSpeed => FrameDistances.Max();

    public List<double> AccelerationOverTime => FrameDistances
        .Zip(FrameDistances.Skip(1), (prev, curr) => curr - prev)
        .Prepend(0.0) // Add an initial acceleration of 0.0
        .ToList();

    public double PeakAcceleration => AccelerationOverTime.Max();

    public Shot(List<PointF> path)
    {
        Path = path;
    }
    
    public Shot() { }


    public void AddPointToPath(PointF point)
    {
        path ??= new List<PointF>();
        path.Add(point);
    }

    public void AddFrameToShot(VideoProcessor.VideoFrame frame)
    {
        frames.Add(frame);
    }

    private double CalculateDistance(PointF point1, PointF point2)
    {
        double dx = point2.X - point1.X;
        double dy = point2.Y - point1.Y;
        return Math.Sqrt(dx * dx + dy * dy);
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