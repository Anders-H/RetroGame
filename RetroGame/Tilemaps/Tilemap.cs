﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroGameClasses.RetroTextures;
using RetroGameClasses.Scene;

namespace RetroGameClasses.Tilemaps
{
    public class Tilemap : ISceneActor
    {
        private int?[,] _tiles;
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
            if (gridSize.X <= 0 || gridSize.Y <= 0)
                throw new ArgumentOutOfRangeException(nameof(gridSize));
            CurrentTexture = texture;
            GridSize = gridSize;
            TileSize = tileSize;
            DisplaySize = displaySize;
            _tiles = new int?[GridSize.X, GridSize.Y];
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
            for (var row = 0; row < DisplaySize.Y; row++)
            {
                for (var col = 0; col < DisplaySize.X; col++)
                {
                    var tile = GetValue(col + TileOffsetX, row + TileOffsetY);
                    if (tile == null)
                        continue;
                    var targetPos = new Vector2(x + TileSize.X * col, y + TileSize.Y * row);
                    var sourcePos = new Rectangle(tile.Value * TileSize.X, 0, TileSize.X, TileSize.Y);
                    spriteBatch.Draw(CurrentTexture, targetPos, sourcePos, Color.White);
                }
            }
        }
        public void SetValue(int x, int y, int? value)
        {
            if (x < 0 || x >= GridSize.X)
                throw new ArgumentOutOfRangeException(nameof(x));
            if (y < 0 || y >= GridSize.X)
                throw new ArgumentOutOfRangeException(nameof(y));
            _tiles[x, y] = value;
        }
        public int GetExistingValue(int x, int y)
        {
            if (x < 0 || x >= GridSize.X)
                throw new ArgumentOutOfRangeException(nameof(x));
            if (y < 0 || y >= GridSize.X)
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
            if (y < 0 || y >= GridSize.X)
                return null;
            return _tiles[x, y];
        }
        public bool HasValue(int x, int y)
        {
            if (x < 0 || x >= GridSize.X)
                return false;
            if (y < 0 || y >= GridSize.X)
                return false;
            return _tiles[x, y].HasValue;
        }
    }
}