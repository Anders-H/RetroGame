using System;
using System.Collections.Generic;

namespace RetroGame.Audio;

public class SoundEffect
{
    private static readonly Random Rnd;
    private readonly RetroGame _parent;
    private readonly List<Microsoft.Xna.Framework.Audio.SoundEffect> _soundEffects;
    private int _index;

    static SoundEffect()
    {
        Rnd = new Random();
    }

    public SoundEffect(RetroGame parent)
    {
        _parent = parent;
        _soundEffects = [];
        _index = -1;
    }

    public void Initialize(params string[] soundEffectNames)
    {
        foreach (var soundEffectName in soundEffectNames)
            _soundEffects.Add(_parent.Content.Load<Microsoft.Xna.Framework.Audio.SoundEffect>(soundEffectName));
    }

    public void PlayNext()
    {
        _index++;

        if (_index >= _soundEffects.Count)
            _index = 0;

        _soundEffects[_index].Play();
    }

    public void PlayRandom()
    {
        _index = Rnd.Next(0, _soundEffects.Count);
        _soundEffects[_index].Play();
    }
}