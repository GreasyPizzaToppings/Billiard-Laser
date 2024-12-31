using System;

/// <summary>
/// Stores the results of the image processing for laser detection
/// </summary>
public class LaserDetectionResults : IDisposable
{
    private bool disposed = false;

    public Bitmap? OriginalImage { get; set; }
    public Bitmap? TransformedImage { get; set; }
    public Bitmap? LaserMask { get; set; }
    public Bitmap? TableMask { get; set; }
    public Bitmap? TableWithMaskApplied { get; set; }
    public Bitmap? AllCandidatesHighlighted { get; set; }
    public Bitmap? LaserHighlighted { get; set; } //the chosen candidate
    public Laser? Laser { get; set; }

    public LaserDetectionResults()
    {
    }

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
                OriginalImage?.Dispose();
                TransformedImage?.Dispose();
                LaserMask?.Dispose();
                TableMask?.Dispose();
                TableWithMaskApplied?.Dispose();
                AllCandidatesHighlighted?.Dispose();
                LaserHighlighted?.Dispose();

                // Set large objects to null to help the GC
                OriginalImage = null;
                TransformedImage = null;
                LaserMask = null;
                TableMask = null;
                TableWithMaskApplied = null;
                AllCandidatesHighlighted = null;
                LaserHighlighted = null;
                Laser = null;
            }
            disposed = true;
        }
    }

    ~LaserDetectionResults()
    {
        Dispose(false);
    }
}