using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGame;
using RetroGame.Input;
using RetroGame.Scene;
using RetroGame.Text;
using System.Runtime.Versioning;

namespace BreakOut;

[SupportedOSPlatform("windows")]
public class IntroScene : Scene
{
    private KeyboardStateChecker Keyboard { get; } = new();
    private TextBlock Text { get; } = new(CharacterSet.Uppercase);

    public IntroScene(RetroGame.RetroGame retroGame) : base(retroGame)
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

        base.Update(gameTime, ticks);
    }

    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch) =>
        Text.Draw(spriteBatch, ColorPalette.White);
}