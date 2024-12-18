﻿using Emgu.CV.Structure;
using Emgu.CV;
using Emgu.CV.Util;
using System;
using System.Drawing;
using System.Linq;

public class Ball : IDisposable
{
    private VectorOfPoint _contour;
    private readonly object _lock = new();
    private bool _disposed = false; // To detect redundant calls

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
            // Always create a new Bitmap to return
            Bitmap resultImage = new Bitmap(baseImage);

            if (_contour == null || _contour.Size <= 0) return resultImage;

            using (Image<Rgb, byte> output = resultImage.ToImage<Rgb, byte>())
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
                    return resultImage;
                }
            }
        }
    }

    // Protected implementation of Dispose pattern.
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // Dispose managed state (managed objects).
                _contour?.Dispose();
            }

            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~Ball()
    {
        Dispose(false);
    }
}
