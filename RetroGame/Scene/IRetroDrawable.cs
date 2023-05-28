using Microsoft.Xna.Framework.Graphics;

namespace RetroGame.Scene;

public interface IRetroDrawable
{
    void Draw(SpriteBatch spriteBatch, ulong ticks);
}