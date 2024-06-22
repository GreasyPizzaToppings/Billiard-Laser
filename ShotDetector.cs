public class ShotDetector
{
    public List<Shot> Shots { get; private set; }
    private Shot currentShot;
    private bool isCueBallMoving;

    public event EventHandler<Shot> ShotFinished;

    public ShotDetector()
    {
        isCueBallMoving = false;
        currentShot = new Shot();
        Shots = new List<Shot>();
    }

    public void ProcessFrame(Ball cueBall, VideoFrame frame)
    {
        currentShot.AddFrameToShot(frame);
        currentShot.AddPointToPath(cueBall.Centre);

        Boolean ballNotMoving = false;
        if (isCueBallMoving && currentShot != null && currentShot.ShotFrameCount >= 25 && currentShot.FrameDistances.TakeLast(25).Sum() < 2) {
            ballNotMoving = true;
        }

        double distanceTravelled = cueBall.Displacement;
        Boolean ballMoving = distanceTravelled > 1;

        Console.WriteLine("Distance travelled in 1 frame: " + distanceTravelled);
        Console.WriteLine("Distance in Last 25: " + currentShot.FrameDistances.TakeLast(25).Sum());

        if (ballMoving)
        {
            //mark as moving if not already
            if (!isCueBallMoving)
            {
                isCueBallMoving = true;
                currentShot = new Shot();
                currentShot.AddFrameToShot(frame);
                currentShot.AddPointToPath(cueBall.Centre);
            }
        }

        else if (ballNotMoving)
        {
            // ball marked as not moving already
            if (isCueBallMoving)
            {
                if (currentShot.DistanceTravelled > 1)
                {
                    OnShotFinished(currentShot);
                }

                // The shot is invalid (false shot), discard it
                else
                {
                    Console.WriteLine("False shot detected, discarding...");
                }

                currentShot = new Shot();
                isCueBallMoving = false;
            }
        }
    }

    protected virtual void OnShotFinished(Shot shot)
    {
        Shots.Add(shot);
        ShotFinished?.Invoke(this, shot);
    }
}