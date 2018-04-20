using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RetroGame
{
    public abstract class Scene
    {
        protected RetroGame Parent { get; }
        protected Scene(RetroGame parent)
        {
            Parent = parent;
        }
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public void Exit() => Parent.Exit();
    }
}
