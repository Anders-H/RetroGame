using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroGameClasses;
using RetroGameClasses.RetroTextures;
using RetroGameClasses.Sprites;

namespace BatchDemo
{
	public class Game1 : RetroGame
	{
		public static RetroTexture Star { get; set; }
		public Game1() : base(320, 200, RetroDisplayMode.WindowedWithUpscaling) { }
		protected override void LoadContent()
		{
			Star = RetroTexture.ScaffoldSimpleTexture(GraphicsDevice, 1, 1, Color.White);
			CurrentScene = new StarsScene(this);
			base.LoadContent();
		}
	}

	public class BatchSprite : Sprite, IBatchSprite
	{
		private static Random Rnd { get; } = new Random();
		private float SpeedX { get; }
		public bool IsAlive => true;
		public BatchSprite()
		{
			Y = Rnd.Next(0, 200);
			SpeedX = (float)(Rnd.NextDouble() * 4);
		}
		public void Act()
		{
			X -= SpeedX;
			if (X < 0)
			{
				X = 320;
				Y = Rnd.Next(0, 200);
			}
		}
		public void Draw(SpriteBatch spriteBatch) => Draw(spriteBatch, Game1.Star, 0);
	}

	public class StarsScene : Scene
	{
		private bool _adding = true;
		private readonly Batch _batch = new Batch();
		public StarsScene(RetroGame parent) : base(parent) { }
		public override void Update(GameTime gameTime)
		{
			if (_batch.Count < 1000 && _adding)
				_batch.AppendLast(new BatchSprite());
			else if (_adding)
				_adding = false;
			else if (_batch.Count > 0)
				_batch.RemoveFirst();
			_batch.Act();
		}
		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) => _batch.Draw(spriteBatch);
	}
}
