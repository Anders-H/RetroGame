using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGameClasses;
using RetroGameClasses.Input;
using RetroGameClasses.RetroTextures;
using RetroGameClasses.Scene;
using RetroGameClasses.Sprites;
using RetroGameClasses.Text;

namespace BatchDemo
{
	public class Game1 : RetroGame
	{
		public static RetroTexture Star { get; set; }
		public Game1() : base(320, 200, RetroDisplayMode.FullscreenWithUpscalingAndBorder) { }
		protected override void LoadContent()
		{
			Star = RetroTexture.ScaffoldSimpleTexture(GraphicsDevice, 1, 1, Color.White);
			CurrentScene = new StarsScene(this);
			BackColor = Color.Black;
			base.LoadContent();
		}
	}

	public class BatchSprite : Sprite, IBatchSprite
	{
		private static Random Rnd { get; } = new Random();
		public float SpeedX { get; }
		private Color Color { get; }
		public bool IsAlive => true;
		public BatchSprite()
		{
			Y = Rnd.Next(0, 200);
			SpeedX = (float)(Rnd.NextDouble() * 4);
			if (SpeedX > 3)
				Color = ColorPaletteHelper.GetColor(ColorPalette.White);
			else if (SpeedX > 2)
				Color = ColorPaletteHelper.GetColor(ColorPalette.LightGrey);
			else if (SpeedX > 1)
				Color = ColorPaletteHelper.GetColor(ColorPalette.Grey);
			else
				Color = ColorPaletteHelper.GetColor(ColorPalette.DarkGrey);
		}
		public void Act(ulong ticks)
		{
			X -= SpeedX;
			if (X < 0)
			{
				X = 320;
				Y = Rnd.Next(0, 200);
			}
		}
		public void Draw(SpriteBatch spriteBatch, ulong ticks) => Draw(spriteBatch, Game1.Star, 0, Color);
	}

	public class StarsScene : Scene
	{
		private bool _adding = true;
		private KeyboardStateChecker Keyboard { get; } = new KeyboardStateChecker();
		private readonly TextBlock _textBlock = new TextBlock(CharacterSet.Uppercase);
		private readonly Batch _batch1 = new Batch();
		private readonly Batch _batch2 = new Batch();
		private readonly Batch _batch3 = new Batch();
		private readonly Batch _batch4 = new Batch();
		public StarsScene(RetroGame parent) : base(parent)
		{
			AddToAutoUpdate(Keyboard, _batch1, _batch2, _batch3, _batch4);
			AddToAutoDraw(_batch1, _batch2, _batch3, _batch4);
		}
		private int Count => _batch1.Count + _batch2.Count + _batch3.Count + _batch4.Count;
		public override void Update(GameTime gameTime, ulong ticks)
		{
			if (Count < 1000 && _adding)
			{
				var s = new BatchSprite();
				if (s.SpeedX > 3)
					_batch4.AppendLast(s);
				else if (s.SpeedX > 2)
					_batch3.AppendLast(s);
				else if (s.SpeedX > 1)
					_batch2.AppendLast(s);
				else
					_batch1.AppendLast(s);
			}
			else if (_adding)
				_adding = false;
			else if (Count > 0)
			{
				if (_batch1.Count > 0)
					_batch1.RemoveFirst();
				if (_batch2.Count > 0)
					_batch2.RemoveFirst();
				if (_batch3.Count > 0)
					_batch3.RemoveFirst();
				if (_batch4.Count > 0)
					_batch4.RemoveFirst();
			}
			if (Keyboard.IsKeyPressed(Keys.Escape))
				Exit();
			base.Update(gameTime, ticks);
		}
		public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
		{
			base.Draw(gameTime, ticks, spriteBatch);
			_textBlock.DirectDraw(spriteBatch, 0, 0, $"sprites in batch: {Count}", ColorPalette.Yellow);
		}
	}
}
