using Emgu.CV.Structure;
using Emgu.CV;
using Emgu.CV.Util;
using System;
using System.Drawing;
using System.Linq;
using Accord.IO;

public class Ball : IDisposable
{
    private VectorOfPoint _contour;
    private readonly object _lock = new();
    private bool _disposed = false; // To detect redundant calls
    private Point? _centre = null;
    private double? _area = null;
    private Point? _velocity;
    private double? _speed;
    private double? _acceleration;
    private double? _displacement;

    public double Area
    {
        get
        {
            lock (_lock)
            {
                if (!_area.HasValue)
                {
                    if (_contour == null || _disposed || _contour.Size <= 1) return 0;
                    _area = CvInvoke.ContourArea(_contour);
                }
                return _area.Value;
            }
        }
    }

    public double Confidence { get; set; }
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

    public Point Centre
    {
        get
        {
            lock (_lock)
            {
                if (!_centre.HasValue)
                {
                    if (_contour == null || _contour.Size == 0)
                    {
                        return Point.Empty;
                    }
                    
                    if (_contour.Size == 1)
                    {
                        // If only one point, that point is the center
                        var points = _contour.ToArray();
                        _centre = points[0];
                    }
                    else
                    {
                        var moments = CvInvoke.Moments(_contour);
                        if (Math.Abs(moments.M00) < double.Epsilon)
                        {
                            return Point.Empty;
                        }
                        _centre = new Point(
                            (int)(moments.M10 / moments.M00),
                            (int)(moments.M01 / moments.M00)
                        );
                    }
                }
                return _centre.Value;
            }
        }
    }

    public double Radius
    {
        get
        {
            lock (_lock)
            {
                if (_contour == null || _contour.Size <= 1) return 0;

                var points = _contour.ToArray();
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
    }


    public double Displacement
    {
        get => _displacement ?? 0;
        set => _displacement = value;
    }

    public Point Velocity 
    { 
        get => _velocity ?? Point.Empty;
        set => _velocity = value;
    }

    public double Speed
    {
        get => _speed ?? 0;
        set => _speed = value;
    }

    public double Acceleration
    {
        get => _acceleration ?? 0;
        set => _acceleration = value;
    }

    /// <summary>
    /// Create a ball object
    /// </summary>
    /// <param name="contour"></param>
    /// <exception cref="ArgumentNullException">contour is null or has no points</exception>
    public Ball(VectorOfPoint contour)
    {
        if (contour == null || contour.Size == 0)
        {
            throw new ArgumentNullException(nameof(contour));
        }

        //deep copy to avoid contour disposal
        _contour = new VectorOfPoint();
        _contour.Push(contour);
    }

    public Ball() { }

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

    public override string ToString()
    {
        return $"Ball:\n" +
            $"Centre Position: ({this.Centre.X}, {this.Centre.Y})\n" +
            $"Displacement: {this.Displacement:F2}\n" +
            $"Velocity: ({this.Velocity.X}, {this.Velocity.Y})\n" +
            $"Speed: {this.Speed:F2}\n" +
            $"Acceleration: {this.Acceleration:F2}\n" +
            $"Radius: {this.Radius:F2}\n" +
            $"Area: {this.Area:F2}\n" +
            $"Confidence: {this.Confidence:F2}\n";
    }

    public Ball Clone()
    {
        lock (_lock)
        {
            var clone = new Ball();
            
            // Deep copy the contour if it exists
            if (_contour != null)
            {
                clone._contour = new VectorOfPoint();
                clone._contour.Push(_contour);
            }

            // Copy all other properties
            clone._centre = _centre;
            clone._area = _area;
            clone._velocity = _velocity;
            clone._speed = _speed;
            clone._acceleration = _acceleration;
            clone._displacement = _displacement;
            clone.Confidence = Confidence;

            return clone;
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
