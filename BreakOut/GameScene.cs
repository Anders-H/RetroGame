using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroGame;

namespace BreakOut
{
    public class GameScene : IScene
    {
        private IScene ScenePointer { get; set; }
        public GameScene(IScene scenePointer)
        {
        }
        public void Update(GameTime gameTime)
        {
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
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
        public Bat()
        {
            X = 0;
            Y = 50;
        }
        public override void Draw(SpriteBatch s) => s.Draw(Game1.BatTexture, new Vector2(X, Y), Color.White);
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
        }
        public override void Draw(SpriteBatch s) => s.Draw(Game1.BallTexture, new Vector2(X, Y), Color.White);
    }
}
