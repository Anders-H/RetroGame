using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RetroGameClasses
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
        public Color BorderColor { get; set; }
        public Color BackColor { get; set; }
        public RetroGame(int resolutionWidth, int resolutionHeight, RetroDisplayMode displayMode)
        {
            BorderColor = ColorPaletteHelper.GetColor(ColorPalette.LightBlue);
            BackColor = ColorPaletteHelper.GetColor(ColorPalette.Blue);
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
                    var w = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                    var h = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                    var wborder = w*0.9;
                    var hborder = h*0.9;
                    PhysicalWidth = ResolutionWidth;
                    PhysicalHeight = ResolutionHeight;
                    var dscale = 10.20;
                    for (var scale = 50; scale > 1; scale--)
                    {
                        dscale -= 0.20;
                        if (wborder < ResolutionWidth * dscale || hborder < ResolutionHeight * dscale)
                            continue;
                        PhysicalWidth = (int)(ResolutionWidth*dscale);
                        PhysicalHeight = (int)(ResolutionHeight*dscale);
                        break;
                    }
                    G.PreferredBackBufferWidth = w;
                    G.PreferredBackBufferHeight = h;
                    BorderOffsetX = w/2 - PhysicalWidth/2;
                    BorderOffsetY = h/2 - PhysicalHeight/2;
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
                PhysicalWidth = ResolutionWidth;
                PhysicalHeight = ResolutionHeight;
                for (var scale = 10; scale > 1; scale--)
                {
                    if (w < ResolutionWidth*scale || h < ResolutionHeight*scale)
                        continue;
                    PhysicalWidth = ResolutionWidth*(scale - 1);
                    PhysicalHeight = ResolutionHeight*(scale - 1);
                    break;
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
                    G.GraphicsDevice.Clear(BackColor);
                    SpriteBatch.Begin();
                    CurrentScene.Draw(gameTime, SpriteBatch);
                    SpriteBatch.End();
                    G.GraphicsDevice.SetRenderTarget(null);
                    SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
                    G.GraphicsDevice.Clear(BorderColor);
                    SpriteBatch.Draw(RenderTarget, new Rectangle(BorderOffsetX, BorderOffsetY, PhysicalWidth, PhysicalHeight), new Rectangle(0, 0, ResolutionWidth, ResolutionHeight), Color.White);
                    SpriteBatch.End();
                    G.GraphicsDevice.SetRenderTarget(null);
                }
                else if (Upscaling)
                {
                    G.GraphicsDevice.SetRenderTarget(RenderTarget);
                    G.GraphicsDevice.Clear(BackColor);
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
                    G.GraphicsDevice.Clear(BackColor);
                    SpriteBatch.Begin();
                    CurrentScene.Draw(gameTime, SpriteBatch);
                    SpriteBatch.End();
                }
            }
            base.Draw(gameTime);
        }
    }
}
