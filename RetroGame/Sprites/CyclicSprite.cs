using Microsoft.Xna.Framework.Graphics;
using RetroGame.RetroTextures;

namespace RetroGame.Sprites;

public class CyclicSprite : Sprite
{
    private long Tick { get; set; }
    private int[] Cells { get; }
    private int CurrentCellPointer { get; set; }
    public RetroTexture CurrentTexture { get; }
    public int AnimationTickDelay { get; set; }
        
    public int CurrentCell =>
        Cells[CurrentCellPointer];
        
    public CyclicSprite(RetroTexture texture, int delay, params int[] cells)
    {
        CurrentCellPointer = 0;
        Tick = 0;
        CurrentTexture = texture;
        AnimationTickDelay = delay;
        Cells = cells;
    }
        
    public void Act()
    {
        Tick++;

        if (Tick < AnimationTickDelay)
            return;

        Tick = 0;
        CurrentCellPointer++;

        if (CurrentCellPointer >= Cells.Length)
            CurrentCellPointer = 0;
    }
        
    public void Draw(SpriteBatch spriteBatch) =>
        Draw(spriteBatch, CurrentTexture, CurrentCell);
}