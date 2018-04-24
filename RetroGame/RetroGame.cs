using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RetroGame
{
    public class RetroGame : Game
    {
        public bool Upscaling { get; }
        public bool Border { get; }
        public bool Fullscreen { get; }
        private GraphicsDeviceManager G { get; }
        public int ResolutionWidth { get; }
        public int ResolutionHeight { get; }
        public int PhysicalWidth { get; }
        public int PhysicalHeight { get; }
        public int BorderOffsetX { get; }
        public int BorderOffsetY { get; }
        private SpriteBatch SpriteBatch { get; set; }
        private RenderTarget2D RenderTarget { get; set; }
        public Scene CurrentScene { get; set; }
        public RetroGame(int resolutionWidth, int resolutionHeight, DisplayMode displayMode)
        {
            Upscaling = DisplayModeHelper.Upscaling(displayMode);
            Border = DisplayModeHelper.Border(displayMode);
            Fullscreen = DisplayModeHelper.Fullscreen(displayMode);
            G = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            ResolutionWidth = resolutionWidth;
            ResolutionHeight = resolutionHeight;
            if (Fullscreen)
            {
                if (Border)
                {
                    PhysicalWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                    PhysicalHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                    G.PreferredBackBufferWidth = PhysicalWidth;
                    G.PreferredBackBufferHeight = PhysicalHeight;
                    var xdiff = PhysicalWidth - ResolutionWidth;
                    var ydiff = PhysicalHeight - ResolutionHeight;
                    var transpose = 1.3;
                    if (xdiff > ydiff)
                    {
                        PhysicalHeight = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height*0.8);
                        var factor = ((double)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height/(double)PhysicalWidth)*transpose;
                        PhysicalWidth = (int) (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width*factor);
                    }
                    else
                    {
                        PhysicalWidth = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width * 0.8);
                        var factor = ((double)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / (double)PhysicalHeight)*transpose;
                        PhysicalHeight = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * factor);
                    }
                    BorderOffsetX = ((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width/2) - (PhysicalWidth/2));
                    BorderOffsetY = ((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height/2) - (PhysicalHeight/2));
                    G.IsFullScreen = true;
                }
                else
                {
                    PhysicalWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                    PhysicalHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                    G.PreferredBackBufferWidth = PhysicalWidth;
                    G.PreferredBackBufferHeight = PhysicalHeight;
                    G.IsFullScreen = true;
                }
            }
            else if (Upscaling)
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
            else
            {
                PhysicalWidth = ResolutionWidth;
                PhysicalHeight = ResolutionHeight;
                G.PreferredBackBufferWidth = PhysicalWidth;
                G.PreferredBackBufferHeight = PhysicalHeight;
                G.IsFullScreen = false;
            }
        }
        protected override void Initialize()
        {
            if (Upscaling)
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
                if (Border)
                {
                    G.GraphicsDevice.SetRenderTarget(RenderTarget);
                    G.GraphicsDevice.Clear(Color.Black);
                    SpriteBatch.Begin();
                    CurrentScene.Draw(gameTime, SpriteBatch);
                    SpriteBatch.End();
                    G.GraphicsDevice.SetRenderTarget(null);
                    SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
                    G.GraphicsDevice.Clear(Color.LightBlue);
                    SpriteBatch.Draw(RenderTarget, new Rectangle(BorderOffsetX, BorderOffsetY, PhysicalWidth, PhysicalHeight), new Rectangle(0, 0, ResolutionWidth, ResolutionHeight), Color.White);
                    SpriteBatch.End();
                    G.GraphicsDevice.SetRenderTarget(null);
                }
                else if (Upscaling)
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
                else
                {
                    G.GraphicsDevice.Clear(Color.Black);
                    SpriteBatch.Begin();
                    CurrentScene.Draw(gameTime, SpriteBatch);
                    SpriteBatch.End();
                }
            }
            base.Draw(gameTime);
        }
    }
}
