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
    public void ProcessImage(PictureBox pictureBox1)
    {
        // Assuming pictureBox1 contains the image
        Bitmap bmp = new Bitmap(pictureBox1.Image);
        Image<Bgr, byte> image = bmp.ToImage<Bgr, byte>();
        Image<Gray, float> laplacianImage = new Image<Gray, float>(image.Size);
        CvInvoke.Laplacian(image, laplacianImage, DepthType.Cv32F, ksize: 1);

        // Convert to 8-bit grayscale for visualization
        Image<Gray, byte> outputImage = laplacianImage.ConvertScale<byte>(scale: 1, shift: 128);

        Image<Gray, byte> grayImage = outputImage.Convert<Gray,byte>();
        
        grayImage._GammaCorrect(3.0d);
        grayImage.Mul(3.0f);
        //grayImage._EqualizeHist();


        Bitmap newBitmap = new Bitmap(grayImage.Width, grayImage.Height);

        //for (int x = 0; x < grayImage.Width; x++)
        //{
        //    for (int y = 0; y < grayImage.Height; y++)
        //    {
        //        byte grayValue = (byte)grayImage[y, x].Intensity; // Get grayscale value
        //        Color pixelColor = Color.FromArgb(grayValue, grayValue, grayValue);
        //        newBitmap.SetPixel(x, y, pixelColor); // Set pixel color
        //    }
        //}

        // Assign the newBitmap to your PictureBox
        //pictureBox1.Image = newBitmap;
        //pictureBox1.Image = grayImage.;
        //Mat mat = new Mat(image);
        // Now you can use the image with Emgu CV as before
        // For example, using Hough Circle Transform to detect circles
        double cannyThreshold = 100;
        double circleAccumulatorThreshold = 18;

        CircleF[] circles = CvInvoke.HoughCircles(
            grayImage.PyrDown().PyrUp(),
            HoughModes.Gradient,
            1.5, // Resolution of the accumulator used to detect centers of the circles
            10, // Minimum distance between the centers of the detected circles
            cannyThreshold, // The higher threshold of the two passed to Canny edge detector
            circleAccumulatorThreshold, // Accumulator threshold for the circle centers at the detection stage
            5,
            10// Minimum radius of detected circle
              // Maximum radius of detected circle
        );

        //// Process detected circles
        foreach (CircleF circle in circles)
        {
            // Draw circles on the image
            image.Draw(circle, new Bgr(Color.Red), 2);
        }

        //// Display the result back in pictureBox1
        pictureBox1.Image = image.ToBitmap();

        //// Define thresholds
        //double cannyThreshold = 180;
        //double circleAccumulatorThreshold = 120;
        //Bitmap image = (Bitmap)pictureBox1.Image;
        //// Convert image to grayscale
        //Image<Gray, byte> grayImage = image.ToImage<Gray, byte>();

        //// Use Hough Circle Transform to detect circles
        //CircleF[] circles = CvInvoke.HoughCircles(
        //    grayImage,
        //    HoughModes.Gradient,
        //    2.0, // Resolution of the accumulator used to detect centers of the circles
        //    20, // Minimum distance between the centers of the detected circles
        //    cannyThreshold, // The higher threshold of the two passed to Canny edge detector
        //    circleAccumulatorThreshold, // Accumulator threshold for the circle centers at the detection stage
        //    5, // Minimum radius of detected circle
        //    0 // Maximum radius of detected circle
        //);

        //// Process detected circles
        //foreach (CircleF circle in circles)
        //{
        //    // Draw circles on the image
        //    pictureBox1.Image.Draw(circle, new Bgr(Color.Red), 2);
        //}

        ////Bitmap image = (Bitmap)pictureBox1.Image;

        //// Convert the Bitmap to an EmguCV Image<Bgr, byte>
        //Image<Bgr, byte> imagee = image.ToImage<Bgr,byte>();

        //// Get the Mat from the Image
        //Mat img = imagee.Mat;
        //using (UMat gray = new UMat())
        //using (UMat cannyEdges = new UMat())
        ////using (Emgu.CV.Mat triangleRectangleImage = new Emgu.CV.Mat(img.Size, DepthType.Cv8U, 3)) //image to draw triangles and rectangles on
        //using (Emgu.CV.Mat circleImage = new Emgu.CV.Mat(image.Size, DepthType.Cv8U, 3)) //image to draw circles on
        ////using (Emgu.CV.Mat lineImage = new Emgu.CV.Mat(img.Size, DepthType.Cv8U, 3)) //image to drtaw lines on
        //{
        //    //Convert the image to grayscale and filter out the noise
        //    Emgu.CV.CvInvoke.CvtColor(imagee, gray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);

        //    //Remove noise
        //    CvInvoke.GaussianBlur(gray, gray, new Size(3, 3), 1);

        //    #region circle detection
        //    double cannyThreshold = 180.0;
        //    double circleAccumulatorThreshold = 120;
        //    CircleF[] circles = CvInvoke.HoughCircles(gray, HoughModes.Gradient, 2.0, 20.0, cannyThreshold,
        //        circleAccumulatorThreshold, 5);
        //    #endregion

        //    #region Canny and edge detection
        //    double cannyThresholdLinking = 120.0;
        //    CvInvoke.Canny(gray, cannyEdges, cannyThreshold, cannyThresholdLinking);
        //    LineSegment2D[] lines = CvInvoke.HoughLinesP(
        //        cannyEdges,
        //        1, //Distance resolution in pixel-related units
        //        Math.PI / 45.0, //Angle resolution measured in radians.
        //        20, //threshold
        //        30, //min Line width
        //        10); //gap between lines
        //    #endregion

        //    #region draw circles
        //    circleImage.SetTo(new MCvScalar(0));
        //    foreach (CircleF circle in circles)
        //        CvInvoke.Circle(circleImage, Point.Round(circle.Center), (int)circle.Radius,
        //            new Bgr(Color.Brown).MCvScalar, 2);

        //    //Drawing a light gray frame around the image
        //    CvInvoke.Rectangle(circleImage,
        //        new Rectangle(Point.Empty, new Size(circleImage.Width - 1, circleImage.Height - 1)),
        //        new MCvScalar(120, 120, 120));
        //    //Draw the labels
        //    CvInvoke.PutText(circleImage, "Circles", new Point(20, 20), FontFace.HersheyDuplex, 0.5,
        //        new MCvScalar(120, 120, 120));
        //    #endregion



        //    Emgu.CV.Mat result = new Emgu.CV.Mat();
        //    CvInvoke.VConcat(new Emgu.CV.Mat[] { img,  circleImage }, result);
        //    Bitmap resultBitmap = result.ToBitmap();

        //    // Display the Bitmap in the PictureBox
        //    pictureBox1.Image = resultBitmap;
        //    //return result;
        //}
    }


}