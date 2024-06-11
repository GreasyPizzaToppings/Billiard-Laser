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
using Accord.MachineLearning;
using Accord.Math;
using static BallDetector;
using AForge.Imaging;
using static System.Windows.Forms.AxHost;
using OpenCvSharp.Extensions;


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

    //default cloth mask values
    public Rgb LowerMaskRgb = new Rgb(40, 80, 40);
    public Rgb UpperMaskRgb = new Rgb(70, 255, 255);

    //default image manipulation values
    public Boolean enableBlur = false;
    public Boolean enableSharpening = true;

    private Color[] ballColors = {
        Color.Brown,
        Color.Green,
        Color.Blue,
        Color.Black,
        Color.White,
        Color.Yellow,
        Color.Purple,
        Color.DarkRed,
        Color.DarkViolet,
        Color.AliceBlue,
        Color.DarkCyan,
        Color.LightBlue,
        Color.Red
    };

    /// <summary>
    /// Detect and draw boxes around balls based on contours
    /// </summary>
    /// <param name="inputImage">Input image of table to process</param>
    /// <returns>Image with balls highlighted</returns>
    public Bitmap FindAllBalls(Bitmap inputImage)
    {
        Bitmap processedImage = inputImage;

        // Apply sharpening if enabled
        if (enableSharpening)
        {
            processedImage = SharpenImage(processedImage);
        }

        // Apply blurring if enabled
        if (enableBlur)
        {
            processedImage = BlurImage(processedImage);
        }

        Bitmap tableMask = GetTableMask(processedImage);

        //shows how it would look like if the mask is applied to original image
        //Bitmap appliedMask = ApplyMask(blurredImage, tableMask);
        //return appliedMask;
        VectorOfVectorOfPoint contours = GetContours(tableMask);
        VectorOfVectorOfPoint filteredContours = FilterContours(contours);
        Image<Rgb, byte> inputImageCopy = inputImage.ToImage<Rgb, byte>();

        SquareVectors squareVectors = DrawRectanglesAroundBalls(filteredContours, inputImageCopy); //change back to filters
        Image<Rgb, byte> ballsHighlighted = squareVectors.output;

        //        squareVectors.output = appliedMask.ToImage<Rgb, byte>();
        //FindCtrsColor(squareVectors, maskedImage); //prints stuff to console

        return ballsHighlighted.ToBitmap();
    }
    

    /// <summary>
    /// Find all balls, and return all the stages of image processing involved
    /// </summary>
    /// <param name="tableImage">Image of the table</param>
    /// <returns></returns>
    public ImageProcessingResults FindAllBallsDebug(Bitmap tableImage)
    {
        Bitmap sharpenedImage = SharpenImage(tableImage);
        Bitmap blurredImage = BlurImage(tableImage);
        Bitmap blurredAndSharpenedImage = BlurImage(sharpenedImage);

        
        Bitmap tableMask = GetTableMask(blurredAndSharpenedImage);
        Bitmap tableWithMaskApplied = ApplyMask(tableImage, tableMask); //for debugging only

        VectorOfVectorOfPoint allContoursFound = GetContours(tableMask);
        VectorOfVectorOfPoint filteredContoursFound = FilterContours(allContoursFound); //remove non-ball anomalies

        // final image with balls detected
        Bitmap filteredBallsHighlighted = DrawRectanglesAroundBalls(filteredContoursFound, tableImage.ToImage<Rgb, byte>()).output.ToBitmap();
        
        // image with non-filtered contours
        Bitmap allBallsHighlighted = DrawRectanglesAroundBalls(allContoursFound, tableImage.ToImage<Rgb, byte>()).output.ToBitmap();

        return new ImageProcessingResults
        {
            OriginalImage = tableImage,
            BlurredImage = blurredImage,
            SharpenedImage = sharpenedImage,
            BlurredAndSharpenedImage = blurredAndSharpenedImage,
            ImageMask = tableMask,
            ImageWithMaskApplied = tableWithMaskApplied,
            AllBallsFound = allBallsHighlighted,
            FilteredBallsFound = filteredBallsHighlighted
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

        // Create the Convolution filter
        Convolution filter = new Convolution(kernel);

        // Apply the filter
        Bitmap resultImage = filter.Apply(image);

        return resultImage;
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
        //Emgu.CV.Mat maskedObjects = new Emgu.CV.Mat();
        //Emgu.CV.Mat inputMat = new Emgu.CV.Mat();
        //Emgu.CV.Mat outputMat = new Emgu.CV.Mat();
        Image<Bgra, byte> inputMat = inputImage.ToImage<Bgra, byte>();
        Image<Bgra, byte> outputMat = tableMask.ToImage<Bgra, byte>();
        Image<Bgra, byte> maskedObjects = new Image<Bgra, byte>(inputMat.Size);
        // Perform bitwise AND operation with the mask

        // Perform bitwise AND operation with the mask
        CvInvoke.BitwiseAnd(inputMat, outputMat, maskedObjects);
        //Emgu.CV.CvInvoke.BitwiseAnd(BitmapToMat(inputImage, inputMat), BitmapToMat(tableMask, outputMat), maskedObjects);
        for (int y = 0; y < maskedObjects.Height; y++)
        {
            for (int x = 0; x < maskedObjects.Width; x++)
            {
                Bgra color = maskedObjects[y, x];
                if (color.Blue == 0 && color.Green == 0 && color.Red == 0)
                {
                    color.Alpha = 0; // make the pixel transparent
                    maskedObjects[y, x] = color;
                }
            }
        }
        return maskedObjects.ToBitmap();
       

        //return rgbaMat.ToBitmap();
        //return maskedObjects.ToBitmap();
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
        Color nearestColor = Color.Empty;
        double distance = double.MaxValue;
        //For detected vectors, find the closest color it's associated to. 
        //so something like... 
        //
        foreach (Color c in ballColors)
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
    private VectorOfVectorOfPoint FilterContours(VectorOfVectorOfPoint contours, double min_s = 8, double max_s = 9000)
    {
        List<BallAreasAndContours> ballAreasAndContours = new List<BallAreasAndContours>();
        VectorOfVectorOfPoint filteredContours = new VectorOfVectorOfPoint();
        for (int i = 0; i < contours.Size; i++)
        {
            using (Emgu.CV.Util.VectorOfPoint contour = contours[i])
            {
                Emgu.CV.Structure.RotatedRect rotRect = Emgu.CV.CvInvoke.MinAreaRect(contour);
                float w = rotRect.Size.Width;
                float h = rotRect.Size.Height;
                double area = Emgu.CV.CvInvoke.ContourArea(contour);
                

                //this assumes the the balls are of the same width and height. 
                //maybe now I'll try to warp the images. but for now, nah. 
                
                //filter out non-squares or non-ball shaped things
                //if ((h > w * 1.5) || (w > h*1.5)) continue;

                //filter out balls with very small area or too big areas
                if ((area < (min_s*min_s)) || (area > (max_s*max_s)))
                    continue;
                BallAreasAndContours anc = new BallAreasAndContours(area, contour);                
                ballAreasAndContours.Add(anc);
                //Console.WriteLine($"Filter contours: Area: {area}");
                
            }
        }
        double averageArea = ballAreasAndContours.Average(b => b.area);
        foreach (var i in ballAreasAndContours)
        {
            if(i.area + 10 > averageArea && i.area - 10 < averageArea)filteredContours.Push(i.contour);
        }
        return filteredContours;
    }

    /// <summary>
    /// Draw rectangles around the balls we detected
    /// </summary>
    /// <param name="ctrs"></param>
    /// <param name="img"></param>
    /// <returns></returns>
    private SquareVectors DrawRectanglesAroundBalls(VectorOfVectorOfPoint ctrs, Image<Rgb, byte> img)
    {
        Image<Rgb, byte> output = img.Copy();
        List<Point[]> squareCtrs = new List<Point[]>();

        for (int i = 0; i < ctrs.Size; i++)
        {
            using (VectorOfPoint contour = ctrs[i])
            {
                // Calculate moments
                Emgu.CV.Moments moments = CvInvoke.Moments(contour, false);

                // Calculate minimum area rectangle
                Emgu.CV.Structure.RotatedRect rotatedRect = CvInvoke.MinAreaRect(contour);
                float w = rotatedRect.Size.Width; // width
                float h = rotatedRect.Size.Height; // height

                // Calculate box points and draw contours
                PointF[] boxPoints = CvInvoke.BoxPoints(rotatedRect);
                Point[] boxPointsInt = Array.ConvertAll(boxPoints, Point.Round);

                using (VectorOfPoint boxContour = new VectorOfPoint(boxPointsInt))
                {
                    using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
                    {
                        contours.Push(boxContour);
                        squareCtrs.Add(boxPointsInt);
                        CvInvoke.DrawContours(output, contours, -1, new MCvScalar(255, 100, 1), 2);
                    }
                }
            }
        }

        return new SquareVectors(squareCtrs, output);
    }
    public static Bitmap GetDominantColor(SquareVectors sV)
    {
        Bitmap image = sV.output.ToBitmap();

        // Get the dimensions of the image


        // Reshape the image into a 2D array, where each row represents a pixel
        foreach (Point[] p in sV.points)
        {
            int startX = p[0].X;
            int startY = p[0].Y;
            int endX = p[2].X;
            int endY = p[2].Y;
            int w = Math.Abs(endX - startX);
            int h = Math.Abs(endY - startY);
            double[][] pixels = new double[w * h][];
            int indexX = 0;

            for (int i = startX; i < endX; i++)
            {
                int indexY = 0;
                for (int j = startY; j < endY; j++)
                {
                    Color pixelColor = image.GetPixel(i, j);
                    pixels[indexX * h + indexY] = new double[] { pixelColor.R, pixelColor.G, pixelColor.B };
                    indexY++;
                }
                indexX++;
            }

            // Set the desired number of colors for the image
            int n_colors = 6;

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
        return null;
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