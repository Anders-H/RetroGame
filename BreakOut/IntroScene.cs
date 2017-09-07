using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGame.Input;

namespace BreakOut
{
    public class IntroScene : RetroGame.Scene
    {
        private KeyboardStateChecker Keyboard { get; } = new KeyboardStateChecker();

        public IntroScene(RetroGame.RetroGame retroGame) : base(retroGame)
        {
        }
        public override void Update(GameTime gameTime)
        {
            Keyboard.UpdateState();
            if (Keyboard.IsKeyPressed(Keys.LeftControl))
                Parent.CurrentScene = new GameScene(Parent);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.SpriteFont, "Press Fire (Left Ctrl) to start!", new Vector2(10, 10), Color.White);
        }
    }
}
