public class ShotDetector
{
    public List<Shot> Shots { get; private set; }
    private Shot currentShot;
    private bool isCueBallMoving;
    private int stationaryFrameCount;
    private const int StationaryThreshold = 60; // Adjust this value as needed


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

    public void ProcessFrame(Ball cueBall, VideoProcessor.VideoFrame frame)
    {
        double distanceTravelled = Math.Sqrt((cueBall.DeltaX * cueBall.DeltaX) + (cueBall.DeltaY * cueBall.DeltaY));
        Console.WriteLine("Distance travelled in 1 frame: " + distanceTravelled);

        currentShot.AddFrameToShot(frame);
        currentShot.AddPointToPath(cueBall.Centre);
        distancesTravelled.Add(distanceTravelled);

        // Check if the cue ball is moving
        if (distanceTravelled > 0.05) //todo find better way
        {
            // Cue ball started moving, start a new shot
            if (!isCueBallMoving)
            {
                isCueBallMoving = true;
                currentShot = new Shot();
                stationaryFrameCount = 0;
                distancesTravelled.Clear();
            }
        }

        else
        {
            // ball not moving
            if (isCueBallMoving)
            {
                stationaryFrameCount++;
                
                // if shot ended
                if (stationaryFrameCount >= StationaryThreshold)
                {
                    isCueBallMoving = false;
                    OnShotFinished(currentShot);
                }
            }
        }
    }

    protected virtual void OnShotFinished(Shot shot)
    {
        Shots.Add(shot);
        ShotFinished?.Invoke(this, shot);
    }
}