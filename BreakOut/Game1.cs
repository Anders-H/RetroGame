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
            BatTexture = new Texture2D(GraphicsDevice, 10, 30, false, SurfaceFormat.Color);
            BallTexture = new Texture2D(GraphicsDevice, 4, 4, false, SurfaceFormat.Color);
            SpriteFont = Content.Load<SpriteFont>("TheFont");
            CurrentScene = new IntroScene(CurrentScene);
            base.LoadContent();
        }
    }
}
