using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BreakOut
{
    public class Game1 : RetroGame.RetroGame
    {
        public const bool FullScreen =
#if DEBUG
            false;
#else
            true;
#endif
        public static Texture2D BatTexture { get; set; }
        public static Texture2D BallTexture { get; set; }
        public static SpriteFont SpriteFont { get; set; }
        public Game1() : base(320, 200, FullScreen)
        {
        }
        protected override void LoadContent()
        {
            var batPixels = new Color[10 * 30];
            for (var i = 0; i < batPixels.Length; i++)
                batPixels[i] = Color.White;
            BatTexture = new Texture2D(GraphicsDevice, 10, 30);
            BatTexture.SetData(batPixels);

            var ballPixels = new Color[4 * 4];
            for (var i = 0; i < ballPixels.Length; i++)
                ballPixels[i] = Color.White;
            BallTexture = new Texture2D(GraphicsDevice, 4, 4);
            BallTexture.SetData(ballPixels);

            SpriteFont = Content.Load<SpriteFont>("TheFont");
            CurrentScene = new IntroScene(this);
            base.LoadContent();
        }
    }
}
