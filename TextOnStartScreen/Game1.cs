using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGame;
using RetroGame.Input;
using RetroGame.Scene;
using RetroGame.Text;

namespace TextOnStartScreen;

public class Game1 : RetroGame.RetroGame
{
    private const RetroDisplayMode DisplayMode = RetroDisplayMode.Windowed;

    public Game1() : base(640, 360, DisplayMode)
    {
    }

    protected override void LoadContent()
    {
        BackColor = Color.Black;
        CurrentScene = new StartScene(this);
        base.LoadContent();
    }
}

public class StartScene : Scene
{
    private const string LogoText = "å SECRET AGENT MAN Med svenska tecken å ä ö Å Ä Ö";
    private readonly TextBlock _logoTextBlock;
    private KeyboardStateChecker Keyboard { get; }
    private const string UppercaseText = "mats j. larsson med svenska tecken å ä ö";
    private readonly TextBlock _uppercaseTextBlock;

    public StartScene(RetroGame.RetroGame parent) : base(parent)
    {
        Keyboard = new KeyboardStateChecker();
        _logoTextBlock = new TextBlock(CharacterSet.Lowercase);
        _uppercaseTextBlock = new TextBlock(CharacterSet.Uppercase);
        AddToAutoUpdate(Keyboard);
    }

    public override void Update(GameTime gameTime, ulong ticks)
    {
        if (Keyboard.IsKeyPressed(Keys.Escape))
            Exit();
    }

    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        _logoTextBlock.DirectDraw(spriteBatch, 2, 2, LogoText, ColorPalette.White);
        _uppercaseTextBlock.DirectDraw(spriteBatch, 2, 10, UppercaseText, ColorPalette.White);
        base.Draw(gameTime, ticks, spriteBatch);
    }
}