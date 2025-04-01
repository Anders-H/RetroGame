using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RetroGame.Scene;

namespace RetroGame.Text;

public class TypeWriter : IRetroActor, ISceneActor
{
    private readonly int _x;
    private readonly int _y;
    private readonly int _charactersWidth;
    private readonly int _charactersHeight;
    private readonly List<string> _rows;
    private int _currentRow;
    private int _currentCharacter;

    public TypeWriter(int x, int y, int charactersWidth, int charactersHeight)
    {
        _x = x;
        _y = y;
        _charactersWidth = charactersWidth;
        _charactersHeight = charactersHeight;
        _rows = [];
    }

    public void SetText(params string[] text)
    {
        foreach (var t in text)
            _rows.Add(t);

        _rows.Add("");
    }

    public void Act(ulong ticks)
    {
        
    }

    public void Draw(SpriteBatch spriteBatch, ulong ticks)
    {
    }
}