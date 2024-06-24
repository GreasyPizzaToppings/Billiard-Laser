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
    public readonly VectorOfPoint contour;

    // the path of the centre
    public List<Point> path;

    public Ball(VectorOfPoint contour, List<Point>? path = null)
    {
        this.contour = contour;
        this.path = path;
    }

    public Point Centre
    {
        get
        {
            //todo BUG!!
            if (contour == null || contour.ToArray().Length <= 0) return new Point(0, 0); //invalid contour

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

    /// <summary>
    /// how much the ball moved from its last position change
    /// </summary>
    public double Displacement {
        get {
            if (path == null || path.Count < 2) return 0;
            return Math.Sqrt(Math.Pow(path[path.Count - 1].X - path[path.Count - 2].X, 2) + Math.Pow(path[path.Count - 1].Y - path[path.Count - 2].Y, 2));
        }
    }

    //Draw on the base image
    public Bitmap Draw(Bitmap baseImage)
    {
        if (contour == null) return baseImage;
        VectorOfPoint contourCopy = new VectorOfPoint(contour.ToArray());

        //invalid contour
        if (contourCopy == null || contourCopy.ToArray().Length <= 0) return baseImage;

        Image<Rgb, byte> output = baseImage.ToImage<Rgb, byte>();
        try
        {
            using (VectorOfVectorOfPoint vvp = new VectorOfVectorOfPoint(contourCopy))
            {
                CvInvoke.DrawContours(output, vvp, -1, new MCvScalar(200, 0, 250), 3);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("error drawing contours: " + ex.Message);
            return baseImage;
        }

        return output.ToBitmap();
    }
}