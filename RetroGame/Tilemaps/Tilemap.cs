using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroGameClasses.RetroTextures;
using RetroGameClasses.Scene;

namespace RetroGameClasses.Tilemaps
{
    public class Tilemap : ISceneActor
    {
        public RetroTexture CurrentTexture { get; }
        public Point GridSize { get; }
        public Point TileSize { get; }
        public Point DisplaySize { get; }
        public int PixelOffsetX { get; set; }
        public int PixelOffsetY { get; set; }
        public int TileOffsetX { get; set; }
        public int TileOffsetY { get; set; }
        public int Delay { get; set; }
        public int SpeedX { get; set; } = -1;
        public int SpeedY { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Tilemap(RetroTexture texture, Point gridSize, Point tileSize, Point displaySize)
        {
            CurrentTexture = texture;
            GridSize = gridSize;
            TileSize = tileSize;
            DisplaySize = displaySize;
        }
        public Tilemap(RetroTexture texture, int gridSizeX, int gridSizeY, int tileSizeX, int tileSizeY, int displaySizeX, int displaySizeY) : this(texture, new Point(gridSizeX, gridSizeY), new Point(tileSizeX, tileSizeY), new Point(displaySizeX, displaySizeY)) { }
        public void Act(ulong ticks)
        {
            if (Delay > 0 && ticks%(ulong)Delay != 0)
                return;
            PixelOffsetX += SpeedX;
            if (PixelOffsetX >= TileSize.X)
            {
                PixelOffsetX = 0;
                TileOffsetX += 1;
            }
        }
        public void Draw(SpriteBatch spriteBatch, ulong ticks)
        {
            var x = X + PixelOffsetX;
            var y = Y + PixelOffsetY;

        }
    }
}
