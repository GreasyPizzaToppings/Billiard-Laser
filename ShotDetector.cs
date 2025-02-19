using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

public class ShotDetector : IDisposable
{
    private Shot currentShot;
    private Shot completedShot;
    private bool isTrackingShot;
    private bool disposed;

    // Configurable parameters
    public int MinFramesForShot = 20;             // Minimum frames needed to consider it a valid shot
    public double MinTotalDistance = 50.0;        // Minimum total distance for a valid shot
    public double StationaryThreshold = 5;        // Maximum movement allowed to consider ball stationary
    public int StationaryFrameWindow = 15;        // Number of frames to check for ball being stationary

    //keep a rolling window of this data even without a shot being tracked
    private const int queueSize = 10;
    private Queue<Point> recentPositions = new(queueSize);
    private Queue<VideoFrame> recentFrames = new(queueSize);

    private double recentDisplacement;

    public event EventHandler<Shot>? ShotFinished;

    public ShotDetector()
    {
        ResetState();
    }

    public void ResetState()
    {
        currentShot?.Dispose();
        currentShot = new Shot();
        completedShot?.Dispose();
        completedShot = new Shot();

        isTrackingShot = false;
        recentPositions.Clear();

        foreach (var frame in recentFrames) frame.Dispose();
        recentFrames.Clear();
    }

    public void ProcessFrame(Ball? cueBall, VideoFrame frame)
    {
        if (disposed) throw new ObjectDisposedException(nameof(ShotDetector));
        if (cueBall == null) return;

        // queues storing data to be ready for new shots
        recentPositions.Enqueue(cueBall.Centre);
        if (recentPositions.Count > queueSize) recentPositions.Dequeue();

        recentDisplacement = cueBall.Displacement;

        recentFrames.Enqueue(frame.Clone());
        if (recentFrames.Count > queueSize) recentFrames.Dequeue().Dispose();

        if (isTrackingShot)
        {
            currentShot.AddFrame(frame.Clone());
            currentShot.cueBallPath.Add(cueBall.Centre);
        }

        if (isTrackingShot && StoppedMoving() && IsValidShot()) FinalizeShot();
        else if (StartedMoving() & !isTrackingShot) StartNewShot();
    }

    private bool IsValidShot() {
        if (currentShot.DistanceTravelled < MinTotalDistance || currentShot.FrameCount < MinFramesForShot) return false;
        return true;
    }

    /// <summary>
    /// The ball is moving if it moved more than the threshold since the last frame
    /// </summary>
    /// <returns></returns>
    private bool StartedMoving()
    {
        return recentDisplacement >= StationaryThreshold;
    }

    /// <summary>
    /// The ball is deemed stopped if it hasnt moved above StationaryThreshold in the last StationaryFrameWindow frames
    /// </summary>
    /// <returns></returns>
    private bool StoppedMoving() {
        if (currentShot.FrameDistances.TakeLast(StationaryFrameWindow).Sum() < StationaryThreshold) return true;
        return false;
    }

    private void StartNewShot()
    {
        currentShot.Dispose();
        currentShot = new Shot();

        // Add recent history
        foreach (var position in recentPositions) currentShot.cueBallPath.Add(position);
        foreach (var videoFrame in recentFrames) currentShot.AddFrame(videoFrame.Clone());
        
        isTrackingShot = true;
    }

    private void FinalizeShot()
    {
        if (currentShot != null)
        {
            completedShot = currentShot;
            currentShot = new Shot();

            Task.Run(() => 
            {
                OnShotFinished(completedShot.Clone());
                completedShot.Dispose();
                completedShot = new Shot();
            });

            isTrackingShot = false;
        }
    }

    protected virtual void OnShotFinished(Shot shot)
    {
        ShotFinished?.Invoke(this, shot);
    }

    public void Dispose()
    {
        if (currentShot != null) currentShot.Dispose();
        disposed = true;
    }
}