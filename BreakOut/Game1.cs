﻿using Microsoft.Xna.Framework;
using RetroGame;
using RetroGame.RetroTextures;

namespace BreakOut;

public class Game1 : RetroGame.RetroGame
{
    public static RetroTexture BatTexture { get; set; }
        
    public static RetroTexture BallTexture { get; set; }
        
    public Game1() : base(320, 200, RetroDisplayMode.Fullscreen)
    {
    }
        
    protected override void LoadContent()
    {
        BatTexture = RetroTexture.ScaffoldSimpleTexture(GraphicsDevice, 10, 30, Color.White);
        BallTexture = RetroTexture.ScaffoldSimpleTexture(GraphicsDevice, 4, 4, Color.White);
        CurrentScene = new IntroScene(this);
        base.LoadContent();
    }
}