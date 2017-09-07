using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGame.Input;

namespace BreakOut
{
    public class GameScene : RetroGame.Scene
    {
        private KeyboardStateChecker Keyboard { get; } = new KeyboardStateChecker();
        private Bat Bat { get; } = new Bat();
        private Ball Ball { get; } = new Ball();
        public GameScene(RetroGame.RetroGame retroGame) : base(retroGame)
        {
        }
        public override void Update(GameTime gameTime)
        {
            Keyboard.UpdateState();
            Bat.Move(Keyboard);
            Ball.Move(Bat, 0, 0, 316, 196);
            //Death condition.
            if (Ball.X <= 4)
                Parent.CurrentScene = new IntroScene(Parent);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Bat.Draw(spriteBatch);
            Ball.Draw(spriteBatch);
        }
    }

    public abstract class Sprite
    {
        public int X { get; set; }
        public int Y { get; set; }
        public abstract void Draw(SpriteBatch s);
    }

    public class Bat : Sprite
    {
        public const int Width = 10;
        public const int Height = 30;
        public Bat()
        {
            X = 0;
            Y = 50;
        }
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
        public override void Draw(SpriteBatch s) => s.Draw(Game1.BatTexture, new Vector2(X, Y), Color.White);
    }

    public class Ball : Sprite
    {
        public const int Width = 4;
        public const int Height = 4;
        public int SpeedX { get; set; }
        public int SpeedY { get; set; }
        public Ball()
        {
            X = 20;
            Y = 60;
            SpeedX = 2;
            SpeedY = 1;
        }

        public void Move(Bat bat, int left, int top, int right, int bottom)
        {
            var nextX = X + SpeedX;
            var nextY = Y + SpeedY;
            //Check intersection.
            var nextPosition = new Rectangle(nextX, nextY, Width, Height);
            var currentBatPosition = new Rectangle(bat.X, bat.Y, Bat.Width, Bat.Height);
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
            }
            //Collide with walls.
            if (nextX < left || nextX > right)
            {
                SpeedX = -SpeedX;
                nextX = X + SpeedX;
            }
            if (nextY < top || nextY > bottom)
            {
                SpeedY = -SpeedY;
                nextY = Y + SpeedY;
            }
            X = nextX;
            Y = nextY;
        }
        public override void Draw(SpriteBatch s) => s.Draw(Game1.BallTexture, new Vector2(X, Y), Color.White);
    }
}
