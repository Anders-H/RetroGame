using Microsoft.Xna.Framework;
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
            if (_score > _displayScore)
                _displayScore++;
            else if (_score < _displayScore)
                _displayScore = _score;

            var s = _displayScore.ToString();

            _scoreString = s.Length switch
            {
                1 => $"score:        {s}",
                2 => $"score:       {s}",
                3 => $"score:      {s}",
                4 => $"score:     {s}",
                5 => $"score:    {s}",
                6 => $"score:   {s}",
                7 => $"score:  {s}",
                8 => $"score: {s}",
                _ => $"score:{s}"
            };

            _scoreInvalidated = _score != _displayScore;
        }

        base.Update(gameTime, ticks);
    }

    public string ScoreString =>
        _scoreString;
}