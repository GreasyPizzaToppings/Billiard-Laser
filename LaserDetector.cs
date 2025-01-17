using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV;

public class LaserDetector : TableObjectDetector
{
    //laser detection masks
    public Rgb LowerLaserMask = new Rgb(42, 145, 52);
    public Rgb UpperLaserMask = new Rgb(97, 255, 220);

    public double MinLaserArea = 4;
    public double MaxLaserArea = 100;

    public LaserDetector(bool enableBlur = true, bool enableSharpening = false, bool enableTableBoundary = false)
        : base(enableBlur, enableSharpening, enableTableBoundary)
    {
    }

    public LaserDetectionResults ProcessLaserDetection(Bitmap tableImage)
    {
        LaserDetectionResults laserDetectionResults = new LaserDetectionResults();
        laserDetectionResults.OriginalImage = new Bitmap(tableImage); // Store original image

        Bitmap workingImage = tableImage;
        Bitmap? transformedImage = null;

        // Apply any image transformations if enabled
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

        laserDetectionResults.TransformedImage = transformedImage != null ? new Bitmap(transformedImage) : null;

        using (var workingImageRgb = workingImage.ToImage<Rgb, byte>())
        {
            // Create mask for red laser detection
            laserDetectionResults.LaserMask = GetMaskImage(workingImage, LowerLaserMask, UpperLaserMask);
            laserDetectionResults.LaserMaskApplied = ApplyMask(workingImage, laserDetectionResults.LaserMask);

            using (var allContoursFound = GetAllContours(laserDetectionResults.LaserMaskApplied))
            {
                VectorOfPoint? tableContour = EnableTableBoundary ? GetTableContour(allContoursFound, tableImage.Size) : null;
                using (var allCandidatesImage = workingImage.ToImage<Rgb, byte>()) // get image with all candidates highlighted for debug
                using (var filteredCandidatesImage = workingImage.ToImage<Rgb, byte>()) //get image with filtered candidates highlighted for debug
                {
                    for (int i = 0; i < allContoursFound.Size; i++)
                    {
                        using (var contour = allContoursFound[i])
                        {
                            double area = CvInvoke.ContourArea(contour);
                            CvInvoke.DrawContours(
                            allCandidatesImage,
                            new VectorOfVectorOfPoint(contour),
                            -1,
                            new MCvScalar(0, 255, 0), // Green for all candidates
                            2);

                            // try and filter out non lasers
                            if (area >= MinLaserArea && area <= MaxLaserArea)
                            {
                                //table boundary checking if enabled
                                if (tableContour != null && IsContourInside(contour, tableContour))
                                {
                                    CvInvoke.DrawContours(
                                    filteredCandidatesImage,
                                    new VectorOfVectorOfPoint(contour),
                                    -1,
                                    new MCvScalar(0, 255, 0), // Green for all candidates
                                    2);
                                }
                            }
                        }
                    }
                    laserDetectionResults.AllCandidatesHighlighted = allCandidatesImage.ToBitmap();
                    laserDetectionResults.FilteredCandidatesHighlighted = filteredCandidatesImage.ToBitmap();
                }

                //brightest laser point within area range is our pick
                double maxIntensity = 0;
                for (int i = 0; i < allContoursFound.Size; i++)
                {
                    using (var contour = allContoursFound[i])
                    {
                        double area = CvInvoke.ContourArea(contour);
                        if (area < MinLaserArea || area > MaxLaserArea)
                            continue;

                        var moments = CvInvoke.Moments(contour);
                        int centerX = (int)(moments.M10 / moments.M00);
                        int centerY = (int)(moments.M01 / moments.M00);

                        using (var mask = new Mat(tableImage.Size, DepthType.Cv8U, 1))
                        {
                            mask.SetTo(new MCvScalar(0));
                            CvInvoke.DrawContours(mask, allContoursFound, i, new MCvScalar(255), -1);

                            using (var roi = new Mat())
                            {
                                CvInvoke.BitwiseAnd(workingImageRgb, workingImageRgb, roi, mask);
                                var mean = CvInvoke.Mean(roi, mask);
                                double intensity = mean.V2; // Red channel intensity

                                if (intensity > maxIntensity)
                                {
                                    maxIntensity = intensity;
                                    laserDetectionResults.Laser = new Laser(
                                        new Point(centerX, centerY),
                                        intensity,
                                        area
                                    );
                                }
                            }
                        }
                    }
                }
            }
        }

        // Draw circle around the detected laser point for debug, but on the original image for best clarity
        if (laserDetectionResults.Laser != null)
        {
            using (var img = laserDetectionResults.OriginalImage.ToImage<Rgb, byte>())
            {
                //blue circle
                CvInvoke.Circle(
                    img,
                    laserDetectionResults.Laser.Location,
                    10,
                    new MCvScalar(0, 0, 255), 
                    2);
                laserDetectionResults.LaserHighlighted = img.ToBitmap();
            }
        }

        return laserDetectionResults;
    }

}
