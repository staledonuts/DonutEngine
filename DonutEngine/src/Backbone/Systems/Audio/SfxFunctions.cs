using Raylib_cs;
using static Raylib_cs.Raylib;
namespace DonutEngine.Backbone.Systems.Audio;

public partial class AudioControl
{
    public int GetSFXPlaylistLength()
    {
        return soundsLibrary.Count();
    }

    public string[] GetSFXPlaylist()
    {
        return soundsLibrary.Keys.ToArray<string>();
    }

    public void LoadSFX(string name, string filename) 
    {
        Sound sound = LoadSound(filename);
        soundsLibrary.Add(name, sound);
    }

    public void PlaySFX(string name) 
    {
        Sound sound;
        if (soundsLibrary.TryGetValue(name, out sound)) 
        {
            Raylib.SetSoundVolume(sound, DonutSystems.settingsVars.currentSFXVolume);
            PlaySound(sound);
        }
    }

    public void PlaySFX(string name, float minPitch, float maxPitch) 
    {
        Sound sound;
        if (soundsLibrary.TryGetValue(name, out sound)) 
        {
            Raylib.SetSoundVolume(sound, DonutSystems.settingsVars.currentSFXVolume);
            SetSoundPitch(sound, random.NextSingle() * (maxPitch - minPitch) + minPitch);
            PlaySound(sound);
        }
    }

    public void StopSFX(string name) 
    {
        Sound sound;
        if (soundsLibrary.TryGetValue(name, out sound)) 
        {
            StopSound(sound);
        }
    }

    public void UnloadSFX(string name) 
    {
        Sound sound;
        if (soundsLibrary.TryGetValue(name, out sound)) 
        {
            UnloadSound(sound);
            soundsLibrary.Remove(name);
        }
    }
}