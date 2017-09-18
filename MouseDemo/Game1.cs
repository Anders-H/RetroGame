using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGame.Input;

namespace MouseDemo
{
    public class Game1 : RetroGame.RetroGame
    {
        public static Texture2D PointerTexture { get; set; }
        public static Texture2D StampTexture { get; set; }
        public Game1() : base(800, 600, false, false)
        {
        }
        protected override void LoadContent()
        {
            var pointerPixels = new Color[MousePointer.Width * MousePointer.Height];
            for (var i = 0; i < pointerPixels.Length; i++)
                pointerPixels[i] = Color.White;
            PointerTexture = new Texture2D(GraphicsDevice, MousePointer.Width, MousePointer.Height);
            PointerTexture.SetData(pointerPixels);

            var ballPixels = new Color[Stamp.Width * Stamp.Height];
            for (var i = 0; i < ballPixels.Length; i++)
                ballPixels[i] = Color.White;
            StampTexture = new Texture2D(GraphicsDevice, Stamp.Width, Stamp.Height);
            StampTexture.SetData(ballPixels);
            CurrentScene = new MyScene(this);
            base.LoadContent();
        }
    }
    public abstract class Sprite
    {
        public int X { get; set; }
        public int Y { get; set; }
        public abstract void Draw(SpriteBatch s);
    }
    public class MousePointer : Sprite
    {
        public const int Width = 5;
        public const int Height = 5;
        public override void Draw(SpriteBatch s) => s.Draw(Game1.PointerTexture, new Vector2(X, Y), Color.Green);
    }
    public class Stamp : Sprite
    {
        public const int Width = 25;
        public const int Height = 25;
        public override void Draw(SpriteBatch s) => s.Draw(Game1.StampTexture, new Vector2(X, Y), Color.Blue);
        public bool Intersects(Point p) => new Rectangle(X, Y, Width, Height).Intersects(new Rectangle(p, new Point(1, 1)));
    }
    public class MyScene : RetroGame.Scene
    {
        private MousePointer MousePointer { get; } = new MousePointer();
        private List<Stamp> Stamps { get; } = new List<Stamp>();
        private MouseStateChecker Mouse { get; } = new MouseStateChecker();
        public MyScene(RetroGame.RetroGame parent) : base(parent)
        {
        }
        public override void Update(GameTime gameTime)
        {
            Mouse.UpdateState();
            MousePointer.X = Mouse.Location.X - 2;
            MousePointer.Y = Mouse.Location.Y - 2;
            if (Mouse.LeftButtonPressed)
                Stamps.Add(new Stamp {X = Mouse.Location.X - 12, Y = Mouse.Location.Y - 12});
            else if (Mouse.RightButtonPressed)
                Stamps.Remove(Stamps.FirstOrDefault(x => x.Intersects(Mouse.Location)));
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Stamps.ForEach(x => x.Draw(spriteBatch));
            MousePointer.Draw(spriteBatch);
        }
    }
}
