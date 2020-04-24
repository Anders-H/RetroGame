using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGameClasses;
using RetroGameClasses.Input;
using RetroGameClasses.Scene;
using RetroGameClasses.Text;

namespace BatchDemo
{
    public class IntroScene : Scene
    {
        private KeyboardStateChecker Keyboard { get; } = new KeyboardStateChecker();
        private readonly TextBlock _textBlock = new TextBlock(CharacterSet.Uppercase);
        
        public IntroScene(RetroGame parent) : base(parent)
        {
        }

        public override void Update(GameTime gameTime, ulong ticks)
        {
            Keyboard.Act(ticks);
            
            if (Keyboard.IsKeyPressed(Keys.Escape))
                Exit();
            else if (Keyboard.IsKeyPressed(Keys.Enter))
                Parent.CurrentScene = new StarsScene(Parent);
        }

        public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
        {
            _textBlock.DirectDraw(spriteBatch, 0, 0, "sprite batch demo", ColorPalette.White);
            _textBlock.DirectDraw(spriteBatch, 0, 16, "this demo will display a starfield of", ColorPalette.LightBlue);
            _textBlock.DirectDraw(spriteBatch, 0, 24, "1000 stars.", ColorPalette.LightBlue);
            _textBlock.DirectDraw(spriteBatch, 0, 40, "press enter to start", ColorPalette.Blue);
            _textBlock.DirectDraw(spriteBatch, 0, 48, "press esc to quit", ColorPalette.Blue);
        }
    }
}