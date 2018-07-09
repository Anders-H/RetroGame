using Microsoft.Xna.Framework;
using RetroGameClasses.RetroTextures;
using RetroGameClasses.Scene;

namespace RetroGameClasses.Tilemaps
{
    public class Tilemap : IRetroActor
    {
        public RetroTexture CurrentTexture { get; }
        public Point GridSize { get; }
        public Point TileSize { get; }
        public Point DisplaySize { get; }
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
        }
    }
}
