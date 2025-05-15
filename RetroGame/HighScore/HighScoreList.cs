using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGame.Input;
using RetroGame.Text;

namespace RetroGame.HighScore;

public class HighScoreList
{
    private readonly bool _scrollIn;
    private readonly int _listX;
    private int _listY;
    private int _targetY;
    private readonly bool _shadow;
    private const int MaxItems = 15;
    private readonly List<HighScoreListItem> _items;
    private readonly TextBlock _textBlock;
    private int _charactersLeftToEdit;
    private int _editingIndex;
    private readonly ColorPalette[] _blink;
    private int _blinkIndex;
    private const string TypableCharacters = "abcdefghijklmnopqrstuvwxyz";
    private int _typableCharactersIndex;
    private readonly int _resolutionHeight;

    public HighScoreList(int resolutionWidth, int resolutionHeight, bool scrollIn, bool shadow, int y)
    {
        _resolutionHeight = resolutionHeight;
        _scrollIn = scrollIn;
        _items = [];
        _blink = [ColorPalette.Black, ColorPalette.DarkGrey, ColorPalette.Grey, ColorPalette.LightGrey, ColorPalette.White, ColorPalette.LightGrey, ColorPalette.Grey, ColorPalette.DarkGrey];
        _blinkIndex = 0;

        for (var i = 0; i < MaxItems; i++)
            _items.Add(new HighScoreListItem((i + 1) * 100, "aaa"));

        Sort();
        _textBlock = new TextBlock(CharacterSet.Uppercase);
        _listX = (resolutionWidth / 2) - 60;
        _targetY = y;
        _charactersLeftToEdit = 0;
        _editingIndex = -1;
        _shadow = shadow;
        ResetVisuals();
    }

    public HighScoreList(int resolutionWidth, int resolutionHeight, bool scrollIn, bool shadow) : this(resolutionWidth, resolutionHeight, scrollIn, shadow, (resolutionHeight / 2) - 60)
    {
    }

    public void BeginEdit(int newHighScore)
    {
        _charactersLeftToEdit = 3;
        AddNew(newHighScore);
        _editingIndex = _items.IndexOf(_items.First(x => x.Name == "   "));
    }

    public bool StillEditing =>
        _charactersLeftToEdit > 0;

    public void ResetVisuals()
    {
        _listY = _scrollIn ? _resolutionHeight : _targetY;
    }

    public void ResetVisuals(int targetY)
    {
        _targetY = targetY;
        _listY = _scrollIn ? _resolutionHeight : _targetY;
    }

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
            _items.Add(new HighScoreListItem(100, "aaa"));
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
        else if (keyboard.PressDown())
        {
            _typableCharactersIndex--;

            if (_typableCharactersIndex < 0)
                _typableCharactersIndex = TypableCharacters.Length - 1;
        }
        else if (keyboard.PressUp())
        {
            _typableCharactersIndex++;

            if (_typableCharactersIndex >= TypableCharacters.Length)
                _typableCharactersIndex = 0;
        }
        else if (keyboard.IsFirePressed())
        {
            Typed(TypableCharacters[_typableCharactersIndex].ToString());
        }
    }

    private void Typed(string character)
    {
        switch (_charactersLeftToEdit)
        {
            case 3:
                SetCharacterAt(0, character);
                _charactersLeftToEdit--;
                _typableCharactersIndex = 0;
                break;
            case 2:
                SetCharacterAt(1, character);
                _charactersLeftToEdit--;
                _typableCharactersIndex = 0;
                break;
            case 1:
                SetCharacterAt(2, character);
                _charactersLeftToEdit--;
                _typableCharactersIndex = 0;
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
        if (_listY > _targetY)
            _listY--;

        var strings = GetStrings();
        var y = _listY;

        if (StillEditing)
        {
            for (var i = 0; i < strings.Count; i++)
            {
                if (i == _editingIndex)
                {
                    if (ticks % 10 > 5)
                    {
                        _blinkIndex++;

                        if (_blinkIndex >= _blink.Length)
                            _blinkIndex = 0;
                    }

                    if (_shadow)
                    {
                        _textBlock.DirectDraw(spriteBatch, _listX + 1, y + 1, strings[i], ColorPalette.Black);
                        _textBlock.DirectDraw(spriteBatch, _listX + ((3 - _charactersLeftToEdit) * 8) + 1, y + 1, TypableCharacters[_typableCharactersIndex].ToString(), ColorPalette.Black);
                    }

                    _textBlock.DirectDraw(spriteBatch, _listX, y, strings[i], ColorPalette.LightGrey);
                    _textBlock.DirectDraw(spriteBatch, _listX + ((3 - _charactersLeftToEdit) * 8), y, TypableCharacters[_typableCharactersIndex].ToString(), _blink[_blinkIndex]);
                }
                else
                {
                    if (_shadow)
                        _textBlock.DirectDraw(spriteBatch, _listX + 1, y + 1, strings[i], ColorPalette.Black);

                    _textBlock.DirectDraw(spriteBatch, _listX, y, strings[i], ColorPalette.Grey);
                }

                y += 8;
            }
        }
        else
        {
            foreach (var t in strings)
            {
                if (_shadow)
                    _textBlock.DirectDraw(spriteBatch, _listX + 1, y + 1, t, ColorPalette.Black);

                _textBlock.DirectDraw(spriteBatch, _listX, y, t, ColorPalette.White);
                y += 8;
            }
        }
    }
}