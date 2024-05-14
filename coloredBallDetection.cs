using System;
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
public class coloredBallDetection
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
        Color.Red,
        Color.Green,
        Color.Blue,
        Color.Black,
        Color.White,
        Color.Yellow,
        Color.Brown,
        Color.Pink
    };
    public coloredBallDetection()
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
            ColorConversion.Bgr2Bgra);

        bitmap.UnlockBits(bmpData);
    }
    //Detects balls based on contours.
    public void ballDetection(PictureBox pictureBox1)
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

        ////// Create image with masked objects on table
        Emgu.CV.Mat maskedObjects = new Emgu.CV.Mat();
        Emgu.CV.CvInvoke.BitwiseAnd(transformed, transformed, maskedObjects, maskInv);
        pictureBox1.Image = maskedObjects.ToBitmap();

        //Find contours and filter them
        VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
        Emgu.CV.Mat hierarchy = new Emgu.CV.Mat();
        Emgu.CV.CvInvoke.FindContours(maskInv, contours, hierarchy, RetrType.External, ChainApproxMethod.ChainApproxSimple);
        VectorOfVectorOfPoint filteredContours = filter_Contours(contours, 90, 2000, 3.445);
        Image<Rgb, byte> test = bmp.ToImage<Rgb, byte>();
        SquareVectors squareVectors = DrawRectangles(filteredContours, test);
        Image<Rgb, byte> toOutput = squareVectors.output;
        squareVectors.output = maskedObjects.ToBitmap().ToImage<Rgb, byte>();
        FindCtrsColor(squareVectors, pictureBox1);
        pictureBox1.Image = toOutput.ToBitmap();

    }
    public Color colorApproximate(double blue, double green, double red)
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
    public void FindCtrsColor(SquareVectors sV, PictureBox p1)
    {
        Image<Rgb, byte> img = sV.output;
        p1.Image = img.ToBitmap();
        Image<Gray, byte> mask = new Image<Gray, byte>(img.Width, img.Height);
        //Instead of checking each ball, check each ball color, and find which is the closest ball
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

                        if (color.Blue == 0 && color.Red == 0 && color.Green == 0) { continue; }
                        if (color.Blue == 255 && color.Red == 255 && color.Green == 255) { continue; }
                        if (color.Blue < color.Green && color.Red < color.Green) { continue; }
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
            //Console.WriteLine(avgColor);
            // Print the average color
            //Console.WriteLine("Average color: B={0}, G={1}, R={2}", avgColor.V0, avgColor.V1, avgColor.V2);
            

        }
        //Console.WriteLine(colorApproximate(avgBlue, avgGreen, avgRed));
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
