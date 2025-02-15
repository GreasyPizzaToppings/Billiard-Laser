/// <summary>
/// Stores the results of the image processing for cueball detection
/// </summary>
public class CueBallDetectionResults : IDisposable
{
    private bool disposed = false;

    public VideoFrame? OriginalFrame { get; set; }
    public Bitmap? WorkingImage { get; set; }
    public Bitmap? CueBallMask { get; set; }
    public Bitmap? CueBallMaskApplied { get; set; }
    public Bitmap? AllContoursHighlighted { get; set; }
    public Bitmap? CueBallCandidatesHighlighted { get; set; }
    public Bitmap? ScoredCandidatesHighlighted { get; set; }
    public Bitmap? CueBallHighlighted { get; set; }
    public Bitmap? TableMaskApplied { get; set; }

    public Ball? CueBall { get; set; }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                // Dispose managed resources
                OriginalFrame?.Dispose();
                WorkingImage?.Dispose();
                CueBallMask?.Dispose();
                CueBallMaskApplied?.Dispose();
                AllContoursHighlighted?.Dispose();
                CueBallCandidatesHighlighted?.Dispose();
                ScoredCandidatesHighlighted?.Dispose();
                CueBallHighlighted?.Dispose();
                TableMaskApplied?.Dispose();
                CueBall?.Dispose();

                // Set large objects to null to help the GC
                OriginalFrame = null;
                WorkingImage = null;
                CueBallMask = null;
                CueBallMaskApplied = null;
                AllContoursHighlighted = null;
                CueBallCandidatesHighlighted = null; 
                ScoredCandidatesHighlighted = null;
                CueBallHighlighted = null;
                TableMaskApplied = null;
                CueBall = null;
            }

            disposed = true;
        }
    }

    ~CueBallDetectionResults()
    {
        Dispose(false);
    }
}