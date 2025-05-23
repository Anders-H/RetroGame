﻿using System.Runtime.Versioning;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using RetroGame;
using RetroGame.RetroTextures;
using RetroGame.Scene;
using RetroGame.Text;

namespace BreakOut;

[SupportedOSPlatform("windows")]
public class Game1 : RetroGame.RetroGame
{
    public static RetroTexture Flip { get; set; }
    public static RetroTexture BatTexture { get; set; }
    public static RetroTexture BallTexture { get; set; }
    public static SoundEffect SoundEffect { get; set; }
    public static TextBlockStaticVerticalCenter TextBlock { get; set; }

    public Game1() : base(320, 200, RetroDisplayMode.Fullscreen, true)
    {
    }

    protected override void LoadContent()
    {
        CurrentScene = new ContentLoader(this);
        base.LoadContent();
    }
}

[SupportedOSPlatform("windows")]
public class ContentLoader : ContentLoaderScene
{
    public ContentLoader(RetroGame.RetroGame parent) : base(parent)
    {
    }

    public override void LoadOne()
    {
        switch (ResourceNumber)
        {
            case 0:
                Game1.SoundEffect = Parent.Content.Load<SoundEffect>("sound");
                break;
            case 1:
                Game1.BatTexture = RetroTexture.ScaffoldSimpleTexture(Parent.GraphicsDevice, 10, 30, Color.White);
                break;
            case 2:
                Game1.BallTexture = RetroTexture.ScaffoldSimpleTexture(Parent.GraphicsDevice, 4, 4, Color.White);
                break;
            case 3:
                Game1.TextBlock = new TextBlockStaticVerticalCenter(320, 10, "Hello", "Good bye");
                break;
            case 4:
                Game1.Flip = RetroTexture.LoadContent(Parent.GraphicsDevice, Parent.Content, 100, 100, 1, "flip");
                break;
            default:
                LoadingComplete = true;
                Parent.CurrentScene = new IntroScene(Parent);
                break;
        }
    }
}