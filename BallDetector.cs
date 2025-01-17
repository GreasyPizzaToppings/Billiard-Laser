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
    private Size imageSize = new Size(0, 0);

    //Default cloth mask
    public Rgb LowerClothMask = new Rgb(44, 107, 0); //todo: allow user to set default min/max masks
    public Rgb UpperClothMask = new Rgb(64, 255, 255);

    //Default cueball mask
    public Rgb LowerCueBallMask = new Rgb(0, 0, 160);
    public Rgb UpperCueBallMask = new Rgb(50, 90, 255);

    public BallDetector(bool enableBlur = false, bool enableSharpening = false, bool enableTableBoundary = false)
        : base(enableBlur, enableSharpening, enableTableBoundary)
    {
    }

    /// <summary>
    /// Return all the balls and the stages of image processing
    /// </summary>
    /// <param name="tableImage">Image of the table</param>
    /// <returns></returns>
    public BallDetectionResults ProcessBallDetection(Bitmap tableImage)
    {
        Bitmap workingImage = tableImage;
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
        Bitmap tableWithMaskApplied = ApplyMask(tableImage, tableMask);

        using (var allContoursFound = GetAllContours(tableWithMaskApplied))
        {
            VectorOfPoint? tableContour = EnableTableBoundary ? GetTableContour(allContoursFound, tableImage.Size) : null;
            using (var filteredContoursFound = tableContour != null ? FilterBallContours(allContoursFound, tableContour) : FilterBallContours(allContoursFound))
            {
                Ball cueball = FindCueBall(OnlyBalls(workingImage, filteredContoursFound));

                List<Ball> balls = filteredContoursFound.ToArrayOfArray().Select(contour => new Ball(new VectorOfPoint(contour))).ToList();

                Bitmap cueBallMask = GetMaskImage(tableWithMaskApplied, LowerCueBallMask, UpperCueBallMask);
                Bitmap cueBallHighlighted = cueball.Draw(tableImage);
                Bitmap allBallsHighlighted, filteredBallsHighlighted;

                using (var img = tableImage.ToImage<Rgb, byte>())
                {
                    allBallsHighlighted = DrawContours(allContoursFound, img);
                    filteredBallsHighlighted = DrawContours(filteredContoursFound, img);
                }

                tableContour?.Dispose();

                return new BallDetectionResults
                {
                    OriginalImage = tableImage,
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
    /// <param name="maskedTableImage"></param>
    /// <returns></returns>
    private Ball FindCueBall(Bitmap maskedTableImage)
    {
        Bitmap workingImage = maskedTableImage;
        Bitmap cueballMask = GetMaskImage(workingImage, LowerCueBallMask, UpperCueBallMask);

        using (var maskInv = new Mat())
        using (var tableMat = new Mat())
        {
            CvInvoke.Threshold(BitmapToMat(cueballMask, tableMat), maskInv, 5, 255, ThresholdType.BinaryInv);
            Bitmap cueballMaskApplied = ApplyMask(workingImage, maskInv.ToBitmap());

            using (var allContoursFound = GetAllContours(cueballMaskApplied))
            {
                double MaxArea = 0;
                using (var Cueball = new VectorOfPoint())
                {
                    for (int i = 0; i < allContoursFound.Size; i++)
                    {
                        double area = CvInvoke.ContourArea(allContoursFound[i]);
                        if (MaxArea < area)
                        {
                            MaxArea = area;
                            Cueball.Clear();
                            Cueball.Push(allContoursFound[i]);
                        }
                    }
                    return new Ball(Cueball);
                }
            }
        }
    }


    /// <summary>
    /// Everything but the balls removed from the original image
    /// </summary>
    /// <param name="Image"></param>
    /// <param name="FilteredContours"></param>
    /// <returns></returns>
    private static Bitmap OnlyBalls(Bitmap Image, VectorOfVectorOfPoint FilteredContours)
    {
        Mat result = new Mat(Image.Size, DepthType.Cv8U, 3);
        result.SetTo(new MCvScalar(0, 0, 0));
        for (int i = 0; i < FilteredContours.Size; i++) CvInvoke.DrawContours(result, FilteredContours, i, new MCvScalar(255, 255, 255), -1);
        Bitmap test = ApplyMask(Image, result.ToBitmap());
        //on the image, we apply filtered contours
        //how do we apply filteredcontours on the image? 
        //whatever is inside the filteredcontours, we only show those in the image. 
        return test;
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
                    if ((h > w * 4) || (w > h * 4))
                        continue;

                    // Filter out balls with very small area or too big areas
                    double area = CvInvoke.ContourArea(contour);
                    if ((area < (min_s * min_s)) || (area > (max_s * max_s)))
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

}
