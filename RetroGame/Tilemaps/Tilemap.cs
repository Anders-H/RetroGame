using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroGame.RetroTextures;
using RetroGame.Scene;

namespace RetroGame.Tilemaps;

public class Tilemap : ISceneActor
{
    private readonly int?[,] _tiles;
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
    public bool Repeat { get; set; }

    public Tilemap(RetroTexture texture, Point gridSize, Point tileSize, Point displaySize)
    {
        if (gridSize.X <= 0 || gridSize.Y <= 0)
            throw new ArgumentOutOfRangeException(nameof(gridSize));

        if (tileSize.X <= 0 || tileSize.Y <= 0)
            throw new ArgumentOutOfRangeException(nameof(tileSize));

        CurrentTexture = texture;
        GridSize = gridSize;
        TileSize = tileSize;
        DisplaySize = displaySize;
        _tiles = new int?[GridSize.X, GridSize.Y];
    }

    public Tilemap(RetroTexture texture, int gridSizeX, int gridSizeY, int tileSizeX, int tileSizeY, int displaySizeX, int displaySizeY) : this(texture, new Point(gridSizeX, gridSizeY), new Point(tileSizeX, tileSizeY), new Point(displaySizeX, displaySizeY))
    {
    }

    public void Act(ulong ticks)
    {
        if (Delay > 0 && ticks%(ulong)Delay != 0)
            return;

        (PixelOffsetX, TileOffsetX) = ScrollAxis(PixelOffsetX, TileOffsetX, SpeedX, TileSize.X, GridSize.X, Repeat);
        (PixelOffsetY, TileOffsetY) = ScrollAxis(PixelOffsetY, TileOffsetY, SpeedY, TileSize.Y, GridSize.Y, Repeat);
    }

    internal static (int Pixel, int Tile) ScrollAxis(int pixel, int tile, int speed, int tileSize, int gridSize, bool repeat)
    {
        // Keep offsets in (-tileSize, 0] to fill the leading edge in both directions.
        var offset = (long)pixel + speed;
        var crossed = offset >= 0 ? (offset + tileSize - 1) / tileSize : offset / tileSize;
        var tileOffset = (long)tile - crossed;

        return ((int)(offset - crossed * tileSize), repeat
            ? PositiveModulo(tileOffset, gridSize)
            : checked((int)tileOffset));
    }

    private static int PositiveModulo(long value, int divisor) =>
        (int)((value % divisor + divisor) % divisor);

    public void Draw(SpriteBatch spriteBatch, ulong ticks)
    {
        var x = X + PixelOffsetX;
        var y = Y + PixelOffsetY;

        for (var row = 0; row < DisplaySize.Y; row++)
        {
            for (var col = 0; col < DisplaySize.X; col++)
            {
                var tileX = col + TileOffsetX;
                var tileY = row + TileOffsetY;

                if (Repeat)
                {
                    tileX = PositiveModulo(tileX, GridSize.X);
                    tileY = PositiveModulo(tileY, GridSize.Y);
                }

                var tile = GetValue(tileX, tileY);

                if (tile == null)
                    continue;

                var targetPos = new Vector2(x + TileSize.X * col, y + TileSize.Y * row);
                var sourcePos = CurrentTexture.GetCellBounds(tile.Value);
                spriteBatch.Draw(CurrentTexture, targetPos, sourcePos, Color.White);
            }
        }
    }

    public void SetValue(int x, int y, int? value)
    {
        if (x < 0 || x >= GridSize.X)
            throw new ArgumentOutOfRangeException(nameof(x));

        if (y < 0 || y >= GridSize.Y)
            throw new ArgumentOutOfRangeException(nameof(y));

        _tiles[x, y] = value;
    }

    public int GetExistingValue(int x, int y)
    {
        if (x < 0 || x >= GridSize.X)
            throw new ArgumentOutOfRangeException(nameof(x));

        if (y < 0 || y >= GridSize.Y)
            throw new ArgumentOutOfRangeException(nameof(y));

        var ret = _tiles[x, y];

        if (ret == null)
            throw new SystemException("Value not set.");

        return ret.Value;
    }

    public int? GetValue(int x, int y)
    {
        if (x < 0 || x >= GridSize.X)
            return null;

        if (y < 0 || y >= GridSize.Y)
            return null;

        return _tiles[x, y];
    }

    public bool HasValue(int x, int y)
    {
        if (x < 0 || x >= GridSize.X)
            return false;

        if (y < 0 || y >= GridSize.Y)
            return false;

        return _tiles[x, y].HasValue;
    }
}
