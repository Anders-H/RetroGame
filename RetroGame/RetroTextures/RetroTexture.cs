using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RetroGameClasses.RetroTextures
{
    public class RetroTexture
    {
        public static Texture2D ScaffoldTexture2D(GraphicsDevice graphicsDevice, int width, int height, Color color)
        {
            var size = width*height;
            var pixels = new Color[size];
            for (var i = 0; i < pixels.Length; i++)
                pixels[i] = Color.White;
            var texture = new Texture2D(graphicsDevice, width, height);
            texture.SetData(pixels);
            return texture;
        }

        public static Texture2D ScaffoldTexture2DCells(GraphicsDevice graphicsDevice, int cellWidth, int cellHeight, int cellCount, Color color) =>
            ScaffoldTexture2D(graphicsDevice, cellWidth * cellCount, cellHeight, color);

        public static void PlotCell(Texture2D targetTexture, int cellIndex, Color[,] pixels)
        {
            var width = pixels.GetLength(0);
            var height = pixels.GetLength(1);
            var count = width*height;
            var index = count*cellIndex;
            var targetPixels = new Color[count];
            for (var y = 0; y < height; y++)
                for (var x = 0; x < width; x++)
                {
                    targetPixels[index] = pixels[x, y];
                    index++;
                }
            targetTexture.SetData(0, new Rectangle(cellIndex*width, 0, width, height), targetPixels, 0, 1);
        }
    }
}
