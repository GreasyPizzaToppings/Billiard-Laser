using Emgu.CV.Structure;
using Emgu.CV;
using Emgu.CV.Util;
using System;
using System.Drawing;
using System.Linq;
using System.Threading;

public class Ball : IDisposable
{
    private VectorOfPoint _contour;
    private readonly object _lock = new object();

    public VectorOfPoint Contour
    {
        get
        {
            lock (_lock)
            {
                return new VectorOfPoint(_contour.ToArray());
            }
        }
    }

    public Ball(VectorOfPoint contour)
    {
        _contour = new VectorOfPoint(contour.ToArray());
    }

    public Point Centre
    {
        get
        {
            lock (_lock)
            {
                if (_contour == null || _contour.Size == 0) return new Point(0, 0);

                var points = _contour.ToArray();
                int centrex = (int)points.Average(point => point.X);
                int centrey = (int)points.Average(point => point.Y);
                return new Point(centrex, centrey);
            }
        }
    }

    public double Radius
    {
        get
        {
            lock (_lock)
            {
                if (_contour == null || _contour.Size == 0) return 0;

                var points = _contour.ToArray();
                double radius = 0;
                Point center = Centre;
                foreach (var point in points)
                {
                    double distance = Math.Sqrt(Math.Pow(point.X - center.X, 2) + Math.Pow(point.Y - center.Y, 2));
                    if (distance > radius)
                    {
                        radius = distance;
                    }
                }
                return radius;
            }
        }
    }

    public Bitmap Draw(Bitmap baseImage)
    {
        lock (_lock)
        {
            if (_contour == null || _contour.Size <= 0) return baseImage;

            using (Image<Rgb, byte> output = baseImage.ToImage<Rgb, byte>())
            using (VectorOfPoint contourCopy = new VectorOfPoint(_contour.ToArray()))
            using (VectorOfVectorOfPoint vvp = new VectorOfVectorOfPoint(contourCopy))
            {
                try
                {
                    CvInvoke.DrawContours(output, vvp, -1, new MCvScalar(200, 0, 250), 3);
                    return output.ToBitmap();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error drawing contours: " + ex.Message);
                    return baseImage;
                }
            }
        }
    }

    public void Dispose()
    {
        lock (_lock)
        {
            _contour?.Dispose();
            _contour = null;
        }
    }
}
