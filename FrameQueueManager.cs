using System;
using System.Collections.Generic;
using System.ComponentModel;

public class FrameQueueManager<T> where T : IDisposable
{
    private readonly T[] frames;
    private readonly BindingList<int> frameIndices = new BindingList<int>();
    private int writePosition = 0;
    private int readPosition = 0;
    private int count = 0;
    private readonly int batchClearSize;

    public readonly int MaxFrames;
    public IBindingList FrameIndices => frameIndices;
    public int Count => count;

    public FrameQueueManager(int maxFrames)
    {
        MaxFrames = maxFrames;
        frames = new T[maxFrames];
        batchClearSize = (int)(maxFrames * 0.15); // clear 15% at once
    }

    /// <summary>
    /// Adds a new frame to the queue, removing oldest if at capacity
    /// </summary>
    public void Enqueue(T frame)
    {
        if (frame == null) throw new ArgumentNullException(nameof(frame));

        // if we're at capacity, batch clear old frames
        if (count >= MaxFrames)
        {
            // clear batch of oldest frames
            for (int i = 0; i < batchClearSize; i++)
            {
                frames[readPosition]?.Dispose();
                frames[readPosition] = default;
                if (frameIndices.Count > 0) frameIndices.RemoveAt(0);
                readPosition = (readPosition + 1) % MaxFrames;
                count--;
            }
        }

        // add new frame
        frames[writePosition] = frame;
        if (frame is VideoFrame videoFrame) frameIndices.Add(videoFrame.Index);
        
        writePosition = (writePosition + 1) % MaxFrames;
        count++;
    }

    /// <summary>
    /// Attempts to get a frame at a specific Index without removing it
    /// </summary>
    public T GetFrame(int index)
    {
        int position = frameIndices.IndexOf(index);
        if (position == -1) return default;
        
        // calculate actual position in circular buffer
        int actualPos = (readPosition + position) % MaxFrames;
        return frames[actualPos];
    }

    /// <summary>
    /// Gets the latest frame without removing it
    /// </summary>
    public T GetLatestFrame()
    {
        if (count == 0) return default;
        int lastPos = (writePosition - 1 + MaxFrames) % MaxFrames;
        return frames[lastPos];
    }

    /// <summary>
    /// Safely disposes all frames and clears the queue
    /// </summary>
    public void Clear()
    {
        for (int i = 0; i < MaxFrames; i++) frames[i]?.Dispose();
        Array.Clear(frames, 0, frames.Length);
        frameIndices.Clear();
        writePosition = 0;
        readPosition = 0;
        count = 0;
    }

    /// <summary>
    /// Safely disposes the queue manager and all frames
    /// </summary>
    public void Dispose() => Clear();
}