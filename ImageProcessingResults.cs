public class ImageProcessingResults : EventArgs
{
    public Bitmap OriginalImage { get; set; }
    public Bitmap BlurredImage { get; set; }
    public Bitmap SharpenedImage { get; set; }
    public Bitmap BlurredAndSharpenedImage { get; set; }
    public Bitmap ImageMask { get; set; }
    public Bitmap ImageWithMaskApplied { get; set; }
    public Bitmap AllBallsFound { get; set; }
    public Bitmap FilteredBallsFound { get; set; }
}
