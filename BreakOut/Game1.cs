using System;
using System.Runtime.Versioning;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using RetroGame;
using RetroGame.RetroTextures;

namespace BreakOut;

[SupportedOSPlatform("windows")]
public class Game1 : RetroGame.RetroGame
{
    public static RetroTexture BatTexture { get; set; }
    public static RetroTexture BallTexture { get; set; }
    public static SoundEffect SoundEffect { get; set; }

    public Game1() : base(320, 200, RetroDisplayMode.Fullscreen, true)
    {
    }

    protected override void LoadContent()
    {
        if (GraphicsDevice == null)
            throw new SystemException();

        SoundEffect = Content.Load<SoundEffect>("sound");
        BatTexture = RetroTexture.ScaffoldSimpleTexture(GraphicsDevice, 10, 30, Color.White);
        BallTexture = RetroTexture.ScaffoldSimpleTexture(GraphicsDevice, 4, 4, Color.White);
        CurrentScene = new IntroScene(this);
        base.LoadContent();
    }
}