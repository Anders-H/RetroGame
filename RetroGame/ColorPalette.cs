using System;
using Microsoft.Xna.Framework;

namespace RetroGame;

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
    public static Color GetColor(ColorPalette color) =>
        color switch
        {
            ColorPalette.Transparent => Color.FromNonPremultiplied(0, 0, 0, 0),
            ColorPalette.Black => Color.FromNonPremultiplied(0, 0, 0, 255),
            ColorPalette.White => Color.FromNonPremultiplied(255, 255, 255, 255),
            ColorPalette.Red => Color.FromNonPremultiplied(136, 0, 0, 255),
            ColorPalette.Cyan => Color.FromNonPremultiplied(170, 255, 238, 255),
            ColorPalette.Purple => Color.FromNonPremultiplied(204, 68, 204, 255),
            ColorPalette.Green => Color.FromNonPremultiplied(0, 204, 85, 255),
            ColorPalette.Blue => Color.FromNonPremultiplied(0, 0, 170, 255),
            ColorPalette.Yellow => Color.FromNonPremultiplied(238, 238, 119, 255),
            ColorPalette.Orange => Color.FromNonPremultiplied(221, 136, 85, 255),
            ColorPalette.Brown => Color.FromNonPremultiplied(102, 68, 0, 255),
            ColorPalette.LightRed => Color.FromNonPremultiplied(255, 119, 119, 255),
            ColorPalette.DarkGrey => Color.FromNonPremultiplied(51, 51, 51, 255),
            ColorPalette.Grey => Color.FromNonPremultiplied(119, 119, 119, 255),
            ColorPalette.LightGreen => Color.FromNonPremultiplied(170, 255, 102, 255),
            ColorPalette.LightBlue => Color.FromNonPremultiplied(0, 136, 255, 255),
            ColorPalette.LightGrey => Color.FromNonPremultiplied(187, 187, 187, 255),
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };
}