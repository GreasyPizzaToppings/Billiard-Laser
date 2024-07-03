using System.Windows.Controls;

/// <summary>
/// A shot is what happens from when the cueball starts moving to when it stops
/// </summary>
public class Shot : IDisposable
{
    public List<PointF> cueBallPath = new List<PointF>();
    private List<VideoFrame> _frames = new List<VideoFrame>();

    public void AddFrame(VideoFrame frame) => _frames.Add(new VideoFrame(frame.frame, frame.index)); //create copy of frame to avoid disposing the original frame
    public VideoFrame GetFrameCopy (int index) => new VideoFrame(_frames[index].frame, _frames[index].index);
    public int FrameCount => _frames.Count;

    /// <summary>
    /// distance travelled per frame
    /// </summary>
    public List<double> FrameDistances => cueBallPath.Count > 1 ? cueBallPath.Zip(cueBallPath.Skip(1), CalculateDistance).ToList() : new List<double>();

    /// <summary>
    /// sum total of distance travelled over the frames of its cueBallPath
    /// </summary>
    public List<double> DistanceTravelledOverTime => FrameDistances.Aggregate(new List<double>(), (cumulativeDistances, distance) =>
    {
        cumulativeDistances.Add(cumulativeDistances.LastOrDefault() + distance);
        return cumulativeDistances;
    });

    public double AverageSpeed => _frames.Count > 0 ? DistanceTravelled / _frames.Count : 0;
    
    public double DistanceTravelled =>  FrameDistances.Sum();

    public double MaxSpeed => FrameDistances.Max();

    public List<double> AccelerationOverTime => FrameDistances
        .Zip(FrameDistances.Skip(1), (prev, curr) => curr - prev)
        .Prepend(0.0) // Add an initial acceleration of 0.0
        .ToList();

    /// <summary>
    /// how much the ball moved from its last position change
    /// </summary>
    public double Displacement
    {
        get
        {
            if (cueBallPath == null || cueBallPath.Count < 2) return 0;
            return Math.Sqrt(Math.Pow(cueBallPath[cueBallPath.Count - 1].X - cueBallPath[cueBallPath.Count - 2].X, 2) + Math.Pow(cueBallPath[cueBallPath.Count - 1].Y - cueBallPath[cueBallPath.Count - 2].Y, 2));
        }
    }

    public double MaxAcceleration => AccelerationOverTime.Max();

    public double AverageAcceleration => AccelerationOverTime.Average();

    public Shot(List<PointF> path) { this.cueBallPath = path; }
    
    public Shot() { }

    /// <summary>
    /// The string of a Shot is represented by the start and end frame numbers
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        VideoFrame startFrame = _frames.First();
        VideoFrame endFrame = _frames.Last();

        return $"{startFrame} - {endFrame}";
    }

    private static double CalculateDistance(PointF point1, PointF point2)
    {
        double dx = point2.X - point1.X;
        double dy = point2.Y - point1.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    public void Dispose()
    {
        foreach (var frame in _frames)
        {
            frame.Dispose();
        }
    }
}