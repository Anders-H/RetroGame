﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RetroGame;

public class RetroGame : Game
{
    public bool Fullscreen { get; }
    public bool Border { get; set; }
    private GraphicsDeviceManager G { get; }
    public int ResolutionWidth { get; }
    public int ResolutionHeight { get; }
    public int PhysicalWidth { get; }
    public int PhysicalHeight { get; }
    private SpriteBatch SpriteBatch { get; set; }
    private RenderTarget2D RenderTarget { get; set; }
    public Scene.Scene CurrentScene { get; set; }
    public Color BorderColor { get; set; }
    public Color BackColor { get; set; }
    internal static Texture2D Font64 { get; set; }
    private int OffsetX { get; }
    private int OffsetY { get; }

    public RetroGame(int resolutionWidth, int resolutionHeight, RetroDisplayMode displayMode) : this(resolutionWidth, resolutionHeight, displayMode, false)
    {
    }

    public RetroGame(int resolutionWidth, int resolutionHeight, RetroDisplayMode displayMode, bool border)
    {
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

        var ratioX = actualWidth/(double)ResolutionWidth;
        var ratioY = actualHeight/(double)ResolutionHeight;
        var ratio = ratioX < ratioY ? ratioX : ratioY;
        PhysicalWidth = (int)(ResolutionWidth*ratio);
        PhysicalHeight = (int)(ResolutionHeight*ratio);

        if (Border)
        {
            PhysicalWidth -= (int)(PhysicalHeight * 0.12);
            PhysicalHeight -= (int)(PhysicalHeight * 0.12);
        }

        OffsetX = actualWidth/2 - PhysicalWidth/2;
        OffsetY = actualHeight/2 - PhysicalHeight/2;

        G.PreferredBackBufferWidth = ResolutionWidth;
        G.PreferredBackBufferHeight = ResolutionHeight;

        G.IsFullScreen = Fullscreen;
        if (Fullscreen)
            G.HardwareModeSwitch = false;

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

        SpriteBatch.End();
        base.Draw(gameTime);
    }
}