using Microsoft.Xna.Framework.Graphics;
using RetroGame.RetroTextures;

namespace RetroGame.Sprites;

public class CollisionSprite : Sprite
{
    private long Tick { get; set; }
    private int[] Cells { get; }
    private int CurrentCellPointer { get; }
    public CollisionTexture CurrentTexture { get; }
        
    public int CurrentCell =>
        Cells[CurrentCellPointer];
        
    public CollisionSprite(CollisionTexture texture, params int[] cells)
    {
        CurrentCellPointer = 0;
        Tick = 0;
        CurrentTexture = texture;
        Cells = cells;
    }
        
    public void Draw(SpriteBatch spriteBatch) =>
        Draw(spriteBatch, CurrentTexture, CurrentCell);

    public bool Intersects(CollisionSprite sprite) =>
        CurrentTexture.Intersects(
            CurrentCellPointer,
            Point,
            sprite.CurrentTexture.CollisionZones[sprite.CurrentCellPointer],
            sprite.Point
        );
}