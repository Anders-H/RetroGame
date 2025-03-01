using System.Text;

namespace RetroGame.HighScore;

public class HighScoreListItem
{
    public int Score { get; set; }
    public string Name { get; set; }

    public HighScoreListItem() : this(0, "")
    {
    }

    public HighScoreListItem(int score, string name)
    {
        Score = score;
        Name = name;
    }

    public override string ToString()
    {
        var n = Name;

        if (n.Length > 3)
            n = n.Substring(0, 3);
        else if (n.Length < 3)
            n = n.PadRight(3, ' ');

        var s = Score.ToString();

        var result = new StringBuilder();
        result.Append(n);
        
        while (result.Length + s.Length < 15)
            result.Append('.');

        result.Append(s);
        return result.ToString();
    }
}