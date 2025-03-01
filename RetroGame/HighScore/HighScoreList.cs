using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroGame.Text;

namespace RetroGame.HighScore;

public class HighScoreList
{
    private const int MaxItems = 15;
    private readonly List<HighScoreListItem> _items;
    private readonly TextBlock _textBlock;

    public HighScoreList()
    {
        _items = [];

        for (var i = 0; i < MaxItems; i++)
        {
            _items.Add(new HighScoreListItem((i + 1) * 100, "AAA"));
        }

        Sort();
        _textBlock = new TextBlock();
    }

    private void Sort()
    {
        var temp = new List<HighScoreListItem>();
        temp.AddRange(_items);
        _items.Clear();

        foreach (var highScoreListItem in temp.OrderByDescending(x => x.Score))
            _items.Add(highScoreListItem);
    }

    public void Add(int score, string name)
    {
        if (_items[^1].Score == score)
            _items.RemoveAt(_items.Count - 1);

        _items.Add(new HighScoreListItem(score, name));
        Sort();
        
        while (_items.Count > MaxItems)
            _items.RemoveAt(_items.Count - 1);
    }

    public bool Qualify(int score)
    {
        return _items[^1].Score <= score;
    }

    public List<string> GetStrings()
    {
        var needsSorting = false;

        while (_items.Count > MaxItems)
        {
            _items.RemoveAt(_items.Count - 1);
            needsSorting = true;
        }

        while (_items.Count < MaxItems)
        {
            _items.Add(new HighScoreListItem(100, "AAA"));
            needsSorting = true;
        }

        if (needsSorting)
            Sort();

        return _items.Select(highScoreListItem => highScoreListItem.ToString()).ToList();
    }

    public void Draw(SpriteBatch spriteBatch, ulong ticks)
    {
        var strings = GetStrings();
        var x = 0;
        var y = 0;

        for (var i = 0; i < strings.Count; i++)
        {
            _textBlock.DirectDraw(spriteBatch, x, y, strings[i], Color.White);
            y += 8;
        }
    }
}