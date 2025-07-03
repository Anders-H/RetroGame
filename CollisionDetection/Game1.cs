using System;
using System.Runtime.Versioning;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGame;
using RetroGame.Input;
using RetroGame.RetroTextures;
using RetroGame.Scene;
using RetroGame.Sprites;

namespace CollisionDetection;

public class Game1 : RetroGame.RetroGame
{
    public static CollisionTexture Texture;
    public static CollisionSpriteWithMovement Sprite1;
    public static CollisionSpriteWithMovement Sprite2;
    public static CollisionSpriteWithMovement Sprite3;
        
    public Game1() : base(128, 128, RetroDisplayMode.Fullscreen)
    {
    }

    [SupportedOSPlatform("windows")]
    protected override void LoadContent()
    {
        if (GraphicsDevice == null)
            throw new SystemException();

        BackColor = ColorPaletteHelper.GetColor(ColorPalette.Blue);

        Texture = CollisionTexture.ScaffoldTextureCells(
            GraphicsDevice,
            10,
            10,
            1,
            1,
            ColorPaletteHelper.GetColor(ColorPalette.Transparent)
        );

        Texture.PlotCell(Texture,
            0,
            ColorPalette.Transparent,
            "11........" +
            "11........" +
            "11........" +
            "11........" +
            "11........" +
            "11........" +
            "11........" +
            "11........" +
            "1111111111" +
            "1111111111"
        );

        Texture.AddCollision(0, 0, 0, 2, 8);
        Texture.AddCollision(0, 0, 9, 10, 2);
            
        Sprite1 = new CollisionSpriteWithMovement(Texture, 0);
        Sprite2 = new CollisionSpriteWithMovement(Texture, 0);
        Sprite3 = new CollisionSpriteWithMovement(Texture, 0);

        CurrentScene = new CollisionScene(this);
        base.LoadContent();
    }
}

public class CollisionSpriteWithMovement : CollisionSprite, IRetroActor
{
    public int SpeedX { get; set; }
    public int SpeedY { get; set; }
    public ulong Mod { get; set; }
        
    public CollisionSpriteWithMovement(CollisionTexture texture, params int[] cells) : base(texture, cells)
    {
    }

    public void Act(ulong ticks)
    {
        if (ticks % Mod != 0)
            return;

        X += SpeedX;
        Y += SpeedY;
            
        if (X > 117 || X < 0)
            SpeedX = -SpeedX;

        if (Y > 117 || Y < 0)
            SpeedY = -SpeedY;
    }
}

[SupportedOSPlatform("windows")]
public class CollisionScene : Scene
{
    private KeyboardStateChecker Keyboard { get; } = new();
        
    public CollisionScene(RetroGame.RetroGame parent) : base(parent)
    {
        Game1.Sprite1.X = 60;
        Game1.Sprite1.Y = 60;
        Game1.Sprite2.X = 64;
        Game1.Sprite2.Y = 56;
        Game1.Sprite3.X = 56;
        Game1.Sprite3.Y = 64;
        Game1.Sprite2.SpeedX = 1;
        Game1.Sprite3.SpeedY = 1;
        Game1.Sprite1.Mod = 100;
        Game1.Sprite2.Mod = 10;
        Game1.Sprite3.Mod = 12;
        AddToAutoUpdate(Keyboard, Game1.Sprite1, Game1.Sprite2, Game1.Sprite3);
    }

    public override void Update(GameTime gameTime, ulong ticks)
    {
        if (Keyboard.IsKeyPressed(Keys.Escape))
            Exit();

        base.Update(gameTime, ticks);
    }
        
    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        var sprite1Color = Game1.Sprite1.Intersects(Game1.Sprite2) || Game1.Sprite1.Intersects(Game1.Sprite3)
            ? ColorPalette.White
            : ColorPalette.LightBlue;

        var sprite2Color = Game1.Sprite2.Intersects(Game1.Sprite1) || Game1.Sprite2.Intersects(Game1.Sprite3)
            ? ColorPalette.White
            : ColorPalette.LightBlue;

        var sprite3Color = Game1.Sprite3.Intersects(Game1.Sprite1) || Game1.Sprite3.Intersects(Game1.Sprite2)
            ? ColorPalette.White
            : ColorPalette.LightBlue;

        Game1.Sprite1.Draw(spriteBatch, Game1.Texture, 0, sprite1Color);
        Game1.Sprite2.Draw(spriteBatch, Game1.Texture, 0, sprite2Color);
        Game1.Sprite3.Draw(spriteBatch, Game1.Texture, 0, sprite3Color);
    }
}