using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DisplayMode = RetroGame.DisplayMode;
using Texture = RetroGame.Textures.Texture;

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
        public Game1() : base(320, 200, DisplayMode.FullscreenWithUpscalingAndBorder)
        {
        }
        protected override void LoadContent()
        {
            BatTexture = Texture.ScaffoldTexture2D(GraphicsDevice, 10, 30, Color.White);
            BallTexture = Texture.ScaffoldTexture2D(GraphicsDevice, 4, 4, Color.White);
            SpriteFont = Content.Load<SpriteFont>("TheFont");
            CurrentScene = new IntroScene(this);
            base.LoadContent();
        }
    }
}
