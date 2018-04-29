using Microsoft.Xna.Framework;
using RetroGameClasses;
using RetroGameClasses.RetroTextures;
using RetroDisplayMode = RetroGameClasses.RetroDisplayMode;


namespace TextureScaffolding
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : RetroGame
    {
        public Game1() : base(128, 128, RetroDisplayMode.Windowed)
        {
        }

        protected override void LoadContent()
        {
            var t = RetroTexture.ScaffoldTexture2DCells(GraphicsDevice, 4, 4, 4, ColorPaletteHelper.GetColor(ColorPalette.Black));

            var cell = new Color[2, 2];
            cell[0, 0] = ColorPaletteHelper.GetColor(ColorPalette.White);
            RetroTexture.PlotCell(t, 0, cell);

        }
    }
}
