using Microsoft.Xna.Framework;
using RetroGame;
using RetroGame.RetroTextures;

namespace BatchDemo;

public class Game1 : RetroGame.RetroGame
{
    public static RetroTexture Star { get; set; }

    public Game1() : base(320, 200, RetroDisplayMode.Fullscreen)
    {
    }
        
    protected override void LoadContent()
    {
        Star = RetroTexture.ScaffoldSimpleTexture(GraphicsDevice, 1, 1, Color.White);
        CurrentScene = new IntroScene(this);
        BackColor = Color.Black;
        base.LoadContent();
    }
}