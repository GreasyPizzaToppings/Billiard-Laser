
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

        // Find the top, bottom, left, and right edges of the ball
        int leftEdge = FindEdge(grayImage, maxX, maxY, -1, 0, maxBrightness, threshold);
        int rightEdge = FindEdge(grayImage, maxX, maxY, 1, 0, maxBrightness, threshold);
        int topEdge = FindEdge(grayImage, maxX, maxY, 0, -1, maxBrightness, threshold);
        int bottomEdge = FindEdge(grayImage, maxX, maxY, 0, 1, maxBrightness, threshold);

        // Calculate the radius and center coordinates of the circle
        int radius = Math.Max(rightEdge - leftEdge, bottomEdge - topEdge) / 2;
        int centerX = leftEdge + radius;
        int centerY = topEdge + radius;

        // Print the center coordinates to the console
        Console.WriteLine($"Center coordinates: ({centerX}, {centerY})");

        Graphics g = Graphics.FromImage(image);
        Pen pen = new Pen(Color.Red, 3);
        g.DrawEllipse(pen, centerX - radius, centerY - radius, 2 * radius, 2 * radius);

        // Draw a dot in the middle of the cue ball area
        Brush brush = new SolidBrush(Color.Blue);
        g.FillEllipse(brush, centerX - 2, centerY - 2, 4, 4);

        pictureBoxImage.Image = image;
    }

    private int FindEdge(Bitmap grayImage, int startX, int startY, int directionX, int directionY, int maxBrightness, int threshold)
    {
        int x = startX;
        int y = startY;

        // Move in the specified direction until a dimmer spot is found
        while (x >= 0 && x < grayImage.Width && y >= 0 && y < grayImage.Height)
        {
            int brightness = grayImage.GetPixel(x, y).R;
            if (brightness < maxBrightness - threshold)
            {
                break;
            }
            x += directionX;
            y += directionY;
        }

        // Move backward in the opposite direction to find the edge
        x -= directionX;
        y -= directionY;
        while (x >= 0 && x < grayImage.Width && y >= 0 && y < grayImage.Height)
        {
            int brightness = grayImage.GetPixel(x, y).R;
            if (brightness < maxBrightness - threshold)
            {
                return directionX == 0 ? y + directionY : x + directionX;
            }
            x -= directionX;
            y -= directionY;
        }

        // If no edge is found, return the start position
        return directionX == 0 ? startY : startX;
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