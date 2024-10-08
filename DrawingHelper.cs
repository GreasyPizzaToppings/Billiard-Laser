﻿using System.Drawing;

public static class DrawingHelper
{
    
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
        using (var pen = new Pen(Color.Magenta, 1f))
        {
            graphics.DrawPolygon(pen, points);
        }
        return image;
    }

    public static Bitmap DrawBallPath(List<PointF> ballPath, Size originalSize, Size targetSize, Bitmap image)
    {
        using (Graphics g = Graphics.FromImage(image))
        using (Pen pen = new Pen(Color.Orange, 1f))
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

    public static Bitmap DrawAccelerationGraph(Bitmap image, Shot shot)
    {
        using (Graphics graphics = Graphics.FromImage(image))
        {
            // Clear the graph
            graphics.Clear(Color.White);

            int xAxisHeight = image.Height - (image.Height / 2);

            // Draw the axes
            using (Pen axisPen = new Pen(Color.Black))
            {
                graphics.DrawLine(axisPen, 0, xAxisHeight, image.Width, xAxisHeight); // X-axis
                graphics.DrawLine(axisPen, 20, 0, 20, image.Height); // Y-axis
            }

            // Calculate the scaling factors
            double maxSpeed = shot.FrameDistances.Max();

            double scaleX = 0;
            if (shot.FrameDistances.Count > 1) scaleX = (image.Width - 40) / (double)(shot.AccelerationOverTime.Count - 1);
            else scaleX = (image.Width - 40);

            double scaleY = 0;
            if (maxSpeed != 0) scaleY = (image.Height - 40) / maxSpeed;
            else scaleY = image.Height - 40;

            // Draw the acceleration line
            using (Pen linePen = new Pen(Color.Blue))
            {
                for (int i = 0; i < shot.AccelerationOverTime.Count - 1; i++)
                {
                    float x1 = (float)(20 + i * scaleX);
                    float y1 = (float)(xAxisHeight - shot.AccelerationOverTime[i] * scaleY);
                    float x2 = (float)(20 + (i + 1) * scaleX);
                    float y2 = (float)(xAxisHeight - shot.AccelerationOverTime[i + 1] * scaleY);
                    graphics.DrawLine(linePen, x1, y1, x2, y2);
                }
            }
        }

        return image;
    }

    public static Bitmap DrawDistanceTravelledGraph(Bitmap image, Shot shot)
    {
        using (Graphics graphics = Graphics.FromImage(image))
        {
            // Clear the graph
            graphics.Clear(Color.White);

            // Draw the axes
            using (Pen axisPen = new Pen(Color.Black))
            {
                graphics.DrawLine(axisPen, 0, image.Height - 20, image.Width, image.Height - 20); // X-axis
                graphics.DrawLine(axisPen, 20, 0, 20, image.Height); // Y-axis
            }

            // Calculate the scaling factors
            double maxDistance = shot.DistanceTravelledOverTime.Max();
            double scaleX = (image.Width - 40) / (double)(shot.DistanceTravelledOverTime.Count - 1);
            double scaleY;
            if (maxDistance > 0) scaleY = (image.Height - 40) / maxDistance;
            else scaleY = (image.Height - image.Height / 2);


            // Draw the distance travelled line
            using (Pen linePen = new Pen(Color.Green))
            {
                for (int i = 0; i < shot.DistanceTravelledOverTime.Count - 1; i++)
                {
                    float x1 = (float)(20 + i * scaleX);
                    float y1 = (float)(image.Height - 20 - shot.DistanceTravelledOverTime[i] * scaleY);
                    float x2 = (float)(20 + (i + 1) * scaleX);
                    float y2 = (float)(image.Height - 20 - shot.DistanceTravelledOverTime[i + 1] * scaleY);
                    graphics.DrawLine(linePen, x1, y1, x2, y2);
                }
            }
        }
        return image;
    }

    public static Bitmap DrawSpeedOverTimeGraph(Bitmap image, Shot shot)
    {
        using (Graphics graphics = Graphics.FromImage(image))
        {
            // Clear the graph
            graphics.Clear(Color.White);

            // Draw the axes
            using (Pen axisPen = new Pen(Color.Black))
            {
                graphics.DrawLine(axisPen, 0, image.Height - 20, image.Width, image.Height - 20); // X-axis
                graphics.DrawLine(axisPen, 20, 0, 20, image.Height); // Y-axis
            }

            // Calculate the scaling factors
            double maxSpeed = shot.FrameDistances.Max();

            double scaleX = 0;
            if (shot.FrameDistances.Count > 1) scaleX = (image.Width - 40) / (double)(shot.FrameDistances.Count - 1);
            else scaleX = (image.Width - 40);
            
            double scaleY = 0;
            if (maxSpeed != 0) scaleY = (image.Height - 40) / maxSpeed;
            else scaleY = image.Height - 40;

            // Draw the speed over time line
            using (Pen linePen = new Pen(Color.Orange))
            {
                for (int i = 0; i < shot.FrameDistances.Count - 1; i++)
                {
                    float x1 = (float)(20 + i * scaleX);
                    float y1 = (float)(image.Height - 20 - shot.FrameDistances[i] * scaleY);
                    float x2 = (float)(20 + (i + 1) * scaleX);
                    float y2 = (float)(image.Height - 20 - shot.FrameDistances[i + 1] * scaleY);
                    graphics.DrawLine(linePen, x1, y1, x2, y2);
                }
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