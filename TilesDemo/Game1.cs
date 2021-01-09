using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGameClasses;
using RetroGameClasses.Input;
using RetroGameClasses.RetroTextures;
using RetroGameClasses.Scene;
using RetroGameClasses.Tilemaps;

namespace TilesDemo
{
    public class Game1 : RetroGame
    {
        internal static RetroTexture TilesTexture { get; set; }
        public static RetroTexture ResolutionReference { get; set; }

        public Game1() : base(320, 200, RetroDisplayMode.Fullscreen, true)
        {
        }

        protected override void LoadContent()
        {
            TilesTexture = new RetroTexture(GraphicsDevice, 32, 32, 10);
            TilesTexture.SetData(Content.Load<Texture2D>("test_tiles"));
            ResolutionReference = new RetroTexture(GraphicsDevice, 320, 200, 1);
            ResolutionReference.SetData(Content.Load<Texture2D>("resolution_reference"));
            BorderColor = ColorPaletteHelper.GetColor(ColorPalette.LightGreen);
            BackColor = ColorPaletteHelper.GetColor(ColorPalette.DarkGrey);
            CurrentScene = new TextureDemoScene(this);
        }
    }

    public class TextureDemoScene: Scene
    {
        private KeyboardStateChecker Keyboard { get; } = new KeyboardStateChecker();
        private Tilemap TilesLayer1 { get; }
        private Tilemap TilesLayer2 { get; }
        private Tilemap TilesLayer3 { get; }

        public TextureDemoScene(RetroGame parent) : base(parent)
        {
            var rnd = new Random();
            
            TilesLayer1 = new Tilemap(Game1.TilesTexture, 20, 3, 32, 32, 11, 3)
            {
                Delay = 3,
                Repeat = true,
                Y = 32
            };
            
            for (var i = 0; i < 200; i++)
                TilesLayer1.SetValue(rnd.Next(20), rnd.Next(3), rnd.Next(10));
            
            TilesLayer2 = new Tilemap(Game1.TilesTexture, 10, 5, 32, 32, 11, 5)
            {
                Delay = 2,
                Repeat = true
            };
            
            for (var x = 0; x < 10; x++)
            {
                TilesLayer2.SetValue(x, 0, 0);
                TilesLayer2.SetValue(x, 2, 4);
                TilesLayer2.SetValue(x, 4, 0);
            }
            
            TilesLayer2.SetValue(0, 1, 0);
            TilesLayer2.SetValue(0, 2, 0);
            TilesLayer2.SetValue(0, 3, 0);
            TilesLayer2.SetValue(5, 2, null);
            
            TilesLayer3 = new Tilemap(Game1.TilesTexture, 100, 3, 32, 32, 11, 3)
            {
                Repeat = true,
                Y = 128
            };
            
            for (var i = 0; i < 200; i++)
                TilesLayer3.SetValue(rnd.Next(100), rnd.Next(3), rnd.Next(10));
            
            AddToAutoUpdate(Keyboard, TilesLayer1, TilesLayer2);
            AddToAutoDraw(TilesLayer1, TilesLayer2, TilesLayer3);
        }

        public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, ticks, spriteBatch);
            Game1.ResolutionReference.Draw(spriteBatch, 0, 0, 0);
        }

        public override void Update(GameTime gameTime, ulong ticks)
        {
            //Quit.
            if (Keyboard.IsKeyPressed(Keys.Escape))
                Exit();

            TilesLayer3.Y = (int)(128.0 + Math.Sin(ticks/20.0) * 20);
            TilesLayer3.Act(ticks);
            base.Update(gameTime, ticks);
        }
    }
}