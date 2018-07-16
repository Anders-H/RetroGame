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
        internal static RetroTexture TilesTexture { get; set; }
        public Game1() : base(320, 200, RetroDisplayMode.WindowedWithUpscaling)
        {
        }
        protected override void LoadContent()
        {
            TilesTexture = new RetroTexture(GraphicsDevice, 32, 32, 10);
            TilesTexture.SetData(Content.Load<Texture2D>("test_tiles"));
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
            Tiles = new Tilemap(Game1.TilesTexture, 10, 5, 32, 32, 5, 5) {Delay = 10};
            Tiles.SetValue(0, 0, 0);
            Tiles.SetValue(1, 0, 0);
            Tiles.SetValue(2, 0, 0);
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
