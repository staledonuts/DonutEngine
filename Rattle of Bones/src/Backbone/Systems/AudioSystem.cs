using Raylib_cs;
using static Raylib_cs.Raylib;
using System;
using System.Collections.Generic;
namespace DonutEngine.Backbone;
public class AudioControl : Systems.SystemsClass
{
    private static AudioControl? instance = null;

    private List<AudioStream> audioStreams = new List<AudioStream>();
    private Dictionary<string, Sound> soundsLibrary = new Dictionary<string, Sound>();
    private Dictionary<string, Music> musicsLibrary = new Dictionary<string, Music>();

    private Music currentMusic;

    public static AudioControl Instance 
    {
        get 
        {
            if (instance == null) 
            {
                instance = new AudioControl();
            }
            return instance;
        }
    }

    public override void Start()
    {
        //throw new NotImplementedException();
    }

    public override void Update()
    {
        UpdateMusicStream(currentMusic);
    }

    public override void DrawUpdate()
    {
        //throw new NotImplementedException();
    }

    public override void LateUpdate()
    {
        //throw new NotImplementedException();
    }

    public override void Shutdown()
    {
        
    }


    public void LoadSFX(string name, string filename) 
    {
        Sound sound = LoadSound(filename);
        soundsLibrary.Add(name, sound);
    }

    public void PlaySFX(string name) 
    {
        Sound sound;
        if (soundsLibrary.TryGetValue(name, out sound)) {
            PlaySound(sound);
        }
    }

    public void StopSFX(string name) 
    {
        Sound sound;
        if (soundsLibrary.TryGetValue(name, out sound)) {
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

    public void LoadMusic(string name, string filename) 
    {
        Music music = LoadMusicStream(filename);
        musicsLibrary.Add(name, music);
    }

    public void PlayMusic(string name) 
    {
        Music music;
        if (musicsLibrary.TryGetValue(name, out music)) 
        {
            currentMusic = music;
            PlayMusicStream(music);
        }
    }

    public void PauseMusic()
    {
        PauseMusicStream(currentMusic);
    }

    public void ResumeMusic() 
    {
        ResumeMusicStream(currentMusic);
    }

    public void StopMusic() 
    {
        StopMusicStream(currentMusic);
    }

    public void SetMusicVolume(float volume) 
    {
        SetMusicVolume(volume);
    }

    public void UnloadMusic(string name) 
    {
        Music music;
        if (musicsLibrary.TryGetValue(name, out music)) 
        {
            UnloadMusicStream(music);
            musicsLibrary.Remove(name);
        }
    }    
}
