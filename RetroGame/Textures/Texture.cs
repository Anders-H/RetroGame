using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RetroGame.Textures
{
    public class Texture
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
    }
}
