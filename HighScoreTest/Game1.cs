using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGame;
using RetroGame.HighScore;
using RetroGame.Input;
using RetroGame.Scene;

namespace HighScoreTest;

public class Game1 : RetroGame.RetroGame
{
    public static HighScoreList HighScoreList { get; }

    static Game1()
    {
        HighScoreList = new HighScoreList();
    }

    public Game1() : base(320, 200, RetroDisplayMode.Fullscreen, false)
    {
    }

    protected override void LoadContent()
    {
        base.LoadContent();
        CurrentScene = new DisplayHighScoreScene(this);
    }
}

public class DisplayHighScoreScene : Scene
{
    private KeyboardStateChecker Keyboard { get; }

    public DisplayHighScoreScene(RetroGame.RetroGame parent) : base(parent)
    {
        Keyboard = new KeyboardStateChecker();
        AddToAutoUpdate(Keyboard);
    }

    public override void Update(GameTime gameTime, ulong ticks)
    {
        if (Keyboard.IsKeyPressed(Keys.Escape))
            Exit();
        else if (Keyboard.IsKeyPressed(Keys.Enter))
            Parent.CurrentScene = new EditHighScoreScene(Parent);

        base.Update(gameTime, ticks);
    }

    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        Game1.HighScoreList.Draw(spriteBatch, ticks);
        base.Draw(gameTime, ticks, spriteBatch);
    }
}

public class EditHighScoreScene : Scene
{
    private KeyboardStateChecker Keyboard { get; }

    public EditHighScoreScene(RetroGame.RetroGame parent) : base(parent)
    {
        Keyboard = new KeyboardStateChecker();
        AddToAutoUpdate(Keyboard);
    }

    public override void Update(GameTime gameTime, ulong ticks)
    {
        if (Keyboard.IsKeyPressed(Keys.Escape))
            Parent.CurrentScene = new DisplayHighScoreScene(Parent);

        base.Update(gameTime, ticks);
    }
}