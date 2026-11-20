using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RetroGame.Scene;

namespace RetroGame.Sprites;

public class Batch : ISceneActor
{
    private List<IBatchSprite> Sprites { get; } = new List<IBatchSprite>();
        
    public void InsertFirst(IBatchSprite sprite) =>
        Sprites.Insert(0, sprite);
        
    public void AppendLast(IBatchSprite sprite) =>
        Sprites.Add(sprite);
        
    public void RemoveFirst() =>
        Sprites.RemoveAt(0);
        
    public void RemoveLast() =>
        Sprites.RemoveAt(Sprites.Count - 1);
        
    public int Count =>
        Sprites.Count;
        
    public void Act(ulong ticks)
    {
        foreach (var sprite in Sprites)
            if (sprite.IsAlive)
                sprite.Act(ticks);

        // RemoveAll preserves the relative order of surviving sprites.
        Sprites.RemoveAll(static sprite => !sprite.IsAlive);
    }

    public void Draw(SpriteBatch spriteBatch, ulong ticks)
    {
        foreach (var sprite in Sprites)
            if (sprite.IsAlive)
                sprite.Draw(spriteBatch, ticks);
    }
}
