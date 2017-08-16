using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RetroGame
{
    public class RetroGame : Game
    {
        private GraphicsDeviceManager G { get; }
        public int ResolutionWidth { get; }
        public int ResolutionHeight { get; }
        public int PhysicalWidth { get; }
        public int PhysicalHeight { get; }
        private SpriteBatch SpriteBatch { get; set; }
        private RenderTarget2D RenderTarget { get; set; }
        public IScene CurrentScene { get; set; }
        public RetroGame(int resolutionWidth, int resolutionHeight, bool fullScreen)
        {
            G = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            ResolutionWidth = resolutionWidth;
            ResolutionHeight = resolutionHeight;
            if (fullScreen)
            {
                PhysicalWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                PhysicalHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                G.PreferredBackBufferWidth = PhysicalWidth;
                G.PreferredBackBufferHeight = PhysicalHeight;
                G.IsFullScreen = true;
            }
            else
            {
                var w = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                var h = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                if (w >= ResolutionWidth * 4 && h >= ResolutionHeight * 4)
                {
                    PhysicalWidth = ResolutionWidth * 3;
                    PhysicalHeight = ResolutionHeight * 3;
                }
                else if (w >= ResolutionWidth * 3 && h >= ResolutionHeight * 3)
                {
                    PhysicalWidth = ResolutionWidth * 2;
                    PhysicalHeight = ResolutionHeight * 2;
                }
                else
                {
                    PhysicalWidth = ResolutionWidth;
                    PhysicalHeight = ResolutionHeight;
                }
                G.PreferredBackBufferWidth = PhysicalWidth;
                G.PreferredBackBufferHeight = PhysicalHeight;
                G.IsFullScreen = false;
            }
        }
        protected override void Initialize()
        {
            RenderTarget = new RenderTarget2D(GraphicsDevice, ResolutionWidth, ResolutionHeight);
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            base.Initialize();
        }
        protected sealed override void Update(GameTime gameTime)
        {
            CurrentScene?.Update(gameTime);
            base.Update(gameTime);
        }
        protected sealed override void Draw(GameTime gameTime)
        {
            if (CurrentScene != null)
            {
                G.GraphicsDevice.SetRenderTarget(RenderTarget);
                G.GraphicsDevice.Clear(Color.Black);
                SpriteBatch.Begin();
                CurrentScene.Draw(gameTime, SpriteBatch);
                SpriteBatch.End();
                G.GraphicsDevice.SetRenderTarget(null);
                SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
                SpriteBatch.Draw(RenderTarget, new Rectangle(0, 0, PhysicalWidth, PhysicalHeight), new Rectangle(0, 0, ResolutionWidth, ResolutionHeight), Color.White);
                SpriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
}
