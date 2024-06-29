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
        //invalid cue ball
        if (cueBall == null || cueBall.contour == null || cueBall.contour.ToArray().Length <= 0) {
            return;
        }

        currentShot.frames.Add(frame);
        currentShot.cueBallPath.Add(cueBall.Centre);

        Boolean ballNotMoving = false;

        if (isCueBallMoving && currentShot != null && currentShot.frames.Count >= 25 && currentShot.FrameDistances.TakeLast(10).Sum() < 5) {
            ballNotMoving = true;
        }
        
        // criteria for ball moving:
        // 1. ball has moved more than 10 pixel in the last frame
        Boolean ballMoving = currentShot.Displacement > 10;

        Console.WriteLine("Distance travelled in 1 frame: " + currentShot.Displacement);
        Console.WriteLine("Distance in Last 10: " + currentShot.FrameDistances.TakeLast(10).Sum());

        if (ballMoving)
        {
            //mark as moving if not already
            if (!isCueBallMoving)
            {
                isCueBallMoving = true;
                currentShot = new Shot();
                currentShot.frames.Add(frame);
                currentShot.cueBallPath.Add(cueBall.Centre);
            }
        }

        else if (ballNotMoving)
        {
            // ball marked as not moving already
            if (isCueBallMoving)
            {
                if (currentShot.DistanceTravelled > 10)
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