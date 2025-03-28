﻿using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGame;
using RetroGame.Input;
using RetroGame.RetroTextures;
using RetroGame.Scene;
using RetroGame.Sprites;

namespace MouseDemo;

[SupportedOSPlatform("windows")]
public class Game1 : RetroGame.RetroGame
{
    public static RetroTexture PointerTexture { get; set; }
    public static RetroTexture MouseStampTexture { get; set; }

    public Game1() : base(320, 200, RetroDisplayMode.Windowed)
    {
    }
    
    protected override void LoadContent()
    {
        PointerTexture = RetroTexture.ScaffoldSimpleTexture(GraphicsDevice, 5, 5, Color.White);
        MouseStampTexture = RetroTexture.ScaffoldSimpleTexture(GraphicsDevice, 25, 25, Color.White);
        CurrentScene = new MyScene(this);
        base.LoadContent();
    }
}

public class MousePointer : Sprite
{
    public MousePointer()
    {
        Width = 5;
        Height = 5;
    }
}

[SupportedOSPlatform("windows")]
public class Stamp : Sprite
{
    public Stamp()
    {
        Width = 25;
        Height = 25;
    }

    public bool Intersects(Point p) =>
        new Rectangle(IntX, IntY, Width, Height).Intersects(new Rectangle(p, new Point(1, 1)));
}

[SupportedOSPlatform("windows")]
public class MyScene : Scene
{
    private KeyboardStateChecker Keyboard { get; } = new();
    private MousePointer MousePointer { get; } = new();
    private List<Stamp> MouseStamps { get; } = [];
    private MouseStateChecker Mouse { get; } = new();

    public MyScene(RetroGame.RetroGame parent) : base(parent)
    {
        AddToAutoUpdate(Keyboard, Mouse);
    }

    public override void Update(GameTime gameTime, ulong ticks)
    {
        MousePointer.X = Mouse.Location.X - 2;
        MousePointer.Y = Mouse.Location.Y - 2;

        if (Mouse.LeftButtonPressed)
            MouseStamps.Add(new Stamp { X = Mouse.Location.X - 12, Y = Mouse.Location.Y - 12 });
        else if (Mouse.RightButtonPressed)
            MouseStamps.Remove(MouseStamps.FirstOrDefault(x => x.Intersects(Mouse.Location)));

        if (Keyboard.IsKeyPressed(Keys.Escape))
            Exit();
        base.Update(gameTime, ticks);
    }

    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        MouseStamps.ForEach(x => spriteBatch.Draw(Game1.MouseStampTexture, new Vector2(x.X, x.Y), Color.Blue));
        MousePointer.Draw(spriteBatch, Game1.PointerTexture, 0);
    }
}