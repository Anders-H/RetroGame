using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGame;
using RetroGame.Input;
using RetroGame.Scene;
using RetroGame.Sprites;
using RetroGame.Text;

namespace BatchDemo;

public class StarsScene : Scene
{
    private bool _adding = true;
    private KeyboardStateChecker Keyboard { get; } = new();
    private readonly TextBlock _textBlock = new(CharacterSet.Uppercase);
    private readonly Batch _batch1 = new();
    private readonly Batch _batch2 = new();
    private readonly Batch _batch3 = new();
    private readonly Batch _batch4 = new();
        
    public StarsScene(RetroGame.RetroGame parent) : base(parent)
    {
        AddToAutoUpdate(Keyboard, _batch1, _batch2, _batch3, _batch4);
        AddToAutoDraw(_batch1, _batch2, _batch3, _batch4);
    }
        
    private int Count =>
        _batch1.Count + _batch2.Count + _batch3.Count + _batch4.Count;
        
    public override void Update(GameTime gameTime, ulong ticks)
    {
        if (Count < 1000 && _adding)
        {
            var s = new BatchSprite();
            if (s.SpeedX > 3)
                _batch4.AppendLast(s);
            else if (s.SpeedX > 2)
                _batch3.AppendLast(s);
            else if (s.SpeedX > 1)
                _batch2.AppendLast(s);
            else
                _batch1.AppendLast(s);
        }
        else if (_adding)
            _adding = false;
        else if (Count > 0)
        {
            if (_batch1.Count > 0)
                _batch1.RemoveFirst();
            if (_batch2.Count > 0)
                _batch2.RemoveFirst();
            if (_batch3.Count > 0)
                _batch3.RemoveFirst();
            if (_batch4.Count > 0)
                _batch4.RemoveFirst();
        }
            
        if (Keyboard.IsKeyPressed(Keys.Escape))
            Exit();
            
        base.Update(gameTime, ticks);
    }
    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, ticks, spriteBatch);
            
        _textBlock.DirectDraw(
            spriteBatch,
            0,
            0,
            $"sprites in batch: {Count}",
            ColorPalette.Yellow);
    }
}