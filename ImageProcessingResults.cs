public class ImageProcessingResults : EventArgs
{
    public Bitmap? OriginalImage { get; set; }
    public Bitmap? BlurredImage { get; set; }
    public Bitmap? SharpenedImage { get; set; }
    public Bitmap? BlurredAndSharpenedImage { get; set; }
    public Bitmap? ImageMask { get; set; }
    public Bitmap? ImageWithMaskApplied { get; set; }
    public Bitmap? AllBallsHighlighted { get; set; }
    public Bitmap? FilteredBallsHighlighted { get; set; }
    public Bitmap? TableHighlighted { get; set; }
    public Bitmap? CueBallHighlighted { get; set; }
}
