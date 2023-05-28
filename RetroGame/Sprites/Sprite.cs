using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroGame.RetroTextures;

namespace RetroGame.Sprites;

public abstract class Sprite
{
    public float X { get; set; }
    public float Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
        
    public int IntX =>
        (int)Math.Round(X);
        
    public int IntY =>
        (int)Math.Round(Y);
        
    public Vector2 Location =>
        new Vector2(X, Y);

    public Point Point =>
        new Point((int)X, (int)Y);
        
    public Point Size =>
        new Point(Width, Height);
        
    public Rectangle FullRectangle =>
        new Rectangle(IntX, IntY, Width, Height);
        
    public void Draw(SpriteBatch spriteBatch, RetroTexture texture, int cellIndex) =>
        texture.Draw(spriteBatch, cellIndex, IntX, IntY);
        
    public void Draw(SpriteBatch spriteBatch, RetroTexture texture, int cellIndex, Color color) =>
        texture.Draw(spriteBatch, cellIndex, IntX, IntY, color);
        
    public void Draw(SpriteBatch spriteBatch, RetroTexture texture, int cellIndex, ColorPalette color) =>
        texture.Draw(spriteBatch, cellIndex, IntX, IntY, color);
}