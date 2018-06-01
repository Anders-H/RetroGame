using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using RetroGameClasses.Scene;

namespace RetroGameClasses.Sprites
{
	public class Batch : ISceneActor
	{
		private List<IBatchSprite> Sprites { get; } = new List<IBatchSprite>();
		public void InsertFirst(IBatchSprite sprite) => Sprites.Insert(0, sprite);
		public void AppendLast(IBatchSprite sprite) => Sprites.Add(sprite);
		public void RemoveFirst() => Sprites.RemoveAt(0);
		public void RemoveLast() => Sprites.RemoveAt(Sprites.Count - 1);
		public int Count => Sprites.Count;
		public void Act(ulong ticks)
		{
			IBatchSprite deadSprite = null;
			foreach (var batchSprite in Sprites)
			{
				if (batchSprite.IsAlive)
					batchSprite.Act(ticks);
				else if (deadSprite == null)
					deadSprite = batchSprite;
			}
			if (deadSprite != null)
				Sprites.Remove(deadSprite);
		}
		public void Draw(SpriteBatch spriteBatch, ulong ticks)
		{
			foreach (var batchSprite in Sprites.Where(x => x.IsAlive))
				batchSprite.Draw(spriteBatch, ticks);
		}
	}
}
