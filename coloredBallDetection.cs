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
using ScottPlot.Colormaps;
using Accord.MachineLearning;
using Accord.Math;

public class ColoredBallDetection
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
    

    public ColoredBallDetection()
	{

	}
    //Emgu CV bitmap to Mat doesn't convert some bitmaps properly so I had to implement this method. 
    static void BitmapToMat(Bitmap bitmap, Emgu.CV.Mat mat)
    {
        BitmapData bmpData = bitmap.LockBits(
            new Rectangle(0, 0, bitmap.Width, bitmap.Height),
            ImageLockMode.ReadOnly,
            PixelFormat.Format24bppRgb);

        CvInvoke.CvtColor(
            new Emgu.CV.Mat(bmpData.Height, bmpData.Width, DepthType.Cv8U, 3, bmpData.Scan0, bmpData.Stride),
            mat,
            ColorConversion.Rgb2Rgba);

        bitmap.UnlockBits(bmpData);
    }
    //Detects balls based on contours.
    public void BallDetection(PictureBox pictureBox1)
    {
        //Get image from the picture box
        Bitmap bmp = new Bitmap(pictureBox1.Image);

        Emgu.CV.Mat transformed = new Emgu.CV.Mat();
        BitmapToMat(bmp, transformed);
        Emgu.CV.Mat blurredImage = new Emgu.CV.Mat();
        CvInvoke.GaussianBlur(transformed, blurredImage, new System.Drawing.Size(5, 5), 0, 0, Emgu.CV.CvEnum.BorderType.Default);

        // Mask
        Emgu.CV.Mat hsv = new Emgu.CV.Mat();
        Emgu.CV.CvInvoke.CvtColor(blurredImage, hsv, ColorConversion.Bgr2Hsv);
        Emgu.CV.Mat mask = new Emgu.CV.Mat();
        
        // Green hue colors
        ScalarArray lower = new ScalarArray(new MCvScalar(35, 40, 40)); 
        ScalarArray upper = new ScalarArray(new MCvScalar(70, 255, 255));
        Emgu.CV.CvInvoke.InRange(hsv, lower, upper, mask);

        // Filter mask
        Emgu.CV.Mat kernel = Emgu.CV.CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new System.Drawing.Size(5, 5), new Point(-1, -1));
        Emgu.CV.Mat maskClosing = new Emgu.CV.Mat();
        Emgu.CV.CvInvoke.MorphologyEx(mask, maskClosing, MorphOp.Close, kernel, new Point(-1, -1), 1, Emgu.CV.CvEnum.BorderType.Reflect, new MCvScalar());

        // Apply threshold
        Emgu.CV.Mat maskInv = new Emgu.CV.Mat();
        Emgu.CV.CvInvoke.Threshold(maskClosing, maskInv, 5, 255, ThresholdType.BinaryInv);

        // masks the pool table out to only find contour inside of masked objects
        Emgu.CV.Mat maskedObjects = new Emgu.CV.Mat();
        Emgu.CV.CvInvoke.BitwiseAnd(transformed, transformed, maskedObjects, maskInv);
        //pictureBox1.Image = maskedObjects.ToBitmap();

        //Find contours and filters only the ones that are balls
        VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
        Emgu.CV.Mat hierarchy = new Emgu.CV.Mat();
        Emgu.CV.CvInvoke.FindContours(maskInv, contours, hierarchy, RetrType.External, ChainApproxMethod.ChainApproxSimple);
        VectorOfVectorOfPoint filteredContours = FilterContours(contours);
        Image<Rgb, byte> test = bmp.ToImage<Rgb, byte>();
        
        //Draws rectangles on the contours
        SquareVectors squareVectors = DrawRectangles(filteredContours, test);
        Image<Rgb, byte> toOutput = squareVectors.output;
        ////pictureBox1.Image = maskedObjects.ToBitmap();
        squareVectors.output = maskedObjects.ToBitmap().ToImage<Rgb, byte>();

        //List<MCvScalar> BallColorAverages =  FindCtrsColor(squareVectors, pictureBox1);
        //FindCtrsColor(squareVectors, pictureBox1);
        GetDominantColor(squareVectors, pictureBox1);
        //ColorApproximate(BallColorAverages, squareVectors.points);
        //pictureBox1.Image = squareVectors.output.ToBitmap();
        
    }
    public static void GetDominantColor(SquareVectors sV, PictureBox p1)
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
        }
        //p1.Image = paletteImage;
        // Display the color palette as an image
        

        //Bitmap bitmap = sV.output.ToBitmap();
        //var maxWidth = bitmap.Width;
        //var maxHeight = bitmap.Height;

        ////TODO: validate the region being requested

        ////Used for tally
        //int r = 0;
        //int g = 0;
        //int b = 0;
        //int totalPixels = 0;
        ////foreach (Point[] p in sV.points)
        ////{
        //    //int startX = p[0].X;
        //    //int startY = p[0].Y;
        //    for (int x = 0; x < maxWidth; x++)
        //    {
        //        for (int y = 0; y <maxHeight; y++)
        //        {
        //            Color pixel = bitmap.GetPixel(x, y);
        //            if (pixel.R == 0 && pixel.B == 0 && pixel.G == 0) 
        //                continue;
        //            r += pixel.R;
        //            g += pixel.G;
        //            b += pixel.B;
        //            totalPixels++;
        //        }
        //    }

        //    //Calculate average
        //    r /= totalPixels;
        //    g /= totalPixels;
        //    b /= totalPixels;
        //    Console.WriteLine(Color.FromArgb(r, g, b));
        ////}


    }

    public Color ColorApproximate(double avgBlue, double avgRed, double avgGreen)
    {
        //Color nearestColor = Color.Empty;
        Color nearestColor = Color.Empty;
        double distance = double.MaxValue;
        //For detected vectors, find the closest color it's associated to. 
        //so something like... 


        foreach (Color c in ballColors)
        {


            double redDiff = Math.Pow(c.R - avgRed, 2.0);
            double greenDiff = Math.Pow(c.G - avgGreen, 2.0);
            double blueDiff = Math.Pow(c.B - avgBlue, 2.0);
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
        //if (c != Color.Red) 
        
        //List<MCvScalar> ballsCopy = balls;
        //balls = ballsCopy;
    }

        
    public void ColorApproximate(List<MCvScalar> balls, List<Point[]> v)
    {

        //Color nearestColor = Color.Empty;
        MCvScalar nearestColor = new MCvScalar(0);
        double distance = double.MaxValue;
        //For detected vectors, find the closest color it's associated to. 
        //so something like... 
        int initialCount = balls.Count;
        int positionToRemove = 0;
        while (balls.Count > 0)
        {
            if (balls.Count < initialCount)
            {
                ballColors = new Color[1];
                ballColors[0] = Color.Red;
            }
            foreach (Color c in ballColors)
            {
                

                for (int i = 0; i < balls.Count; i++)
                {
                    double redDiff = Math.Pow(c.R - balls[i].V0, 2.0);
                    double greenDiff = Math.Pow(c.G - balls[i].V1, 2.0);
                    double blueDiff = Math.Pow(c.B - balls[i].V2, 2.0);
                    double temp = Math.Sqrt(redDiff + greenDiff + blueDiff);
                    if (temp == 0)
                    {
                        nearestColor = balls[i];
                        positionToRemove = i;
                        break;
                    }
                    //This will do an approximation of colors
                    else if (temp < distance)
                    {
                        distance = temp;
                        positionToRemove = i;
                        nearestColor = balls[i];
                    }
                }
                //if (c != Color.Red) 
                balls.RemoveAt(positionToRemove);
                distance = double.MaxValue;
                //List<MCvScalar> ballsCopy = balls;
                //balls = ballsCopy;
                Console.WriteLine($"{v[positionToRemove][0]},{v[positionToRemove][1]},{v[positionToRemove][2]},{v[positionToRemove][3]}");
                v.RemoveAt(positionToRemove);
                Console.WriteLine($"{c}: {nearestColor.V0}, {nearestColor.V1}, {nearestColor.V2}");
            }
        }
    }
    public void FindCtrsColor(SquareVectors sV, PictureBox p1)
    {
        Image<Rgb, byte> img = sV.output;
        p1.Image = img.ToBitmap();
        Image<Gray, byte> mask = new Image<Gray, byte>(img.Width, img.Height);
        //Instead of checking each ball, check each ball color, and find which is the closest ball
        List<MCvScalar> BallColorAverages = new List<MCvScalar>();
        foreach (var v in sV.points)
        {
            mask.Draw(v, new Gray(255), -1);
            //get non-zero indices in matrix
            Matrix<byte> idx = new Matrix<byte>(mask.Size);
            mask.CopyTo(idx);

            List<Rgb> colors = new List<Rgb>();

            for (int i = 0; i < img.Rows; i++)
            {
                for (int j = 0; j < img.Cols; j++)
                {
                    // If the mask is non-zero at this pixel
                    if (idx.Data[i, j] != 0)
                    {

                        // Get the color of the pixel
                        Rgb color = img[i, j];

                        //Console.WriteLine(color);
                        //Console.WriteLine(color);
                        // Add the color to the list
                        if (color.Red == 0 && color.Blue == 0 && color.Green == 0) continue;
                        //Console.WriteLine(color);
                        colors.Add(color);
                    }
                }
            }

            // Now 'colors' contains the BGR values of all pixels inside the mask
            double sumBlue = 0, sumGreen = 0, sumRed = 0;

            foreach (Rgb color in colors)
            {
                sumBlue += color.Blue;
                sumGreen += color.Green;
                sumRed += color.Red;
            }

            int numColors = colors.Count;

            double avgBlue = sumBlue / numColors;
            double avgGreen = sumGreen / numColors;
            double avgRed = sumRed / numColors;

            Console.WriteLine("Average color: B={0}, G={1}, R={2}", avgBlue, avgGreen, avgRed);

            //MCvScalar avgColor = CvInvoke.Mean(img, mask);
            ////Console.WriteLine(avgColor);
            //////Print the average color
            ////Console.WriteLine("Average color: B={0}, G={1}, R={2}", avgColor.V0, avgColor.V1, avgColor.V2);
            //BallColorAverages.Add(avgColor);
            Console.WriteLine(ColorApproximate(avgBlue, avgGreen, avgRed));
        }

        // Compute the average color
        //return BallColorAverages;

    }
    public VectorOfVectorOfPoint FilterContours(VectorOfVectorOfPoint contours, double min_s = 20, double max_s = 9000, double alpha = 3.445)
    {
        VectorOfVectorOfPoint filteredContours = new VectorOfVectorOfPoint();
        for (int i = 0; i < contours.Size; i++)
        {
            using (Emgu.CV.Util.VectorOfPoint contour = contours[i])
            {
                Emgu.CV.Structure.RotatedRect rotRect = Emgu.CV.CvInvoke.MinAreaRect(contour);
                float w = rotRect.Size.Width;
                float h = rotRect.Size.Height;
                double area = Emgu.CV.CvInvoke.ContourArea(contour);
                
                //if the width and height of the detected ball is too large compared to the other property, not the ball we want
                if ((h > w * 1.5) || (w > h*1.5))
                    continue;
                //if the detected balls are too large or too small, it's not a ball
                if ((area < min_s) || (area > max_s))
                    continue;

                filteredContours.Push(contour);
            }
        }
        return filteredContours;
    }
    public SquareVectors DrawRectangles(VectorOfVectorOfPoint ctrs, Image<Rgb, byte> img)
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
}

 