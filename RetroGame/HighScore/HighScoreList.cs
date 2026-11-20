using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGame.Input;
using RetroGame.Text;
using RetroGame.Scene;

namespace RetroGame.HighScore;

public class HighScoreList : ISceneActor
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
    private string[] _strings;
    private const string TypableCharacters = "abcdefghijklmnopqrstuvwxyz";
    private int _typableCharactersIndex;
    private readonly int _resolutionHeight;
    public string TypedName { get; private set; }

    public HighScoreList(int resolutionWidth, int resolutionHeight, bool scrollIn, bool shadow, int y)
    {
        TypedName = "";
        _resolutionHeight = resolutionHeight;
        _scrollIn = scrollIn;
        _items = [];
        _blink = [ColorPalette.Black, ColorPalette.DarkGrey, ColorPalette.Grey, ColorPalette.LightGrey, ColorPalette.White, ColorPalette.LightGrey, ColorPalette.Grey, ColorPalette.DarkGrey];
        _blinkIndex = 0;

        for (var i = 0; i < MaxItems; i++)
            _items.Add(new HighScoreListItem((i + 1) * 100, "aaa"));

        _items.Reverse();
        RefreshStrings();
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
        TypedName = "";
        _typableCharactersIndex = 0;
        _charactersLeftToEdit = 0;
        _editingIndex = Insert(newHighScore, "   ");
        _charactersLeftToEdit = _editingIndex >= 0 ? 3 : 0;
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

    public void AddNew(int newHighScore) =>
        Add(newHighScore, "   ");

    public void Add(int score, string name) =>
        TryAdd(score, name);

    public bool TryAdd(int score, string name) =>
        Insert(score, name) >= 0;

    private int Insert(int score, string name)
    {
        if (!Qualify(score))
            return -1;

        // New entries precede existing ties, matching Qualify's inclusive comparison.
        var index = _items.FindIndex(item => item.Score <= score);

        if (index < 0)
            index = _items.Count;

        _items.Insert(index, new HighScoreListItem(score, name));

        if (_items.Count > MaxItems)
            _items.RemoveAt(MaxItems);

        // Keep an ongoing edit attached to its entry when another score is added.
        if (_charactersLeftToEdit > 0 && index <= _editingIndex)
        {
            _editingIndex++;

            if (_editingIndex >= _items.Count)
            {
                _editingIndex = -1;
                _charactersLeftToEdit = 0;
            }
        }

        RefreshStrings();
        return index;
    }

    public bool Qualify(int score) =>
        _items.Count < MaxItems || score >= _items[^1].Score;

    private void RefreshStrings()
    {
        _strings = new string[_items.Count];

        for (var i = 0; i < _items.Count; i++)
            _strings[i] = _items[i].ToString();
    }

    public List<string> GetStrings() =>
        new(_strings);

    public void Act(ulong ticks)
    {
        if (_listY > _targetY)
            _listY--;

        if (StillEditing && ticks % 10 > 5)
            _blinkIndex = (_blinkIndex + 1) % _blink.Length;
    }

    public void Edit(KeyboardStateChecker keyboard)
    {
        if (!StillEditing)
            return;

        var oldName = _items[_editingIndex].Name;
        var typedLetter = false;

        for (var i = 0; i < TypableCharacters.Length; i++)
        {
            if (!keyboard.IsKeyPressed((Keys)((int)Keys.A + i)))
                continue;

            Typed(TypableCharacters[i].ToString());
            typedLetter = true;
            break;
        }

        if (!typedLetter && keyboard.PressDown())
        {
            _typableCharactersIndex--;

            if (_typableCharactersIndex < 0)
                _typableCharactersIndex = TypableCharacters.Length - 1;
        }
        else if (!typedLetter && keyboard.PressUp())
        {
            _typableCharactersIndex++;

            if (_typableCharactersIndex >= TypableCharacters.Length)
                _typableCharactersIndex = 0;
        }
        else if (!typedLetter && (keyboard.IsKeyPressed(Keys.Back) || keyboard.IsKeyPressed(Keys.Delete) || keyboard.PressLeft()))
        {
            var item = _items[_editingIndex];

            switch (_charactersLeftToEdit)
            {
                case 1:
                    item.Name = $"{item.Name.Substring(0, 1)}  ";
                    _charactersLeftToEdit = 2;
                    break;
                case 2:
                    item.Name = "   ";
                    _charactersLeftToEdit = 3;
                    break;
            }
        }
        else if (!typedLetter && keyboard.IsFirePressed())
        {
            Typed(TypableCharacters[_typableCharactersIndex].ToString());
        }

        if (_items[_editingIndex].Name != oldName)
            RefreshStrings();
        if (!StillEditing)
            TypedName = _items[_editingIndex].Name;
    }

    private void Typed(string character)
    {
        if (!StillEditing)
            return;

        var index = 3 - _charactersLeftToEdit;
        var item = _items[_editingIndex];
        item.Name = item.Name[..index] + character + new string(' ', 2 - index);
        _charactersLeftToEdit--;
        _typableCharactersIndex = 0;
    }

    public void Draw(SpriteBatch spriteBatch, ulong ticks)
    {
        var strings = _strings;
        var y = _listY;

        if (StillEditing)
        {
            for (var i = 0; i < strings.Length; i++)
            {
                if (i == _editingIndex)
                {
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
