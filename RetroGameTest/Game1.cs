using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGame;
using RetroGame.Input;

namespace RetroGameTest
{
    public class Game1 : RetroGame.RetroGame
    {
        public static Texture2D HelloTexture { get; set; }

        public const bool FullScreen =
#if DEBUG
            false;
#else
            true;
#endif
        public Game1() : base(800, 600, FullScreen)
        {
        }
        protected override void LoadContent()
        {
            HelloTexture = Content.Load<Texture2D>("hello");
            CurrentScene = new Scene();
            base.LoadContent();
        }
    }

    public class Scene : IScene
    {
        private int X { get; set; } = 200;
        private int Y { get; set; } = 200;
        private KeyboardStateChecker Keyboard { get; } = new KeyboardStateChecker();
        public void Update(GameTime gameTime)
        {
            Keyboard.UpdateState();
            if (Keyboard.IsKeyDown(Keys.Left))
                X--;
            else if (Keyboard.IsKeyDown(Keys.Right))
                X++;
            if (Keyboard.IsKeyDown(Keys.Up))
                Y--;
            else if (Keyboard.IsKeyDown(Keys.Down))
                Y++;
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.HelloTexture, new Vector2(X, Y), Color.White);
        }
    }
}
