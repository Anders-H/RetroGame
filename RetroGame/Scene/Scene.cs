using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RetroGameClasses.Scene
{
	public abstract class Scene
	{
		private readonly List<IRetroActor> _autoUpdate = new List<IRetroActor>();
		private readonly List<IRetroDrawable> _autoDraw = new List<IRetroDrawable>();
		internal ulong Ticks { get; set; }
		protected RetroGame Parent { get; }
		protected Scene(RetroGame parent)
		{
			Parent = parent;
			Ticks = 0;
		}
		public virtual void Update(GameTime gameTime, ulong ticks) =>
			_autoUpdate.ForEach(x => x.Act(ticks));
		public virtual void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch) =>
			_autoDraw.ForEach(x => x.Draw(spriteBatch, ticks));
		public void Exit() => Parent.Exit();
		public void ResetTicks() => Ticks = 0;
		public void AddToAutoUpdate(params IRetroActor[] actors) => _autoUpdate.AddRange(actors);
		public void AddToAutoDraw(params IRetroDrawable[] actors) => _autoDraw.AddRange(actors);
	}
}
