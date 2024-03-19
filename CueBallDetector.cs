using System;

public class CueBallDetector
{
    public void FindAndDrawCueBall(PictureBox pictureBoxImage, int threshold = 50)
    {
        Bitmap image = new Bitmap(pictureBoxImage.Image);
        Bitmap grayImage = GrayscaleBitmap(image);

        int maxX = 0, maxY = 0;
        int maxBrightness = 0;

        // Find the brightest pixel (assuming it's the cue ball)
        for (int x = 0; x < grayImage.Width; x++)
        {
            for (int y = 0; y < grayImage.Height; y++)
            {
                int brightness = grayImage.GetPixel(x, y).R;
                if (brightness > maxBrightness)
                {
                    maxBrightness = brightness;
                    maxX = x;
                    maxY = y;
                }
            }
        }

        // Find the left and right edges of the ball
        int leftEdge = FindEdge(grayImage, maxX, maxY, -1, maxBrightness, threshold);
        int rightEdge = FindEdge(grayImage, maxX, maxY, 1, maxBrightness, threshold);

        // Calculate the radius and draw the circle
        int radius = (rightEdge - leftEdge) / 2;
        int centerX = leftEdge + radius;
        int centerY = maxY;

        Graphics g = Graphics.FromImage(image);
        Pen pen = new Pen(Color.Red, 3);
        g.DrawEllipse(pen, centerX - radius, centerY - radius, 2 * radius, 2 * radius);
        pictureBoxImage.Image = image;
    }

    private int FindEdge(Bitmap grayImage, int startX, int startY, int direction, int maxBrightness, int threshold)
    {
        int x = startX;

        // Move in the specified direction until a dimmer spot is found
        while (x >= 0 && x < grayImage.Width)
        {
            int brightness = grayImage.GetPixel(x, startY).R;
            if (brightness < maxBrightness - threshold)
            {
                break;
            }
            x += direction;
        }

        // Move backward in the opposite direction to find the edge
        x -= direction;
        while (x >= 0 && x < grayImage.Width)
        {
            int brightness = grayImage.GetPixel(x, startY).R;
            if (brightness < maxBrightness - threshold)
            {
                return x + direction;
            }
            x -= direction;
        }

        // If no edge is found, return the start position
        return startX;
    }

    private Bitmap GrayscaleBitmap(Bitmap source)
    {
        Bitmap grayscale = new Bitmap(source.Width, source.Height);
        for (int x = 0; x < source.Width; x++)
        {
            for (int y = 0; y < source.Height; y++)
            {
                Color c = source.GetPixel(x, y);
                int lum = (int)(0.3 * c.R + 0.59 * c.G + 0.11 * c.B);
                grayscale.SetPixel(x, y, Color.FromArgb(lum, lum, lum));
            }
        }
        return grayscale;
    }
}