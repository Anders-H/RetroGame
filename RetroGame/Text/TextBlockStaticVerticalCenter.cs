using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RetroGame.Scene;

namespace RetroGame.Text;

public class TextBlockStaticVerticalCenter : ISceneActor
{
    private readonly TextBlock _textBlock;
    private readonly int _y;
    private readonly List<string> _text;
    private readonly List<int> _verticalPositions;

    public TextBlockStaticVerticalCenter(int resolutionWidth, int y, params string[] text)
    {
        _textBlock = new TextBlock();
        _y = y;
        _verticalPositions = [];
        _text = [];
        _text.AddRange(text);

        foreach (var t in _text)
            _verticalPositions.Add((resolutionWidth - t.Length * 8) / 2);
    }

    public void Act(ulong ticks)
    {
    }

    public void Draw(SpriteBatch spriteBatch, ulong ticks)
    {
        var y = _y;

        for (var i = 0; i < _text.Count; i++)
        {
            var text = _text[i];
            var x = _verticalPositions[i];
            _textBlock.DirectDraw(spriteBatch, _verticalPositions[i], y, text, ColorPalette.White);
            y += 8;
        }
    }
}