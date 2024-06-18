public class ImageProcessingResults : EventArgs
{
    public Bitmap? OriginalImage { get; set; }
    public Bitmap? TransformedImage { get; set; }
    public Bitmap? CueballMask { get; set; }
    public Bitmap? CueballImage { get; set; }
    public Bitmap? TableMask { get; set; }
    public Bitmap? TableWithMaskApplied { get; set; }
    public Bitmap? AllBallsHighlighted { get; set; }
    public Bitmap? FilteredBallsHighlighted { get; set; }
    public Bitmap? TableHighlighted { get; set; }
    public Bitmap? CueBallHighlighted { get; set; }
}
