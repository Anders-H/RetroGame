using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGameClasses;
using RetroGameClasses.Input;
using RetroGameClasses.RetroTextures;
using RetroGameClasses.Scene;
using RetroGameClasses.Tilemaps;

namespace TilesDemo
{
    public class Game1 : RetroGame
    {
        internal Texture2D Tiles { get; set; }
        public Game1() : base(320, 200, RetroDisplayMode.WindowedWithUpscaling)
        {
        }
        protected override void LoadContent()
        {
            Tiles = Content.Load<Texture2D>("test_tiles");
            BorderColor = ColorPaletteHelper.GetColor(ColorPalette.LightGreen);
            BackColor = ColorPaletteHelper.GetColor(ColorPalette.DarkGrey);
            CurrentScene = new TextureDemoScene(this);
        }
    }

    public class TextureDemoScene: Scene
    {
        private KeyboardStateChecker Keyboard { get; } = new KeyboardStateChecker();

        private Tilemap Tiles { get; }
        public TextureDemoScene(RetroGame parent) : base(parent)
        {
            var texture = new RetroTexture(parent.GraphicsDevice, 32, 32, 10);
            Tiles = new Tilemap(texture, 10, 5, 32, 32, 5, 5);
            AddToAutoUpdate(Keyboard, Tiles);
            AddToAutoDraw(Tiles);
        }
        public override void Update(GameTime gameTime, ulong ticks)
        {
            //Quit.
            if (Keyboard.IsKeyPressed(Keys.Escape))
                Exit();
            base.Update(gameTime, ticks);
        }
    }
}
