namespace RetroGameClasses
{
    public enum RetroDisplayMode
    {
        Windowed,
        WindowedWithUpscaling,
        FullscreenWithUpscaling,
        FullscreenWithUpscalingAndBorder
    }

    internal static class DisplayModeHelper
    {
        internal static bool Upscaling(RetroDisplayMode displayMode) =>
            displayMode == RetroDisplayMode.FullscreenWithUpscaling ||
            displayMode == RetroDisplayMode.FullscreenWithUpscalingAndBorder ||
            displayMode == RetroDisplayMode.WindowedWithUpscaling;
        internal static bool Border(RetroDisplayMode displayMode) =>
            displayMode == RetroDisplayMode.FullscreenWithUpscalingAndBorder;
        internal static bool Fullscreen(RetroDisplayMode displayMode) =>
            displayMode == RetroDisplayMode.FullscreenWithUpscaling ||
            displayMode == RetroDisplayMode.FullscreenWithUpscalingAndBorder;
    }
}
