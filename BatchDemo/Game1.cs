using Microsoft.Xna.Framework;
using RetroGameClasses;
using RetroGameClasses.RetroTextures;

namespace BatchDemo
{
	public class Game1 : RetroGame
	{
		public static RetroTexture Star { get; set; }

		public Game1() : base(320, 200, RetroDisplayMode.Windowed)
		{
		}
		
		protected override void LoadContent()
		{
			Star = RetroTexture.ScaffoldSimpleTexture(GraphicsDevice, 1, 1, Color.White);
			CurrentScene = new IntroScene(this);
			BackColor = Color.Black;
			base.LoadContent();
		}
	}
}