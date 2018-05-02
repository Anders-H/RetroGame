using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroGameClasses.RetroTextures;

namespace RetroGameClasses.Sprites
{
    public abstract class Sprite
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Vector2 Location => new Vector2(X, Y);
        public int Width { get; set; }
        public int Height { get; set; }
        public Point Size => new Point(Width, Height);
        public Rectangle FullRectangle => new Rectangle(X, Y, Width, Height);
        public void Draw(SpriteBatch spriteBatch, RetroTexture texture, int cellIndex) =>
            texture.Draw(spriteBatch, cellIndex, X, Y);
        public void Draw(SpriteBatch spriteBatch, RetroTexture texture, int cellIndex, Color color) =>
            texture.Draw(spriteBatch, cellIndex, X, Y, color);
    }
}
