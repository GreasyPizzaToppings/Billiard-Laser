using System;

public class Laser
{
    public Point Location { get; set; }
    public double Intensity { get; set; }
    public double Area { get; set; }
    public double Confidence { get; set; }

    public Laser(Point location, double intensity, double area)
    {
        Location = location;
        Intensity = intensity;
        Area = area;
        Confidence = 0;
    }
}
