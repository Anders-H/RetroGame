using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using RetroGame.Scene;

namespace RetroGame.Text;

public class TypeWriter : ISceneActor
{
    private readonly int _x;
    private readonly int _y;
    private readonly int _charactersHeight;
    private readonly List<string> _rows;
    private int _currentCharacter;
    private readonly string[] _visibleRows;
    private readonly TextBlock _textBlock;
    private readonly ColorPalette _color;

    public TypeWriter(int x, int y, int charactersHeight, ColorPalette color)
    {
        _x = x;
        _y = y;
        _charactersHeight = charactersHeight;
        _rows = [];
        _visibleRows = new string[charactersHeight];
        _textBlock = new TextBlock(CharacterSet.Uppercase);
        _color = color;
    }

    public void SetText(params string[] text)
    {
        foreach (var t in text)
            _rows.Add(t);

        _rows.Add("");
    }

    public void Act(ulong ticks)
    {
        if (_rows.Count <= 0 && ticks % 100 == 0)
        {
            PopRow();
            return;
        }

        if (ticks % 4 != 0 || _rows.Count <= 0)
        {
            return;
        }

        _currentCharacter++;
        var row = _visibleRows.LastOrDefault() ?? "";

        if (row.Length < _currentCharacter)
        {
            PopRow();
            _currentCharacter = 0;
        }

    }

    private void PopRow()
    {
        for (var y = 1; y < _visibleRows.Length; y++)
            _visibleRows[y - 1] = _visibleRows[y];

        if (_rows.Count > 0)
        {
            _visibleRows[^1] = _rows[0];
            _rows.RemoveAt(0);
        }
        else
        {
            _visibleRows[^1] = "";
        }
    }

    public void Draw(SpriteBatch spriteBatch, ulong ticks)
    {
        var yPos = _y;

        for (var y = 0; y < _charactersHeight; y++)
        {
            var row = _visibleRows[y] ?? "";

            if (y < _charactersHeight - 1)
            {
                _textBlock.DirectDraw(spriteBatch, _x, yPos, row, _color);
            }
            else
            {
                if (row.Length > 0 && _currentCharacter > 0)
                    _textBlock.DirectDraw(spriteBatch, _x, yPos, _currentCharacter < row.Length ? row.Substring(0, _currentCharacter) : row, _color);
            }

            yPos += 8;
        }
    }
}