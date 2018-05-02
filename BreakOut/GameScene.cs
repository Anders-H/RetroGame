using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGameClasses;
using RetroGameClasses.Input;
using RetroGameClasses.Sprites;

namespace BreakOut
{
    public class GameScene : Scene
    {
        private KeyboardStateChecker Keyboard { get; } = new KeyboardStateChecker();
        private Bat Bat { get; } = new Bat();
        private Ball Ball { get; } = new Ball();
        public GameScene(RetroGame retroGame) : base(retroGame) { }
        public override void Update(GameTime gameTime)
        {
            Keyboard.UpdateState();
            Bat.Move(Keyboard);
            Ball.Move(Bat, 0, 0, 316, 196);
            //Death condition.
            if (Ball.X <= 4)
                Parent.CurrentScene = new IntroScene(Parent);
            //Quit.
            if (Keyboard.IsKeyPressed(Keys.Escape))
                Exit();
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Bat.Draw(spriteBatch, Game1.BatTexture, 0);
            Ball.Draw(spriteBatch, Game1.BallTexture, 0);
        }
    }

    public class Bat : Sprite
    {
        public Bat()
        {
            X = 0;
            Y = 50;
            Width = 10;
            Height = 30;
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
    }

    public class Ball : Sprite
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

        public void Move(Bat bat, int left, int top, int right, int bottom)
        {
            var nextX = X + SpeedX;
            var nextY = Y + SpeedY;
            //Check intersection.
            var nextPosition = new Rectangle(nextX, nextY, Width, Height);
            var currentBatPosition = new Rectangle(bat.X, bat.Y, bat.Width, bat.Height);
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
    }
}
