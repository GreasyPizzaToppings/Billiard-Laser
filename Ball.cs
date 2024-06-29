using Emgu.CV.Structure;
using Emgu.CV;
using Emgu.CV.Util;

/// <summary>
/// A ball is a contour with centre and radius that can be drawn
/// </summary>
public class Ball
{
    public readonly VectorOfPoint contour;

    public Ball(VectorOfPoint contour)
    {
        this.contour = contour;
    }

    public Point Centre
    {
        get
        {
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