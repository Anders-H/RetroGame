using Microsoft.Xna.Framework;
using RetroGameClasses;
using RetroGameClasses.RetroTextures;
using RetroDisplayMode = RetroGameClasses.RetroDisplayMode;

namespace BreakOut
{
    public class Game1 : RetroGame
    {
        public static RetroTexture BatTexture { get; set; }
        
        public static RetroTexture BallTexture { get; set; }
        
        public Game1() : base(320, 200, RetroDisplayMode.Windowed)
        {
        }
        
        protected override void LoadContent()
        {
            BatTexture = RetroTexture.ScaffoldSimpleTexture(GraphicsDevice, 10, 30, Color.White);
            BallTexture = RetroTexture.ScaffoldSimpleTexture(GraphicsDevice, 4, 4, Color.White);
            CurrentScene = new IntroScene(this);
            base.LoadContent();
        }
    }
}