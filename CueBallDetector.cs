using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using OpenCvSharp.Extensions;
using CvInvoke = Emgu.CV.CvInvoke;
using Point = System.Drawing.Point;
using ThresholdType = Emgu.CV.CvEnum.ThresholdType;
using VectorOfPoint = Emgu.CV.Util.VectorOfPoint;

public class CueBallDetector : TableObjectDetector
{
    //tracking parameters
    private Point? lastVelocity = null;
    private int lastDetectionFrameNumber = -1;
    private const double MaxAccelerationPerSecond = 20000; // pixels/second^2
    private Queue<(Ball, int)> lastCueBalls = new(); //last detected cueballs and their rawFrame indexes
    
    public int MaxPositionHistory = 3; //use the last N cue ball detections to aid in detecting the next one
    public double MaxMovementPerSecond = 3000;
    public double AreaWeight = 0.3;
    public double DistanceWeight = 0.4;
    public double VelocityWeight = 0.3;
    public static double MinBallArea = 5;
    public static double MaxBallArea = 100;

    //Default cloth mask
    public Rgb LowerClothMask = new Rgb(44, 107, 0); //todo: allow user to set default min/max masks
    public Rgb UpperClothMask = new Rgb(64, 255, 255);

    //Default cueball mask
    public Rgb LowerCueBallMask = new Rgb(0, 0, 160);
    public Rgb UpperCueBallMask = new Rgb(50, 90, 255);


    public CueBallDetector(bool enableBlur = false, bool enableSharpening = false, bool enableTableBoundary = false)
        : base(enableBlur, enableSharpening, enableTableBoundary)
    {
    }

    /// <summary>
    /// Return the cueball and the stages of image processing
    /// </summary>
    /// <param name="frame">Video frame containing the table image</param>
    /// <returns></returns>
    public CueBallDetectionResults GetCueBallResults(VideoFrame rawFrame)
    {
        Bitmap workingImage = new Bitmap(rawFrame.frame);

        if (EnableSharpening) workingImage = SharpenImage(workingImage);
        if (EnableBlur) workingImage = BlurImage(workingImage);

        Bitmap tableMaskApplied = ApplyMask(workingImage, GetMaskImage(workingImage, LowerClothMask, UpperClothMask));

        using var allContours = GetAllContours(tableMaskApplied);
        using VectorOfPoint? tableContour = EnableTableBoundary ? GetTableContour(allContours, workingImage.Size) : null;
        using var image = rawFrame.frame.ToImage<Rgb, byte>();
        using var filteredBallContours = tableContour != null ? FilterBallContours(allContours, tableContour) : FilterBallContours(allContours);
        CueBallDetectionResults results = ProcessCueBallDetection(rawFrame, filteredBallContours);

        return new CueBallDetectionResults
        {
            OriginalFrame = rawFrame.Clone(),
            WorkingImage = workingImage,
            TableMaskApplied = tableMaskApplied,
            AllContoursHighlighted = DrawContours(allContours, image),

            CueBall = results.CueBall,
            CueBallMask = results.CueBallMask,
            CueBallMaskApplied = results.CueBallMaskApplied,
            CueBallCandidatesHighlighted = results.CueBallCandidatesHighlighted,
            ScoredCandidatesHighlighted = results.ScoredCandidatesHighlighted,
            CueBallHighlighted = results.CueBallHighlighted
        };
    }


    /// <summary>
    /// Get the cueball and the processing results
    /// </summary>
    /// <param name="frame"></param>
    /// <param name="filteredBallContours"></param>
    /// <returns></returns>
    private CueBallDetectionResults ProcessCueBallDetection(VideoFrame frame, Emgu.CV.Util.VectorOfVectorOfPoint filteredBallContours)
    {
        //remove everything from image except the balls
        Mat onlyBallsMask = new Mat(frame.frame.Size, DepthType.Cv8U, 3);
        onlyBallsMask.SetTo(new MCvScalar(0, 0, 0));
        for (int i = 0; i < filteredBallContours.Size; i++) CvInvoke.DrawContours(onlyBallsMask, filteredBallContours, i, new MCvScalar(255, 255, 255), -1);
        Bitmap onlyBallsImage = ApplyMask(frame.frame, onlyBallsMask.ToBitmap());

        Bitmap cueballMask = GetMaskImage(onlyBallsImage, LowerCueBallMask, UpperCueBallMask);

        using var maskInv = new Mat();
        using var tableMat = new Mat();
        CvInvoke.Threshold(BitmapToMat(cueballMask, tableMat), maskInv, 5, 255, ThresholdType.BinaryInv);
        Bitmap cueballMaskApplied = ApplyMask(onlyBallsImage, maskInv.ToBitmap());

        using var allCueBallContoursFound = GetAllContours(cueballMaskApplied);
        var candidates = new List<Ball>();

        for (int i = 0; i < allCueBallContoursFound.Size; i++)
        {
            var contour = new VectorOfPoint();
            contour.Push(allCueBallContoursFound[i]);
            candidates.Add(new Ball(contour));
        }

        using var frameImage = frame.frame.ToImage<Rgb, byte>();
        CueBallDetectionResults results = new CueBallDetectionResults
        {
            CueBallCandidatesHighlighted = DrawContours(allCueBallContoursFound, frameImage),
            CueBallMask = cueballMask,
            CueBallMaskApplied = cueballMaskApplied
        };

        if (candidates.Any())
        {
            int framesDelta = lastDetectionFrameNumber >= 0 ? frame.Index - lastDetectionFrameNumber : 1; //use 1 rawFrame at least to avoid 0 delta time
            if (framesDelta > MaxPositionHistory) lastCueBalls.Clear(); //reset tracking if we havent found the cueball in a while
            TimeSpan timeDelta = TimeSpan.FromSeconds(((double)framesDelta / frame.FrameRate));

            ScoreCandidates(candidates, timeDelta);

            var bestCandidate = candidates.OrderByDescending(c => c.Confidence).First();

            if (bestCandidate.Confidence > 0)
            {
                // Calculate and log tracking stats
                Point? avgPos = GetAveragePosition();
                if (avgPos.HasValue)
                {
                    // Calculate current velocity
                    Point currentVelocity = new Point(
                        (int)((bestCandidate.Centre.X - avgPos.Value.X) / timeDelta.TotalSeconds),
                        (int)((bestCandidate.Centre.Y - avgPos.Value.Y) / timeDelta.TotalSeconds)
                    );

                    // Calculate distance moved
                    double distanceMoved = Math.Sqrt(
                        Math.Pow(bestCandidate.Centre.X - avgPos.Value.X, 2) +
                        Math.Pow(bestCandidate.Centre.Y - avgPos.Value.Y, 2)
                    );

                    // Calculate velocity magnitude
                    double currentSpeed = Math.Sqrt(
                        Math.Pow(currentVelocity.X, 2) +
                        Math.Pow(currentVelocity.Y, 2)
                    );

                    // Calculate acceleration if we have previous velocity
                    string accelerationInfo = "N/A";
                    if (lastVelocity.HasValue)
                    {
                        double previousSpeed = Math.Sqrt(
                            Math.Pow(lastVelocity.Value.X, 2) +
                            Math.Pow(lastVelocity.Value.Y, 2)
                        );

                        double acceleration = (currentSpeed - previousSpeed) / timeDelta.TotalSeconds
                        accelerationInfo = $"{acceleration:F1} px/s²";
                    }

                    Console.WriteLine(
                        $"\nCueBall Stats:\n" +
                        $"  Position: ({bestCandidate.Centre.X}, {bestCandidate.Centre.Y})\n" +
                        $"  Distance Moved: {distanceMoved:F1} px\n" +
                        $"  Time Delta: {timeDelta.ToString():F3} s\n" +
                        $"  Current Speed: {currentSpeed:F1} px/s\n" +
                        $"  Velocity: ({currentVelocity.X:F1}, {currentVelocity.Y:F1}) px/s\n" +
                        $"  Acceleration: {accelerationInfo}\n" +
                        $"  Confidence: {bestCandidate.Confidence:F3}\n" +
                        $"  Area: {bestCandidate.Area:F1} px²"
                    );

                    lastVelocity = currentVelocity;
                }

                lastCueBalls.Enqueue((bestCandidate, frame.Index));
                while (lastCueBalls.Count > MaxPositionHistory) lastCueBalls.Dequeue();
                lastDetectionFrameNumber = frame.Index;

                results.CueBall = bestCandidate;
            }
        }

        results.CueBallHighlighted = results.CueBall != null ? results.CueBall.Draw(new Bitmap(frame.frame)) : new Bitmap(frame.frame);
        results.ScoredCandidatesHighlighted = GetScoredCandidatesImage(frame.frame, candidates);
        return results;
    }

    /// <summary>
    /// Score how probable the candidates are to the cue ball
    /// </summary>
    /// <param name="candidates"></param>
    /// <param name="timeSinceLastDetection"></param>
    private void ScoreCandidates(List<Ball> candidates, TimeSpan timeSinceLastDetection)
    {
        if (!candidates.Any()) return;

        Point? averagePosition = GetAveragePosition();
        double averageArea = GetAverageArea();
        double deltaTime = timeSinceLastDetection.TotalSeconds;

        foreach (var candidate in candidates)
        {
            double distanceScore = 1.0;
            double velocityScore = 1.0;
            double areaScore = 1.0;

            if (averagePosition.HasValue)
            {
                // Calculate distance score with more aggressive penalty
                double maxDistance = MaxMovementPerSecond * deltaTime;
                double distanceTravelled = Math.Sqrt(
                    Math.Pow(candidate.Centre.X - averagePosition.Value.X, 2) +
                    Math.Pow(candidate.Centre.Y - averagePosition.Value.Y, 2));

                // Immediately reject if movement is physically impossible
                if (distanceTravelled > maxDistance)
                {
                    candidate.Confidence = -1;
                    continue;
                }

                // More aggressive distance penalty using exponential falloff
                if (maxDistance < double.Epsilon) distanceScore = (distanceTravelled < double.Epsilon) ? 1.0 : 0.0;
                else distanceScore = Math.Exp(-2.0 * distanceTravelled / maxDistance);

                // Calculate velocity score if we have enough history
                if (lastVelocity.HasValue && deltaTime > 0)
                {
                    // Current velocity vector
                    Point currentVelocity = new Point(
                        (int)((candidate.Centre.X - averagePosition.Value.X) / deltaTime),
                        (int)((candidate.Centre.Y - averagePosition.Value.Y) / deltaTime)
                    );

                    // Calculate acceleration
                    double acceleration = Math.Sqrt(
                        Math.Pow(currentVelocity.X - lastVelocity.Value.X, 2) +
                        Math.Pow(currentVelocity.Y - lastVelocity.Value.Y, 2)
                    ) / deltaTime;

                    // Immediately reject if movement is physically impossible
                    if (acceleration > MaxAccelerationPerSecond)
                    {
                        candidate.Confidence = -1;
                        continue;
                    }
                }
            }

            // invalid area
            if (candidate.Area < MinBallArea || candidate.Area > MaxBallArea) {
                candidate.Confidence = -1;
                continue;
            }

            // Calculate area score
            if (averageArea > double.Epsilon)
            {
                // Calculate how much the area deviates from the average (as a ratio)
                double areaDeviation = Math.Abs(candidate.Area - averageArea) / averageArea;
                
                // Use exponential decay to score - closer to average = closer to 1.0
                areaScore = Math.Exp(-areaDeviation);
            }
            else
            {
                // No history - use expected range
                double expectedArea = (MinBallArea + MaxBallArea) / 2.0;
                if (expectedArea > double.Epsilon)
                {
                    double areaDeviation = Math.Abs(candidate.Area - expectedArea) / expectedArea;
                    areaScore = Math.Exp(-areaDeviation);
                }
            }

            // Combine scores with weights
            candidate.Confidence = 
                (areaScore * AreaWeight) + 
                (distanceScore * DistanceWeight) + 
                (velocityScore * VelocityWeight);
        }
    }

    private Point? GetAveragePosition()
    {
        if (lastCueBalls.Count == 0) return null;

        int sumX = 0;
        int sumY = 0;
        foreach (var ball in lastCueBalls)
        {
            sumX += ball.Item1.Centre.X;
            sumY += ball.Item1.Centre.Y;
        }

        return new Point(
            sumX / lastCueBalls.Count,
            sumY / lastCueBalls.Count
        );
    }

    private double GetAverageArea()
    {
        if (lastCueBalls.Count == 0) return 0;

        double sumArea = 0;
        foreach (var ball in lastCueBalls) sumArea += ball.Item1.Area;
        return sumArea / lastCueBalls.Count;
    }

    /// <summary>
    /// Remove the contours unlikely to be a ball
    /// </summary>
    /// <param name="contours"></param>
    /// <param name="min_s"></param>
    /// <param name="max_s"></param>
    /// <returns></returns>
    private static VectorOfVectorOfPoint FilterBallContours(VectorOfVectorOfPoint contours, VectorOfPoint tableContour = null, double min_s = 5, double max_s = 50)
    {
        using (VectorOfVectorOfPoint filteredContours = new VectorOfVectorOfPoint())
        {
            // Show table contour if enabled
            if (tableContour != null)
            {
                using VectorOfPoint tableCopy = new VectorOfPoint(tableContour.ToArray());
                filteredContours.Push(tableCopy);
            }

            for (int i = 0; i < contours.Size; i++)
            {
                using VectorOfPoint contour = contours[i];
                // Filter out contours that are not inside the table contour
                if (tableContour != null && !IsContourInside(contour, tableContour))
                    continue;

                RotatedRect rotRect = CvInvoke.MinAreaRect(contour);
                float w = rotRect.Size.Width;
                float h = rotRect.Size.Height;

                // Allows some ball-speed to be detected (elongated ball shape)
                if ((h > w * 5) || (w > h * 5))
                    continue;

                // Filter out balls with very small area or too big areas
                double area = CvInvoke.ContourArea(contour);
                if ((area < MinBallArea) || (area > MaxBallArea))
                    continue;

                using VectorOfPoint contourCopy = new VectorOfPoint(contour.ToArray());
                filteredContours.Push(contourCopy);
            }

            // Create a copy of the filtered contours to return
            return new VectorOfVectorOfPoint(filteredContours.ToArrayOfArray());
        }
    }

    private Bitmap GetScoredCandidatesImage(Bitmap baseImage, List<Ball> candidates)
    {
        using var image = baseImage.ToImage<Rgb, byte>();
        foreach (var candidate in candidates)
        {
            int intensity = (int)(candidate.Confidence * 255);
            CvInvoke.Circle(
                image,
                candidate.Centre,
                10,
                new MCvScalar(intensity, intensity, intensity), // Grayscale intensity based on score
                2);
        }
        return image.ToBitmap();
    }

    public override string ToString()
    {
        return $"CueBallDetector Settings:\n" +
               $"  Blur: {EnableBlur}\n" +
               $"  Sharpen: {EnableSharpening}\n" +
               $"  TableBoundary: {EnableTableBoundary}\n" +
               $"  Lower Cloth Mask: RGB({LowerClothMask.Red}, {LowerClothMask.Green}, {LowerClothMask.Blue})\n" +
               $"  Upper Cloth Mask: RGB({UpperClothMask.Red}, {UpperClothMask.Green}, {UpperClothMask.Blue})\n" +
               $"  Lower CueBall Mask: RGB({LowerCueBallMask.Red}, {LowerCueBallMask.Green}, {LowerCueBallMask.Blue})\n" +
               $"  Upper CueBall Mask: RGB({UpperCueBallMask.Red}, {UpperCueBallMask.Green}, {UpperCueBallMask.Blue})\n" +
               $"  Min Ball Area: {MinBallArea}\n" +
               $"  Max Ball Area: {MaxBallArea}\n" +
               $"  Max Movement/Sec: {MaxMovementPerSecond}\n" +
               $"  Intensity Weight: {AreaWeight}\n" +
               $"  Distance Weight: {DistanceWeight}\n" +
               $"  Position History Size: {MaxPositionHistory}\n" +
               $"  Velocity Weight: {VelocityWeight}\n" +
               $"  Max Acceleration: {MaxAccelerationPerSecond}\n" +
               $"  Last Frame Number Processed: {lastDetectionFrameNumber}";
    }
}
