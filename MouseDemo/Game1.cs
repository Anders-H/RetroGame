using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroGame.Input;
using RetroGame.Sprites;
using Texture = RetroGame.Textures.Texture;

namespace MouseDemo
{
    public class Game1 : RetroGame.RetroGame
    {
        public static Texture2D PointerTexture { get; set; }
        public static Texture2D MouseStampTexture { get; set; }
        public Game1() : base(800, 600, false, false)
        {
        }
        protected override void LoadContent()
        {
            PointerTexture = Texture.ScaffoldTexture2D(GraphicsDevice, 5, 5, Color.White);
            MouseStampTexture = Texture.ScaffoldTexture2D(GraphicsDevice, 25, 25, Color.White);
            CurrentScene = new MyScene(this);
            base.LoadContent();
        }
    }
    public class MousePointer : Sprite
    {
        public MousePointer()
        {
            Width = 5;
            Height = 5;
        }
        public override void Draw(SpriteBatch s) => s.Draw(Game1.PointerTexture, new Vector2(X, Y), Color.Green);
    }
    public class Stamp : Sprite
    {
        public Stamp()
        {
            Width = 25;
            Height = 25;
        }
        public override void Draw(SpriteBatch s) => s.Draw(Game1.MouseStampTexture, Location, Color.Blue);
        public bool Intersects(Point p) => new Rectangle(X, Y, Width, Height).Intersects(new Rectangle(p, new Point(1, 1)));
    }
    public class MyScene : RetroGame.Scene
    {
        private MousePointer MousePointer { get; } = new MousePointer();
        private List<Stamp> MouseStamps { get; } = new List<Stamp>();
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
                MouseStamps.Add(new Stamp {X = Mouse.Location.X - 12, Y = Mouse.Location.Y - 12});
            else if (Mouse.RightButtonPressed)
                MouseStamps.Remove(MouseStamps.FirstOrDefault(x => x.Intersects(Mouse.Location)));
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            MouseStamps.ForEach(x => spriteBatch.Draw(Game1.MouseStampTexture, new Vector2(x.X, x.Y), Color.Blue));
            MousePointer.Draw(spriteBatch);
        }
    }
}
