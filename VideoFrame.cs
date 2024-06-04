public class VideoFrame
{

    public Bitmap frame { get; set; }
    public int index { get; set; }


    public VideoFrame(Bitmap frame, int index)
    {
        this.frame = frame;
        this.index = index;
    }


    public override string ToString()
    {
        return index.ToString();
    }
}