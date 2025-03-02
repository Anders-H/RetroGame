using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGame.Input;
using RetroGame.Text;

namespace RetroGame.HighScore;

public class HighScoreList
{
    private readonly int _resolutionWidth;
    private readonly int _resolutionHeight;
    private readonly int _listX;
    private readonly int _listY;
    private const int MaxItems = 15;
    private readonly List<HighScoreListItem> _items;
    private readonly TextBlock _textBlock;
    private int _charactersLeftToEdit;
    private int _editingIndex;

    public HighScoreList(int resolutionWidth, int resolutionHeight)
    {
        _items = [];

        for (var i = 0; i < MaxItems; i++)
        {
            _items.Add(new HighScoreListItem((i + 1) * 100, "AAA"));
        }

        Sort();
        _textBlock = new TextBlock();
        _resolutionWidth = resolutionWidth;
        _resolutionHeight = resolutionHeight;
        _listX = (_resolutionWidth / 2) - 60;
        _listY = (_resolutionHeight / 2) - 60;
        _charactersLeftToEdit = 0;
        _editingIndex = -1;
    }

    public void BeginEdit(int newHighScore)
    {
        _charactersLeftToEdit = 3;
        AddNew(newHighScore);
        _editingIndex = _items.IndexOf(_items.First(x => x.Name == "   "));
    }

    public bool StillEditing =>
        _charactersLeftToEdit > 0;

    private void Sort()
    {
        var temp = new List<HighScoreListItem>();
        temp.AddRange(_items);
        _items.Clear();

        foreach (var highScoreListItem in temp.OrderByDescending(x => x.Score))
            _items.Add(highScoreListItem);
    }

    public void AddNew(int newHighScore) =>
        Add(newHighScore, "   ");

    public void Add(int score, string name)
    {
        _items.Add(new HighScoreListItem(score, name));
        Sort();
        
        while (_items.Count > MaxItems)
            _items.RemoveAt(_items.Count - 1);

        if (_items[^1].Score == score)
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

    public void Edit(KeyboardStateChecker keyboard)
    {
        if (keyboard.IsKeyPressed(Keys.A))
            Typed("a");
        else if (keyboard.IsKeyPressed(Keys.B))
            Typed("b");
        else if (keyboard.IsKeyPressed(Keys.C))
            Typed("c");
        else if (keyboard.IsKeyPressed(Keys.D))
            Typed("d");
        else if (keyboard.IsKeyPressed(Keys.E))
            Typed("e");
        else if (keyboard.IsKeyPressed(Keys.F))
            Typed("f");
        else if (keyboard.IsKeyPressed(Keys.G))
            Typed("g");
        else if (keyboard.IsKeyPressed(Keys.H))
            Typed("h");
        else if (keyboard.IsKeyPressed(Keys.I))
            Typed("i");
        else if (keyboard.IsKeyPressed(Keys.J))
            Typed("j");
        else if (keyboard.IsKeyPressed(Keys.K))
            Typed("k");
        else if (keyboard.IsKeyPressed(Keys.L))
            Typed("l");
        else if (keyboard.IsKeyPressed(Keys.M))
            Typed("m");
        else if (keyboard.IsKeyPressed(Keys.N))
            Typed("n");
        else if (keyboard.IsKeyPressed(Keys.O))
            Typed("o");
        else if (keyboard.IsKeyPressed(Keys.P))
            Typed("p");
        else if (keyboard.IsKeyPressed(Keys.Q))
            Typed("q");
        else if (keyboard.IsKeyPressed(Keys.R))
            Typed("r");
        else if (keyboard.IsKeyPressed(Keys.S))
            Typed("s");
        else if (keyboard.IsKeyPressed(Keys.T))
            Typed("t");
        else if (keyboard.IsKeyPressed(Keys.U))
            Typed("u");
        else if (keyboard.IsKeyPressed(Keys.V))
            Typed("v");
        else if (keyboard.IsKeyPressed(Keys.W))
            Typed("w");
        else if (keyboard.IsKeyPressed(Keys.X))
            Typed("x");
        else if (keyboard.IsKeyPressed(Keys.Y))
            Typed("y");
        else if (keyboard.IsKeyPressed(Keys.Z))
            Typed("z");
    }

    private void Typed(string character)
    {
        switch (_charactersLeftToEdit)
        {
            case 3:
                SetCharacterAt(0, character);
                _charactersLeftToEdit--;
                break;
            case 2:
                SetCharacterAt(1, character);
                _charactersLeftToEdit--;
                break;
            case 1:
                SetCharacterAt(2, character);
                _charactersLeftToEdit--;
                break;
        }
    }

    private void SetCharacterAt(int index, string character)
    {
        var item = _items[_editingIndex];

        switch (index)
        {
            case 0:
                item.Name = character + "  ";
                break;
            case 1:
                item.Name = item.Name.Substring(0, 1) + character + " ";
                break;
            case 2:
                item.Name = item.Name.Substring(0, 2) + character;
                break;
        }
    }

    public void Draw(SpriteBatch spriteBatch, ulong ticks)
    {
        var strings = GetStrings();
        var x = _listX;
        var y = _listY;

        if (StillEditing)
        {
            for (var i = 0; i < strings.Count; i++)
            {
                if (i == _editingIndex)
                {
                    var blink = ticks % 10 > 5;
                    _textBlock.DirectDraw(spriteBatch, x, y, strings[i], ColorPalette.Grey);
                    _textBlock.DirectDraw(spriteBatch, x + ((3 - _charactersLeftToEdit) * 8), y, "*", blink ? ColorPalette.White : ColorPalette.Green);
                }
                else
                {
                    _textBlock.DirectDraw(spriteBatch, x, y, strings[i], ColorPalette.DarkGrey);
                }

                y += 8;
            }
        }
        else
        {
            foreach (var t in strings)
            {
                _textBlock.DirectDraw(spriteBatch, x, y, t, ColorPalette.White);
                y += 8;
            }
        }
    }
}