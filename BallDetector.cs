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

public class BallDetector
{
    private Size imageSize = new Size(0, 0);

    //image manipulation settings
    public Boolean EnableBlur = false;
    public Boolean EnableSharpening = false;
    public Boolean EnableTableBoundary = false;

    //Default cloth mask
    public Rgb LowerClothMask = new Rgb(40, 80, 40);
    public Rgb UpperClothMask = new Rgb(70, 255, 255);

    //Default cueball mask
    public Rgb LowerCueBallMask = new Rgb(0, 0, 160);
    public Rgb UpperCueBallMask = new Rgb(50, 90, 255);


    /// <summary>
    /// Return all the balls and the stages of image processing
    /// </summary>
    /// <param name="tableImage">Image of the table</param>
    /// <returns></returns>
    public ImageProcessingResults ProcessTableImage(Bitmap tableImage)
    {
        imageSize = tableImage.Size;

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
            VectorOfPoint? tableContour = EnableTableBoundary ? GetTableContour(allContoursFound) : null;
            using (var filteredContoursFound = tableContour != null ? FilterContours(allContoursFound, tableContour) : FilterContours(allContoursFound))
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

                return new ImageProcessingResults
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
        for(int i = 0; i < FilteredContours.Size; i++)CvInvoke.DrawContours(result, FilteredContours, i, new MCvScalar(255, 255, 255),-1);
        Bitmap test = ApplyMask(Image, result.ToBitmap());
        //on the image, we apply filtered contours
        //how do we apply filteredcontours on the image? 
        //whatever is inside the filteredcontours, we only show those in the image. 
        return test;
    }

    private static Bitmap SharpenImage(Bitmap image)
    {
        // Define the kernel
        int[,] kernel = {
            { -1, -1, -1 },
            { -1, 9, -1 },
            { -1, -1, -1 }
         };

        Convolution filter = new Convolution(kernel);
        return filter.Apply(image);
    }

    //Emgu CV bitmap to Mat doesn't convert some bitmaps properly so I had to implement this method. 
    private static Mat BitmapToMat(Bitmap bitmap, Mat mat, DepthType depthType = DepthType.Cv8U)
    {
        BitmapData bmpData = bitmap.LockBits(
            new Rectangle(0, 0, bitmap.Width, bitmap.Height),
            ImageLockMode.ReadOnly,
            PixelFormat.Format24bppRgb);

        CvInvoke.CvtColor(
            new Mat(bmpData.Height, bmpData.Width, depthType, 3, bmpData.Scan0, bmpData.Stride),
            mat,
            ColorConversion.Rgb2Rgba);

        bitmap.UnlockBits(bmpData);
        return mat;
    }

    private static Bitmap BlurImage(Bitmap inputImage)
    {
        using (Mat transformed = new Mat())
        using (var blurredImage = new Mat())
        {
            BitmapToMat(inputImage, transformed);
            CvInvoke.GaussianBlur(transformed, blurredImage, new System.Drawing.Size(5, 5), 0, 0, Emgu.CV.CvEnum.BorderType.Default);
            return blurredImage.ToBitmap();
        }
    }

    private static Bitmap ApplyMask(Bitmap inputImage, Bitmap tableMask)
    {
        using (Mat maskedObjects = new Mat())
        using (Mat inputMat = new Mat())
        using (Mat outputMat = new Mat())
        {
            Emgu.CV.CvInvoke.BitwiseAnd(BitmapToMat(inputImage, inputMat), BitmapToMat(tableMask, outputMat), maskedObjects);
            return maskedObjects.ToBitmap();
        }
    }

    /// <summary>
    /// get the mask image for the table to remove the cloth
    /// </summary>
    /// <param name="tableImage">Image of the table to mask</param>
    /// <returns></returns>
    private static Bitmap GetMaskImage(Bitmap tableImage, Rgb LowerMaskRgb, Rgb UpperMaskRgb)
    {
        using (Mat imageMat = BitmapExtension.ToMat(tableImage))
        using (Mat hsv = new Mat())
        using (Mat mask = new Mat())
        using (Mat kernel = Emgu.CV.CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new System.Drawing.Size(5, 5), new Point(-1, -1)))
        using (Mat maskClosing = new Mat())
        using (Mat maskInv = new Mat())
        {
            Emgu.CV.CvInvoke.CvtColor(imageMat, hsv, ColorConversion.Bgr2Hsv);

            ScalarArray LowerMaskValue = new ScalarArray(new MCvScalar(LowerMaskRgb.Red, LowerMaskRgb.Green, LowerMaskRgb.Blue));
            ScalarArray UpperMaskValue = new ScalarArray(new MCvScalar(UpperMaskRgb.Red, UpperMaskRgb.Green, UpperMaskRgb.Blue));
            Emgu.CV.CvInvoke.InRange(hsv, LowerMaskValue, UpperMaskValue, mask);

            Emgu.CV.CvInvoke.MorphologyEx(mask, maskClosing, MorphOp.Close, kernel, new Point(-1, -1), 1, Emgu.CV.CvEnum.BorderType.Reflect, new MCvScalar());

            Emgu.CV.CvInvoke.Threshold(maskClosing, maskInv, 5, 255, ThresholdType.BinaryInv);
            return maskInv.ToBitmap();
        }
    }


    /// <summary>
    /// Find what Emgu thinks are edges
    /// </summary>
    /// <param name="tableMask"></param>
    /// <returns></returns>
    private static VectorOfVectorOfPoint GetAllContours(Bitmap tableMask)
    {
        VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
        using (Mat hierarchy = new Mat())
        using (Mat outputMat = new Mat())
        {
            CvInvoke.CvtColor(BitmapExtension.ToMat(tableMask), outputMat, ColorConversion.Bgr2Gray);
            Emgu.CV.CvInvoke.FindContours(outputMat, contours, hierarchy, RetrType.Tree, ChainApproxMethod.ChainApproxSimple);
            return contours;
        }
    }


    /// <summary>
    /// Remove the contours unlikely to be a ball
    /// </summary>
    /// <param name="contours"></param>
    /// <param name="min_s"></param>
    /// <param name="max_s"></param>
    /// <returns></returns>
    private static VectorOfVectorOfPoint FilterContours(VectorOfVectorOfPoint contours, VectorOfPoint tableContour = null, double min_s = 5, double max_s = 50)
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


    /// <summary>
    /// Check if a Contour is completely inside another Contour.
    /// </summary>
    /// <param name="innerContour">The Contour that is being checked if it is inside the outer Contour.</param>
    /// <param name="outerContour">The Contour that is being checked to contain the inner Contour.</param>
    /// <returns>True if the inner Contour is completely inside the outer Contour, false otherwise.</returns>
    private static bool IsContourInside(VectorOfPoint innerContour, VectorOfPoint outerContour)
    {
        for (int i = 0; i < innerContour.Size; i++)
        {
            Point point = innerContour[i];
            double result = CvInvoke.PointPolygonTest(outerContour, point, false);
            if (result < 0) return false;
        }
        return true;
    }

    /// <summary>
    /// Given all contours found in the image, find the table Contour
    /// </summary>
    /// <param name="contours"></param>
    /// <returns>VectorOfPoint Contour with points if found, empty VectorOfPoint if not found</returns>
    private VectorOfPoint GetTableContour(VectorOfVectorOfPoint allContours)
    {
        double imageArea = this.imageSize.Width * this.imageSize.Height;
        double maxArea = 0;
        int maxIndex = -1;

        //find the biggest Contour that isnt the whole frame or close to it. 90% and under seems to work
        for (int i = 0; i < allContours.Size; i++)
        {
            VectorOfPoint Contour = allContours[i];
            double area = CvInvoke.ContourArea(Contour);

            if (area < (imageArea * 0.90) && area > maxArea)
            {
                maxArea = area;
                maxIndex = i;
            }
        }

        if (maxIndex == -1)
        {
            Console.WriteLine("No valid table Contour found.");
            return new VectorOfPoint();
        }

        return allContours[maxIndex]; //table
    }


    /// <summary>
    /// Draw the contours as they are exactly
    /// </summary>
    /// <param name="ctrs"></param>
    /// <param name="img"></param>
    /// <returns></returns>
    private static Bitmap DrawContours(VectorOfVectorOfPoint ctrs, Image<Rgb, byte> img)
    {
        Image<Rgb, byte> output = img.Copy();

        for (int i = 0; i < ctrs.Size; i++)
        {
            VectorOfPoint Contour = ctrs[i];
            CvInvoke.DrawContours(output, new VectorOfVectorOfPoint(Contour), -1, new MCvScalar(244, 0, 250), 2);
        }

        return output.ToBitmap();
    }
}