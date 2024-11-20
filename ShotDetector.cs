public class ShotDetector
{
    private Shot currentShot;
    private bool isCueBallMoving;

    public event EventHandler<Shot> ShotFinished;

    public ShotDetector()
    {
        ResetState();
    }

    public void ResetState()
    {
        // Dispose of any existing shot
        if (currentShot != null)
        {
            currentShot.Dispose();
        }

        // Reset to initial state
        isCueBallMoving = false;
        currentShot = new Shot();
    }

    public void ProcessFrame(Ball cueBall, VideoFrame frame)
    {
        //Console.WriteLine($"\nShot Detector: Frame {frame.index}\n" +
            //$"Cueball contour length: {cueBall.Contour.ToArray().Length}\n");

        //invalid cue ball
        if (cueBall == null || cueBall.Contour == null || cueBall.Contour.ToArray().Length <= 0) {
            return;
        }

        currentShot.AddFrame(frame);
        currentShot.cueBallPath.Add(cueBall.Centre);

        Boolean ballNotMoving = false;

        if (isCueBallMoving && currentShot != null && currentShot.FrameCount >= 25 && currentShot.FrameDistances.TakeLast(10).Sum() < 5) {
            ballNotMoving = true;
        }
        
        // criteria for ball moving:
        // 1. ball has moved more than 10 pixel in the last frame
        Boolean ballMoving = currentShot.Displacement > 10;

        //Console.WriteLine("Distance travelled in 1 frame: " + currentShot.Displacement);
        //Console.WriteLine("Distance in Last 10: " + currentShot.FrameDistances.TakeLast(10).Sum());

        if (ballMoving)
        {
            //mark as moving if not already
            if (!isCueBallMoving)
            {
                isCueBallMoving = true;
                currentShot.Dispose();
                currentShot = new Shot();
                currentShot.AddFrame(frame);
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
                    //Console.WriteLine("False shot detected, discarding...");
                }

                currentShot = new Shot();
                isCueBallMoving = false;
            }
        }
    }

    protected virtual void OnShotFinished(Shot shot)
    {
        ShotFinished?.Invoke(this, shot);
    }
}