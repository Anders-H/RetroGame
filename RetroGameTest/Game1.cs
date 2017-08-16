using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGame;

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
        private int x = 0;
        public void Update(GameTime gameTime)
        {
            if (x < 790)
                x++;
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.HelloTexture, new Vector2(x, 20), Color.White);
        }
    }
}
