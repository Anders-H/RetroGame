using System;

namespace TilemapEditor
{
    public class EditableTilemap
    {
        private readonly int?[,] _tiles;
        public EditableTilemap(int gridSizeX, int gridSizeY)
        {
            GridSizeX = gridSizeX;
            GridSizeY = gridSizeY;
            _tiles = new int?[gridSizeX, gridSizeY];
        }
        public int GridSizeX { get; }
        public int GridSizeY { get; }
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
