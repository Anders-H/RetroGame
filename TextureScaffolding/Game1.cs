using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGameClasses;
using RetroGameClasses.Input;
using RetroGameClasses.RetroTextures;
using RetroDisplayMode = RetroGameClasses.RetroDisplayMode;


namespace TextureScaffolding
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : RetroGame
    {
        public static RetroTexture Texture;
        public Game1() : base(128, 128, RetroDisplayMode.WindowedWithUpscaling)
        {
        }
        protected override void LoadContent()
        {
            BackColor = ColorPaletteHelper.GetColor(ColorPalette.Blue);
            Texture = RetroTexture.ScaffoldTexture2DCells(GraphicsDevice, 2, 2, 4, ColorPaletteHelper.GetColor(ColorPalette.Transparent));

            var cell = new Bitmap(2, 2, ColorPalette.Transparent);
            cell.SetPixels(
                "2 " +
                "  ");
            RetroTexture.PlotCell(Texture, 0, cell);

            cell = new Bitmap(2, 2, ColorPalette.Transparent);
            cell.SetPixels(
                " 2" +
                "  ");
            RetroTexture.PlotCell(Texture, 1, cell);

            cell = new Bitmap(2, 2, ColorPalette.Transparent);
            cell.SetPixels(
                "  " +
                " 2");
            RetroTexture.PlotCell(Texture, 2, cell);

            cell = new Bitmap(2, 2, ColorPalette.Transparent);
            cell.SetPixels(
                "  " +
                "2 ");
            RetroTexture.PlotCell(Texture, 3, cell);

            CurrentScene = new DemoScene(this);
            base.LoadContent();
        }
    }

    public class DemoScene : Scene
    {
        private int _dly;
        private int _frame;
        private int _x;
        private int _xspeed = 1;
        private KeyboardStateChecker Keyboard { get; } = new KeyboardStateChecker();
        public DemoScene(RetroGame retroGame) : base(retroGame) { }
        public override void Update(GameTime gameTime)
        {
            Keyboard.UpdateState();
            //Quit.
            if (Keyboard.IsKeyPressed(Keys.Escape))
                Exit();
            //Logic.
            _dly++;
            if (_dly > 3)
                _dly = 0;
            else
                return;
            if (_x >= 126)
                _xspeed = -1;
            else if (_x < 0)
                _xspeed = 1;
            _x += _xspeed;
            _frame++;
            if (_frame > 3)
                _frame = 0;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) =>
            Game1.Texture.Draw(spriteBatch, _frame, _x, 20);
    }
}
