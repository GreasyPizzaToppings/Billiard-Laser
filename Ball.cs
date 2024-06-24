using System;
using System.Drawing;

public class Ball
{
    public readonly VectorOfPoint contour;

    // the path of the centre
    public List<Point> path;

    public Ball(VectorOfPoint contour, List<Point>? path = null)
    {
        get { return _prevCentre; }
        set
        {
            _prevCentre = value;
            UpdateDeltas();
        }
    }

    public Point Centre { 
        get {
            if (contour == null || contour.ToArray().Length <= 0) return new Point(0, 0); //invalid contour

    public Ball(PointF centre, float radius)
    {
        Centre = centre;
        PrevCentre = centre;
        Radius = radius;
    }

    public Ball(PointF centre, PointF prevCentre, float radius)
    {
        Centre = centre;
        PrevCentre = prevCentre;
        PrevCentre = centre;
        Radius = radius;
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