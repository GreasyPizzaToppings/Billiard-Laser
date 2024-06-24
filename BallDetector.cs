using Accord;
using Accord.MachineLearning;
using Accord.Math;
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

        VectorOfVectorOfPoint allContoursFound = GetAllContours(tableWithMaskApplied);
        VectorOfPoint? tableContour = EnableTableBoundary ? GetTableContour(allContoursFound) : null;
        VectorOfVectorOfPoint filteredContoursFound = tableContour != null ? FilterContours(allContoursFound, tableContour) : FilterContours(allContoursFound);

        Ball cueball = FindCueBall(OnlyBalls(workingImage, filteredContoursFound));
        List<Ball> balls = filteredContoursFound.ToArrayOfArray().Select(contour => new Ball(new VectorOfPoint(contour))).ToList();

        return new ImageProcessingResults
        {
            OriginalImage = tableImage,
            TransformedImage = transformedImage,
            CueBallMask = GetMaskImage(tableWithMaskApplied, LowerCueBallMask, UpperCueBallMask),
            CueBallHighlighted = DrawContours(new VectorOfVectorOfPoint(new VectorOfPoint[] { cueball.contour }), tableImage.ToImage<Rgb, byte>()),
            TableMask = tableMask,
            TableWithMaskApplied = tableWithMaskApplied,
            AllBallsHighlighted = DrawContours(allContoursFound, tableImage.ToImage<Rgb, byte>()),
            FilteredBallsHighlighted = DrawContours(filteredContoursFound, tableImage.ToImage<Rgb, byte>()),
            TableBoundaryHighlighted = tableContour != null? DrawContours(new VectorOfVectorOfPoint(new VectorOfPoint[] { tableContour }), tableImage.ToImage<Rgb, byte>()) : null,

            CueBall = cueball,
            Balls = balls
        };
    }

    /// <summary>
    /// Apple cueball mask to the masked table image. The cue ball is the biggest area contour
    /// </summary>
    /// <param name="maskedTableImage"></param>
    /// <returns></returns>
    private Ball FindCueBall(Bitmap maskedTableImage)
    {
        //For the masked image, it should only show the filtered image already


        double MaxArea = 0;
        VectorOfPoint Cueball = new VectorOfPoint(0);
        for (int i = 0; i < allContoursFound.Size; i++)
        {
            double area = CvInvoke.ContourArea(allContoursFound[i]);
            if (MaxArea < area)
            {
                MaxArea = area;
                Cueball = allContoursFound[i];
            }
        }

        return new Ball(Cueball);
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
        Mat transformed = new Mat();
        BitmapToMat(inputImage, transformed);
        Mat blurredImage = new Mat();
        CvInvoke.GaussianBlur(transformed, blurredImage, new System.Drawing.Size(5, 5), 0, 0, Emgu.CV.CvEnum.BorderType.Default);

        return blurredImage.ToBitmap();
    }

    private static Bitmap ApplyMask(Bitmap inputImage, Bitmap tableMask)
    {
        Mat maskedObjects = new Mat();
        Mat inputMat = new Mat();
        Mat outputMat = new Mat();
        Emgu.CV.CvInvoke.BitwiseAnd(BitmapToMat(inputImage, inputMat), BitmapToMat(tableMask, outputMat), maskedObjects);
        return maskedObjects.ToBitmap();
    }


    /// <summary>
    /// get the mask image for the table to remove the cloth
    /// </summary>
    /// <param name="tableImage">Image of the table to mask</param>
    /// <returns></returns>
    private static Bitmap GetMaskImage(Bitmap tableImage, Rgb LowerMaskRgb, Rgb UpperMaskRgb)
    {
        Mat imageMat = BitmapExtension.ToMat(tableImage);
        Mat hsv = new Mat();
        Emgu.CV.CvInvoke.CvtColor(imageMat, hsv, ColorConversion.Bgr2Hsv);
        Mat mask = new Mat();

        //mask based on a range of hues (cloth colour)
        ScalarArray LowerMaskValue = new ScalarArray(new MCvScalar(LowerMaskRgb.Red, LowerMaskRgb.Green, LowerMaskRgb.Blue));
        ScalarArray UpperMaskValue = new ScalarArray(new MCvScalar(UpperMaskRgb.Red, UpperMaskRgb.Green, UpperMaskRgb.Blue));
        Emgu.CV.CvInvoke.InRange(hsv, LowerMaskValue, UpperMaskValue, mask);

        // Filter mask
        Mat kernel = Emgu.CV.CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new System.Drawing.Size(5, 5), new Point(-1, -1));
        Mat maskClosing = new Mat();
        Emgu.CV.CvInvoke.MorphologyEx(mask, maskClosing, MorphOp.Close, kernel, new Point(-1, -1), 1, Emgu.CV.CvEnum.BorderType.Reflect, new MCvScalar());

        // Apply threshold
        Mat maskInv = new Mat();
        Emgu.CV.CvInvoke.Threshold(maskClosing, maskInv, 5, 255, ThresholdType.BinaryInv);
        return maskInv.ToBitmap();
    }


    /// <summary>
    /// Find what Emgu thinks are edges
    /// </summary>
    /// <param name="tableMask"></param>
    /// <returns></returns>
    private static VectorOfVectorOfPoint GetAllContours(Bitmap tableMask)
    {
        VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
        Mat hierarchy = new Mat();
        Mat outputMat = new Mat();
        CvInvoke.CvtColor(BitmapExtension.ToMat(tableMask), outputMat, ColorConversion.Bgr2Gray);
        Emgu.CV.CvInvoke.FindContours(outputMat, contours, hierarchy, RetrType.Tree, ChainApproxMethod.ChainApproxSimple);
        return contours;
    }


    /// <summary>
    /// Remove the contours unlikely to be a ball. Return as balls instead of contours
    /// </summary>
    /// <param name="contours"></param>
    /// <param name="min_s"></param>
    /// <param name="max_s"></param>
    /// <returns></returns>
    private static VectorOfVectorOfPoint FilterContours(VectorOfVectorOfPoint contours, VectorOfPoint tableContour = null, double min_s = 5, double max_s = 50)
    {
        Console.WriteLine("---");

        VectorOfVectorOfPoint filteredContours = new VectorOfVectorOfPoint();

        //show table contour if enabled
        if (tableContour != null) filteredContours.Push(tableContour);

        for (int i = 0; i < contours.Size; i++)
        {
            using (VectorOfPoint contour = contours[i])
            {
                //filter out contours that are not inside the table contour
                if (tableContour != null && !IsContourInside(contour, tableContour)) continue;

                RotatedRect rotRect = CvInvoke.MinAreaRect(contour);
                float w = rotRect.Size.Width;
                float h = rotRect.Size.Height;

                //allows some ball-speed to be detected (elongated ball shape)
                if ((h > w * 4) || (w > h * 4)) continue;

                //filter out balls with very small area or too big areas
                double area = CvInvoke.ContourArea(contour);
                if ((area < (min_s * min_s)) || (area > (max_s * max_s)))
                    continue;

                filteredContours.Push(contour);

                Console.WriteLine($"Accepted Contour Info: \nWidth: {w}\nHeight: {h}\nArea: {area}\n");
            }
        }

        Console.WriteLine("---");

        return filteredContours;
    }


    /// <summary>
    /// Check if a contour is completely inside another contour.
    /// </summary>
    /// <param name="innerContour">The contour that is being checked if it is inside the outer contour.</param>
    /// <param name="outerContour">The contour that is being checked to contain the inner contour.</param>
    /// <returns>True if the inner contour is completely inside the outer contour, false otherwise.</returns>
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
    /// Given all contours found in the image, find the table contour
    /// </summary>
    /// <param name="contours"></param>
    /// <returns>VectorOfPoint contour with points if found, empty VectorOfPoint if not found</returns>
    private VectorOfPoint GetTableContour(VectorOfVectorOfPoint allContours)
    {
        double imageArea = this.imageSize.Width * this.imageSize.Height;
        double maxArea = 0;
        int maxIndex = -1;

        //find the biggest contour that isnt the whole frame or close to it. 90% and under seems to work
        for (int i = 0; i < allContours.Size; i++)
        {
            VectorOfPoint contour = allContours[i];
            double area = CvInvoke.ContourArea(contour);

            if (area < (imageArea * 0.90) && area > maxArea)
            {
                maxArea = area;
                maxIndex = i;
            }
        }

        if (maxIndex == -1)
        {
            Console.WriteLine("No valid table contour found.");
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
            using (VectorOfPoint contour = ctrs[i])
            {
                CvInvoke.DrawContours(output, new VectorOfVectorOfPoint(contour), -1, new MCvScalar(244, 0, 250), 2);

            }
        }

        return output.ToBitmap();
    }

    //TODO: improve and use, or remove if cant think of anything
    private static Color ColorApproximate(double blue, double green, double red)
    {
        Color[] BallColors = {
            Color.Red,
            Color.Green,
            Color.Blue,
            Color.Black,
            Color.White,
            Color.Yellow,
            Color.Brown,
            Color.Pink
        };

        Color nearestColor = Color.Empty;
        double distance = double.MaxValue;
        //For detected vectors, find the closest color it's associated to. 
        //so something like... 
        //
        foreach (Color c in BallColors)
        {
            double redDiff = Math.Pow(c.R - red, 2.0);
            double greenDiff = Math.Pow(c.G - green, 2.0);
            double blueDiff = Math.Pow(c.B - blue, 2.0);
            double temp = Math.Sqrt(redDiff + greenDiff + blueDiff);
            if (temp == 0)
            {
                nearestColor = c;
                break;
            }
            //This will do an approximation of colors
            else if (temp < distance)
            {
                distance = temp;
                nearestColor = c;
            }
        }

        return nearestColor;
    }

    private static Bitmap GetDominantColorOfImage(Bitmap image)
    {
        int w = image.Width;
        int h = image.Height;
        double[][] pixels = new double[w * h][];
        int indexX = 0;
        // Resize the image (optional)
        for (int i = 0; i < w; i++)
        {
            int indexY = 0;
            for (int j = 0; j < h; j++)
            {
                Color pixelColor = image.GetPixel(i, j);
                pixels[indexX * h + indexY] = new double[] { pixelColor.R, pixelColor.G, pixelColor.B };
                indexY++;
            }
            indexX++;
        }

        // Set the desired number of colors for the image
        int n_colors = 1;

        // Create a KMeans model with the specified number of clusters and fit it to the pixels
        KMeans kmeans = new KMeans(n_colors);
        var clusters = kmeans.Learn(pixels);

        // Get the cluster centers (representing colors) from the model
        double[][] colorPalette = clusters.Centroids;

        // Convert the color palette to integers and reshape it for display
        byte[][] colorPaletteInt = colorPalette.Apply(x => x.Apply(y => (byte)y));
        Bitmap paletteImage = new Bitmap(n_colors, 1);
        for (int i = 0; i < n_colors; i++)
        {
            paletteImage.SetPixel(i, 0, Color.FromArgb(colorPaletteInt[i][0], colorPaletteInt[i][1], colorPaletteInt[i][2]));
            Console.WriteLine(Color.FromArgb(colorPaletteInt[i][0], colorPaletteInt[i][1], colorPaletteInt[i][2]));
        }
        return paletteImage;
    }
}