using System;
using System.Drawing;

public class Ball
{
    private PointF _prevCentre;
    public PointF PrevCentre
    {
        get { return _prevCentre; }
        set
        {
            _prevCentre = value;
            UpdateDeltas();
        }
    }

    public PointF Centre { get; set; }
    public float Radius { get; set; }
    public float DeltaX { get; private set; }
    public float DeltaY { get; private set; }

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

    public Ball() { }

    private void UpdateDeltas()
    {
        DeltaX = Centre.X - PrevCentre.X;
        DeltaY = Centre.Y - PrevCentre.Y;
    }
}