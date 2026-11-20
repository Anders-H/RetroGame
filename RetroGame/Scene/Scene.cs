using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RetroGame.Scene;

public abstract class Scene
{
    private readonly List<IRetroActor> _autoUpdate = [];
    private readonly List<IRetroDrawable> _autoDraw = [];
    internal ulong Ticks { get; set; }
    protected RetroGame Parent { get; }
        
    protected Scene(RetroGame parent)
    {
        Parent = parent;
        Ticks = 0;
    }
        
    public virtual void Update(GameTime gameTime, ulong ticks)
    {
        foreach (var actor in _autoUpdate)
            actor.Act(ticks);
    }
        
    public virtual void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        foreach (var drawable in _autoDraw)
            drawable.Draw(spriteBatch, ticks);
    }
        
    internal void Advance(GameTime gameTime)
    {
        Update(gameTime, Ticks);
        Ticks++;
    }

    public void Exit() =>
        Parent.Exit();
        
    public void ResetTicks() =>
        Ticks = 0;
        
    public void AddToAutoUpdate(params IRetroActor[] actors) =>
        _autoUpdate.AddRange(actors);
        
    public void AddToAutoDraw(params IRetroDrawable[] actors) =>
        _autoDraw.AddRange(actors);

    public virtual void BeginScene()
    {
    }
}
