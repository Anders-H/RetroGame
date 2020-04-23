using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGameClasses;
using RetroGameClasses.Input;
using RetroGameClasses.RetroTextures;
using RetroGameClasses.Scene;
using RetroGameClasses.Sprites;
using RetroDisplayMode = RetroGameClasses.RetroDisplayMode;


namespace CollisionDetection
{
	public class Game1 : RetroGame
	{
		public static RetroTexture Texture;
		
		public Game1() : base(128, 128, RetroDisplayMode.WindowedWithUpscaling)
		{
		}
		
		protected override void LoadContent()
		{

			
			base.LoadContent();
		}
	}
}