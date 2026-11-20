using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using RetroGame.Input;
using RetroGame.Text;

namespace RetroGame.Scene;

public abstract class IngameScene : Scene
{
    private int _score;
    private int _displayScore;
    private string _scoreString;
    private bool _scoreInvalidated;
    protected KeyboardStateChecker Keyboard { get; }
    protected readonly TextBlock Text;

    protected IngameScene(RetroGame parent) : base(parent)
    {
        _scoreInvalidated = true;
        Score = 0;
        Text = new TextBlock(CharacterSet.Uppercase);
        Keyboard = new KeyboardStateChecker();
        AddToAutoUpdate(Keyboard);
        MediaPlayer.Stop();
    }

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            _scoreInvalidated = true;
        }
    }

    protected int DirectAddScore(int diff)
    {
        _score += diff;
        _displayScore = _score;
        _scoreInvalidated = true;
        return _score;
    }

    public override void Update(GameTime gameTime, ulong ticks)
    {
        if (_scoreInvalidated)
        {
            _displayScore = NextDisplayScore(_displayScore, _score);
            _scoreString = "score:" + _displayScore.ToString().PadLeft(9);

            _scoreInvalidated = _score != _displayScore;
        }

        base.Update(gameTime, ticks);
    }

    internal static int NextDisplayScore(int current, int target)
    {
        var difference = (long)target - current;
        var distance = Math.Abs(difference);

        var step = distance switch
        {
            > 500 => 5,
            > 400 => 4,
            > 300 => 3,
            > 200 => 2,
            _ => 1
        };

        return (int)(current + Math.Sign(difference) * Math.Min(distance, step));
    }

    public string ScoreString =>
        _scoreString;
}
