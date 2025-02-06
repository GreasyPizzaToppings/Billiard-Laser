using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;

public class FrameQueueManager<T> where T : IDisposable
{
    private readonly ConcurrentQueue<T> frames = new ConcurrentQueue<T>();
    private readonly BindingList<int> frameIndices = new BindingList<int>();
    private readonly object lockObject = new object();

    public readonly int MaxFrames;
    public IBindingList FrameIndices => frameIndices;
    public int Count => frames.Count;

    public FrameQueueManager(int maxFrames)
    {
        this.MaxFrames = maxFrames;
    }

    /// <summary>
    /// Adds a new frame to the queue, removing oldest if at capacity
    /// </summary>
    public void Enqueue(T frame)
    {
        if (frame == null)
            throw new ArgumentNullException(nameof(frame));

        lock (lockObject)
        {
            // Remove oldest frame if at capacity
            if (frames.Count >= MaxFrames)
            {
                if (frames.TryDequeue(out T oldFrame))
                {
                    oldFrame.Dispose();
                    if (frameIndices.Count > 0)
                        frameIndices.RemoveAt(0);
                }
            }

            frames.Enqueue(frame);

            // Add index if frame has one
            if (frame is VideoFrame videoFrame)
                frameIndices.Add(videoFrame.index);
        }
    }

    /// <summary>
    /// Attempts to get a frame at a specific index without removing it
    /// </summary>
    public T GetFrame(int index)
    {
        lock (lockObject)
        {
            return frames.FirstOrDefault(f =>
                f is VideoFrame videoFrame &&
                videoFrame.index == index);
        }
    }

    /// <summary>
    /// Gets the latest frame without removing it
    /// </summary>
    public bool TryPeekLatest(out T frame)
    {
        return frames.TryPeek(out frame);
    }

    /// <summary>
    /// Removes and returns the oldest frame
    /// </summary>
    public bool TryDequeue(out T frame)
    {
        if (frames.TryDequeue(out frame))
        {
            lock (lockObject)
            {
                if (frameIndices.Count > 0)
                    frameIndices.RemoveAt(0);
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Safely disposes all frames and clears the queue
    /// </summary>
    public void Clear()
    {
        lock (lockObject)
        {
            while (frames.TryDequeue(out T frame))
            {
                frame.Dispose();
            }
            frameIndices.Clear();
        }
    }

    /// <summary>
    /// Returns all frames as a list without removing them
    /// </summary>
    public List<T> ToList()
    {
        lock (lockObject)
        {
            return frames.ToList();
        }
    }

    /// <summary>
    /// Safely disposes the queue manager and all frames
    /// </summary>
    public void Dispose()
    {
        Clear();
    }
}