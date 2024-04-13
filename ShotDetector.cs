public class ShotDetector
{
    public List<List<PointF>> Shots { get; private set; }
    
    private List<PointF> currentShot;

    private bool isCueBallMoving;
    private int stationaryFrameCount;

    private const int StationaryThreshold = 70; // Adjust this value as needed
    
    public int shotStartFrame = 0;
    public int shotEndFrame = 0;

    private double totalSpeed;
    private Queue<double> lastTenSpeeds;

    public event EventHandler<List<PointF>> ShotFinished;

    public ShotDetector()
    {
        isCueBallMoving = false;
        currentShot = new List<PointF>();
        stationaryFrameCount = 0;
        Shots = new List<List<PointF>>();

        totalSpeed = 0;
        lastTenSpeeds = new Queue<double>();
    }

    public void ProcessFrame(Ball cueBall, int frameIndex)
    {
        // Calculate the speed of the cue ball
        double speed = Math.Sqrt(cueBall.deltaX * cueBall.deltaX + cueBall.deltaY * cueBall.deltaY);
        Console.WriteLine(speed);

        // Check if the cue ball is moving
        if (speed > 0)
        {
            // Cue ball started moving, start a new shot
            if (!isCueBallMoving)
            {
                isCueBallMoving = true;
                currentShot.Clear();
                stationaryFrameCount = 0;

                shotStartFrame = frameIndex;
                shotEndFrame = 0;
                totalSpeed = 0;
                lastTenSpeeds = new Queue<double>();
            }

            currentShot.Add(cueBall.centre);
            totalSpeed += speed;

            lastTenSpeeds.Enqueue(speed);
            if (lastTenSpeeds.Count > 10)
            {
                totalSpeed -= lastTenSpeeds.Dequeue();
            }
        }

        else
        {
            if (isCueBallMoving)
            {
                // Cue ball has stopped moving
                stationaryFrameCount++;

                if (stationaryFrameCount >= StationaryThreshold)
                {
                    // Cue ball has been stationary for the specified duration, end the current shot
                    isCueBallMoving = false;
                    shotEndFrame = frameIndex;
                    double averageSpeed = totalSpeed / currentShot.Count;
                    double averageSpeedLastTen = lastTenSpeeds.Count > 0 ? lastTenSpeeds.Average() : 0;
                    OnShotFinished(new List<PointF>(currentShot), averageSpeed, averageSpeedLastTen);
                }
            }
        }
    }

    protected virtual void OnShotFinished(List<PointF> shot, double averageSpeed, double averageSpeedLastTen)
    {
        Shots.Add(shot);
        ShotFinished?.Invoke(this, shot);

        Console.WriteLine($"Average speed over the whole shot: {averageSpeed}");
        Console.WriteLine($"Average speed for the last 10 frames: {averageSpeedLastTen}");
    }
}