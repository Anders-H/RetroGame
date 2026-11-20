using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RetroGame;

public class RetroGame : Game
{
    private GraphicsDeviceManager G { get; }
    private SpriteBatch SpriteBatch { get; set; }
    private RenderTarget2D RenderTarget { get; set; }
    internal static Texture2D Font64 { get; set; }
    public bool Fullscreen { get; }
    public bool Border { get; set; }
    public int ResolutionWidth { get; }
    public int ResolutionHeight { get; }
    public int PhysicalWidth => GraphicsDevice.PresentationParameters.BackBufferWidth;
    public int PhysicalHeight => GraphicsDevice.PresentationParameters.BackBufferHeight;
    private Scene.Scene _currentScene;

    public Scene.Scene CurrentScene
    {
        get => _currentScene;
        set
        {
            if (ReferenceEquals(_currentScene, value))
                return;
            _currentScene = value;
            _currentScene?.BeginScene();
        }
    }
    public Color BorderColor { get; set; }
    public Color BackColor { get; set; }

    public RetroGame(int resolutionWidth, int resolutionHeight, RetroDisplayMode displayMode) : this(resolutionWidth, resolutionHeight, displayMode, false)
    {
    }

    public RetroGame(int resolutionWidth, int resolutionHeight, RetroDisplayMode displayMode, bool border)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(resolutionWidth);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(resolutionHeight);
        Border = border;
        BorderColor = ColorPaletteHelper.GetColor(ColorPalette.LightBlue);
        BackColor = ColorPaletteHelper.GetColor(ColorPalette.Blue);
        Fullscreen = displayMode == RetroDisplayMode.Fullscreen;

        G = new GraphicsDeviceManager(this)
        {
            SynchronizeWithVerticalRetrace = true,
            GraphicsProfile = GraphicsProfile.HiDef,
            HardwareModeSwitch = false,
            PreferMultiSampling = true,
        };
        
        Content.RootDirectory = "Content";
        ResolutionWidth = resolutionWidth;
        ResolutionHeight = resolutionHeight;

        Window.AllowUserResizing = false;
        G.PreferredBackBufferWidth = Fullscreen ? GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width : ResolutionWidth;
        G.PreferredBackBufferHeight = Fullscreen ? GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height : ResolutionHeight;
        G.IsFullScreen = Fullscreen;
        G.ApplyChanges();
    }

    protected override void LoadContent()
    {
        Font64 = Content.Load<Texture2D>("c64font");
        base.LoadContent();
    }

    protected override void Initialize()
    {
        RenderTarget = new RenderTarget2D(GraphicsDevice, ResolutionWidth, ResolutionHeight);
        SpriteBatch = new SpriteBatch(GraphicsDevice);
        base.Initialize();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            SpriteBatch?.Dispose();
            RenderTarget?.Dispose();
        }

        base.Dispose(disposing);
    }

    protected sealed override void Update(GameTime gameTime)
    {
        var scene = CurrentScene;
        scene?.Advance(gameTime);
        base.Update(gameTime);
    }

    internal static Rectangle GetDestinationBounds(int physicalWidth, int physicalHeight,
        int resolutionWidth, int resolutionHeight, bool border)
    {
        var marginX = border ? Math.Max(1, physicalWidth / 20) : 0;
        var marginY = border ? Math.Max(1, physicalHeight / 20) : 0;

        var scale = Math.Min((physicalWidth - 2 * marginX) / (double)resolutionWidth,
            (physicalHeight - 2 * marginY) / (double)resolutionHeight);

        var width = Math.Max(1, (int)(resolutionWidth * scale));
        var height = Math.Max(1, (int)(resolutionHeight * scale));
        return new Rectangle((physicalWidth - width) / 2, (physicalHeight - height) / 2, width, height);
    }

    protected sealed override void Draw(GameTime gameTime)
    {
        if (CurrentScene == null)
            return;

        G.GraphicsDevice.SetRenderTarget(RenderTarget);
        G.GraphicsDevice.Clear(BackColor);
        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        CurrentScene.Draw(gameTime, CurrentScene.Ticks, SpriteBatch);
        SpriteBatch.End();
        G.GraphicsDevice.SetRenderTarget(null);
        G.GraphicsDevice.Clear(Border ? BorderColor : BackColor);
        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        var dest = GetDestinationBounds(PhysicalWidth, PhysicalHeight, ResolutionWidth, ResolutionHeight, Border);
        var source = new Rectangle(0, 0, ResolutionWidth, ResolutionHeight);
        SpriteBatch.Draw(RenderTarget, dest, source, Color.White);
        SpriteBatch.End();
        base.Draw(gameTime);
    }
}
