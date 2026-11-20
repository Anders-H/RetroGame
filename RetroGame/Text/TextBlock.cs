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

    public void Draw(SpriteBatch spriteBatch, Color color) =>
        DrawCharacters(spriteBatch, color);

    public void Draw(SpriteBatch spriteBatch, ulong ticks) =>
        DrawCharacters(spriteBatch, null);

    private void DrawCharacters(SpriteBatch spriteBatch, Color? color)
    {
        var font = EmbeddedFont.GetTexture(spriteBatch.GraphicsDevice);
        for (var y = 0; y < Rows; y++)
            for (var x = 0; x < Columns; x++)
            {
                var character = Characters[x, y];
                if (character == null)
                    continue;
                var position = new Vector2(x * 8 + DrawOffsetX, y * 8 + DrawOffsetY);

                spriteBatch.Draw(font, position, GetCharacterBounds(character.Value),
                    color ?? ColorPaletteHelper.GetColor(ColorMap[x, y]));
            }
    }

    internal Rectangle GetCharacterBounds(Petscii character)
    {
        var specialIndex = character switch
        {
            Petscii.Å or Petscii.ShiftÅ => 0,
            Petscii.Ä or Petscii.ShiftÄ => 1,
            Petscii.Ö or Petscii.ShiftÖ => 2,
            _ => -1
        };

        if (specialIndex >= 0)
        {
            var lowercase = (CharacterSet is CharacterSet.Lowercase or CharacterSet.LowercaseInverted)
                && (character is Petscii.Å or Petscii.Ä or Petscii.Ö);
            return new Rectangle(256, (specialIndex + (lowercase ? 3 : 0)) * 8, 8, 8);
        }

        return new Rectangle((int)CharacterSet * 64 + (int)character / 16 * 8,
            (int)character % 16 * 8, 8, 8);
    }

    public void DirectDraw(SpriteBatch spriteBatch, int x, int y, string text, ColorPalette color) =>
        DirectDraw(spriteBatch, x, y, text, ColorPaletteHelper.GetColor(color));

    public void DirectDraw(SpriteBatch spriteBatch, int x, int y, string text, Color color)
    {
        if (string.IsNullOrWhiteSpace(text))
            return;

        var font = EmbeddedFont.GetTexture(spriteBatch.GraphicsDevice);
        for (var i = 0; i < text.Length; i++)
        {
            var c = text[i];

            if (c == ' ')
                continue;

            var pet = PetsciiHelper.GetCharacter(c);
            spriteBatch.Draw(font, new Vector2(x + i * 8, y), GetCharacterBounds(pet), color);
        }
    }
}
