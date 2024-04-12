

public class CueBallDetector
{

    //creates a new cueball from the image or try and find and existing one that has moved
    public Ball FindCueBall(Ball prevBall, Image inputImage, int threshold = 100)
    {
        Bitmap image = new Bitmap(inputImage);
        Bitmap grayImage = GrayscaleBitmap(image);
        int maxX = 0, maxY = 0;
        int maxBrightness = 0;

        // Find the brightest pixel (assuming it's the cue ball)
        //if we know past position of cue ball, search in a limited area (5 radiuses either side of the prev center)
        if (prevBall != null)
        {
            for (int x = (int)(prevBall.centre.X - Math.Abs((int)(prevBall.radius * 7))); x <= prevBall.centre.X + Math.Abs((int)(prevBall.radius * 7)); x++)
            {
                //check for boundaries of the image
                if ((x+1) > inputImage.Width) continue;
                if (x < 0) continue;

                for (int y = (int)(prevBall.centre.Y - Math.Abs((int)(prevBall.radius * 7))); y <= prevBall.centre.Y + Math.Abs((int)(prevBall.radius * 7)); y++)
                {
                    //check for boundaries of the image
                    if ((y+1) > inputImage.Height) continue;
                    if (y < 0) continue;

                    //find brightest pixel
                    int brightness = grayImage.GetPixel(x, y).R;
                    if (brightness > maxBrightness)
                    {
                        maxBrightness = brightness;
                        maxX = x;
                        maxY = y;
                    }
                }
            }
        }
        else
        {
            //else look the whole image for it
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
        }

        // Find the top, bottom, left, and right edges of the ball
        int leftEdge = FindEdge(grayImage, maxX, maxY, -1, 0, maxBrightness, threshold);
        int rightEdge = FindEdge(grayImage, maxX, maxY, 1, 0, maxBrightness, threshold);
        int topEdge = FindEdge(grayImage, maxX, maxY, 0, -1, maxBrightness, threshold);
        int bottomEdge = FindEdge(grayImage, maxX, maxY, 0, 1, maxBrightness, threshold);

        // Calculate the horizontal and vertical radii as float values
        float horizontalRadius = (rightEdge - leftEdge) / 2f;
        float verticalRadius = (bottomEdge - topEdge) / 2f;

        // Calculate the average radius as a float value
        float radius = (horizontalRadius + verticalRadius) / 2f;

        //never let radius be too small, otherwise it breaks
        if (Math.Abs(radius) < 1f) {
            radius = -1f;
        }

        // Calculate the center coordinates of the circle as float values
        float centerX = leftEdge + horizontalRadius;
        float centerY = topEdge + verticalRadius;

        //Create cue ball
        return new Ball(new PointF(centerX, centerY), radius);
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