using Microsoft.Xna.Framework.Graphics;
using RetroGameClasses.RetroTextures;

namespace RetroGameClasses.Sprites
{
    public class CollisionSprite : Sprite
    {
        private long Tick { get; set; }
        private int[] Cells { get; set; }
        private int CurrentCellPointer { get; set; }
        public CollisionTexture CurrentTexture { get; private set; }
		
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
        
        //TODO: API for intersection check.
    }
}