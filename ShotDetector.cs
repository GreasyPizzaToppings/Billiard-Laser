public class ShotDetector
{
    public List<Shot> Shots { get; private set; }
    
    private Shot currentShot;

    private bool isCueBallMoving;
    private int stationaryFrameCount;

    private const int StationaryThreshold = 60; // Adjust this value as needed
    
    public int shotStartFrame = 0;
    public int shotEndFrame = 0;


    //todo? just use a list of all distances travelled?
    private double totalDistanceTravelled;
    private Queue<double> lastTenDistanceTravelled;

    public event EventHandler<Shot> ShotFinished;

    public ShotDetector()
    {
        isCueBallMoving = false;
        currentShot = new Shot();
        stationaryFrameCount = 0;
        Shots = new List<Shot>();

        totalDistanceTravelled = 0;
        lastTenDistanceTravelled = new Queue<double>();
    }

    public void ProcessFrame(Ball cueBall, int frameIndex)
    {
        double distanceTravelled = Math.Sqrt((cueBall.DeltaX * cueBall.DeltaX) + (cueBall.DeltaY * cueBall.DeltaY));
        Console.WriteLine("Distance travelled in 1 frame: " + distanceTravelled);

        // Check if the cue ball is moving
        if (distanceTravelled > 0.05)
        {
            // Cue ball started moving, start a new shot
            if (!isCueBallMoving)
            {
                isCueBallMoving = true;
                currentShot = new Shot();
                stationaryFrameCount = 0;

                shotStartFrame = frameIndex;
                shotEndFrame = 0;
                totalDistanceTravelled = 0;
                lastTenDistanceTravelled = new Queue<double>();
            }

            currentShot.AddPointToPath(cueBall.Centre);
            totalDistanceTravelled += distanceTravelled;

            lastTenDistanceTravelled.Enqueue(distanceTravelled);
            if (lastTenDistanceTravelled.Count > 10)
            {
                totalDistanceTravelled -= lastTenDistanceTravelled.Dequeue();
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
                    double averageSpeed = totalDistanceTravelled / currentShot.Path.Count;
                    double averageSpeedLastTen = lastTenDistanceTravelled.Count > 0 ? lastTenDistanceTravelled.Average() : 0;
                    OnShotFinished(currentShot, averageSpeed, averageSpeedLastTen);
                }
            }
        }
    }

    protected virtual void OnShotFinished(Shot shot, double averageSpeed, double averageSpeedLastTen)
    {
        Shots.Add(shot);
        ShotFinished?.Invoke(this, shot);

        Console.WriteLine($"Average speed over the whole shot: {averageSpeed}");
        Console.WriteLine($"Average speed for the last 10 frames: {averageSpeedLastTen}");
    }
}