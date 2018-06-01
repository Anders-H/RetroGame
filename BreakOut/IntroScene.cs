using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGameClasses;
using RetroGameClasses.Input;
using RetroGameClasses.Scene;
using RetroGameClasses.Text;

namespace BreakOut
{
	public class IntroScene : Scene
	{
		private KeyboardStateChecker Keyboard { get; } = new KeyboardStateChecker();
		private TextBlock Text { get; } = new TextBlock(CharacterSet.Uppercase);
		public IntroScene(RetroGame retroGame) : base(retroGame)
		{
			AddToAutoUpdate(Keyboard);
			Text.SetText(1, 1, "press fire (left ctrl) to start!");
		}
		public override void Update(GameTime gameTime, ulong ticks)
		{
			if (Keyboard.IsKeyPressed(Keys.LeftControl))
				Parent.CurrentScene = new GameScene(Parent);
			if (Keyboard.IsKeyPressed(Keys.Escape))
				Exit();
		}
		public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
		{
			Text.Draw(spriteBatch, ColorPalette.White);
		}
	}
}
