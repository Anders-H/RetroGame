﻿using Microsoft.Xna.Framework.Input;
using RetroGame.Scene;

namespace RetroGame.Input;

public class KeyboardStateChecker : IRetroActor
{
    private KeyboardState KeyboardState { get; set; }
    private KeyboardState OldKeyboardState { get; set; }

    public KeyboardStateChecker()
    {
        Act(0);
    }
    
    public void Act(ulong ticks)
    {
        OldKeyboardState = KeyboardState;
        KeyboardState = Keyboard.GetState();
    }
    
    public bool IsFirePressed() =>
        IsKeyPressed(Keys.LeftControl) || IsKeyPressed(Keys.RightControl);

    public bool IsKeyUp(Keys key) =>
        KeyboardState.IsKeyUp(key);

    public bool IsKeyDown(Keys key) =>
        KeyboardState.IsKeyDown(key);

    public bool IsKeyPressed(Keys key) =>
        KeyboardState.IsKeyDown(key) && OldKeyboardState.IsKeyUp(key);
}