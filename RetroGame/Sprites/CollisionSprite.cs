using System;
using Microsoft.Xna.Framework.Graphics;
using RetroGame.RetroTextures;

namespace RetroGame.Sprites;

public class CollisionSprite : Sprite
{
    public CollisionTexture CurrentTexture { get; }
        
    public int CurrentCell { get; }
        
    public CollisionSprite(CollisionTexture texture, params int[] cells)
    {
        ArgumentNullException.ThrowIfNull(texture);
        ArgumentNullException.ThrowIfNull(cells);

        if (cells.Length == 0)
            throw new ArgumentException("At least one cell is required.", nameof(cells));

        foreach (var cell in cells)
            if (cell < 0 || cell >= texture.CellCount)
                throw new ArgumentOutOfRangeException(nameof(cells));

        CurrentTexture = texture;
        CurrentCell = cells[0];
    }
        
    public void Draw(SpriteBatch spriteBatch) =>
        Draw(spriteBatch, CurrentTexture, CurrentCell);

    public bool Intersects(CollisionSprite sprite) =>
        CurrentTexture.Intersects(
            CurrentCell,
            Point,
            sprite.CurrentTexture.CollisionZones[sprite.CurrentCell],
            sprite.Point
        );
}