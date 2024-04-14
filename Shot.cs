using System;
using System.Drawing.Drawing2D;

public class Shot
{
    private List<PointF> path;

    public List<PointF> Path
    {
        get { return path; }
        set { path = value; }
    }

    public double AverageSpeed { get; set; }
    public double PeakSpeed { get; set; }
    public double PeakAcceleration { get; set; }
    public double ShotDuration { get; set; }
    public double DistanceTravelled { get; set; }

    public Shot(List<PointF> path, double averageSpeed, double peakSpeed, double peakAcceleration, double shotDuration, double distanceTravelled)
    {
        Path = path;
        AverageSpeed = averageSpeed;
        PeakSpeed = peakSpeed;
        PeakAcceleration = peakAcceleration;
        ShotDuration = shotDuration;
        DistanceTravelled = distanceTravelled;
    }

    public void AddPointToPath(PointF point)
    {
        if (path == null)
        {
            path = new List<PointF>();
        }
        path.Add(point);
    }

    public Shot() { }
}