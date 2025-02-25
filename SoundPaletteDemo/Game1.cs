using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using RetroGame;
using RetroGame.Audio;
using RetroGame.Input;
using RetroGame.Scene;

namespace SoundPaletteDemo;

public class Game1 : RetroGame.RetroGame
{
    public static SoundEffect SoundEffect { get; set; }
    public static Song Song { get; set; }

    public Game1() : base(320, 200, RetroDisplayMode.Windowed)
    {
        SoundEffect = new SoundEffect(this);
    }

    protected override void LoadContent()
    {
        CurrentScene = new SoundScene(this);
        SoundEffect.Initialize("sfx_gun1", "sfx_gun4", "sfx_gun7", "sfx_gun10");
        Song = Content.Load<Song>("08-Floating");
        base.LoadContent();
    }
}

public class SoundScene : Scene
{
    private KeyboardStateChecker Keyboard { get; } = new();

    public SoundScene(RetroGame.RetroGame retroGame) : base(retroGame)
    {
        AddToAutoUpdate(Keyboard);
    }

    public override void Update(GameTime gameTime, ulong ticks)
    {
        if (Keyboard.IsKeyPressed(Keys.Escape))
            Exit();

        if (Keyboard.IsKeyPressed(Keys.D1))
            Game1.SoundEffect.PlayNext();

        if (Keyboard.IsKeyPressed(Keys.D2))
            Game1.SoundEffect.PlayRandom();

        if (Keyboard.IsKeyPressed(Keys.D3))
            MediaPlayer.Play(Game1.Song);

        if (Keyboard.IsKeyPressed(Keys.D4))
            MediaPlayer.Stop();

        base.Update(gameTime, ticks);
    }
}