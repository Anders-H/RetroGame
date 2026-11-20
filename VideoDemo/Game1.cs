#nullable enable
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using RetroGame;
using RetroGame.Scene;

namespace VideoDemo;

public class Game1 : RetroGame.RetroGame
{
    public static VideoPlayer Player { get; }
    public static Video? Video { get; set; }

    static Game1()
    {
        Player = new VideoPlayer();
        Player.IsLooped = false;
    }

    public Game1() : base(640, 400, RetroDisplayMode.Windowed)
    {
    }

    protected override void LoadContent()
    {
        Video = Content.Load<Video>("havet-logo");
        CurrentScene = new VideoScene(this);
    }
}

public class VideoScene : Scene
{
    public VideoScene(RetroGame.RetroGame parent) : base(parent)
    {
    }

    public override void Update(GameTime gameTime, ulong ticks)
    {
        if (Game1.Player.State == MediaState.Stopped)
        {
            Exit();
        }
    }

    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        //G.Clear(Color.Black);

        if (Game1.Player.State != MediaState.Stopped)
        {
            var videoTexture = Game1.Player.GetTexture();

            spriteBatch.Begin();
            spriteBatch.Draw(
                videoTexture,
                new Rectangle(0, 0,
                    640,
                    400),
                Color.White);
            spriteBatch.End();
        }
        base.Draw(gameTime, ticks, spriteBatch);
    }
}