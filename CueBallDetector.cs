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
public class CueBallDetector
{
   
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
    public void loadOriginalImage(PictureBox pictureBoxImage)
    {

        //Saves the file in the picturebox into a folder somewhere like tmp
        //once the picture is saved
        //string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        //Console.WriteLine($"Base directory: {baseDirectory}");
        //string tmpfile = "../../../tmp/temp.jpg";
        //pictureBoxImage.Image.Save(tmpfile);

        //src = Cv.LoadImage(tmpfile, LoadMode.Color);
        //Cv.SaveImage("Orig.jpg", src);
        //IplImage src, gray, hough;
        ////Bitmap bitmap = new Bitmap(pictureBoxImage.Image); // your bitmap image
        ////Image<Bgr, Byte> img = bitmap.ToImage<Bgr, byte>();

        //Bitmap bmp = new Bitmap(pictureBoxImage.Image);
        //src = new IplImage(bmp.Width, bmp.Height, BitDepth.U8, 3);  //creates the OpenCvSharp IplImage;
        //src.CopyFrom(bmp);
        ////then we do whatever the fuck is gonna happen. 
        ////so do the loadcv thingies that the person in the youtube vid did. 
        //gray = Cv.CreateImage(src.Size, BitDepth.U8, 1);
        //Cv.CvtColor(src, gray, ColorConversion.RgbToGray);DllNotFoundException: Unable to load DLL 'opencv_core240' or one of its dependencies: The specified module could not be found. (0x8007007E)
        //Cv.Smooth(gray, gray, SmoothType.Gaussian, 9);
        //Cv.Canny(gray, gray, 10, 30, ApertureSize.Size3);
        ////Cv.SaveImage("greycircles.jpg", gray);
        //hough = src.Clone();
        //var storage = new CvMemStorage();
        //CvSeq<CvCircleSegment> seq = gray.HoughCircles(storage, HoughCirclesMethod.Gradient, 1, 50, 10, 25, 0, 40);
        //foreach (CvCircleSegment segment in seq)
        //{
        //    hough.Circle(segment.Center, (int)segment.Radius, CvColor.Red, 3);
        //    Console.WriteLine("Center = " + segment.Center + " Radius = " + (int)segment.Radius);
        //}
        //Cv.SaveImage("detected_circles.jpg", hough);
        //pictureBoxImage.ImageLocation = "detected_circles.jpg";

    }
    //public void greyscale(PictureBox pictureBoxImage)
    //{

    //    //Cv.SaveImage("Gray.jpg", gray);

    //}
    public void Filter(PictureBox pictureBox1)
    {
        UMat cannyEdges = new UMat();
        UMat gray = new UMat();
        double brightness = 1.0; // Increase brightness by 100% (1.0 means no change)
        double contrast = 3.0; // Increase contrast by 200%
        // theory of image: 
        // turn into gray image so filtering is easier 
        // remove the noise of the gray image. 
        // can be by brightening the things hard. 
        Bitmap bmp = new Bitmap(pictureBox1.Image);
        Image<Bgr, byte> image = bmp.ToImage<Bgr, byte>();
        Image<Gray, float> grayImage = image.Convert<Gray, float>();
        

        // Adjust brightness and contrast
        CvInvoke.cvConvertScale(grayImage, grayImage, contrast, brightness);
        Emgu.CV.CvInvoke.GaussianBlur(grayImage, grayImage, new System.Drawing.Size(3, 3), 1);
        Image<Gray, float> sobelImage = grayImage.Sobel(1, 0, 11).Add(grayImage.Sobel(0, 1,11)).AbsDiff(new Gray(0.0));
        Bitmap bmp1 = sobelImage.ToBitmap();
        pictureBox1.Image = bmp1;
        Mat grey = sobelImage.Convert<Gray, byte>().Mat;
        //cleaning the noise



        double cannyThreshold = 120;
        double circleAccumulatorThreshold = 120;
        double cannyThresholdLinking = 130.0;
        // Canny function 
        Emgu.CV.CvInvoke.Canny(grey, cannyEdges, cannyThreshold, cannyThresholdLinking);

        LineSegment2D[] lines = Emgu.CV.CvInvoke.HoughLinesP(
                    cannyEdges,
                    1, //Distance resolution in pixel-related units
                    Math.PI / 45.0, //Angle resolution measured in radians.
                    20, //threshold
                    30, //min Line width
                    10); //gap between lines
                         //        Image<Gray, byte> imageCV = grayImage;

        // circle detection region
        CircleF[] circles = Emgu.CV.CvInvoke.HoughCircles(cannyEdges, HoughModes.Gradient, 2.0, 20.0, cannyThreshold, circleAccumulatorThreshold, 5);

        //region canny and edge detection
        //circls
        Emgu.CV.Mat circleImage = new Mat(grey.Size, DepthType.Cv8U, 3);
        circleImage.SetTo(new MCvScalar(0));
        foreach (CircleF circle in circles)
            CvInvoke.Circle(circleImage, Point.Round(circle.Center), (int)circle.Radius,
                new Bgr(Color.Gray).MCvScalar, 2);

        //Drawing a light gray frame around the image
        CvInvoke.Rectangle(circleImage,
            new Rectangle(Point.Empty, new Size(circleImage.Width - 1, circleImage.Height - 1)),
            new MCvScalar(120, 120, 120));
        //Draw the labels
        CvInvoke.PutText(circleImage, "Circles", new Point(20, 20), FontFace.HersheyDuplex, 0.5,
            new MCvScalar(120, 120, 120));

        //end region
        Mat lineImage = new Mat(grey.Size, DepthType.Cv8U, 3);
        //lines
        lineImage.SetTo(new MCvScalar(0));
        foreach (LineSegment2D line in lines)
            CvInvoke.Line(lineImage, line.P1, line.P2, new Bgr(Color.Green).MCvScalar, 2);
        //Drawing a light gray frame around the image
        CvInvoke.Rectangle(lineImage,
            new Rectangle(Point.Empty, new Size(lineImage.Width - 1, lineImage.Height - 1)),
            new MCvScalar(120, 120, 120));
        //Draw the labels
        CvInvoke.PutText(lineImage, "Lines", new Point(20, 20), FontFace.HersheyDuplex, 0.5,
            new MCvScalar(120, 120, 120));
        Mat result = new Mat();
        CvInvoke.VConcat(new Mat[] { image.Mat, circleImage, lineImage }, result);

        Image<Bgr, byte> resultImage = result.ToImage<Bgr, byte>();

        pictureBox1.Image = resultImage.ToBitmap<Bgr,byte>();
        //foreach (CircleF[] circle in seq)
        //{
        //    foreach (CircleF singleCircle in circle)
        //    {
        //        image.Draw(singleCircle, new Bgr(Color.Red), 2);
        //    }
        //}
        //pictureBox1.Image = image.ToBitmap();

    }


}