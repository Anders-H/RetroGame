using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RetroGame.Input
{
    public class MouseStateChecker : IInputStateChecker
    {
        private MouseState MouseState { get; set; }
        private MouseState OldMouseState { get; set; }
        public MouseStateChecker()
        {
            UpdateState();
        }
        public void UpdateState()
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
