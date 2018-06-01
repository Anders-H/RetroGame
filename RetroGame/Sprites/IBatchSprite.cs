using RetroGameClasses.Scene;

namespace RetroGameClasses.Sprites
{
	public interface IBatchSprite : ISceneActor
	{
		bool IsAlive { get; }
	}
}
