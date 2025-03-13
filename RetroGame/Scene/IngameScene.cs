using Microsoft.Xna.Framework.Graphics;
using RetroGame.Input;
using RetroGame.Text;

namespace RetroGame.Scene;

public abstract class IngameScene : Scene
{
    private int _score;
    private string _scoreString;
    protected KeyboardStateChecker Keyboard { get; }
    protected readonly TextBlock Text;

    protected IngameScene(RetroGame parent) : base(parent)
    {
        _score = 0;
        _scoreString = "score: 0";
        Text = new TextBlock(CharacterSet.Uppercase);
        Keyboard = new KeyboardStateChecker();
        AddToAutoUpdate(Keyboard);
    }

    public int Score
    {
        get => _score;
        set
        {
            if (value != _score)
            {
                _score = value;

                if (_score < 0)
                    _score = 0;

                _scoreString = $"score: {_score}";
            }
        }
    }

    protected void DrawScore(SpriteBatch spriteBatch, int x, int y, ColorPalette color) =>
        Text.DirectDraw(spriteBatch, 480, 0, _scoreString, color);

}