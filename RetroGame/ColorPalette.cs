using System;
using Microsoft.Xna.Framework;

namespace RetroGameClasses
{
    public enum ColorPalette
    {
        Transparent = -1,
        Black = 0,
        White = 1,
        Red = 2,
        Cyan = 3,
        Purple = 4,
        Green = 5,
        Blue = 6,
        Yellow = 7,
        Orange = 8,
        Brown = 9,
        LightRed = 10,
        DarkGrey = 11,
        Grey = 12,
        LightGreen = 13,
        LightBlue = 14,
        LightGrey = 15
    }

    public static class ColorPaletteHelper
    {
        public static Color GetColor(ColorPalette color)
        {
            switch (color)
            {
                case ColorPalette.Transparent:
                    return Color.FromNonPremultiplied(0, 0, 0, 0);
                case ColorPalette.Black:
                    return Color.FromNonPremultiplied(0, 0, 0, 255);
                case ColorPalette.White:
                    return Color.FromNonPremultiplied(255, 255, 255, 255);
                case ColorPalette.Red:
                    return Color.FromNonPremultiplied(136, 0, 0, 255);
                case ColorPalette.Cyan:
                    return Color.FromNonPremultiplied(170, 255, 238, 255);
                case ColorPalette.Purple:
                    return Color.FromNonPremultiplied(204, 68, 204, 255);
                case ColorPalette.Green:
                    return Color.FromNonPremultiplied(0, 204, 85, 255);
                case ColorPalette.Blue:
                    return Color.FromNonPremultiplied(0, 0, 170, 255);
                case ColorPalette.Yellow:
                    return Color.FromNonPremultiplied(238, 238, 119, 255);
                case ColorPalette.Orange:
                    return Color.FromNonPremultiplied(221, 136, 85, 255);
                case ColorPalette.Brown:
                    return Color.FromNonPremultiplied(102, 68, 0, 255);
                case ColorPalette.LightRed:
                    return Color.FromNonPremultiplied(255, 119, 119, 255);
                case ColorPalette.DarkGrey:
                    return Color.FromNonPremultiplied(51, 51, 51, 255);
                case ColorPalette.Grey:
                    return Color.FromNonPremultiplied(119, 119, 119, 255);
                case ColorPalette.LightGreen:
                    return Color.FromNonPremultiplied(170, 255, 102, 255);
                case ColorPalette.LightBlue:
                    return Color.FromNonPremultiplied(0, 136, 255, 255);
                case ColorPalette.LightGrey:
                    return Color.FromNonPremultiplied(187, 187, 187, 255);
                default:
                    throw new ArgumentOutOfRangeException(nameof(color), color, null);
            }
        }
    }
}
