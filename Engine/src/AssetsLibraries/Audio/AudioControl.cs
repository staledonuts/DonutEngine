#nullable disable
using Raylib_cs;
using static Raylib_cs.Raylib;
using Newtonsoft.Json;
using Engine.Assets.Audio;
using Engine.Systems;
using System.Collections;
namespace Engine.Assets;

public class AudioControl : SystemClass, IUpdateSys, ILateUpdateSys
{
    
    private Dictionary<string, Queue<SoundEffect>> soundInstances;
    private Dictionary<string, SoundEffect> SoundsLibrary { get; set; }
    private Dictionary<string, MusicTrack> MusicLibrary { get; set; }
    private MusicTrack currentMusic;
    private Random random;
    private string currentSongName;

    public AudioControl()
    {
        soundInstances = new Dictionary<string, Queue<SoundEffect>>();
        SoundsLibrary = new();
        MusicLibrary = new();
        random = new();
    }
    public override void Initialize()
    {
        Raylib.TraceLog(TraceLogLevel.Info, "------[ Setting up AudioSystem ]------");
        InitAudioDevice();
        InitAudioLibrary();
        Raylib.TraceLog(TraceLogLevel.Info, "------[ AudioSystem Initialized ]------");
    }
    public void ReloadAudioLibrary()
    {
        Raylib.StopMusicStream(currentMusic.Music);
        foreach(KeyValuePair<string, SoundEffect> sfx in SoundsLibrary!)
        {
            sfx.Value.Dispose();
        }
        foreach(KeyValuePair<string, MusicTrack> music in MusicLibrary!)
        {
            music.Value.Dispose();
        }
        SoundsLibrary = new Dictionary<string, SoundEffect>();
        MusicLibrary = new Dictionary<string, MusicTrack>();
        InitAudioLibrary();
    }
    public void InitAudioLibrary()
    {
        LoadSoundEffectsLib();
        LoadMusicTrackLib();
    }
    public void SetMasterVolume()
    {
        Raylib.SetMasterVolume(Settings.audioSettings.MasterVolume);
    }
    void LoadMusicTrackLib()
    {
        try
        {
            MusicLibrary = new();
            File.Exists(Paths.AudioDefPath+"MusicDef.json");
            try
            {
                Raylib.TraceLog(TraceLogLevel.Info, "------[ Setting up Music Library ]------");
                MusicLibrary = new();
                string JsonData = File.ReadAllText(Paths.AudioDefPath+"MusicDef.json");
                MusicLibrary = JsonConvert.DeserializeObject<Dictionary<string, MusicTrack>>(JsonData);
                Raylib.TraceLog(TraceLogLevel.Info, "------[ Music Library Setup Done ]------");
            }
            catch (Exception e)
            {
                Raylib.TraceLog(TraceLogLevel.Error, e.Message);
            }
        }
        catch (Exception e)
        {
            Raylib.TraceLog(TraceLogLevel.Error, e.Message);
        }
    }
    void LoadSoundEffectsLib()
    {
        try
        {
            SoundsLibrary = new();
            File.Exists(Paths.AudioDefPath+"SoundDef.json");
            try
            {
                if(Settings.cVars.Debugging) { Raylib.TraceLog(TraceLogLevel.Info, "------[ Setting up SFX Library ]------"); }
                string JsonData = File.ReadAllText(Paths.AudioDefPath+"SoundDef.json");
                SoundsLibrary = JsonConvert.DeserializeObject<Dictionary<string, SoundEffect>>(JsonData);
                if(Settings.cVars.Debugging) { Raylib.TraceLog(TraceLogLevel.Info, "------[ SFX Library Setup Done ]------"); }
            }
            catch (Exception e)
            {
                Raylib.TraceLog(TraceLogLevel.Error, e.Message);
            }
        }
        catch (Exception e)
        {
            Raylib.TraceLog(TraceLogLevel.Error, e.Message);
        }
    }
    void CheckSoundInstances()
    {
        if(soundInstances != null)
        {
            foreach(KeyValuePair<string, Queue<SoundEffect>> sfx in soundInstances)
            {
                if(!IsSoundPlaying(sfx.Value.Peek().Sound))
                {
                    sfx.Value.Dequeue();
                }
            }
        }
    }
    public void Update()
    {
        //CheckSoundInstances();
    }
    public void LateUpdate()
    {
        if(currentMusic != null)
        {
            UpdateMusicStream(currentMusic.Music);
        }
    }
    public override void Shutdown()
    {
        foreach(KeyValuePair<string, SoundEffect> key in SoundsLibrary)
        {
            key.Value.Dispose();
        }
        CloseAudioDevice();
    }
    #region Music
    public void PlayMusic(string name) 
    {
        MusicTrack musicTrack;
        if (MusicLibrary.TryGetValue(name, out musicTrack)) 
        {
            currentSongName = name;
            currentMusic = musicTrack;
            Raylib.SetMusicVolume(currentMusic.Music, currentMusic.Volume);
            PlayMusicStream(currentMusic.Music);
            Raylib.TraceLog(TraceLogLevel.Info, $"Playing: {name}");
        }
        else
        {
            Raylib.TraceLog(TraceLogLevel.Error, $"Invalid song name: {name}");
        }
    }
    public void FadeOutCurrentMusic()
    {
        
    }
    public IEnumerable<Music> CrossFadeOut(Music music)
    {
        float elapsedTime = 0;
        float waitTime = 3f;
        while (elapsedTime < waitTime)
        {
            Raylib.SetMusicVolume(music, Raymath.Lerp(Settings.audioSettings.MusicVolume, 0, 0.05f));
            elapsedTime += Raylib.GetFrameTime();
        }
        yield return music;
    }
    public IEnumerable<Music> CrossFadeIn(Music music)
    {
        float elapsedTime = 0;
        float waitTime = 3f;
        while (elapsedTime < waitTime)
        {
            Raylib.SetMusicVolume(music, Raymath.Lerp(0, Settings.audioSettings.MusicVolume, 0.05f));
            elapsedTime += Raylib.GetFrameTime();
        }
        yield return music;
    }
    public void ToggleMusic()
    {
        if(IsMusicStreamPlaying(currentMusic.Music))
        {
            PauseMusicStream(currentMusic.Music);
            Raylib.TraceLog(TraceLogLevel.Info, "Music Paused");

        }
        else
        {
            ResumeMusicStream(currentMusic.Music);

            Raylib.TraceLog(TraceLogLevel.Info, "Music Resumed");
        }
    }
    public void StopMusic() 
    {
        StopMusicStream(currentMusic.Music);
        Raylib.TraceLog(TraceLogLevel.Info, "Stopping Music: "+currentMusic.Music.ToString());
    }
    public void SetMusicVolume(float volume) 
    {
        currentMusic.SetVolume(volume);
        SetMusicVolume(currentMusic.Volume);
        Raylib.TraceLog(TraceLogLevel.Info, $"Current song volume set to: {volume}");
    }
    public void UnloadMusic(string name) 
    {
        MusicTrack music;
        if (MusicLibrary.TryGetValue(name, out music)) 
        {
            UnloadMusicStream(music.Music);
            MusicLibrary.Remove(name);
        }
    }
    #endregion

    #region SFX
    public void PlaySFX(Sound sfx, float minPitch, float maxPitch)
    {
        Raylib.SetSoundVolume(sfx, Settings.audioSettings.SfxVolume);
        SetSoundPitch(sfx, random.NextSingle() * (maxPitch - minPitch) + minPitch);
        PlaySound(sfx);
    }
    
    void PlaySFX(string name, SoundEffect soundEffect)
    {
        // If this sound effect is not in the dictionary yet, add it
        if (!soundInstances.ContainsKey(name))
        {
            soundInstances[name] = new Queue<SoundEffect>();
        }

        // Only play the sound if the maximum number of instances has not been reached
        if (soundInstances[name].Count <= soundEffect.MaxInstances)
        {
            // Add this instance to the list
            soundInstances[name].Enqueue(soundEffect);
            Raylib.SetSoundVolume(soundEffect.Sound, Settings.audioSettings.SfxVolume);
            Raylib.SetSoundPitch(soundEffect.Sound, random.NextSingle() * (soundEffect.MaxPitch - soundEffect.MinPitch) + soundEffect.MinPitch);
            Raylib.PlaySound(soundEffect.Sound);
            Raylib.TraceLog(TraceLogLevel.Debug, $"Playing {soundInstances[name].Count} of {soundEffect.MaxInstances} for {name}");
        }
        else
        {
            StopSFX(name);
            soundInstances[name].Enqueue(soundEffect);
            Raylib.SetSoundVolume(soundEffect.Sound, Settings.audioSettings.SfxVolume);
            Raylib.SetSoundPitch(soundEffect.Sound, random.NextSingle() * (soundEffect.MaxPitch - soundEffect.MinPitch) + soundEffect.MinPitch);
            Raylib.PlaySound(soundEffect.Sound);
            Raylib.TraceLog(TraceLogLevel.Debug, $"Playing {soundInstances[name].Count} of {soundEffect.MaxInstances} for {name}");
        }
    }

    public void PlaySFX(string name) 
    {
        if (SoundsLibrary!.TryGetValue(name, out SoundEffect sound)) 
        {
            PlaySFX(name, sound);
        }
        else
        {
            Raylib.TraceLog(TraceLogLevel.Error, $"No SoundEffect with name {name} found");
        }
    }
    public void StopSFX(string name)
    {
        if(SoundsLibrary!.TryGetValue(name, out SoundEffect sound))
        {
            
            StopSound(sound.Sound);
            if (soundInstances.ContainsKey(name))
            {
                Raylib.StopSound(soundInstances[name].Dequeue().Sound);
            }
        }
            // If this sound effect is in the dictionary, remove the sound instance
        else
        {
            Raylib.TraceLog(TraceLogLevel.Info, $"No playing instance with name: {name}");
        }
    }
    #endregion
}
