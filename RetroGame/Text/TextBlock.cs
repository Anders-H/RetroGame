using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RetroGameClasses.Text
{
    public class TextBlock
    {
        private Petscii?[,] Characters { get; }
        private CharacterSet CharacterSet { get; }
        public TextBlock() : this(40, 25, CharacterSet.Lowercase) { }
        public TextBlock(CharacterSet characterSet) : this(40, 25, characterSet) { }
        public TextBlock(int columns, int rows, CharacterSet characterSet)
        {
            Characters = new Petscii?[columns, rows];
            CharacterSet = characterSet;
        }
        public void SetText(int x, int y, string text)
        {
            var startX = x;
            if (string.IsNullOrEmpty(text))
                return;
            foreach (var t in text)
            {
                Characters[x, y] = GetCharacter(t);
                x++;
                if (x < Characters.GetLength(0))
                    continue;
                y++;
                x = startX;
                if (y >= Characters.GetLength(1))
                    return;
            }
        }
        private static Petscii GetCharacter(char c)
        {
            switch (c)
            {
                case '@': return Petscii.At;
                case 'a': return Petscii.A;
                case 'b': return Petscii.B;
                case 'c': return Petscii.C;
                case 'e': return Petscii.E;
                case 'f': return Petscii.F;
                case 'h': return Petscii.H;
                case 'i': return Petscii.I;
                case 'l': return Petscii.L;
                case 'n': return Petscii.N;
                case 'o': return Petscii.O;
                case 'p': return Petscii.P;
                case 'r': return Petscii.R;
                case 's': return Petscii.S;
                case 't': return Petscii.T;
                case ':': return Petscii.Colon;
                case '0': return Petscii.Num0;
                case '1': return Petscii.Num1;
                case '2': return Petscii.Num2;
                case '3': return Petscii.Num3;
                case '4': return Petscii.Num4;
                case '5': return Petscii.Num5;
                case '6': return Petscii.Num6;
                case '7': return Petscii.Num7;
                case '8': return Petscii.Num8;
                case '9': return Petscii.Num9;
                case ' ': return Petscii.Space;
                case '!': return Petscii.Exclamation;
                case '(': return Petscii.LeftParentheses;
                case ')': return Petscii.RightParentheses;
                default: throw new ArgumentOutOfRangeException(c.ToString());
            }
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
                    var sourceX = (int)CharacterSet*64+(int)c/16*8;
                    var sourceY = (int)c%16*8;
                    spriteBatch.Draw(RetroGame.Font64, new Vector2(x*8, y*8), new Rectangle(sourceX, sourceY, 8, 8), color);
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
                var pet = GetCharacter(c);
                var sourceX = (int)CharacterSet*64 + (int)pet/16*8;
                var sourceY = (int)pet%16*8;
                spriteBatch.Draw(RetroGame.Font64, new Vector2(x + i*8, y), new Rectangle(sourceX, sourceY, 8, 8), color);
            }
        }
    }
}
