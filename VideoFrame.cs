using Accord.IO;

/// <summary>
/// A container for a table image and its associated frame number
/// </summary>
public class VideoFrame : IDisposable
{
    private Bitmap _frame;
    public int Index { get; set; }
    public int FrameRate { get; set; }

    public Bitmap frame
    {
        get { return _frame; }
        set
        {
            if (_frame != value)
            {
                _frame?.Dispose();
                _frame = value;
            }
        }
    }

    public VideoFrame(Bitmap frame, int index, int frameRate=30)
    {
        this._frame = new Bitmap(frame);
        this.Index = index;
        this.FrameRate = frameRate;
    }

    public override string ToString()
    {
        return Index.ToString();
    }

    public void Dispose()
    {
        _frame?.Dispose();
    }

    public VideoFrame Clone()
    {
        return new VideoFrame(
            new Bitmap(frame), // Creates a new copy of the bitmap
            Index,
            FrameRate
        );
    }
}