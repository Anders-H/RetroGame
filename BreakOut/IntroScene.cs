using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGameClasses;
using RetroGameClasses.Input;
using RetroGameClasses.Text;

namespace BreakOut
{
    public class IntroScene : Scene
    {
        private KeyboardStateChecker Keyboard { get; } = new KeyboardStateChecker();
        private TextBlock Text { get; } = new TextBlock();

        public IntroScene(RetroGame retroGame) : base(retroGame)
        {
            Text.SetText(1, 1, "press fire (left ctrl) to start!");
        }
        public override void Update(GameTime gameTime)
        {
            Keyboard.UpdateState();
            if (Keyboard.IsKeyPressed(Keys.LeftControl))
                Parent.CurrentScene = new GameScene(Parent);
            if (Keyboard.IsKeyPressed(Keys.Escape))
                Exit();
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Text.Draw(spriteBatch, ColorPalette.White);
        }
    }
}
