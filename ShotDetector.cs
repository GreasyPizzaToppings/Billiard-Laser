public class ShotDetector
{
    public List<Shot> Shots { get; private set; }
    private Shot currentShot;
    private bool isCueBallMoving;
    private int stationaryFrameCount;
    private const int StationaryThreshold = 60; // Adjust this value as needed
    public int shotStartFrame = 0;
    public int shotEndFrame = 0;
    private List<double> distancesTravelled;

    public event EventHandler<Shot> ShotFinished;

    public ShotDetector()
    {
        isCueBallMoving = false;
        currentShot = new Shot();
        stationaryFrameCount = 0;
        Shots = new List<Shot>();
        distancesTravelled = new List<double>();
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
                distancesTravelled.Clear();
            }

            currentShot.AddPointToPath(cueBall.Centre);
            distancesTravelled.Add(distanceTravelled);
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
                    double averageSpeed = distancesTravelled.Count > 0 ? distancesTravelled.Average() : 0;
                    double averageSpeedLastTen = distancesTravelled.Count > 10
                        ? distancesTravelled.Skip(distancesTravelled.Count - 10).Average()
                        : averageSpeed;
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