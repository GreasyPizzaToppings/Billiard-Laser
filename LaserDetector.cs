using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV;

public class LaserDetector : TableObjectDetector, IDisposable
{
    private bool disposed = false;

    //laser detection masks
    public Rgb LowerLaserMask = new Rgb(42, 145, 52);
    public Rgb UpperLaserMask = new Rgb(97, 255, 220);

    public double MinLaserArea = 3;
    public double MaxLaserArea = 150;

    // temporal tracking parameters
    private Queue<Point> lastPositions = new Queue<Point>();
    private const int MaxPositionHistory = 5;

    private DateTime lastDetectionTime = DateTime.MinValue;
    public double MaxMovementPerSecond = 400.0; // pixels per second
    public double IntensityWeight = 0.35;
    public double DistanceWeight = 0.65;
    //public TimeSpan TrackingTimeout = TimeSpan.FromSeconds(1); // Reset tracking if no detection for this long

    public LaserDetector(bool enableBlur = true, bool enableSharpening = false, bool enableTableBoundary = false)
        : base(enableBlur, enableSharpening, enableTableBoundary)
    {
    }

    public void CalibrateLaserPosition(Point laserPosition) {
        lastPositions.Clear();
        for (int i = 1; i <= MaxPositionHistory; i++)
        {
            lastPositions.Enqueue(laserPosition);
        }
        lastDetectionTime = DateTime.Now;
    }

    /// <summary>
    /// Given an image of the table, identify the laser and the stages of processing
    /// </summary>
    /// <param name="tableImage"></param>
    /// <returns></returns>
    public LaserDetectionResults ProcessLaserDetection(Bitmap tableImage)
    {
        LaserDetectionResults laserDetectionResults = new LaserDetectionResults();
        laserDetectionResults.OriginalImage = new Bitmap(tableImage);

        Bitmap workingImage = new Bitmap(tableImage);
        using var workingMat = new Mat();
        BitmapToMat(workingImage, workingMat);

        if (EnableSharpening) SharpenImage(workingMat);
        if (EnableBlur) BlurImage(workingMat);

        workingImage.Dispose();
        workingImage = workingMat.ToBitmap();
        laserDetectionResults.WorkingImage = new Bitmap(workingImage);

        using (var workingImageRgb = workingImage.ToImage<Rgb, byte>())
        {
            laserDetectionResults.LaserMask = GetMaskImage(workingImage, LowerLaserMask, UpperLaserMask);
            laserDetectionResults.LaserMaskApplied = ApplyMask(workingImage, laserDetectionResults.LaserMask);

            using (var allContoursFound = GetAllContours(laserDetectionResults.LaserMaskApplied))
            {
                VectorOfPoint? tableContour = EnableTableBoundary ? GetTableContour(allContoursFound, tableImage.Size) : null;
                var candidates = new List<Laser>();

                // Collect all valid candidates
                for (int i = 0; i < allContoursFound.Size; i++)
                {
                    using (var contour = allContoursFound[i])
                    {
                        double area = CvInvoke.ContourArea(contour);
                        if (area < MinLaserArea || area > MaxLaserArea)
                            continue;

                        if (tableContour != null && !IsContourInside(contour, tableContour))
                            continue;

                        var moments = CvInvoke.Moments(contour);
                        int centerX = (int)(moments.M10 / moments.M00);
                        int centerY = (int)(moments.M01 / moments.M00);
                        Point location = new Point(centerX, centerY);

                        double intensity = GetContourIntensity(contour, allContoursFound, i, workingImageRgb, tableImage.Size);
                        candidates.Add(new Laser(location, intensity, area));
                    }
                }

                // create image of all contours/candidates detected, regardless of size or brightness or location etc
                laserDetectionResults.AllCandidatesHighlighted = DrawContours(allContoursFound, tableImage.ToImage<Rgb, byte>());

                using (var filteredCandidatesImage = CreateFilteredCandidatesVisualization(workingImage, candidates))
                {
                    laserDetectionResults.FilteredCandidatesHighlighted = filteredCandidatesImage.ToBitmap();
                }

                //process candidates
                if (candidates.Any())
                {
                    var timeSinceLastDetection = DateTime.Now - lastDetectionTime;

                    // Reset tracking if too much time has passed
                    // if (timeSinceLastDetection > TrackingTimeout) lastPositions.Clear();

                    candidates = ScoreCandidates(candidates, timeSinceLastDetection);

                    // Create visualization of scored candidates
                    using (var scoredCandidatesImage = CreateScoredCandidatesVisualization(workingImage, candidates))
                    {
                        laserDetectionResults.ScoredCandidatesHighlighted = scoredCandidatesImage.ToBitmap();
                    }

                    // Select and store best candidate
                    var bestCandidate = candidates.OrderByDescending(c => c.Confidence).First();
                    
                    //if we dont find a laser, just return images
                    if (bestCandidate.Confidence <= 0) {
                        return laserDetectionResults;
                    }

                    laserDetectionResults.Laser = bestCandidate;

                    // Create final visualization
                    using (var finalImage = HighlightBestLaserCandidate(tableImage, bestCandidate, GetAveragePosition()))
                    {
                        laserDetectionResults.LaserHighlighted = finalImage.ToBitmap();
                    }

                    // Update position history
                    lastPositions.Enqueue(bestCandidate.Location);
                    while (lastPositions.Count > MaxPositionHistory)
                    {
                        lastPositions.Dequeue();
                    }

                    lastDetectionTime = DateTime.Now;
                }
            }
        }

        return laserDetectionResults;
    }

    /// <summary>
    /// Gets the average position from the position history queue
    /// </summary>
    private Point? GetAveragePosition()
    {
        if (lastPositions.Count == 0)
            return null;

        int sumX = 0;
        int sumY = 0;
        foreach (var position in lastPositions)
        {
            sumX += position.X;
            sumY += position.Y;
        }

        return new Point(
            sumX / lastPositions.Count,
            sumY / lastPositions.Count
        );
    }

    private Image<Rgb, byte> CreateFilteredCandidatesVisualization(Bitmap baseImage, List<Laser> candidates)
    {
        var image = baseImage.ToImage<Rgb, byte>();
        foreach (var candidate in candidates)
        {
            CvInvoke.Circle(
                image,
                candidate.Location,
                10,
                new MCvScalar(0, 255, 0), // Green for all candidates
                2);
        }
        return image;
    }

    private Image<Rgb, byte> CreateScoredCandidatesVisualization(Bitmap baseImage, List<Laser> candidates)
    {
        var image = baseImage.ToImage<Rgb, byte>();
        foreach (var candidate in candidates)
        {
            int intensity = (int)(candidate.Confidence * 255);
            CvInvoke.Circle(
                image,
                candidate.Location,
                10,
                new MCvScalar(intensity, intensity, intensity), // Grayscale intensity based on score
                2);
        }
        return image;
    }

    private Image<Rgb, byte> HighlightBestLaserCandidate(Bitmap baseImage, Laser bestCandidate, Point? averagePosition)
    {
        var image = baseImage.ToImage<Rgb, byte>();

        // Draw blue circle for current detection
        CvInvoke.Circle(
            image,
            bestCandidate.Location,
            10,
            new MCvScalar(0, 0, 255),
            2);

        // Draw yellow circle for average position if available
        if (averagePosition.HasValue)
        {
            CvInvoke.Circle(
                image,
                averagePosition.Value,
                8,
                new MCvScalar(255, 255, 0),
                1);
        }

        return image;
    }

    private List<Laser> ScoreCandidates(List<Laser> candidates, TimeSpan timeSinceLastDetection)
    {
        if (!candidates.Any()) return candidates;

        // Get average position from history
        Point? averagePosition = GetAveragePosition();

        // Normalize intensities
        double maxIntensity = candidates.Max(c => c.Intensity);
        foreach (var candidate in candidates)
        {
            double normalizedIntensity = candidate.Intensity / maxIntensity;
            double distanceScore = 1.0;

            if (averagePosition.HasValue)
            {
                double maxDistance = MaxMovementPerSecond * timeSinceLastDetection.TotalSeconds;
                double distanceTravelled = Math.Sqrt(
                    Math.Pow(candidate.Location.X - averagePosition.Value.X, 2) +
                    Math.Pow(candidate.Location.Y - averagePosition.Value.Y, 2));

                // dont pick lasers with impossible movements. just wait until we are more confident
                if (distanceTravelled > maxDistance) {
                    candidate.Confidence = -1;
                    continue;
                }

                // Normalize distance score (closer to last position = higher score)
                distanceScore = Math.Max(0, 1 - (distanceTravelled / maxDistance));
            }

            // Calculate final score using weights
            candidate.Confidence = (normalizedIntensity * IntensityWeight) + (distanceScore * DistanceWeight);
        }

        return candidates;
    }

    /// <summary>
    /// Calculates the intensity value for a given contour in an image
    /// </summary>
    /// <param name="contour">The contour to analyze</param>
    /// <param name="allContours">All contours in the image</param>
    /// <param name="contourIndex">Index of the current contour in allContours</param>
    /// <param name="workingImageRgb">The RGB image being analyzed</param>
    /// <param name="imageSize">Size of the image</param>
    /// <returns>The calculated intensity value for the contour</returns>
    private double GetContourIntensity(VectorOfPoint contour, VectorOfVectorOfPoint allContours, int contourIndex,
        Image<Rgb, byte> workingImageRgb, Size imageSize)
    {
        using (var mask = new Mat(imageSize, DepthType.Cv8U, 1))
        {
            mask.SetTo(new MCvScalar(0));
            CvInvoke.DrawContours(mask, allContours, contourIndex, new MCvScalar(255), -1);

            using (var roi = new Mat())
            {
                CvInvoke.BitwiseAnd(workingImageRgb, workingImageRgb, roi, mask);
                var mean = CvInvoke.Mean(roi, mask);
                return mean.V2; // Red channel intensity
            }
        }
    }

    public override string ToString()
    {
        return $"LaserDetector Settings:\n" +
               $"  Blur: {EnableBlur}\n" +
               $"  Sharpen: {EnableSharpening}\n" +
               $"  TableBoundary: {EnableTableBoundary}\n" +
               $"  Lower Laser Mask: RGB({LowerLaserMask.Red}, {LowerLaserMask.Green}, {LowerLaserMask.Blue})\n" +
               $"  Upper Laser Mask: RGB({UpperLaserMask.Red}, {UpperLaserMask.Green}, {UpperLaserMask.Blue})\n" +
               $"  Min Laser Area: {MinLaserArea}\n" +
               $"  Max Laser Area: {MaxLaserArea}\n" +
               $"  Max Movement/Sec: {MaxMovementPerSecond}\n" +
               $"  Intensity Weight: {IntensityWeight}\n" +
               $"  Distance Weight: {DistanceWeight}\n" +
               //$"  Tracking Timeout: {TrackingTimeout.TotalSeconds}s" +
               $"  Position History Size: {MaxPositionHistory}";
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
                lastPositions?.Clear();
            }
            disposed = true;
        }
    }

    ~LaserDetector() => Dispose(false);
}