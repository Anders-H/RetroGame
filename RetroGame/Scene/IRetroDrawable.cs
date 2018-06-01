using Microsoft.Xna.Framework.Graphics;

namespace RetroGameClasses.Scene
{
	public interface IRetroDrawable
	{
		void Draw(SpriteBatch spriteBatch, ulong ticks);
	}
}
