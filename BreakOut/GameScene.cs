using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGame.Input;
using RetroGame.Scene;
using RetroGame.Sprites;
using System.Runtime.Versioning;

namespace BreakOut;

[SupportedOSPlatform("windows")]
public class GameScene : Scene
{
    private KeyboardStateChecker Keyboard { get; } = new();
    private Bat Bat { get; } = new();
    private Ball Ball { get; } = new();

    public GameScene(RetroGame.RetroGame retroGame) : base(retroGame)
    {
        AddToAutoUpdate(Keyboard, Game1.TypeWriter);
        AddToAutoDraw(Bat, Ball, Game1.TypeWriter);
        Game1.TypeWriter.SetText("apples and pears", "apes and snakes");
        Game1.TypeWriter.SetText("staffan och bengt", "klas och charles");
        Game1.TypeWriter.SetText("again apples and pears", "again apes and snakes");
    }

    public override void Update(GameTime gameTime, ulong ticks)
    {
        Bat.Move(Keyboard);
        Ball.Move(Bat, 0, 0, 316, 196);

        //Death condition.
        if (Ball.X <= 4)
            Parent.CurrentScene = new IntroScene(Parent);

        //Quit.
        if (Keyboard.IsKeyPressed(Keys.Escape))
            Exit();

        base.Update(gameTime, ticks);
    }
}

[SupportedOSPlatform("windows")]
public class Bat : Sprite, IRetroDrawable
{
    public Bat()
    {
        X = 0;
        Y = 50;
        Width = 10;
        Height = 30;
    }

    public void Draw(SpriteBatch spriteBatch, ulong ticks) =>
        Draw(spriteBatch, Game1.BatTexture, 0);

    public void Move(KeyboardStateChecker keyboard)
    {
        if (keyboard.IsKeyDown(Keys.Up))
        {
            Y -= 2;

            if (Y < 0)
                Y = 0;
        }
        else if (keyboard.IsKeyDown(Keys.Down))
        {
            Y += 2;

            if (Y > 170)
                Y = 170;
        }
    }
}

[SupportedOSPlatform("windows")]
public class Ball : Sprite, IRetroDrawable
{
    public int SpeedX { get; set; }
    public int SpeedY { get; set; }

    public Ball()
    {
        X = 20;
        Y = 60;
        SpeedX = 2;
        SpeedY = 1;
        Width = 4;
        Height = 4;
    }

    public void Draw(SpriteBatch spriteBatch, ulong ticks) =>
        Draw(spriteBatch, Game1.BallTexture, 0);

    public void Move(Bat bat, int left, int top, int right, int bottom)
    {
        var nextX = X + SpeedX;
        var nextY = Y + SpeedY;

        //Check intersection.
        var nextPosition = new Rectangle((int)nextX, (int)nextY, Width, Height);
        var currentBatPosition = new Rectangle(bat.IntX, bat.IntY, bat.Width, bat.Height);

        if (nextPosition.Intersects(currentBatPosition))
        {
            var locationOnBat = nextY - bat.Y;

            if (locationOnBat <= 6)
            {
                SpeedX = 1;
                SpeedY = -2;
            }
            else if (locationOnBat <= 12)
            {
                SpeedX = 2;
                SpeedY = -1;
            }
            else if (locationOnBat <= 18)
            {
                SpeedX = 3;
                SpeedY = 0;
            }
            else if (locationOnBat <= 24)
            {
                SpeedX = 2;
                SpeedY = 1;
            }
            else
            {
                SpeedX = 1;
                SpeedY = 2;
            }

            Game1.SoundEffect.Play();
        }

        //Collide with walls.
        if (nextX < left || nextX > right)
        {
            SpeedX = -SpeedX;
            nextX = X + SpeedX;
            Game1.SoundEffect.Play();
        }

        if (nextY < top || nextY > bottom)
        {
            SpeedY = -SpeedY;
            nextY = Y + SpeedY;
            Game1.SoundEffect.Play();
        }

        X = nextX;
        Y = nextY;
    }
}