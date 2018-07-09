using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGameClasses;
using RetroGameClasses.Input;
using RetroGameClasses.Scene;

namespace TilesDemo
{
    public class Game1 : RetroGame
    {
        public Game1() : base(320, 200, RetroDisplayMode.WindowedWithUpscaling)
        {
        }
        protected override void LoadContent()
        {
            BorderColor = ColorPaletteHelper.GetColor(ColorPalette.LightGreen);
            BackColor = ColorPaletteHelper.GetColor(ColorPalette.DarkGrey);
            CurrentScene = new TextureDemoScene(this);
        }
    }

    public class TextureDemoScene: Scene
    {
        private KeyboardStateChecker Keyboard { get; } = new KeyboardStateChecker();
        public TextureDemoScene(RetroGame parent) : base(parent)
        {
            AddToAutoUpdate(Keyboard);
        }
        public override void Update(GameTime gameTime, ulong ticks)
        {
            //Quit.
            if (Keyboard.IsKeyPressed(Keys.Escape))
                Exit();
            base.Update(gameTime, ticks);
        }
        public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
        {

        }
    }
}
