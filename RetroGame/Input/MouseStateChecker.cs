using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RetroGameClasses.Scene;

namespace RetroGameClasses.Input
{
	public class MouseStateChecker : IRetroActor
	{
		private MouseState MouseState { get; set; }
		private MouseState OldMouseState { get; set; }
		public MouseStateChecker()
		{
			Act(0);
		}
		public void Act(ulong ticks)
		{
			OldMouseState = MouseState;
			MouseState = Mouse.GetState();
		}
		public Point Location => new Point(MouseState.X, MouseState.Y);
		public bool LeftButtonDown => MouseState.LeftButton == ButtonState.Pressed;
		public bool LeftButtonPressed => MouseState.LeftButton == ButtonState.Pressed && OldMouseState.LeftButton == ButtonState.Released;
		public bool RightButtonDown => MouseState.RightButton == ButtonState.Pressed;
		public bool RightButtonPressed => MouseState.RightButton == ButtonState.Pressed && OldMouseState.RightButton == ButtonState.Released;
	}
}
