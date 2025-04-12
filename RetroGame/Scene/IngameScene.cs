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
        Score = 1; // To provoke the score string to be created
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
            if (value != _score)
            {
                _score = value;

                if (_score < 0)
                    _score = 0;

                var s = _score.ToString();

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
            }
        }
    }

    public string ScoreString =>
        _scoreString;
}