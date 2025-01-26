using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGame;
using RetroGame.Input;
using RetroGame.Scene;
using RetroGame.Text;

namespace TextOnStartScreen
{
    public class Game1 : RetroGame.RetroGame
    {
#if DEBUG
    private const RetroDisplayMode DisplayMode = RetroDisplayMode.Windowed;
#else
        private const RetroDisplayMode DisplayMode = RetroDisplayMode.Fullscreen;
#endif

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
}

public class StartScene : Scene
{
    private const string LogoText = "SECRET AGENT MAN";
    private readonly TextBlock _logoTextBlock;
    private KeyboardStateChecker Keyboard { get; }

    public StartScene(RetroGame.RetroGame parent) : base(parent)
    {
        Keyboard = new KeyboardStateChecker();
        _logoTextBlock = new TextBlock(CharacterSet.Lowercase);
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
        base.Draw(gameTime, ticks, spriteBatch);
    }
}