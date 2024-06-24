/// <summary>
/// Stores the results of the image processing for ball detection
/// </summary>
public class ImageProcessingResults : EventArgs
{
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
}
