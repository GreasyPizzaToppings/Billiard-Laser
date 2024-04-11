using System;

public class Ball
{

	
    public PointF prevCentre { get; set; } //where it came from
    public PointF centre { get; set; } //where it is now

    public float radius { get; set; }
	public float velocityX { get; set; }
	public float velocityY { get; set; }


    public Ball(PointF centre, float radius)
	{
		this.centre = centre;
		this.prevCentre = centre;
		this.radius = radius;
	}

	public Ball() { }
}
