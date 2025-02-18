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
using System.Runtime.InteropServices;

/// <summary>
/// A set of general methods and properties for detecting objects on the billiard table
/// </summary>
public abstract class TableObjectDetector
{
    //image manipulation settings
    public Boolean EnableBlur;
    public Boolean EnableSharpening;
    public Boolean EnableTableBoundary;

    public TableObjectDetector(bool enableBlur = false, bool enableSharpening = false, bool enableTableBoundary = false)
    {
        EnableBlur = enableBlur;
        EnableSharpening = enableSharpening;
        EnableTableBoundary = enableTableBoundary;
    }

    public abstract override string ToString();

    public static void SharpenImage(Mat image)
    {
        float[,] kernelData = new float[,]
        {
            {-1, -1, -1},
            {-1,  9, -1},
            {-1, -1, -1}
        };
        
        using var kernel = new Mat(3, 3, DepthType.Cv32F, 1);
        Marshal.Copy(kernelData.Cast<float>().ToArray(), 0, kernel.DataPointer, 9);
        CvInvoke.Filter2D(image, image, kernel, new Point(-1, -1));
    }

    //Emgu CV bitmap to Mat doesn't convert some bitmaps properly so I had to implement this method. 
    public static Mat BitmapToMat(Bitmap bitmap, Mat mat, DepthType depthType = DepthType.Cv8U)
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

    public static void BlurImage(Mat image)
    {
        CvInvoke.GaussianBlur(image, image, new System.Drawing.Size(5, 5), 0, 0, BorderType.Default);
    }

    /// <summary>
    /// Apply the mask such that all the pixels in the mask range are black but all other pixels have their original colour
    /// </summary>
    /// <param name="inputImage"></param>
    /// <param name="tableMask"></param>
    /// <returns></returns>
    public static Bitmap ApplyMask(Bitmap inputImage, Bitmap tableMask)
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
    /// Get mask image such that every pixel within the lower and upper mask range become black and everything else becomes white
    /// </summary>
    /// <param name="tableImage">Image of the table to mask</param>
    /// <returns></returns>
    public static Bitmap GetMaskImage(Bitmap tableImage, Rgb LowerMaskRgb, Rgb UpperMaskRgb)
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

            Emgu.CV.CvInvoke.Threshold(maskClosing, maskInv, 10, 255, ThresholdType.BinaryInv); //all pixels between this range will become black
            return maskInv.ToBitmap();
        }
    }


    /// <summary>
    /// Find what Emgu thinks are edges
    /// </summary>
    /// <param name="tableMask"></param>
    /// <returns></returns>
    public static VectorOfVectorOfPoint GetAllContours(Bitmap tableMask)
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
    /// Check if a Contour is completely inside another Contour.
    /// </summary>
    /// <param name="innerContour">The Contour that is being checked if it is inside the outer Contour.</param>
    /// <param name="outerContour">The Contour that is being checked to contain the inner Contour.</param>
    /// <returns>True if the inner Contour is completely inside the outer Contour, false otherwise.</returns>
    public static bool IsContourInside(VectorOfPoint innerContour, VectorOfPoint outerContour)
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
    public static VectorOfPoint GetTableContour(VectorOfVectorOfPoint allContours, Size imageSize)
    {
        double imageArea = imageSize.Width * imageSize.Height;
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
    public static Bitmap DrawContours(VectorOfVectorOfPoint ctrs, Image<Rgb, byte> img)
    {
        using (Image<Rgb, byte> output = img.Copy())
        {
            for (int i = 0; i < ctrs.Size; i++)
            {
                VectorOfPoint Contour = ctrs[i];
                CvInvoke.DrawContours(output, new VectorOfVectorOfPoint(Contour), -1, new MCvScalar(244, 0, 250), 2);
            }

            return output.ToBitmap();
        }
    }
}