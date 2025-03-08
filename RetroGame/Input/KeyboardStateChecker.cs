using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RetroGame.Scene;

namespace RetroGame.Input;

public class KeyboardStateChecker : IRetroActor
{
    private KeyboardState KeyboardState { get; set; }
    private KeyboardState OldKeyboardState { get; set; }
    private GamePadState GamePadState { get; set; }
    private GamePadState OldGamePadState { get; set; }

    public KeyboardStateChecker()
    {
        Act(0);
    }
    
    public void Act(ulong ticks)
    {
        OldKeyboardState = KeyboardState;
        KeyboardState = Keyboard.GetState();
        OldGamePadState = GamePadState;
        GamePadState = GamePad.GetState(PlayerIndex.One);
    }
    
    public bool IsFirePressed() =>
        IsKeyPressed(Keys.LeftControl) || IsKeyPressed(Keys.RightControl) || IsPadButtonPressed(Buttons.A);

    public bool MoveUp() =>
        KeyboardState.IsKeyDown(Keys.Up)
        || GamePadState.IsButtonDown(Buttons.DPadUp)
        || GamePadState.IsButtonDown(Buttons.LeftThumbstickUp);

    public bool MoveDown() =>
        KeyboardState.IsKeyDown(Keys.Down)
        || GamePadState.IsButtonDown(Buttons.DPadDown)
        || GamePadState.IsButtonDown(Buttons.LeftThumbstickDown);

    public bool MoveLeft() =>
        KeyboardState.IsKeyDown(Keys.Left)
        || GamePadState.IsButtonDown(Buttons.DPadLeft)
        || GamePadState.IsButtonDown(Buttons.LeftThumbstickLeft);

    public bool MoveRight() =>
        KeyboardState.IsKeyDown(Keys.Right)
        || GamePadState.IsButtonDown(Buttons.DPadRight)
        || GamePadState.IsButtonDown(Buttons.LeftThumbstickRight);

    public bool IsKeyUp(Keys key) =>
        KeyboardState.IsKeyUp(key);

    public bool IsKeyDown(Keys key) =>
        KeyboardState.IsKeyDown(key);

    public bool IsPadButtonUp(Buttons button) =>
        GamePadState.IsButtonUp(button);

    public bool IsPadButtonDown(Buttons button) =>
        GamePadState.IsButtonDown(button);

    public bool IsKeyPressed(Keys key) =>
        KeyboardState.IsKeyDown(key) && OldKeyboardState.IsKeyUp(key);

    public bool IsPadButtonPressed(Buttons button) =>
        GamePadState.IsButtonDown(button) && OldGamePadState.IsButtonUp(button);
}