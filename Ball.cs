using Emgu.CV.Structure;
using Emgu.CV;
using Emgu.CV.Util;
using System;
using System.Drawing;
using AForge.Imaging;

/// <summary>
/// A ball is a contour with a path, that can be drawn
/// </summary>
public class Ball
{
    public VectorOfPoint contour;

    // the path of the centre
    public List<Point> path;

    public Ball(VectorOfPoint contour, List<Point>? path = null)
    {
        this.contour = contour;
        this.path = path;
    }

    public Point Centre { 
        get {
            var points = contour.ToArray().ToList();
            int centrex = (int)points.Average(point => point.X);
            int centrey = (int)points.Average(point => point.Y);
            return new Point(centrex, centrey);
        } 
    }

    public double Radius
    {
        get
        {
            var points = contour.ToArray().ToList();
            double radius = 0;
            foreach (var point in points)
            {
                double distance = Math.Sqrt(Math.Pow(point.X - Centre.X, 2) + Math.Pow(point.Y - Centre.Y, 2));
                if (distance > radius)
                {
                    radius = distance;
                }
            }
            return radius;
        }
    }

    public Point PrevCentre;



    //Draw on the base image
    public Bitmap Draw(Bitmap baseImage)
    {
        //invalid contour
        if (contour == null || contour.ToArray().Length <= 0) return baseImage;

        Image<Rgb, byte> output = baseImage.ToImage<Rgb, byte>();
        CvInvoke.DrawContours(output, new VectorOfVectorOfPoint(contour), -1, new MCvScalar(200, 0, 250), 3);
        return output.ToBitmap();
    }
    
}