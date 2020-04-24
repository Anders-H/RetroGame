using System;
using Microsoft.Xna.Framework;

namespace RetroGameClasses.RetroTextures
{
    public class Bitmap
    {
        private readonly Color[,] _pixels;

        public Bitmap(Point size, ColorPalette color) : this(size.X, size.Y, ColorPaletteHelper.GetColor(color))
        {
        }

        public Bitmap(int width, int height, ColorPalette color) 
            : this(width, height, ColorPaletteHelper.GetColor(color))
        {
        }

        public Bitmap(Point size, Color color) : this(size.X, size.Y, color)
        {
        }
        
        public Bitmap(int width, int height, Color color)
        {
            _pixels = new Color[width, height];
            var c = color;
            for (var y = 0; y < Height; y++)
                for (var x = 0; x < Width; x++)
                    _pixels[x, y] = c;
        }
        
        public int Width =>
            _pixels.GetLength(0);
        
        public int Height =>
            _pixels.GetLength(1);
        
        public Color GetPixel(int x, int y) =>
            _pixels[x, y];
        
        public void SetPixel(int x, int y, Color color) =>
            _pixels[x, y] = color;
        
        public void SetPixels(string pixelData)
        {
            var index = 0;
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    switch (pixelData[index])
                    {
                        case ' ':
                        case '.':
                            break;
                        case '1':
                            SetPixel(x, y, ColorPaletteHelper.GetColor(ColorPalette.Black));
                            break;
                        case '2':
                            SetPixel(x, y, ColorPaletteHelper.GetColor(ColorPalette.White));
                            break;
                        case '3':
                            SetPixel(x, y, ColorPaletteHelper.GetColor(ColorPalette.Red));
                            break;
                        case '4':
                            SetPixel(x, y, ColorPaletteHelper.GetColor(ColorPalette.Cyan));
                            break;
                        case '5':
                            SetPixel(x, y, ColorPaletteHelper.GetColor(ColorPalette.Purple));
                            break;
                        case '6':
                            SetPixel(x, y, ColorPaletteHelper.GetColor(ColorPalette.Green));
                            break;
                        case '7':
                            SetPixel(x, y, ColorPaletteHelper.GetColor(ColorPalette.Yellow));
                            break;
                        case '8':
                            SetPixel(x, y, ColorPaletteHelper.GetColor(ColorPalette.Orange));
                            break;
                        case '9':
                            SetPixel(x, y, ColorPaletteHelper.GetColor(ColorPalette.Brown));
                            break;
                        case 'a':
                        case 'A':
                            SetPixel(x, y, ColorPaletteHelper.GetColor(ColorPalette.LightRed));
                            break;
                        case 'b':
                        case 'B':
                            SetPixel(x, y, ColorPaletteHelper.GetColor(ColorPalette.DarkGrey));
                            break;
                        case 'c':
                        case 'C':
                            SetPixel(x, y, ColorPaletteHelper.GetColor(ColorPalette.Grey));
                            break;
                        case 'd':
                        case 'D':
                            SetPixel(x, y, ColorPaletteHelper.GetColor(ColorPalette.LightGreen));
                            break;
                        case 'e':
                        case 'E':
                            SetPixel(x, y, ColorPaletteHelper.GetColor(ColorPalette.LightBlue));
                            break;
                        case 'f':
                        case 'F':
                            SetPixel(x, y, ColorPaletteHelper.GetColor(ColorPalette.LightGrey));
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    index++;
                }
            }
        }
    }
}