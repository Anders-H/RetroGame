using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGameClasses;
using RetroGameClasses.Input;
using RetroGameClasses.RetroTextures;
using RetroGameClasses.Scene;
using RetroGameClasses.Sprites;
using RetroDisplayMode = RetroGameClasses.RetroDisplayMode;


namespace TextureScaffolding
{
	public class Game1 : RetroGame
	{
		public static RetroTexture Texture;
		public Game1() : base(128, 128, RetroDisplayMode.WindowedWithUpscaling)
		{
		}
		protected override void LoadContent()
		{
			BackColor = ColorPaletteHelper.GetColor(ColorPalette.Blue);
			Texture = RetroTexture.ScaffoldTextureCells(GraphicsDevice, 2, 2, 4, ColorPaletteHelper.GetColor(ColorPalette.Transparent));

			var cell = new Bitmap(2, 2, ColorPalette.Transparent);
			cell.SetPixels(
				"2 " +
				"  ");
			RetroTexture.PlotCell(Texture, 0, cell);

			cell = new Bitmap(2, 2, ColorPalette.Transparent);
			cell.SetPixels(
				" 2" +
				"  ");
			RetroTexture.PlotCell(Texture, 1, cell);

			cell = new Bitmap(2, 2, ColorPalette.Transparent);
			cell.SetPixels(
				"  " +
				" 2");
			RetroTexture.PlotCell(Texture, 2, cell);

			cell = new Bitmap(2, 2, ColorPalette.Transparent);
			cell.SetPixels(
				"  " +
				"2 ");
			RetroTexture.PlotCell(Texture, 3, cell);

			CurrentScene = new AnimationTextureScene(this);
			base.LoadContent();
		}
	}

	public class AnimationTextureScene : Scene
	{
		private int _dly;
		private int _frame;
		private int _x;
		private int _xspeed = 1;
		private KeyboardStateChecker Keyboard { get; } = new KeyboardStateChecker();
		public AnimationTextureScene(RetroGame retroGame) : base(retroGame)
		{
			AddToAutoUpdate(Keyboard);
		}
		public override void Update(GameTime gameTime, ulong ticks)
		{
			//Quit.
			if (Keyboard.IsKeyPressed(Keys.Escape))
				Exit();
			else if (Keyboard.IsKeyPressed(Keys.Enter))
				Parent.CurrentScene = new CyclicSpriteScene(Parent);
			//Logic.
			_dly++;
			if (_dly > 3)
				_dly = 0;
			else
				return;
			if (_x >= 126)
				_xspeed = -1;
			else if (_x < 0)
				_xspeed = 1;
			_x += _xspeed;
			_frame++;
			if (_frame > 3)
				_frame = 0;
			base.Update(gameTime, ticks);
		}
		public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch) =>
			Game1.Texture.Draw(spriteBatch, _frame, _x, 20);
	}

	public class CyclicSpriteScene : Scene
	{
		private float _xspeed = 0.3f;
		private CyclicSprite CyclicSprite { get; }
		private KeyboardStateChecker Keyboard { get; } = new KeyboardStateChecker();
		public CyclicSpriteScene(RetroGame retroGame) : base(retroGame)
		{
			CyclicSprite = new CyclicSprite();
			CyclicSprite.ConfigureCycle(Game1.Texture, 5, 0, 1, 2, 3);
			CyclicSprite.Y = 20;
			AddToAutoUpdate(Keyboard);
		}
		public override void Update(GameTime gameTime, ulong ticks)
		{
			//Quit.
			if (Keyboard.IsKeyPressed(Keys.Escape))
				Exit();
			else if (Keyboard.IsKeyPressed(Keys.Enter))
				Parent.CurrentScene = new AnimationTextureScene(Parent);
			//Logic.
			if (CyclicSprite.X >= 126)
				_xspeed = -0.3f;
			else if (CyclicSprite.X < 0)
				_xspeed = 0.3f;
			CyclicSprite.X += _xspeed;
			CyclicSprite.Act();
			base.Update(gameTime, ticks);
		}
		public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
		{
			CyclicSprite.Draw(spriteBatch);
		}
	}
}
