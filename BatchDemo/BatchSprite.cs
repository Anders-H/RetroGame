using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroGame;
using RetroGame.Sprites;

namespace BatchDemo;

public class BatchSprite : Sprite, IBatchSprite
{
    private static Random Rnd { get; } = new();
    public float SpeedX { get; }
    private Color Color { get; }
        
    public bool IsAlive =>
        true;
        
    public BatchSprite()
    {
        Y = Rnd.Next(0, 200);
        SpeedX = (float)(Rnd.NextDouble() * 4);

        if (SpeedX > 3)
            Color = ColorPaletteHelper.GetColor(ColorPalette.White);
        else if (SpeedX > 2)
            Color = ColorPaletteHelper.GetColor(ColorPalette.LightGrey);
        else if (SpeedX > 1)
            Color = ColorPaletteHelper.GetColor(ColorPalette.Grey);
        else
            Color = ColorPaletteHelper.GetColor(ColorPalette.DarkGrey);
    }
        
    public void Act(ulong ticks)
    {
        X -= SpeedX;
        
        if (X < 0)
        {
            X = 320;
            Y = Rnd.Next(0, 200);
        }
    }
        
    public void Draw(SpriteBatch spriteBatch, ulong ticks) =>
        Draw(spriteBatch, Game1.Star, 0, Color);
}