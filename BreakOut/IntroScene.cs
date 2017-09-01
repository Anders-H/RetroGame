using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGame;
using RetroGame.Input;

namespace BreakOut
{
    public class IntroScene : IScene
    {
        private IScene ScenePointer { get; set; }
        private KeyboardStateChecker Keyboard { get; } = new KeyboardStateChecker();

        public IntroScene(IScene scenePointer)
        {
            ScenePointer = scenePointer;
        }
        public void Update(GameTime gameTime)
        {
            Keyboard.UpdateState();
            if (Keyboard.IsKeyDown(Keys.LeftControl))
                ScenePointer = new GameScene(ScenePointer);
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.SpriteFont, "Press Fire (Left Ctrl) to start!", new Vector2(10, 10), Color.White);
        }
    }
}
