using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RetroGame;
using RetroGame.Input;
using RetroGame.Scene;
using RetroGame.Text;

namespace TextInputOutput;

public class Game1 : RetroGame.RetroGame
{
    public Game1() : base(320, 200, RetroDisplayMode.Windowed)
    {
    }

    protected override void LoadContent()
    {
        CurrentScene = new TextScene(this);
        base.LoadContent();
    }
}

public class TextScene : Scene
{
    private KeyboardStateChecker Keyboard { get; } = new();
    private TextBlock Text { get; } = new(40, 10, CharacterSet.Lowercase);
    
    public TextScene(RetroGame.RetroGame parent) : base(parent)
    {
        Text.DrawOffsetY = 15*8;
        AddToAutoUpdate(Keyboard, Text);
        AddToAutoDraw(Text);
        Text.SetText(0, 9, "Press Enter.");
    }
    
    public override void Update(GameTime gameTime, ulong ticks)
    {
        if (Keyboard.IsKeyPressed(Keys.Enter) && Text.IsReady)
            Text.AppendRows("Detta ar en testtext som kommer att stracka sig over flera rader. Det ar bra, for da far vi se om wordwrapping fungerar.", 1, false);
        else if (Keyboard.IsKeyPressed(Keys.Escape))
            Exit();
        
        base.Update(gameTime, ticks);
    }
}