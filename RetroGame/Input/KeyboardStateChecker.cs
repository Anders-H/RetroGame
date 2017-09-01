using Microsoft.Xna.Framework.Input;

namespace RetroGame.Input
{
    public class KeyboardStateChecker : IInputStateChecker
    {
        private KeyboardState KeyboardState { get; set; }
        private KeyboardState OldKeyboardState { get; set; }
        public KeyboardStateChecker()
        {
            UpdateState();
        }
        public void UpdateState()
        {
            OldKeyboardState = KeyboardState;
            KeyboardState = Keyboard.GetState();
        }
        public bool IsKeyUp(Keys key) => KeyboardState.IsKeyUp(key);
        public bool IsKeyDown(Keys key) => KeyboardState.IsKeyDown(key);
        public bool IsKeyPressed(Keys key) => KeyboardState.IsKeyDown(key) && OldKeyboardState.IsKeyUp(key);
    }
}
