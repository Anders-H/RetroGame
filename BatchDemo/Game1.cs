using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroGameClasses;
using RetroGameClasses.RetroTextures;
using RetroGameClasses.Sprites;
using RetroGameClasses.Text;

namespace BatchDemo
{
    public class Game1 : RetroGame
    {
        public static RetroTexture Star { get; set; }
        public Game1() : base(320, 200, RetroDisplayMode.WindowedWithUpscaling) { }
        protected override void LoadContent()
        {
            Star = RetroTexture.ScaffoldSimpleTexture(GraphicsDevice, 1, 1, Color.White);
            CurrentScene = new StarsScene(this);
            BackColor = Color.Black;
            base.LoadContent();
        }
    }

    public class BatchSprite : Sprite, IBatchSprite
    {
        private static Random Rnd { get; } = new Random();
        public float SpeedX { get; }
        private Color Color { get; }
        public bool IsAlive => true;
        public BatchSprite()
        {
            Y = Rnd.Next(0, 200);
            SpeedX = (float)(Rnd.NextDouble() * 4);
            if (SpeedX > 3)
                Color = ColorPaletteHelper.GetColor(ColorPalette.White);
            else if (SpeedX > 2)
                Color = ColorPaletteHelper.GetColor(ColorPalette.LightGrey);
            else if (SpeedX > 1)
                Color = ColorPaletteHelper.GetColor(ColorPalette.Grey);
            else
                Color = ColorPaletteHelper.GetColor(ColorPalette.DarkGrey);
        }
        public void Act()
        {
            X -= SpeedX;
            if (X < 0)
            {
                X = 320;
                Y = Rnd.Next(0, 200);
            }
        }
        public void Draw(SpriteBatch spriteBatch) => Draw(spriteBatch, Game1.Star, 0, Color);
    }

    public class StarsScene : Scene
    {
        private bool _adding = true;
        private readonly TextBlock _textBlock = new TextBlock(CharacterSet.Uppercase);
        private readonly Batch _batch1 = new Batch();
        private readonly Batch _batch2 = new Batch();
        private readonly Batch _batch3 = new Batch();
        private readonly Batch _batch4 = new Batch();
        public StarsScene(RetroGame parent) : base(parent) { }
        private int Count => _batch1.Count + _batch2.Count + _batch3.Count + _batch4.Count;
        public override void Update(GameTime gameTime)
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
            _batch1.Act();
            _batch2.Act();
            _batch3.Act();
            _batch4.Act();
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _batch1.Draw(spriteBatch);
            _batch2.Draw(spriteBatch);
            _batch3.Draw(spriteBatch);
            _batch4.Draw(spriteBatch);
            _textBlock.DirectDraw(spriteBatch, 0, 0, $"sprites in batch: {Count}", ColorPalette.Yellow);
        }
    }
}
