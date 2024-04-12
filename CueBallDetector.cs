

using Emgu.CV.Structure;
using static System.Windows.Forms.AxHost;

public class CueBallDetector
{

    public Ball FindCueBall(Ball prevBall, Image inputImage, int threshold = 100)
    {
        Bitmap image = new Bitmap(inputImage);
        Bitmap grayImage = GrayscaleBitmap(image);
        int maxX = 0, maxY = 0;
        int maxBrightness = 0;

        // Find the brightest pixel (assuming it's the cue ball)
        // if we know past position of cue ball, search in a limited area (5 radiuses either side of the prev center)
        if (prevBall != null)
        {
            int startX;
            int endX;
            int startY;
            int endY;

            /// find search areas based on movement

            //ball moving right
            if (prevBall.deltaX > 0)
            {
                startX = (int)(prevBall.centre.X - (prevBall.radius * 3));
                endX = (int)(prevBall.centre.X + prevBall.radius * 10);
            }

            //ball moving left
            else if (prevBall.deltaX < 0)
            {
                startX = (int)(prevBall.centre.X - prevBall.radius * 10);
                endX = (int)(prevBall.centre.X + (prevBall.radius * 3));
            }


            //ball not moving left or right
            else { 
                startX = (int)(prevBall.centre.X - (prevBall.radius * 5));
                endX = (int)(prevBall.centre.X + (prevBall.radius * 5));
            }


            //ball moving down
            if (prevBall.deltaY > 0)
            {
                startY = (int)(prevBall.centre.Y - (prevBall.radius * 3));
                endY = (int)(prevBall.centre.Y + prevBall.radius * 10);
            }

            //ball moving up
            else if (prevBall.deltaY < 0)
            {
                startY = (int)(prevBall.centre.Y - prevBall.radius * 10);
                endY = (int)(prevBall.centre.Y + (prevBall.radius * 3));
            }

            //ball not moving up or down
            else {
                startY = (int)(prevBall.centre.Y - prevBall.radius * 5);
                endY = (int)(prevBall.centre.Y + prevBall.radius * 5);
            }


            //search for the moving ball with our set range
            for (int x = startX; x <= endX; x++)
            {
                //check for boundaries of the image
                if ((x + 1) > inputImage.Width) continue;
                if (x < 0) continue;


                for (int y = startY; y <= endY; y++)
                {
                    //check for boundaries of the image
                    if ((y + 1) > inputImage.Height) continue;
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

        //no existing cue ball. look whole image for it 
        else
        {
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
        float radius = Math.Abs((horizontalRadius + verticalRadius) / 2f);

        //never let radius be too small, otherwise it breaks
        if (radius < 1f) {
            radius = 1f;
        }

        // Calculate the center coordinates of the circle as float values
        float centerX = leftEdge + horizontalRadius;
        float centerY = topEdge + verticalRadius;

        //Create cue ball
        Ball cueBall = new Ball(new PointF(centerX, centerY), radius);

        //calculate distance travelled from last ball
        if (prevBall != null) {
            float deltaX = centerX - prevBall.centre.X;
            float delayY = centerY - prevBall.centre.Y;
            cueBall.deltaX = deltaX;
            cueBall.deltaY = delayY;
        }

        return cueBall;
    }


    // the same as other but returns more data

    public object[] FindCueBallDebug(Ball prevBall, Image inputImage, int threshold = 100)
    {
        Bitmap image = new Bitmap(inputImage);
        Bitmap grayImage = GrayscaleBitmap(image);
        int maxX = 0, maxY = 0;
        int maxBrightness = 0;

        int startX = 0;
        int endX = 0;
        int startY = 0;
        int endY = 0;

        
        // Find the brightest pixel (assuming it's the cue ball)
        // if we know past position of cue ball, search in a limited area (5 radiuses either side of the prev center)
        if (prevBall != null)
        {
            /// find search areas based on movement

            //ball moving right
            if (prevBall.deltaX > 0)
            {
                startX = (int)(prevBall.centre.X - (prevBall.radius * 3));
                endX = (int)(prevBall.centre.X + prevBall.radius * 10);
            }

            //ball moving left
            else if (prevBall.deltaX < 0)
            {
                startX = (int)(prevBall.centre.X - prevBall.radius * 10);
                endX = (int)(prevBall.centre.X + (prevBall.radius * 3));
            }


            //ball not moving left or right
            else
            {
                startX = (int)(prevBall.centre.X - (prevBall.radius * 5));
                endX = (int)(prevBall.centre.X + (prevBall.radius * 5));
            }


            //ball moving down
            if (prevBall.deltaY > 0)
            {
                startY = (int)(prevBall.centre.Y - (prevBall.radius * 3));
                endY = (int)(prevBall.centre.Y + prevBall.radius * 10);
            }

            //ball moving up
            else if (prevBall.deltaY < 0)
            {
                startY = (int)(prevBall.centre.Y - prevBall.radius * 10);
                endY = (int)(prevBall.centre.Y + (prevBall.radius * 3));
            }

            //ball not moving up or down
            else
            {
                startY = (int)(prevBall.centre.Y - prevBall.radius * 5);
                endY = (int)(prevBall.centre.Y + prevBall.radius * 5);
            }


            //search for the moving ball with our set range
            for (int x = startX; x <= endX; x++)
            {
                //check for boundaries of the image
                if ((x + 1) > inputImage.Width) continue;
                if (x < 0) continue;


                for (int y = startY; y <= endY; y++)
                {
                    //check for boundaries of the image
                    if ((y + 1) > inputImage.Height) continue;
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

        //no existing cue ball. look whole image for it 
        else
        {
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
        float radius = Math.Abs((horizontalRadius + verticalRadius) / 2f);

        //never let radius be too small, otherwise it breaks
        if (radius < 1f)
        {
            radius = 1f;
        }

        // Calculate the center coordinates of the circle as float values
        float centerX = leftEdge + horizontalRadius;
        float centerY = topEdge + verticalRadius;

        //Create cue ball
        Ball cueBall = new Ball(new PointF(centerX, centerY), radius);

        //calculate distance travelled from last ball
        if (prevBall != null)
        {
            float deltaX = centerX - prevBall.centre.X;
            float delayY = centerY - prevBall.centre.Y;
            cueBall.deltaX = deltaX;
            cueBall.deltaY = delayY;
        }


        //DEBUG stuff

        //we want to return
        //1: what it thinks is the cue ball
        //2: the middle point/ the brighest spot
        //3: the circle that it used to search for the brightest spot

        PointF middle = new PointF(maxX, maxY);
        Point[] searchPoints = new Point[] {
            new Point(startX, startY),
            new Point(endX, startY),
            new Point(endX, endY),
            new Point(startX, endY)
        };

        object[] objects = new object[3];
        objects[0] = cueBall;
        objects[1] = middle;
        objects[2] = searchPoints;

        return objects;
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