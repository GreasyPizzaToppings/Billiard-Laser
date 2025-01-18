using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV;

public class LaserDetector : TableObjectDetector
{
    //laser detection masks
    public Rgb LowerLaserMask = new Rgb(42, 145, 52);
    public Rgb UpperLaserMask = new Rgb(97, 255, 220);

    public double MinLaserArea = 3;
    public double MaxLaserArea = 100;

    // temporal tracking parameters
    private Point? lastPosition = null;
    private DateTime lastDetectionTime = DateTime.MinValue;
    public double MaxMovementPerSecond = 300.0; // pixels per second
    public double IntensityWeight = 0.4;
    public double DistanceWeight = 0.6;
    public TimeSpan TrackingTimeout = TimeSpan.FromSeconds(1); // Reset tracking if no detection for this long

    public LaserDetector(bool enableBlur = true, bool enableSharpening = false, bool enableTableBoundary = false)
        : base(enableBlur, enableSharpening, enableTableBoundary)
    {
    }

    private Image<Rgb, byte> CreateCandidatesVisualization(Bitmap baseImage, List<Laser> candidates)
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

    private Image<Rgb, byte> CreateFinalVisualization(Bitmap baseImage, Laser bestCandidate, Point? lastPosition)
    {
        var image = baseImage.ToImage<Rgb, byte>();

        // Draw blue circle for current detection
        CvInvoke.Circle(
            image,
            bestCandidate.Location,
            10,
            new MCvScalar(0, 0, 255),
            2);

        // Draw previous position if available
        if (lastPosition.HasValue && lastPosition.Value != bestCandidate.Location)
        {
            CvInvoke.Circle(
                image,
                lastPosition.Value,
                8,
                new MCvScalar(255, 0, 0), // Red for previous position
                1);
        }

        return image;
    }

    private List<Laser> ScoreCandidates(List<Laser> candidates, TimeSpan timeSinceLastDetection)
    {
        if (!candidates.Any()) return candidates;

        // Normalize intensities
        double maxIntensity = candidates.Max(c => c.Intensity);
        foreach (var candidate in candidates)
        {
            double normalizedIntensity = candidate.Intensity / maxIntensity;
            double distanceScore = 1.0;

            if (lastPosition.HasValue)
            {
                double maxDistance = MaxMovementPerSecond * timeSinceLastDetection.TotalSeconds;
                double distanceTravelled = Math.Sqrt(
                    Math.Pow(candidate.Location.X - lastPosition.Value.X, 2) +
                    Math.Pow(candidate.Location.Y - lastPosition.Value.Y, 2));

                // Normalize distance score (closer to last position = higher score)
                distanceScore = Math.Max(0, 1 - (distanceTravelled / maxDistance));
            }

            // Calculate final score using weights
            candidate.Confidence = (normalizedIntensity * IntensityWeight) + (distanceScore * DistanceWeight);
        }

        return candidates;
    }

    public LaserDetectionResults ProcessLaserDetection(Bitmap tableImage)
    {
        LaserDetectionResults laserDetectionResults = new LaserDetectionResults();

        Bitmap workingImage = tableImage;
        Bitmap? transformedImage = null;

        // Apply image transformations if enabled
        if (EnableSharpening)
        {
            transformedImage = SharpenImage(workingImage);
            workingImage = transformedImage;
        }

        if (EnableBlur)
        {
            transformedImage = BlurImage(workingImage);
            workingImage = transformedImage;
        }

        laserDetectionResults.WorkingImage = transformedImage != null ? new Bitmap(transformedImage) : new Bitmap(tableImage);

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

                        using (var mask = new Mat(tableImage.Size, DepthType.Cv8U, 1))
                        {
                            mask.SetTo(new MCvScalar(0));
                            CvInvoke.DrawContours(mask, allContoursFound, i, new MCvScalar(255), -1);

                            using (var roi = new Mat())
                            {
                                CvInvoke.BitwiseAnd(workingImageRgb, workingImageRgb, roi, mask);
                                var mean = CvInvoke.Mean(roi, mask);
                                double intensity = mean.V2; // Red channel intensity
                                candidates.Add(new Laser(location, intensity, area));
                            }
                        }
                    }
                }


                // create image of all contours/candidates detected, regardless of size or brightness or location etc
                laserDetectionResults.AllCandidatesHighlighted = DrawContours(allContoursFound, tableImage.ToImage<Rgb, byte>());
                
                using (var filteredCandidatesImage = CreateCandidatesVisualization(workingImage, candidates))
                {
                    laserDetectionResults.FilteredCandidatesHighlighted = filteredCandidatesImage.ToBitmap();
                }

                if (candidates.Any())
                {
                    var timeSinceLastDetection = DateTime.Now - lastDetectionTime;

                    // Reset tracking if too much time has passed
                    if (timeSinceLastDetection > TrackingTimeout)
                    {
                        lastPosition = null;
                    }

                    // Confidence all candidates
                    candidates = ScoreCandidates(candidates, timeSinceLastDetection);

                    // Create visualization of scored candidates
                    using (var scoredCandidatesImage = CreateScoredCandidatesVisualization(workingImage, candidates))
                    {
                        laserDetectionResults.ScoredCandidatesHighlighted = scoredCandidatesImage.ToBitmap();
                    }

                    // Select and store best candidate
                    var bestCandidate = candidates.OrderByDescending(c => c.Confidence).First();
                    laserDetectionResults.Laser = bestCandidate;

                    // Create final visualization
                    using (var finalImage = CreateFinalVisualization(tableImage, bestCandidate, lastPosition))
                    {
                        laserDetectionResults.LaserHighlighted = finalImage.ToBitmap();
                    }

                    // Update tracking state
                    lastPosition = bestCandidate.Location;
                    lastDetectionTime = DateTime.Now;
                }
            }
        }

        return laserDetectionResults;
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
               $"  Tracking Timeout: {TrackingTimeout.TotalSeconds}s";
    }
}