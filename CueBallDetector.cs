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
    private int lastDetectedCbFrameIndex = -1; //frame index of the last detected cueball

    private Queue<Ball> detectedCueBalls = new(); //last N detected cueballs
    public int CueBallHistoryCount = 5; //use the last N cue ball detections to aid in detecting the next one
    public double MaxSpeed = 2000;
    private const double MaxAccelerationPerSecond = 20000; // pixels/second^2
    public double AreaWeight = 0.3;
    public double DistanceWeight = 0.4;
    public double VelocityWeight = 0.3;
    public static double MinBallArea = 5;
    public static double MaxBallArea = 100;

    //Default cloth mask
    public Rgb LowerClothMask = new Rgb(38, 107, 0); //todo: allow user to set default min/max masks
    public Rgb UpperClothMask = new Rgb(64, 255, 255);

    //Default cueball mask
    public Rgb LowerCueBallMask = new Rgb(28, 0, 165);
    public Rgb UpperCueBallMask = new Rgb(80, 175, 255);


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
        Bitmap workingImage = new(rawFrame.frame);

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
        using var onlyBallsMask = new Mat(frame.frame.Size, DepthType.Cv8U, 3);
        onlyBallsMask.SetTo(new MCvScalar(0, 0, 0));
        for (int i = 0; i < filteredBallContours.Size; i++) 
            CvInvoke.DrawContours(onlyBallsMask, filteredBallContours, i, new MCvScalar(255, 255, 255), -1);
        
        using var onlyBallsImage = ApplyMask(frame.frame, onlyBallsMask.ToBitmap());
        using var cueballMask = GetMaskImage(onlyBallsImage, LowerCueBallMask, UpperCueBallMask);
        using var maskInv = new Mat();
        using var tableMat = new Mat();
        
        CvInvoke.Threshold(BitmapToMat(cueballMask, tableMat), maskInv, 5, 255, ThresholdType.BinaryInv);
        using var cueballMaskApplied = ApplyMask(onlyBallsImage, maskInv.ToBitmap());
        using var allCueBallContoursFound = GetAllContours(cueballMaskApplied);
        
        int framesDelta = lastDetectedCbFrameIndex >= 0 ? frame.Index - lastDetectedCbFrameIndex : 1;
        TimeSpan timeDelta = TimeSpan.FromSeconds(((double)framesDelta / frame.FrameRate));

        var candidates = new List<Ball>();
        try
        {
            // Create and initialize candidates with their stats
            for (int i = 0; i < allCueBallContoursFound.Size; i++)
            {
                using var contour = new VectorOfPoint();
                contour.Push(allCueBallContoursFound[i]);
                var candidate = new Ball(contour);

                if (detectedCueBalls.Any())
                {
                    var lastBall = detectedCueBalls.Last();

                    candidate.Velocity = new Point(
                        (int)((candidate.Centre.X - lastBall.Centre.X) / timeDelta.TotalSeconds),
                        (int)((candidate.Centre.Y - lastBall.Centre.Y) / timeDelta.TotalSeconds)
                    );

                    candidate.Speed = Math.Sqrt(
                        Math.Pow(candidate.Velocity.X, 2) +
                        Math.Pow(candidate.Velocity.Y, 2)
                    );

                    candidate.Acceleration = (candidate.Speed - lastBall.Speed) / timeDelta.TotalSeconds;
                    
                    candidate.Displacement = Math.Sqrt(
                        Math.Pow(candidate.Centre.X - lastBall.Centre.X, 2) +
                        Math.Pow(candidate.Centre.Y - lastBall.Centre.Y, 2));
                }

                candidates.Add(candidate);
            }

            using var frameImage = frame.frame.ToImage<Rgb, byte>();
            var results = new CueBallDetectionResults
            {
                CueBallCandidatesHighlighted = DrawContours(allCueBallContoursFound, frameImage),
                CueBallMask = new Bitmap(cueballMask),
                CueBallMaskApplied = new Bitmap(cueballMaskApplied)
            };

            if (candidates.Any())
            {
                if (framesDelta > CueBallHistoryCount) while (detectedCueBalls.Count > 0) detectedCueBalls.Dequeue().Dispose();

                ScoreCandidates(candidates, timeDelta);
                var bestCandidate = candidates.OrderByDescending(c => c.Confidence).First();

                if (bestCandidate.Confidence > 0)
                {
                    Console.WriteLine($"Frame: {frame.Index}\n" + bestCandidate.ToString());

                    detectedCueBalls.Enqueue(bestCandidate.Clone());
                    while (detectedCueBalls.Count > CueBallHistoryCount) detectedCueBalls.Dequeue().Dispose();
                    
                    lastDetectedCbFrameIndex = frame.Index;
                    results.CueBall = bestCandidate.Clone();
                }
            }
            else Console.WriteLine($"Frame: {frame.Index}\nNo cueball found.");

            results.CueBallHighlighted = results.CueBall != null 
                ? results.CueBall.Draw(new Bitmap(frame.frame)) 
                : new Bitmap(frame.frame);
            results.ScoredCandidatesHighlighted = GetScoredCandidatesImage(frame.frame, candidates);
            
            return results;
        }
        finally { foreach (var candidate in candidates) candidate.Dispose(); }
    }

    /// <summary>
    /// Score how probable the candidates are to the cue ball
    /// </summary>
    /// <param name="candidates"></param>
    /// <param name="timeSinceLastDetection"></param>
    private void ScoreCandidates(List<Ball> candidates, TimeSpan timeSinceLastDetection)
    {
        if (!candidates.Any()) return;

        double deltaTime = Math.Abs(timeSinceLastDetection.TotalSeconds); //absolute value to allow playing video back/debugging

        foreach (var candidate in candidates)
        {
            double distanceScore = 1.0;
            double velocityScore = 1.0;
            double areaScore = 1.0;

            // invalid distance travelled
            double maxDistance = MaxSpeed * deltaTime;
            if (candidate.Displacement > maxDistance)
            {
                candidate.Confidence = -1;
                continue;
            }

            // More aggressive distance penalty using exponential falloff
            if (maxDistance < double.Epsilon) distanceScore = (candidate.Displacement < double.Epsilon) ? 1.0 : 0.0;
            else distanceScore = Math.Exp(-2.0 * candidate.Displacement / maxDistance);
            
            // invalid acceleration
            if (Math.Abs(candidate.Acceleration) > MaxAccelerationPerSecond)
            {
                candidate.Confidence = -1;
                continue;
            }

            // invalid area
            if (candidate.Area < MinBallArea || candidate.Area > MaxBallArea) {
                candidate.Confidence = -1;
                continue;
            }

            // Calculate area score
            double averageArea = GetAverageArea();
            if (averageArea > double.Epsilon)
            {
                double areaDeviation = Math.Abs(candidate.Area - averageArea) / averageArea; // Calculate how much the area deviates from the average (as a ratio)
                areaScore = Math.Exp(-areaDeviation); // Use exponential decay to score - closer to average = closer to 1.0
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

    private static Bitmap GetScoredCandidatesImage(Bitmap baseImage, List<Ball> candidates)
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

    //todo decide to keep/use this or remove it
    private Point? GetAveragePosition()
    {
        if (detectedCueBalls.Count == 0) return null;

        int sumX = 0;
        int sumY = 0;
        foreach (var ball in detectedCueBalls)
        {
            sumX += ball.Centre.X;
            sumY += ball.Centre.Y;
        }

        return new Point(
            sumX / detectedCueBalls.Count,
            sumY / detectedCueBalls.Count
        );
    }

    private double GetAverageArea()
    {
        if (detectedCueBalls.Count == 0) return 0;

        double sumArea = 0;
        foreach (var ball in detectedCueBalls) sumArea += ball.Area;
        return sumArea / detectedCueBalls.Count;
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
        using VectorOfVectorOfPoint filteredContours = new VectorOfVectorOfPoint();
        
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
               $"  Max Movement/Sec: {MaxSpeed}\n" +
               $"  Intensity Weight: {AreaWeight}\n" +
               $"  Distance Weight: {DistanceWeight}\n" +
               $"  Position History Size: {CueBallHistoryCount}\n" +
               $"  Velocity Weight: {VelocityWeight}\n" +
               $"  Max Acceleration: {MaxAccelerationPerSecond}\n" +
               $"  Last Frame Number Processed: {lastDetectedCbFrameIndex}";
    }
}
