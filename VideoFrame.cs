/// <summary>
/// A container for a table image and its associated frame number
/// </summary>
public class VideoFrame : IDisposable
{
    private Bitmap _frame;
    public int index { get; set; }

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

    public VideoFrame(Bitmap frame, int index)
    {
        this._frame = frame;
        this.index = index;
    }

    public override string ToString()
    {
        return index.ToString();
    }

    public void Dispose()
    {
        _frame?.Dispose();
    }
}