using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroGameClasses;
using RetroGameClasses.RetroTextures;
using RetroDisplayMode = RetroGameClasses.RetroDisplayMode;

namespace BreakOut
{
    public class Game1 : RetroGame
    {
        public const bool FullScreen =
#if DEBUG
            false;
#else
            true;
#endif
        public static RetroTexture BatTexture { get; set; }
        public static RetroTexture BallTexture { get; set; }
        public static SpriteFont SpriteFont { get; set; }
        public Game1() : base(320, 200, RetroDisplayMode.FullscreenWithUpscalingAndBorder)
        {
        }
        protected override void LoadContent()
        {
            BatTexture = RetroTexture.ScaffoldSimpleTexture(GraphicsDevice, 10, 30, Color.White);
            BallTexture = RetroTexture.ScaffoldSimpleTexture(GraphicsDevice, 4, 4, Color.White);
            SpriteFont = Content.Load<SpriteFont>("TheFont");
            CurrentScene = new IntroScene(this);
            base.LoadContent();
        }
    }
}
