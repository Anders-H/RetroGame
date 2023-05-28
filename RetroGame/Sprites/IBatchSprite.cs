using RetroGame.Scene;

namespace RetroGame.Sprites;

public interface IBatchSprite : ISceneActor
{
    bool IsAlive { get; }
}