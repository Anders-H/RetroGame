using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RetroGame.Scene;

public abstract class ContentLoaderScene : Scene
{
    public int ResourceNumber { get; private set; }
    public bool LoadingComplete { get; set; }
    
    protected ContentLoaderScene(RetroGame parent) : base(parent)
    {
        ResourceNumber = 0;
        LoadingComplete = false;
    }

    public abstract void LoadOne();

    public override void Update(GameTime gameTime, ulong ticks)
    {
        if (!LoadingComplete)
            LoadOne();

        ResourceNumber++;
        base.Update(gameTime, ticks);
    }

    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        if (RetroGame.Floppy == null)
            return;

        spriteBatch.Draw(RetroGame.Floppy, new Vector2(20, 20), Color.White);
        base.Draw(gameTime, ticks, spriteBatch);
    }
}