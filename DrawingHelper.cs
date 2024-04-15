using System.Drawing;

public static class DrawingHelper
{
    public static Bitmap DrawBallOnImage(Ball ball, Bitmap image)
    {
        using (var graphics = Graphics.FromImage(image))
        using (var pen = new Pen(Color.DeepPink, 2f))
        {
            var rect = new Rectangle(
                (int)(ball.Centre.X - ball.Radius),
                (int)(ball.Centre.Y - ball.Radius),
                (int)(2 * ball.Radius),
                (int)(2 * ball.Radius));
            graphics.DrawEllipse(pen, rect);
        }
        return image;
    }

    public static Bitmap DrawPoint(PointF point, Bitmap image)
    {
        using (var graphics = Graphics.FromImage(image))
        using (var brush = new SolidBrush(Color.Black))
        {
            var rect = new RectangleF(point.X - 1, point.Y - 1, 2, 2);
            graphics.FillRectangle(brush, rect);
        }
        return image;
    }

    public static Bitmap DrawPolygon(Point[] points, Bitmap image)
    {
        using (var graphics = Graphics.FromImage(image))
        using (var pen = new Pen(Color.LimeGreen, 1f))
        {
            graphics.DrawPolygon(pen, points);
        }
        return image;
    }

    public static Bitmap DrawBallPath(List<PointF> ballPath, Size originalSize, Size targetSize, Bitmap image)
    {
        using (Graphics g = Graphics.FromImage(image))
        using (Pen pen = new Pen(Color.Red, 2))
        {
            List<PointF> scaledPoints = ScalePoints(ballPath, originalSize, targetSize);
            if (scaledPoints.Count > 1)
            {
                g.DrawLines(pen, scaledPoints.ToArray());
            }
            else if (scaledPoints.Count == 1)
            {
                DrawPoint(scaledPoints[0], image);
            }
        }
        return image;
    }

    private static List<PointF> ScalePoints(List<PointF> points, Size original, Size target)
    {
        // Calculate scaling factors
        float scaleX = (float)target.Width / original.Width;
        float scaleY = (float)target.Height / original.Height;

        List<PointF> scaledPoints = new List<PointF>();

        foreach (PointF point in points)
        {
            float scaledX = point.X * scaleX;
            float scaledY = point.Y * scaleY;
            scaledPoints.Add(new PointF(scaledX, scaledY));
        }

        return scaledPoints;
    }
}