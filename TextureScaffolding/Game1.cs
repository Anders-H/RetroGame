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

namespace TextureScaffolding;

[SupportedOSPlatform("windows")]
public class Game1 : RetroGame.RetroGame
{
    public static RetroTexture Texture;
        
    public Game1() : base(128, 128, RetroDisplayMode.Windowed)
    {
    }
        
    protected override void LoadContent()
    {
        if (GraphicsDevice == null)
            throw new SystemException();

        BackColor = ColorPaletteHelper.GetColor(ColorPalette.Blue);
            
        Texture = RetroTexture.ScaffoldTextureCells(
            GraphicsDevice,
            2,
            2,
            4,
            ColorPaletteHelper.GetColor(ColorPalette.Transparent)
        );

        var cell = new Bitmap(2, 2, ColorPalette.Transparent);
        cell.SetPixels(
            "2 " +
            "  ");
        RetroTexture.PlotCell(Texture, 0, cell);

        cell = new Bitmap(2, 2, ColorPalette.Transparent);
        cell.SetPixels(
            " 2" +
            "  ");
        RetroTexture.PlotCell(Texture, 1, cell);

        cell = new Bitmap(2, 2, ColorPalette.Transparent);
        cell.SetPixels(
            "  " +
            " 2");
        RetroTexture.PlotCell(Texture, 2, cell);

        cell = new Bitmap(2, 2, ColorPalette.Transparent);
        cell.SetPixels(
            "  " +
            "2 ");
        RetroTexture.PlotCell(Texture, 3, cell);

        CurrentScene = new AnimationTextureScene(this);
        base.LoadContent();
    }
}

[SupportedOSPlatform("windows")]
public class AnimationTextureScene : Scene
{
    private int _dly;
    private int _frame;
    private int _x;
    private int _xSpeed = 1;

    private KeyboardStateChecker Keyboard { get; } = new();
        
    public AnimationTextureScene(RetroGame.RetroGame retroGame) : base(retroGame)
    {
        AddToAutoUpdate(Keyboard);
    }
        
    public override void Update(GameTime gameTime, ulong ticks)
    {
        //Quit.
        if (Keyboard.IsKeyPressed(Keys.Escape))
            Exit();
        else if (Keyboard.IsKeyPressed(Keys.Enter))
            Parent.CurrentScene = new CyclicSpriteScene(Parent);

        //Logic.
        _dly++;
        if (_dly > 3)
            _dly = 0;
        else
            return;

        if (_x >= 126)
            _xSpeed = -1;
        else if (_x < 0)
            _xSpeed = 1;

        _x += _xSpeed;
        _frame++;

        if (_frame > 3)
            _frame = 0;

        base.Update(gameTime, ticks);
    }
        
    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch) =>
        Game1.Texture.Draw(spriteBatch, _frame, _x, 20);
}

[SupportedOSPlatform("windows")]
public class CyclicSpriteScene : Scene
{
    private float _xSpeed = 0.3f;
    private CyclicSprite CyclicSprite { get; }
    private KeyboardStateChecker Keyboard { get; } = new();
        
    public CyclicSpriteScene(RetroGame.RetroGame retroGame) : base(retroGame)
    {
        CyclicSprite = new CyclicSprite(Game1.Texture, 5, 0, 1, 2, 3)
        {
            Y = 20
        };

        AddToAutoUpdate(Keyboard);
    }
        
    public override void Update(GameTime gameTime, ulong ticks)
    {
        //Quit.
        if (Keyboard.IsKeyPressed(Keys.Escape))
            Exit();
        else if (Keyboard.IsKeyPressed(Keys.Enter))
            Parent.CurrentScene = new AnimationTextureScene(Parent);

        //Logic.
        if (CyclicSprite.X >= 126)
            _xSpeed = -0.3f;
        else if (CyclicSprite.X < 0)
            _xSpeed = 0.3f;

        CyclicSprite.X += _xSpeed;
        CyclicSprite.Act();
        base.Update(gameTime, ticks);
    }
        
    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        CyclicSprite.Draw(spriteBatch);
    }
}