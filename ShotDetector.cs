public class ShotDetector
{
    private bool isCueBallMoving;
    private List<PointF> currentShot;
    private int stationaryFrameCount;

    private const int StationaryThreshold = 75; // Adjust this value as needed
    
    public int shotStartFrame = 0;
    public int shotEndFrame = 0;

    public event EventHandler<List<PointF>> ShotFinished;

    public ShotDetector()
    {
        isCueBallMoving = false;
        currentShot = new List<PointF>();
        stationaryFrameCount = 0;
    }

    public void ProcessFrame(Ball cueBall, int frameIndex)
    {
        // Calculate the speed of the cue ball
        double speed = Math.Sqrt(cueBall.deltaX * cueBall.deltaX + cueBall.deltaY * cueBall.deltaY);
        Console.WriteLine(speed);

        // Check if the cue ball is moving
        if (speed > 0)
        {
            if (!isCueBallMoving)
            {
                // Cue ball starts moving, start a new shot
                isCueBallMoving = true;
                currentShot.Clear();
                stationaryFrameCount = 0;

                shotStartFrame = frameIndex;
                shotEndFrame = 0; //reset
            }
            currentShot.Add(cueBall.centre);
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
                    OnShotFinished(new List<PointF>(currentShot));
                }
            }
        }
    }

    protected virtual void OnShotFinished(List<PointF> shot)
    {
        ShotFinished?.Invoke(this, shot);
    }
}