using Microsoft.Xna.Framework.Graphics;

namespace RetroGameClasses.Sprites
{
    public interface IBatchSprite
    {
        bool IsAlive { get; }
        void Act();
        void Draw(SpriteBatch spriteBatch);
    }
}
