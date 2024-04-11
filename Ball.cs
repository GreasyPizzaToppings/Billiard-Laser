using System;

public class Ball
{

	
    public Point prevCentre { get; set; } //where it came from
    public Point centre { get; set; } //where it is now

    public int radius { get; set; }
	public int velocityX { get; set; }
	public int velocityY { get; set; }

	

    public Ball(Point centre, int radius)
	{
		this.centre = centre;
		this.prevCentre = centre;
		this.radius = radius;
	}

	public Ball() { }
}
