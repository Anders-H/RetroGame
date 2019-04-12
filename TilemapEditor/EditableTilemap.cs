using System;

namespace TilemapEditor
{
    public class EditableTilemap
    {
        private readonly int?[,] _tiles;
        public int GridSizeX { get; private set; }
        public int GridSizeY { get; private set; }

        public EditableTilemap(int gridSizeX, int gridSizeY)
        {
            GridSizeX = gridSizeX;
            GridSizeY = gridSizeY;
            _tiles = new int?[gridSizeX, gridSizeY];
        }

        public void ResizeGrid(int width, int height)
        {
            if (width < 1 || width > 5000)
                throw new ArgumentException(nameof(width));
            if (height <1 || height > 5000)
                throw new ArgumentException(nameof(height));
            var tiles = new int?[width, height];
            var w = width > GridSizeX ? width : GridSizeX;
            var h = height > GridSizeY ? height : GridSizeY;
            for (var y = 0; y < h; y++)
                for (var x = 0; x < w; x++)
                    if (x < w && x < GridSizeX && y < h && y < GridSizeY)
                        tiles[x, y] = _tiles[x, y];
            GridSizeX = width;
            GridSizeY = height;
        }

        public void SetValue(int x, int y, int? value)
        {
            if (x < 0 || x >= GridSizeX)
                throw new ArgumentOutOfRangeException(nameof(x));
            if (y < 0 || y >= GridSizeY)
                throw new ArgumentOutOfRangeException(nameof(y));
            _tiles[x, y] = value;
        }

        public int GetExistingValue(int x, int y)
        {
            if (x < 0 || x >= GridSizeX)
                throw new ArgumentOutOfRangeException(nameof(x));
            if (y < 0 || y >= GridSizeY)
                throw new ArgumentOutOfRangeException(nameof(y));
            var ret = _tiles[x, y];
            if (ret == null)
                throw new SystemException("Value not set.");
            return ret.Value;
        }

        public int? GetValue(int x, int y)
        {
            if (x < 0 || x >= GridSizeX)
                return null;
            if (y < 0 || y >= GridSizeY)
                return null;
            return _tiles[x, y];
        }

        public bool HasValue(int x, int y)
        {
            if (x < 0 || x >= GridSizeX)
                return false;
            if (y < 0 || y >= GridSizeY)
                return false;
            return _tiles[x, y].HasValue;
        }
    }
}