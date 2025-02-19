using System;
using System.Collections.Generic;
using System.ComponentModel;

/// <summary>
/// A queue that can handle VideoFrames and any other type
/// </summary>
/// <typeparam name="T"></typeparam>
public class QueueManager<T> where T : IDisposable
{
    private readonly T[] items;
    private readonly BindingList<int> indices = new BindingList<int>();
    private int writePosition = 0;
    private int readPosition = 0;
    private int currentCount = 0;
    private int totalCount = 1; // total number of items we have ever added to the queue (can exceed max)

    public readonly int MaxSize;
    public IBindingList Indices => indices;
    public int Count => currentCount;

    public QueueManager(int maxSize)
    {
        MaxSize = maxSize;
        items = new T[maxSize];
    }

    /// <summary>
    /// Adds a new item to the queue, removing oldest if at capacity
    /// </summary>
    public void Enqueue(T item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        if (currentCount >= MaxSize)
        {
            items[readPosition]?.Dispose();
            items[readPosition] = default;
            if (indices.Count > 0) indices.RemoveAt(0);
            readPosition = (readPosition + 1) % MaxSize;
            currentCount--;
        }

        // add new item
        items[writePosition] = item;
        if (item is VideoFrame videoFrame) indices.Add(videoFrame.Index);
        else indices.Add(totalCount);

        writePosition = (writePosition + 1) % MaxSize;
        currentCount++;
        totalCount++;
    }

    /// <summary>
    /// Attempts to get a item at a specific Index without removing it
    /// </summary>
    public T GetItem(int index)
    {
        int position = indices.IndexOf(index);
        if (position == -1) return default;
        
        // calculate actual position in circular buffer
        int actualPos = (readPosition + position) % MaxSize;
        return items[actualPos];
    }

    /// <summary>
    /// Gets the latest item without removing it
    /// </summary>
    public T GetLatestItem()
    {
        if (currentCount == 0) return default;
        int lastPos = (writePosition - 1 + MaxSize) % MaxSize;
        return items[lastPos];
    }

    /// <summary>
    /// Safely disposes all items and clears the queue
    /// </summary>
    public void Clear()
    {
        for (int i = 0; i < MaxSize; i++) items[i]?.Dispose();
        Array.Clear(items, 0, items.Length);
        indices.Clear();
        writePosition = 0;
        readPosition = 0;
        currentCount = 0;
        totalCount = 1;
    }

    /// <summary>
    /// Safely disposes the queue manager and all items
    /// </summary>
    public void Dispose() => Clear();
}