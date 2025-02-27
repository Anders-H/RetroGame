using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RetroGame;

public class RetroGame : Game
{
    private GraphicsDeviceManager G { get; }
    private SpriteBatch SpriteBatch { get; set; }
    private RenderTarget2D RenderTarget { get; set; }
    private int OffsetX { get; }
    private int OffsetY { get; }
    public bool Fullscreen { get; }
    public bool Border { get; set; }
    public int ResolutionWidth { get; }
    public int ResolutionHeight { get; }
    public int PhysicalWidth { get; }
    public int PhysicalHeight { get; }
    public Scene.Scene CurrentScene { get; set; }
    public Color BorderColor { get; set; }
    public Color BackColor { get; set; }
    private bool _crt;
    internal static Texture2D Font64 { get; set; }
    internal static Texture2D Crt { get; set; }

    public RetroGame(int resolutionWidth, int resolutionHeight, RetroDisplayMode displayMode) : this(resolutionWidth, resolutionHeight, displayMode, false, false)
    {
    }

    public RetroGame(int resolutionWidth, int resolutionHeight, RetroDisplayMode displayMode, bool crt) : this(resolutionWidth, resolutionHeight, displayMode, crt, false)
    {
    }

    public RetroGame(int resolutionWidth, int resolutionHeight, RetroDisplayMode displayMode, bool crt, bool border)
    {
        _crt = crt;
        Border = border;
        BorderColor = ColorPaletteHelper.GetColor(ColorPalette.LightBlue);
        BackColor = ColorPaletteHelper.GetColor(ColorPalette.Blue);
        Fullscreen = displayMode == RetroDisplayMode.Fullscreen;
        G = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        ResolutionWidth = resolutionWidth;
        ResolutionHeight = resolutionHeight;

        var actualWidth = Fullscreen
            ? GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width
            : Window.ClientBounds.Width;

        var actualHeight = Fullscreen
            ? GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height
            : Window.ClientBounds.Height;


        Window.AllowUserResizing = false;
        PhysicalWidth = actualWidth;
        PhysicalHeight = actualHeight;
        G.PreferredBackBufferWidth = ResolutionWidth;
        G.PreferredBackBufferHeight = ResolutionHeight;
        G.IsFullScreen = Fullscreen;
        OffsetX = 0;
        OffsetY = 0;

        if (Fullscreen)
            G.HardwareModeSwitch = false;

        G.ApplyChanges();
    }

    protected override void LoadContent()
    {
        Font64 = Content.Load<Texture2D>("c64font");

        if (_crt)
            Crt = Content.Load<Texture2D>("crt");
        
        base.LoadContent();
    }
        
    protected override void Initialize()
    {
        RenderTarget = new RenderTarget2D(GraphicsDevice, ResolutionWidth, ResolutionHeight);
        SpriteBatch = new SpriteBatch(GraphicsDevice);
        base.Initialize();
    }
        
    protected sealed override void Update(GameTime gameTime)
    {
        if (CurrentScene != null)
        {
            CurrentScene.Update(gameTime, CurrentScene.Ticks);
            CurrentScene.Ticks++;
        }
        base.Update(gameTime);
    }

    protected sealed override void Draw(GameTime gameTime)
    {
        if (CurrentScene == null)
            return;

        G.GraphicsDevice.SetRenderTarget(RenderTarget);
        G.GraphicsDevice.Clear(BackColor);
        SpriteBatch.Begin();
        CurrentScene.Draw(gameTime, CurrentScene.Ticks, SpriteBatch);
        SpriteBatch.End();
        G.GraphicsDevice.SetRenderTarget(null);
        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

        var dest = new Rectangle(OffsetX, OffsetY, PhysicalWidth, PhysicalHeight);
        var source = new Rectangle(0, 0, ResolutionWidth, ResolutionHeight);
        SpriteBatch.Draw(RenderTarget, dest, source, Color.White);

        if (_crt)
            SpriteBatch.Draw(Crt, dest, null, Color.White);

        SpriteBatch.End();
        base.Draw(gameTime);
    }
}