using Emgu.CV.Structure;
using Emgu.CV;
//using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using OpenCvSharp.WpfExtensions;
using System.Drawing;
using Emgu.CV.XPhoto;
//using OpenCvSharp;
using Emgu.CV.Reg;
using Accord;
using Point = System.Drawing.Point;
using OpenCvSharp;
using ColorConversion = Emgu.CV.CvEnum.ColorConversion;
using ThresholdType = Emgu.CV.CvEnum.ThresholdType;
using CvInvoke = Emgu.CV.CvInvoke;
using System.Transactions;
using VectorOfPoint = Emgu.CV.Util.VectorOfPoint;
using BorderType = Emgu.CV.CvEnum.BorderType;
public class SquareVectors
{
    public List<Point[]> vectorPointList { get; set; }
    public Image<Bgr, byte> output { get; set; }

    public SquareVectors(List<Point[]> v, Image<Bgr, byte> o)
    { 
        this.vectorPointList = v;
        this.output = o;
    }
}
public class CueBallDetector
{
    //List all the colors in snooker
    private Color[] webcolors = {
        Color.Red,
        Color.Green,
        Color.Blue,
        Color.Black,
        Color.White,
        Color.Yellow,
        Color.Brown,
        Color.Pink
    };
    public void FindAndDrawCueBall(PictureBox pictureBoxImage, int threshold = 50)
    {

        Bitmap image = new Bitmap(pictureBoxImage.Image);
        Bitmap grayImage = GrayscaleBitmap(image);
        
        int maxX = 0, maxY = 0;
        int maxBrightness = 0;

        // Find the brightest pixel (assuming it's the cue ball)
        for (int x = 0; x < grayImage.Width; x+=2)
        {
            for (int y = 0; y < grayImage.Height; y+=2)
            {
                int brightness = grayImage.GetPixel(x, y).R;
                if (brightness > maxBrightness)
                {
                    maxBrightness = brightness;
                    maxX = x;
                    maxY = y;
                }
            }
        }

        // Find the top, bottom, left, and right edges of the ball
        int leftEdge = FindEdge(grayImage, maxX, maxY, -1, 0, maxBrightness, threshold);
        int rightEdge = FindEdge(grayImage, maxX, maxY, 1, 0, maxBrightness, threshold);
        int topEdge = FindEdge(grayImage, maxX, maxY, 0, -1, maxBrightness, threshold);
        int bottomEdge = FindEdge(grayImage, maxX, maxY, 0, 1, maxBrightness, threshold);

        // Calculate the radius and center coordinates of the circle
        int radius = Math.Max(rightEdge - leftEdge, bottomEdge - topEdge) / 2;
        int centerX = leftEdge + radius;
        int centerY = topEdge + radius;

        // Print the center coordinates to the console
        Console.WriteLine($"Center coordinates: ({centerX}, {centerY})");

        Graphics g = Graphics.FromImage(image);
        Pen pen = new Pen(Color.Red, 3);
        g.DrawEllipse(pen, centerX - radius, centerY - radius, 2 * radius, 2 * radius);

        // Draw a dot in the middle of the cue ball area
        Brush brush = new SolidBrush(Color.Blue);
        g.FillEllipse(brush, centerX - 2, centerY - 2, 4, 4);

        pictureBoxImage.Image = image;
    }

    private int FindEdge(Bitmap grayImage, int startX, int startY, int directionX, int directionY, int maxBrightness, int threshold)
    {
        int x = startX;
        int y = startY;

        // Move in the specified direction until a dimmer spot is found
        while (x >= 0 && x < grayImage.Width && y >= 0 && y < grayImage.Height)
        {
            int brightness = grayImage.GetPixel(x, y).R;
            if (brightness < maxBrightness - threshold)
            {
                break;
            }
            x += directionX;
            y += directionY;
        }

        // Move backward in the opposite direction to find the edge
        x -= directionX;
        y -= directionY;
        while (x >= 0 && x < grayImage.Width && y >= 0 && y < grayImage.Height)
        {
            int brightness = grayImage.GetPixel(x, y).R;
            if (brightness < maxBrightness - threshold)
            {
                return directionX == 0 ? y + directionY : x + directionX;
            }
            x -= directionX;
            y -= directionY;
        }

        // If no edge is found, return the start position
        return directionX == 0 ? startY : startX;
    }

    private Bitmap GrayscaleBitmap(Bitmap source)
    {
        Bitmap grayscale = new Bitmap(source.Width, source.Height);
        for (int x = 0; x < source.Width; x++)
        {
            for (int y = 0; y < source.Height; y++)
            {
                Color c = source.GetPixel(x, y);
                int lum = (int)(0.3 * c.R + 0.59 * c.G + 0.11 * c.B);
                grayscale.SetPixel(x, y, Color.FromArgb(lum, lum, lum));
            }
        }
        return grayscale;
    }
    static void BitmapToMat(Bitmap bitmap, Emgu.CV.Mat mat)
    {
        BitmapData bmpData = bitmap.LockBits(
            new Rectangle(0, 0, bitmap.Width, bitmap.Height),
            ImageLockMode.ReadOnly,
            PixelFormat.Format24bppRgb);

        CvInvoke.CvtColor(
            new Emgu.CV.Mat(bmpData.Height, bmpData.Width, DepthType.Cv8U, 3, bmpData.Scan0, bmpData.Stride),
            mat,
            ColorConversion.Bgr2Bgra);

        bitmap.UnlockBits(bmpData);
    }

    public void ball_Detection(PictureBox pictureBox1)
    {
        //Get image from the picture box
        Bitmap bmp = new Bitmap(pictureBox1.Image);

        Emgu.CV.Mat transformed = new Emgu.CV.Mat();
        BitmapToMat(bmp, transformed);
        Emgu.CV.Mat transformedBlur = new Emgu.CV.Mat();
        CvInvoke.GaussianBlur(transformed, transformedBlur, new System.Drawing.Size(5, 5), 0, 0, Emgu.CV.CvEnum.BorderType.Default);
        Emgu.CV.Mat  blurRgb = new Emgu.CV.Mat();
        Emgu.CV.CvInvoke.CvtColor(transformedBlur, blurRgb, ColorConversion.Bgr2Rgb);

        // Mask
        Emgu.CV.Mat hsv = new Emgu.CV.Mat();
        Emgu.CV.CvInvoke.CvtColor(blurRgb, hsv, ColorConversion.Rgb2Hsv);
        Emgu.CV.Mat mask = new Emgu.CV.Mat();
        ScalarArray lower = new ScalarArray(new MCvScalar(35, 40,40)); // Set your lower HSV bounds
        ScalarArray upper = new ScalarArray(new MCvScalar(70, 255, 255)); // Set your upper HSV bounds
        Emgu.CV.CvInvoke.InRange(hsv, lower, upper, mask);
        
        // Filter mask
        Emgu.CV.Mat kernel = Emgu.CV.CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new System.Drawing.Size(5, 5), new Point(-1, -1));
        Emgu.CV.Mat maskClosing = new Emgu.CV.Mat();
        Emgu.CV.CvInvoke.MorphologyEx(mask, maskClosing, MorphOp.Close, kernel, new Point(-1, -1), 1, Emgu.CV.CvEnum.BorderType.Reflect, new MCvScalar());

        // Apply threshold
        Emgu.CV.Mat maskInv = new Emgu.CV.Mat();
        Emgu.CV.CvInvoke.Threshold(maskClosing, maskInv, 5, 255, ThresholdType.BinaryInv);



        ////// Create image with masked objects on table
        Emgu.CV.Mat maskedObjects = new Emgu.CV.Mat();
        Emgu.CV.CvInvoke.BitwiseAnd(transformed, transformed, maskedObjects, maskInv);


        //Find contours and filter them
        VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
        Emgu.CV.Mat hierarchy = new Emgu.CV.Mat();
        Emgu.CV.CvInvoke.FindContours(maskInv, contours, hierarchy, RetrType.External, ChainApproxMethod.ChainApproxSimple);
        VectorOfVectorOfPoint filteredContours = filter_Contours(contours, 90, 2000, 3.445);
        Image<Bgr, byte> test = bmp.ToImage<Bgr, byte>();
        SquareVectors squareVectors = DrawRectangles(filteredContours, test);
        FindCtrsColor(squareVectors,pictureBox1);
        //pictureBox1.Image = squareVectors.output.ToBitmap();

    }
    public Color colorApproximate(double blue, double green, double red)
    {
        Color nearestColor = Color.Empty;
        double distance = double.MaxValue;

        foreach (Color c in webcolors)
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
    public void FindCtrsColor(SquareVectors sV, PictureBox p1)
    {
        Image<Bgr, byte> img = sV.output;
        Image<Gray, byte> mask = new Image<Gray, byte>(img.Width, img.Height);
        foreach (var v in sV.vectorPointList)
        {
            mask.Draw(v, new Gray(255), -1);
            p1.Image = mask.ToBitmap(); ;
            break;
            MCvScalar avgColor = CvInvoke.Mean(img, mask);

            // Print the average color
            Console.WriteLine("Average color: B={0}, G={1}, R={2}", avgColor.V0, avgColor.V1, avgColor.V2);
            Console.WriteLine(colorApproximate(avgColor.V0, avgColor.V1, avgColor.V2));

        }
        
        // Compute the average color
       
    }
    public VectorOfVectorOfPoint filter_Contours(VectorOfVectorOfPoint contours, double min_s = 90, double max_s = 358, double alpha = 3.445)
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
                //this assumes the the balls are of the same width and height. 
                //maybe now I'll try to warp the images. but for now, nah. 
                //if ((h * alpha < w) || (w * alpha < h))
                //    continue;

                if ((area < min_s) || (area > max_s))
                    continue;

                filteredContours.Push(contour);
            }
        }
        return filteredContours;
    }
    public SquareVectors DrawRectangles(VectorOfVectorOfPoint ctrs, Image<Bgr, byte> img)
    {
        Image<Bgr, byte> output = img.Copy();
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