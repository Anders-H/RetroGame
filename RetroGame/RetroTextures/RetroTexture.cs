﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RetroGameClasses.RetroTextures
{
    public class RetroTexture : Texture2D
    {
        public int CellWidth { get; }
        public int CellHeight => Height;
        public int CellCount { get; }
        public RetroTexture(GraphicsDevice graphicsDevice, int cellWidth, int cellHeight, int cellCount) : base(graphicsDevice, cellWidth*cellCount, cellHeight)
        {
            CellWidth = cellWidth;
            CellCount = cellCount;
        }

        public static RetroTexture ScaffoldSimpleTexture(GraphicsDevice graphicsDevice, int width, int height, Color color) =>
            ScaffoldTextureCells(graphicsDevice, width, height, 1, color);
        public static RetroTexture ScaffoldTextureCells(GraphicsDevice graphicsDevice, int cellWidth, int cellHeight, int cellCount, Color color)
        {
            var size = cellWidth*cellCount*cellHeight;
            var pixels = new Color[size];
            for (var i = 0; i < pixels.Length; i++)
                pixels[i] = color;
            var texture = new RetroTexture(graphicsDevice, cellWidth, cellHeight, cellCount);
            texture.SetData(pixels);
            return texture;
        }
        public static void PlotCell(RetroTexture targetTexture, int cellIndex, Bitmap bitmap)
        {
            var targetWidth = targetTexture.Width;
            var targetStart = bitmap.Width*cellIndex;
            var index = 0;
            var targetPixels = new Color[targetTexture.Width*targetTexture.Height];
            targetTexture.GetData(targetPixels);
            for (var y = 0; y < bitmap.Height; y++)
                for (var x = 0; x < targetWidth; x++)
                {
                    if (x >= targetStart && x < targetStart + bitmap.Width)
                        if (bitmap.GetPixel(x - targetStart, y).A > 0)
                            targetPixels[index] = bitmap.GetPixel(x - targetStart, y);
                    index++;
                }
            targetTexture.SetData(targetPixels);
        }
        public void Draw(SpriteBatch spriteBatch, int cellIndex, int x, int y)
            => spriteBatch.Draw(this, new Vector2(x, y), new Rectangle(cellIndex*CellWidth, 0, CellWidth, CellHeight), Color.White);
        public void Draw(SpriteBatch spriteBatch, int cellIndex, int x, int y, Color color)
            => spriteBatch.Draw(this, new Vector2(x, y), new Rectangle(cellIndex* CellWidth, 0, CellWidth, CellHeight), color);
    }
}