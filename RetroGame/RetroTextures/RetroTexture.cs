using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RetroGameClasses.RetroTextures
{
    public class RetroTexture : Texture2D
    {
        public RetroTexture(GraphicsDevice graphicsDevice, int width, int height) : base(graphicsDevice, width, height) { }
        public static RetroTexture ScaffoldSimpleTexture(GraphicsDevice graphicsDevice, int width, int height, Color color)
        {
            var size = width*height;
            var pixels = new Color[size];
            for (var i = 0; i < pixels.Length; i++)
                pixels[i] = color;
            var texture = new RetroTexture(graphicsDevice, width, height);
            texture.SetData(pixels);
            return texture;
        }
        public static RetroTexture ScaffoldTexture2DCells(GraphicsDevice graphicsDevice, int cellWidth, int cellHeight, int cellCount, Color color) =>
            ScaffoldSimpleTexture(graphicsDevice, cellWidth * cellCount, cellHeight, color);
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
        public void Draw(SpriteBatch spriteBatch, int frameIndex, int x, int y)
            => spriteBatch.Draw(this, new Vector2(x, y), new Rectangle(frameIndex*2, 0, 2, 2), Color.White);
    }
}
