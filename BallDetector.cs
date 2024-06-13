using System;
using Emgu.CV.Structure;
using Emgu.CV;
using System.Drawing.Imaging;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using Point = System.Drawing.Point;
using ColorConversion = Emgu.CV.CvEnum.ColorConversion;
using ThresholdType = Emgu.CV.CvEnum.ThresholdType;
using CvInvoke = Emgu.CV.CvInvoke;
using VectorOfPoint = Emgu.CV.Util.VectorOfPoint;
using AForge.Imaging.Filters;
using System.Drawing;
using Accord.MachineLearning;
using Accord.Math;
using static BallDetector;
using AForge.Imaging;
using static System.Windows.Forms.AxHost;
using OpenCvSharp.Extensions;
using ScottPlot.Palettes;
using System.Windows.Markup;
using Emgu.CV.Linemod;

public class BallDetector
{
    public class SquareVectors
    {
        public List<Point[]> points;
        public Image<Rgb, byte> output;
        public SquareVectors(List<Point[]> points, Image<Rgb, byte> output)
        {
            this.points = points;
            this.output = output;
        }
    }

    public class BallAreasAndContours
    {
        public double area;
        public VectorOfPoint contour;
        public BallAreasAndContours(double area, VectorOfPoint contour)
        {
            this.area = area;
            this.contour = contour;
        }
    }

    //default cloth color (green)
    public Rgb LowerMaskRgb = new Rgb(40, 80, 40);
    public Rgb UpperMaskRgb = new Rgb(70, 255, 255);

    //image manipulation of the mask
    public Boolean EnableBlur = false;
    public Boolean EnableSharpening = false;

    public Size imageSize = new Size(0, 0);
    Image<Rgb, byte> tableImage;

    /// <summary>
    /// Get the image with all balls highlighted
    /// </summary>
    /// <param name="tableImage">Image of the table</param>
    /// <returns></returns>
    public Bitmap FindAllBalls(Bitmap tableImage)
    {
        return FindAllBallsDebug(tableImage).FilteredBallsFound;
    }

    /// <summary>
    /// Return all the stages of image processing involved with finding balls
    /// </summary>
    /// <param name="tableImage">Image of the table</param>
    /// <returns></returns>
    public ImageProcessingResults FindAllBallsDebug(Bitmap tableImage)
    {
        this.tableImage = tableImage.ToImage<Rgb, byte>();//debug

        Bitmap workingImage = tableImage;
        imageSize = tableImage.Size;
        Bitmap sharpenedImage = null, blurredImage = null, blurredAndSharpenedImage = null;

        if (EnableSharpening)
        {
            sharpenedImage = SharpenImage(workingImage);
            workingImage = sharpenedImage;
        }

        if (EnableBlur)
        {
            blurredImage = BlurImage(workingImage);
            workingImage = blurredImage;

            if (EnableSharpening) blurredAndSharpenedImage = workingImage;
        }

        Bitmap tableMask = GetTableMask(workingImage);
        Bitmap tableWithMaskApplied = ApplyMask(tableImage, tableMask);

        VectorOfVectorOfPoint allContoursFound = GetContours(tableMask);



        //VectorOfVectorOfPoint filteredContoursFound = FilterContours(allContoursFound); // remove non-ball anomalies
        var tableEdges = GetTableEdges(allContoursFound);
        ((Point leftMostTable, Point rightMostTable, Point topMostTable, Point bottomMostTable), Image<Rgb, byte> outputImage) = tableEdges;

        Bitmap tableHighlighted = outputImage.ToBitmap();


        // final image with balls detected
        //Bitmap filteredBallsHighlighted = DrawContours(filteredContoursFound, tableImage.ToImage<Rgb, byte>()).output.ToBitmap();

        // image with non-filtered contours
        Bitmap allBallsHighlighted = DrawContours(allContoursFound, tableImage.ToImage<Rgb, byte>()).output.ToBitmap();

        return new ImageProcessingResults
        {
            OriginalImage = tableImage,
            BlurredImage = blurredImage,
            SharpenedImage = sharpenedImage,
            BlurredAndSharpenedImage = blurredAndSharpenedImage,
            ImageMask = tableMask,
            ImageWithMaskApplied = tableWithMaskApplied,
            AllBallsFound = allBallsHighlighted,
            FilteredBallsFound = tableHighlighted
        };
    }

    private Bitmap SharpenImage(Bitmap image)
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
    private static Emgu.CV.Mat BitmapToMat(Bitmap bitmap, Emgu.CV.Mat mat, DepthType depthType = DepthType.Cv8U)
    {
        BitmapData bmpData = bitmap.LockBits(
            new Rectangle(0, 0, bitmap.Width, bitmap.Height),
            ImageLockMode.ReadOnly,
            PixelFormat.Format24bppRgb);

        CvInvoke.CvtColor(
            new Emgu.CV.Mat(bmpData.Height, bmpData.Width, depthType, 3, bmpData.Scan0, bmpData.Stride),
            mat,
            ColorConversion.Rgb2Rgba);

        bitmap.UnlockBits(bmpData);
        return mat;
    }

    private Bitmap BlurImage(Bitmap inputImage)
    {
        Emgu.CV.Mat transformed = new Emgu.CV.Mat();
        BitmapToMat(inputImage, transformed);
        Emgu.CV.Mat blurredImage = new Emgu.CV.Mat();
        CvInvoke.GaussianBlur(transformed, blurredImage, new System.Drawing.Size(5, 5), 0, 0, Emgu.CV.CvEnum.BorderType.Default);
        
        return blurredImage.ToBitmap();
    }

    /// <summary>
    /// get the mask image for the table to remove the cloth
    /// </summary>
    /// <param name="tableImage">Image of the table to mask</param>
    /// <returns></returns>
    private Bitmap GetTableMask(Bitmap tableImage)
    {
        Emgu.CV.Mat imageMat = BitmapExtension.ToMat(tableImage);
        Emgu.CV.Mat hsv = new Emgu.CV.Mat();
        Emgu.CV.CvInvoke.CvtColor(imageMat, hsv, ColorConversion.Bgr2Hsv);
        Emgu.CV.Mat mask = new Emgu.CV.Mat();
        
        //mask based on a range of hues (cloth colour)
        ScalarArray LowerMaskValue = new ScalarArray(new MCvScalar(LowerMaskRgb.Red, LowerMaskRgb.Green, LowerMaskRgb.Blue));
        ScalarArray UpperMaskValue = new ScalarArray(new MCvScalar(UpperMaskRgb.Red, UpperMaskRgb.Green, UpperMaskRgb.Blue));
        Emgu.CV.CvInvoke.InRange(hsv, LowerMaskValue, UpperMaskValue, mask);

        // Filter mask
        Emgu.CV.Mat kernel = Emgu.CV.CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new System.Drawing.Size(5, 5), new Point(-1, -1));
        Emgu.CV.Mat maskClosing = new Emgu.CV.Mat();
        Emgu.CV.CvInvoke.MorphologyEx(mask, maskClosing, MorphOp.Close, kernel, new Point(-1, -1), 1, Emgu.CV.CvEnum.BorderType.Reflect, new MCvScalar());

        // Apply threshold
        Emgu.CV.Mat maskInv = new Emgu.CV.Mat();
        Emgu.CV.CvInvoke.Threshold(maskClosing, maskInv, 5, 255, ThresholdType.BinaryInv);
        return maskInv.ToBitmap();
    }

    private Bitmap ApplyMask(Bitmap inputImage, Bitmap tableMask)
    {
        Emgu.CV.Mat maskedObjects = new Emgu.CV.Mat();
        Emgu.CV.Mat inputMat = new Emgu.CV.Mat();
        Emgu.CV.Mat outputMat = new Emgu.CV.Mat();
        Emgu.CV.CvInvoke.BitwiseAnd(BitmapToMat(inputImage, inputMat), BitmapToMat(tableMask, outputMat), maskedObjects);
        return maskedObjects.ToBitmap();
    }

    /// <summary>
    /// Find what Emgu thinks are edges
    /// </summary>
    /// <param name="tableMask"></param>
    /// <returns></returns>
    private VectorOfVectorOfPoint GetContours(Bitmap tableMask)
    {
        VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
        Emgu.CV.Mat hierarchy = new Emgu.CV.Mat();
        Emgu.CV.Mat outputMat = new Emgu.CV.Mat();
        CvInvoke.CvtColor(BitmapExtension.ToMat(tableMask), outputMat, ColorConversion.Bgr2Gray);
        Emgu.CV.CvInvoke.FindContours(outputMat, contours, hierarchy, RetrType.Tree, ChainApproxMethod.ChainApproxSimple);
        return contours;
    }

    //TODO: improve and use, or remove if cant think of anything
    private Color ColorApproximate(double blue, double green, double red)
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

    //sussy
    //remove non-ball contours that are too small or too big
    private VectorOfVectorOfPoint FilterContours(VectorOfVectorOfPoint contours, double min_s = 5, double max_s = 50)
    {
        var tableEdges = GetTableEdges(contours);

        //if (tableEdges == null)
        //{
        //    Console.WriteLine("No valid table edges found.");
        //    return contours;
        //}



       ((Point leftMostTable, Point rightMostTable, Point topMostTable, Point bottomMostTable), Image<Rgb, byte> outputImage) = tableEdges;

        List<double> contourAreas = new List<double>();
        for (int i = 0; i < contours.Size; i++)
        {
            VectorOfPoint contour = contours[i];
            contourAreas.Add(CvInvoke.ContourArea(contour));
        }

        Console.WriteLine("---");
        
        VectorOfVectorOfPoint filteredContours = new VectorOfVectorOfPoint();
        for (int i = 0; i < contours.Size; i++)
        {
            using (Emgu.CV.Util.VectorOfPoint contour = contours[i])
            {
                (Point leftMost, Point rightMost, Point topMost, Point bottomMost) = FindEdges(contour);

                // Check for out-of-bounds of table
                if (leftMost.X < leftMostTable.X || rightMost.X > rightMostTable.X ||
                    topMost.Y < topMostTable.Y || bottomMost.Y > bottomMostTable.Y)
                {
                    continue;
                }

                //filter out non-squares or non-ball shaped things
                Emgu.CV.Structure.RotatedRect rotRect = Emgu.CV.CvInvoke.MinAreaRect(contour);
                float w = rotRect.Size.Width;
                float h = rotRect.Size.Height;
                if ((h > w * 4) || (w > h * 4)) continue; //allows some ball-speed to be detected (elongated)

                //filter out balls with very small area or too big areas
                double area = contourAreas[i];
                if ((area < (min_s*min_s)) || (area > (max_s*max_s)))
                    continue;

                filteredContours.Push(contour);

                Console.WriteLine($"Accepted Contour Info: \nWidth: {w}\nHeight: {h}\nArea: {area}\n");
            }
        }
      
        Console.WriteLine("---");

        //if (contourAreas.Count > 0)
        //{
        //    double? averageArea = contourAreas?.Average(b => b.area);
        //    //find the square differences from the mean
        //    double? sumOfSquaresOfDifferences = contourAreas?.Sum(b => Math.Pow(b.area - averageArea.GetValueOrDefault(), 2));
        //    /* add up all the squared difference and divide the number of data points and take the
        //    sqrt of the deviance */
        //    double? stddev = Math.Sqrt(sumOfSquaresOfDifferences.GetValueOrDefault() / (contourAreas?.Count ?? 0));

        //    List<BallAreasAndContours> filteredList = contourAreas
        //    ?.Where(b => Math.Abs(b.area - averageArea.GetValueOrDefault()) <= 1.1 * stddev.GetValueOrDefault())
        //    .ToList();

        //    if (filteredList != null)
        //        foreach (var i in filteredList)
        //            filteredContours.Push(i.contour);
        //}
        
        return filteredContours;
    }


    public (Point leftMost, Point rightMost, Point topMost, Point bottomMost) FindEdges(VectorOfPoint contour)
    {
        Point leftMost = contour[0];
        Point rightMost = contour[0];
        Point topMost = contour[0];
        Point bottomMost = contour[0];

        for (int i = 1; i < contour.Size; i++)
        {
            Point point = contour[i];

            if (point.X < leftMost.X)
            {
                leftMost = point;
            }

            if (point.X > rightMost.X)
            {
                rightMost = point;
            }

            if (point.Y < topMost.Y)
            {
                topMost = point;
            }

            if (point.Y > bottomMost.Y)
            {
                bottomMost = point;
            }
        }

        return (leftMost, rightMost, topMost, bottomMost);
    }

    public ((Point leftMost, Point rightMost, Point topMost, Point bottomMost), Image<Rgb, byte> debugImage) GetTableEdges(VectorOfVectorOfPoint contours)
    {
        double imageArea = imageSize.Width * imageSize.Height;
        double maxArea = 0;
        int maxIndex = -1;


        for (int i = 0; i < contours.Size; i++)
        {
            VectorOfPoint contour = contours[i];
            double area = CvInvoke.ContourArea(contour);

            //bad code. todo refactor
            if (area < (imageArea*0.90) && area > maxArea)
            {
                maxArea = area;
                maxIndex = i;
            }
        }

        if (maxIndex == -1)
        {
            Console.WriteLine("No valid contours found.");
            return new ((new Point(0, 0), new Point(0, 0), new Point(0, 0), new Point(0, 0)), this.tableImage);
        }

        VectorOfPoint largestContour = contours[maxIndex]; //table

        //draw table
        Image<Rgb, byte> output = this.tableImage.Copy();
        CvInvoke.DrawContours(output, contours, maxIndex, new MCvScalar(255, 0, 0), 5);


        
       return (FindEdges(largestContour), output);
    }

    /// <summary>
    /// Draw the contours as they are exactly
    /// </summary>
    /// <param name="ctrs"></param>
    /// <param name="img"></param>
    /// <returns></returns>
    private SquareVectors DrawContours(VectorOfVectorOfPoint ctrs, Image<Rgb, byte> img)
    {
        Image<Rgb, byte> output = img.Copy();
        List<Point[]> contourPoints = new List<Point[]>();

        for (int i = 0; i < ctrs.Size; i++)
        {
            using (VectorOfPoint contour = ctrs[i])
            {
                // Store the points of the contour
                contourPoints.Add(contour.ToArray());
                CvInvoke.DrawContours(output, new VectorOfVectorOfPoint(contour), -1, new MCvScalar(244, 0, 250), 2);
                
            }
        }

        return new SquareVectors(contourPoints, output);
    }

    private SquareVectors DrawContoursAsCircles(VectorOfVectorOfPoint ctrs, Image<Rgb, byte> img)
    {
        Image<Rgb, byte> output = img.Copy();
        List<Point[]> contourPoints = new List<Point[]>();

        for (int i = 0; i < ctrs.Size; i++)
        {
            using (VectorOfPoint contour = ctrs[i])
            {
                // Store the points of the contour
                contourPoints.Add(contour.ToArray());

                // Calculate the moments of the contour to get the centroid
                var moments = CvInvoke.Moments(contour);

                if (moments.M00 != 0)
                {
                    // Calculate centroid
                    int centerX = (int)(moments.M10 / moments.M00);
                    int centerY = (int)(moments.M01 / moments.M00);
                    Point center = new Point(centerX, centerY);

                    // Calculate the radius as the mean distance from the centroid to the contour points
                    double meanRadius = Math.Sqrt(CvInvoke.ContourArea(contour) / Math.PI);

                    // Draw the circle on the image
                    CvInvoke.Circle(output, center, (int)meanRadius, new MCvScalar(244, 0, 250), 3);
                }
            }
        }

        return new SquareVectors(contourPoints, output);
    }



    public Bitmap dominantColorOfImage(Bitmap image)
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