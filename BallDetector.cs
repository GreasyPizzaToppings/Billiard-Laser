using Accord.IO;
using AForge.Imaging.Filters;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using OpenCvSharp.Extensions;
using System.Drawing.Imaging;
using ColorConversion = Emgu.CV.CvEnum.ColorConversion;
using CvInvoke = Emgu.CV.CvInvoke;
using Point = System.Drawing.Point;
using ThresholdType = Emgu.CV.CvEnum.ThresholdType;
using VectorOfPoint = Emgu.CV.Util.VectorOfPoint;

public class BallDetector : TableObjectDetector
{
    //Default cloth mask
    public Rgb LowerClothMask = new Rgb(44, 107, 0); //todo: allow user to set default min/max masks
    public Rgb UpperClothMask = new Rgb(64, 255, 255);

    //Default cueball mask
    public Rgb LowerCueBallMask = new Rgb(0, 0, 160);
    public Rgb UpperCueBallMask = new Rgb(50, 90, 255);

    public double MaxMovementPerSecond = 3000;
    public double IntensityWeight = 0.2;
    public double DistanceWeight = 0.5;
    public double VelocityWeight = 0.3;
    public static double MinBallArea = 10;
    public static double MaxBallArea = 500;

    private Point? lastVelocity = null;
    private int lastDetectionFrameNumber = -1;
    private const double MaxAccelerationPerSecond = 20000; // pixels/second^2
    private Queue<(Ball, int)> lastCueBalls = new(); //last detected cueballs and their frame indexes
    private const int MaxPositionHistory = 3;

    public BallDetector(bool enableBlur = false, bool enableSharpening = false, bool enableTableBoundary = false)
        : base(enableBlur, enableSharpening, enableTableBoundary)
    {
    }

    /// <summary>
    /// Return all the balls and the stages of image processing
    /// </summary>
    /// <param name="frame">Video frame containing the table image</param>
    /// <returns></returns>
    public BallDetectionResults ProcessBallDetection(VideoFrame frame)
    {
        Bitmap workingImage = frame.frame;
        Bitmap transformedImage = null;

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

        Bitmap tableMask = GetMaskImage(workingImage, LowerClothMask, UpperClothMask);
        Bitmap tableWithMaskApplied = ApplyMask(frame.frame, tableMask);

        using (var allContoursFound = GetAllContours(tableWithMaskApplied))
        {
            VectorOfPoint? tableContour = EnableTableBoundary ? GetTableContour(allContoursFound, workingImage.Size) : null;
            using (var filteredContoursFound = tableContour != null ? FilterBallContours(allContoursFound, tableContour) : FilterBallContours(allContoursFound))
            {
                Ball cueball = FindCueBall(OnlyBalls(frame, filteredContoursFound));

                List<Ball> balls = filteredContoursFound.ToArrayOfArray().Select(contour => new Ball(new VectorOfPoint(contour))).ToList();

                Bitmap cueBallMask = GetMaskImage(tableWithMaskApplied, LowerCueBallMask, UpperCueBallMask);
                Bitmap cueBallHighlighted = cueball != null ? cueball.Draw(workingImage) : new Bitmap(frame.frame); 
                Bitmap allBallsHighlighted, filteredBallsHighlighted;

                using (var img = workingImage.ToImage<Rgb, byte>())
                {
                    allBallsHighlighted = DrawContours(allContoursFound, img);
                    filteredBallsHighlighted = DrawContours(filteredContoursFound, img);
                }

                tableContour?.Dispose();

                return new BallDetectionResults
                {
                    OriginalFrame = frame.Clone(),
                    TransformedImage = transformedImage,
                    CueBallMask = cueBallMask,
                    CueBallHighlighted = cueBallHighlighted,
                    TableMask = tableMask,
                    TableWithMaskApplied = tableWithMaskApplied,
                    AllBallsHighlighted = allBallsHighlighted,
                    FilteredBallsHighlighted = filteredBallsHighlighted,

                    CueBall = cueball,
                    Balls = balls
                };
            }
        }
    }

    /// <summary>
    /// Apple cueball mask to the masked table image. The cue ball is the biggest area Contour
    /// </summary>
    /// <returns></returns>
    private Ball FindCueBall(VideoFrame frame)
    {
        Bitmap workingImage = frame.frame;
        Bitmap cueballMask = GetMaskImage(workingImage, LowerCueBallMask, UpperCueBallMask);

        using (var maskInv = new Mat())
        using (var tableMat = new Mat())
        {
            CvInvoke.Threshold(BitmapToMat(cueballMask, tableMat), maskInv, 5, 255, ThresholdType.BinaryInv);
            Bitmap cueballMaskApplied = ApplyMask(workingImage, maskInv.ToBitmap());

            using (var allContoursFound = GetAllContours(cueballMaskApplied))
            {
                var candidates = new List<Ball>();
                
                // Collect all potential cue ball candidates
                for (int i = 0; i < allContoursFound.Size; i++)
                {
                    var contour = new VectorOfPoint();
                    contour.Push(allContoursFound[i]);
                    candidates.Add(new Ball(contour));
                }

                if (candidates.Any())
                {
                    int framesDelta = lastDetectionFrameNumber >= 0 ? frame.Index - lastDetectionFrameNumber: 1; //use 1 frame at least to avoid 0 delta time
                    TimeSpan timeDelta = TimeSpan.FromSeconds(((double)framesDelta / frame.FrameRate));
                    ScoreCandidates(candidates, timeDelta);

                    var bestCandidate = candidates.OrderByDescending(c => c.Confidence).First();
                    
                    if (bestCandidate.Confidence > 0)
                    {
                        // Calculate and log tracking stats
                        Point? avgPos = GetAveragePosition();
                        if (avgPos.HasValue)
                        {
                            if (timeDelta.TotalSeconds > 0)
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
                                    
                                    double acceleration = (currentSpeed - previousSpeed) / timeDelta.TotalSeconds;
                                    
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
                        }

                        lastCueBalls.Enqueue((bestCandidate, frame.Index));
                        while (lastCueBalls.Count > MaxPositionHistory)
                        {
                            lastCueBalls.Dequeue();
                        }

                        return bestCandidate;
                    }
                }

                return null; // null if no candidates found
            }
        }
    }

    private void ScoreCandidates(List<Ball> candidates, TimeSpan timeSinceLastDetection)
    {
        if (!candidates.Any()) return;

        Point? averagePosition = GetAveragePosition();
        double averageArea = GetAverageArea();
        double deltaTime = timeSinceLastDetection.TotalSeconds;

        foreach (var candidate in candidates)
        {
            // Start with area normalization
            double normalizedArea = averageArea > 0 ? 
                Math.Min(candidate.Area / averageArea, 2.0) :
                candidate.Area / ((MinBallArea + MaxBallArea) / 2);

            double distanceScore = 1.0;
            double velocityScore = 1.0;

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
                distanceScore = Math.Exp(-2.0 * distanceTravelled / maxDistance);

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

            // Combine scores with updated weights
            candidate.Confidence = 
                (normalizedArea * IntensityWeight) + 
                (distanceScore * DistanceWeight) + 
                (velocityScore * VelocityWeight);
        }
    }

    private Point? GetAveragePosition()
    {
        if (lastCueBalls.Count == 0)
            return null;

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
        if (lastCueBalls.Count == 0)
            return 0;

        double sumArea = 0;
        foreach (var ball in lastCueBalls)
        {
            sumArea += ball.Item1.Area;
        }

        return sumArea / lastCueBalls.Count;
    }

    /// <summary>
    /// Everything but the balls removed from the original image
    /// </summary>
    /// <param name="Image"></param>
    /// <param name="FilteredContours"></param>
    /// <returns></returns>
    private static VideoFrame OnlyBalls(VideoFrame frame, VectorOfVectorOfPoint FilteredContours)
    {
        Mat result = new Mat(frame.frame.Size, DepthType.Cv8U, 3);
        result.SetTo(new MCvScalar(0, 0, 0));
        for (int i = 0; i < FilteredContours.Size; i++) CvInvoke.DrawContours(result, FilteredContours, i, new MCvScalar(255, 255, 255), -1);
        Bitmap test = ApplyMask(frame.frame, result.ToBitmap());
        //on the image, we apply filtered contours
        //how do we apply filteredcontours on the image? 
        //whatever is inside the filteredcontours, we only show those in the image. 
        return new VideoFrame(test, frame.Index);
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
                using (VectorOfPoint tableCopy = new VectorOfPoint(tableContour.ToArray()))
                {
                    filteredContours.Push(tableCopy);
                }
            }

            for (int i = 0; i < contours.Size; i++)
            {
                using (VectorOfPoint contour = contours[i])
                {
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

                    using (VectorOfPoint contourCopy = new VectorOfPoint(contour.ToArray()))
                    {
                        filteredContours.Push(contourCopy);
                    }
                }
            }

            // Create a copy of the filtered contours to return
            return new VectorOfVectorOfPoint(filteredContours.ToArrayOfArray());
        }
    }

    public override string ToString()
    {
        return $"BallDetector Settings:\n" +
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
               $"  Intensity Weight: {IntensityWeight}\n" +
               $"  Distance Weight: {DistanceWeight}\n" +
               $"  Position History Size: {MaxPositionHistory}\n" +
               $"  Velocity Weight: {VelocityWeight}\n" +
               $"  Max Acceleration: {MaxAccelerationPerSecond}\n" +
               $"  Last Frame Number Processed: {lastDetectionFrameNumber}";
    }
}
