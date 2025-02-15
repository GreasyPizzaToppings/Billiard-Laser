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

    public override string ToString()
    {
        return $"Laser:\n" + 
                $"Position: ({this.Location.X}, {this.Location.Y})\n" +
                $"Intensity: {this.Intensity:F2}\n" +
                $"Area: {this.Area:F2}\n" +
                $"Confidence: {this.Confidence:F2}\n";
    }
}
