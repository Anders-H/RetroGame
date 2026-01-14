#nullable enable
using Microsoft.Xna.Framework.Media;

namespace RetroGame.Audio;

public static class Jukebox
{
    public static void PlayWithLoop(Song? song)
    {
        MediaPlayer.Stop();

        if (song == null)
            return;

        MediaPlayer.Play(song);
        MediaPlayer.IsRepeating = true;
    }

    public static void Play(Song? song)
    {
        MediaPlayer.Stop();

        if (song == null)
            return;

        MediaPlayer.Play(song);
        MediaPlayer.IsRepeating = false;
    }

    public static void PlayIf(bool play, Song? song, bool loop)
    {
        if (!play)
            return;

        if (loop)
            PlayWithLoop(song);
        else
            Play(song);
    }
}