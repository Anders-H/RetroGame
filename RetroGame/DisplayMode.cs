using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroGame
{
    public enum DisplayMode
    {
        Windowed, WindowedWithUpscaling, FullscreenWithUpscaling, FullscreenWithUpscalingAndBorder
    }

    internal static class DisplayModeHelper
    {
        internal static bool Upscaling(DisplayMode displayMode) =>
            displayMode == DisplayMode.FullscreenWithUpscaling ||
            displayMode == DisplayMode.FullscreenWithUpscalingAndBorder ||
            displayMode == DisplayMode.WindowedWithUpscaling;
        internal static bool Border(DisplayMode displayMode) =>
            displayMode == DisplayMode.FullscreenWithUpscalingAndBorder;
        internal static bool Fullscreen(DisplayMode displayMode) =>
            displayMode == DisplayMode.FullscreenWithUpscaling ||
            displayMode == DisplayMode.FullscreenWithUpscalingAndBorder;
    }
}
