public class Ball
{
    public PointF CurrentPosition { get; set; }
    public PointF PreviousPosition { get; set; }
    public float Radius { get; set; }
    public Velocity Velocity { get; set; }

    public Ball(PointF position, float radius)
    {
        CurrentPosition = position;
        PreviousPosition = position;
        Radius = radius;
        Velocity = new Velocity(0, 0);
    }

    public void UpdateBallPositionAndVelocity(PointF newPosition, float deltaTime)
    {
        // Store the previous position
        PreviousPosition = CurrentPosition;

        // Update the current position
        CurrentPosition = newPosition;

        // Calculate the displacement
        float displacementX = newPosition.X - PreviousPosition.X;
        float displacementY = newPosition.Y - PreviousPosition.Y;

        // Calculate the velocity
        float velocityX = displacementX / deltaTime;
        float velocityY = displacementY / deltaTime;

        // Update the ball's velocity
        Velocity = new Velocity(velocityX, velocityY);

        Console.WriteLine("\nDEBUG: Position: ({0},{1})", CurrentPosition.X, CurrentPosition.Y); //debug
        Console.WriteLine("DEBUG: Velocity: X{0}, Y{1}]\n", Velocity.X, Velocity.Y); //debug
    }
}

public class Velocity
{
    public float X { get; set; }
    public float Y { get; set; }

    public Velocity(float x, float y)
    {
        X = x;
        Y = y;
    }
}