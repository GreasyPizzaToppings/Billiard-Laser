using OpenCvSharp;

namespace billiard_laser
{
    public enum VideoResolution
    {
        P200,
        P270,
        P300,
        P360,
        P480,
        P720,
        P1080
    }

    public static class ResolutionHelper
    {
        private static readonly Dictionary<VideoResolution, OpenCvSharp.Size> resolutions = new()
        {
            { VideoResolution.P200, new OpenCvSharp.Size(355, 200) },
            { VideoResolution.P270, new OpenCvSharp.Size(480, 270) },
            { VideoResolution.P300, new OpenCvSharp.Size(534, 300) },
            { VideoResolution.P360, new OpenCvSharp.Size(640, 360) },
            { VideoResolution.P480, new OpenCvSharp.Size(854, 480) },
            { VideoResolution.P720, new OpenCvSharp.Size(1280, 720) },
            { VideoResolution.P1080, new OpenCvSharp.Size(1920, 1080) }
        };

        public static OpenCvSharp.Size GetSize(VideoResolution resolution) => resolutions[resolution];
    }
} 