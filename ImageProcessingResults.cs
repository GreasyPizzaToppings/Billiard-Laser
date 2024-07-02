using System;
using System.Collections.Generic;
using System.Drawing;

/// <summary>
/// Stores the results of the image processing for ball detection
/// </summary>
public class ImageProcessingResults : IDisposable
{
    private bool disposed = false;

    //images generated
    public Bitmap? OriginalImage { get; set; }
    public Bitmap? TransformedImage { get; set; }
    public Bitmap? CueBallMask { get; set; }
    public Bitmap? CueBallHighlighted { get; set; }
    public Bitmap? TableMask { get; set; }
    public Bitmap? TableWithMaskApplied { get; set; }
    public Bitmap? TableBoundaryHighlighted { get; set; }
    public Bitmap? AllBallsHighlighted { get; set; }
    public Bitmap? FilteredBallsHighlighted { get; set; }

    //objects found
    public Ball? CueBall { get; set; }
    public List<Ball>? Balls { get; set; }

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
                CueBallMask?.Dispose();
                CueBallHighlighted?.Dispose();
                TableMask?.Dispose();
                TableWithMaskApplied?.Dispose();
                TableBoundaryHighlighted?.Dispose();
                AllBallsHighlighted?.Dispose();
                FilteredBallsHighlighted?.Dispose();

                CueBall?.Dispose();
                if (Balls != null)
                {
                    foreach (var ball in Balls)
                    {
                        ball.Dispose();
                    }
                }

                // Set large objects to null to help the GC
                OriginalImage = null;
                TransformedImage = null;
                CueBallMask = null;
                CueBallHighlighted = null;
                TableMask = null;
                TableWithMaskApplied = null;
                TableBoundaryHighlighted = null;
                AllBallsHighlighted = null;
                FilteredBallsHighlighted = null;
                CueBall = null;
                Balls = null;
            }

            disposed = true;
        }
    }

    ~ImageProcessingResults()
    {
        Dispose(false);
    }
}