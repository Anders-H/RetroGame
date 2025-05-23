﻿using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroGame.Scene;

namespace RetroGame.Text;

public class TextBlock : ISceneActor
{
    private Petscii?[,] Characters { get; }
    private ColorPalette[,] ColorMap { get; }
    private CharacterSet CharacterSet { get; }
    private string _pendingAppendingCharacters;
    private int _pendingAppendingCharactersPointer;
    private int _pendingAppendingCharactersTickDelay;
    private int _pendingAppendingCharactersX;
    public bool IsReady => _pendingAppendingCharacters == null;
    public int Columns => Characters.GetLength(0);
    public int Rows => Characters.GetLength(1);
    public int DrawOffsetX { get; set; }
    public int DrawOffsetY { get; set; }

    public TextBlock() : this(40, 25, CharacterSet.Lowercase)
    {
    }

    public TextBlock(CharacterSet characterSet) : this(40, 25, characterSet)
    {
    }

    public TextBlock(int columns, int rows, CharacterSet characterSet)
    {
        Characters = new Petscii?[columns, rows];
        ColorMap = new ColorPalette[columns, rows];

        for (var y = 0; y < rows; y++)
            for (var x = 0; x < columns; x++)
                ColorMap[x, y] = ColorPalette.LightBlue;

        CharacterSet = characterSet;
    }
    public void SetText(int x, int y, string text)
    {
        var startX = x;

        if (string.IsNullOrEmpty(text))
            return;

        foreach (var t in text)
        {
            Characters[x, y] = PetsciiHelper.GetCharacter(t);
            x++;

            if (x < Characters.GetLength(0))
                continue;

            y++;
            x = startX;

            if (y >= Characters.GetLength(1))
                return;
        }
    }
    public void AppendRows(string text, int tickDelay, bool appendLinebreak)
    {
        if (text == null || text.Length <= 0)
            return;

        _pendingAppendingCharacters = WordWrapper.WordWrap(Columns, text).Replace("\r\n", "|");

        if (appendLinebreak)
        {
            if (_pendingAppendingCharacters.Length > 1 && _pendingAppendingCharacters.Last() != '|')
                _pendingAppendingCharacters += '|';
        }
        else
        {
            if (_pendingAppendingCharacters.Length > 1 && _pendingAppendingCharacters.Last() == '|')
                _pendingAppendingCharacters = _pendingAppendingCharacters[..^1];
        }

        _pendingAppendingCharactersPointer = -1;
        _pendingAppendingCharactersTickDelay = tickDelay;
        _pendingAppendingCharactersX = 0;
    }

    public void Act(ulong ticks)
    {
        if (IsReady)
            return;

        if (_pendingAppendingCharactersTickDelay <= 0 || ticks % (ulong)_pendingAppendingCharactersTickDelay != 0)
            return;

        if (_pendingAppendingCharactersPointer >= _pendingAppendingCharacters.Length)
        {
            _pendingAppendingCharacters = null;
            return;
        }

        if (_pendingAppendingCharactersPointer < 0 || _pendingAppendingCharacters[_pendingAppendingCharactersPointer] == '|')
        {
            _pendingAppendingCharactersX = 0;
            _pendingAppendingCharactersPointer++;
            ScrollUp();
            return;
        }

        Characters[_pendingAppendingCharactersX, Rows - 1] = PetsciiHelper.GetCharacter(_pendingAppendingCharacters[_pendingAppendingCharactersPointer]);
        _pendingAppendingCharactersPointer++;
        _pendingAppendingCharactersX++;
    }

    public void ScrollUp()
    {
        for (var y = 0; y < Rows - 1; y++)
            for (var x = 0; x < Columns; x++)
                Characters[x, y] = Characters[x, y + 1];

        for (var x = 0; x < Columns; x++)
            Characters[x, Rows - 1] = null;

        for (var y = 0; y < Rows - 1; y++)
            for (var x = 0; x < Columns; x++)
                ColorMap[x, y] = ColorMap[x, y + 1];
    }

    public void Draw(SpriteBatch spriteBatch, ColorPalette color) =>
        Draw(spriteBatch, ColorPaletteHelper.GetColor(color));

    public void Draw(SpriteBatch spriteBatch, Color color)
    {
        for (var y = 0; y < Characters.GetLength(1); y++)
        {
            for (var x = 0; x < Characters.GetLength(0); x++)
            {
                var c = Characters[x, y];

                if (c == null)
                    continue;

                var sourceX = (int)CharacterSet * 64 + (int)c / 16 * 8;
                var sourceY = (int)c % 16 * 8;
                var pos = new Vector2(x * 8 + DrawOffsetX, y * 8 + DrawOffsetY);
                spriteBatch.Draw(RetroGame.Font64, pos, new Rectangle(sourceX, sourceY, 8, 8), color);
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch, ulong ticks)
    {
        for (var y = 0; y < Characters.GetLength(1); y++)
        {
            for (var x = 0; x < Characters.GetLength(0); x++)
            {
                var c = Characters[x, y];

                if (c == null)
                    continue;

                var sourceX = (int)CharacterSet * 64 + (int)c / 16 * 8;
                var sourceY = (int)c % 16 * 8;
                var pos = new Vector2(x * 8 + DrawOffsetX, y * 8 + DrawOffsetY);
                spriteBatch.Draw(RetroGame.Font64, pos, new Rectangle(sourceX, sourceY, 8, 8), ColorPaletteHelper.GetColor(ColorMap[x, y]));
            }
        }
    }
    public void DirectDraw(SpriteBatch spriteBatch, int x, int y, string text, ColorPalette color) =>
        DirectDraw(spriteBatch, x, y, text, ColorPaletteHelper.GetColor(color));

    public void DirectDraw(SpriteBatch spriteBatch, int x, int y, string text, Color color)
    {
        if (string.IsNullOrWhiteSpace(text))
            return;

        for (var i = 0; i < text.Length; i++)
        {
            var c = text[i];

            if (c == ' ')
                continue;

            var pet = PetsciiHelper.GetCharacter(c);
            int sourceX, sourceY;

            switch (CharacterSet)
            {
                case CharacterSet.Uppercase:
                case CharacterSet.UppercaseInverted:
                    switch (pet)
                    {
                        case Petscii.Å:
                        case Petscii.ShiftÅ:
                            sourceX = 256;
                            sourceY = 0;
                            break;
                        case Petscii.Ä:
                        case Petscii.ShiftÄ:
                            sourceX = 256;
                            sourceY = 8;
                            break;
                        case Petscii.Ö:
                        case Petscii.ShiftÖ:
                            sourceX = 256;
                            sourceY = 16;
                            break;
                        default:
                            sourceX = (int)CharacterSet * 64 + (int)pet / 16 * 8;
                            sourceY = (int)pet % 16 * 8;
                            break;
                    }
                    break;
                case CharacterSet.Lowercase:
                case CharacterSet.LowercaseInverted:
                    switch (pet)
                    {
                        case Petscii.Å:
                            sourceX = 256;
                            sourceY = 24;
                            break;
                        case Petscii.Ä:
                            sourceX = 256;
                            sourceY = 32;
                            break;
                        case Petscii.Ö:
                            sourceX = 256;
                            sourceY = 40;
                            break;
                        case Petscii.ShiftÅ:
                            sourceX = 256;
                            sourceY = 0;
                            break;
                        case Petscii.ShiftÄ:
                            sourceX = 256;
                            sourceY = 8;
                            break;
                        case Petscii.ShiftÖ:
                            sourceX = 256;
                            sourceY = 16;
                            break;
                        default:
                            sourceX = (int)CharacterSet * 64 + (int)pet / 16 * 8;
                            sourceY = (int)pet % 16 * 8;
                            break;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            spriteBatch.Draw(RetroGame.Font64, new Vector2(x + i * 8, y), new Rectangle(sourceX, sourceY, 8, 8), color);
        }
    }
}