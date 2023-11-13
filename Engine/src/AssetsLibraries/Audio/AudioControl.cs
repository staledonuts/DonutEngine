#nullable disable
using Raylib_cs;
using static Raylib_cs.Raylib;
using Newtonsoft.Json;
using Engine.Assets.Audio;
using Engine.Systems;
namespace Engine.Assets;

public static class AudioControl
{
    
    private static Dictionary<string, List<Sound>> soundInstances;
    private static Dictionary<string, ISoundEffect> SoundsLibrary { get; set; }
    private static Dictionary<string, MusicTrack> MusicLibrary { get; set; }
    private static MusicTrack currentMusic = null;
    private static Random random;
    private static string currentSongName = null;

    static AudioControl()
    {
        soundInstances = new Dictionary<string, List<Sound>>();
        SoundsLibrary = new();
        MusicLibrary = new();
        random = new();
    }

    public static void InitAudioControl()
    {
        Raylib.TraceLog(TraceLogLevel.LOG_INFO, "------[ Setting up AudioSystem ]------");
        InitAudioDevice();
        InitAudioLibrary();
        EngineSystems.dUpdate += Update;
        EngineSystems.dLateUpdate += LateUpdate;
        Raylib.TraceLog(TraceLogLevel.LOG_INFO, "------[ AudioSystem Initialized ]------");
    }

    public static void ReloadAudioLibrary()
    {
        Raylib.StopMusicStream(currentMusic.Music);
        foreach(KeyValuePair<string, ISoundEffect> sfx in SoundsLibrary!)
        {
            sfx.Value.Dispose();
        }
        foreach(KeyValuePair<string, MusicTrack> music in MusicLibrary!)
        {
            music.Value.Dispose();
        }
        SoundsLibrary = new Dictionary<string, ISoundEffect>();
        MusicLibrary = new Dictionary<string, MusicTrack>();
        InitAudioLibrary();
    }

    public static void InitAudioLibrary()
    {
        LoadSoundEffectsLib();
        LoadMusicTrackLib();
    }
    static void LoadMusicTrackLib()
    {
        try
        {
            MusicLibrary = new();
            File.Exists(Paths.AudioDefPath+"MusicDef.json");
            try
            {
                Raylib.TraceLog(TraceLogLevel.LOG_INFO, "------[ Setting up Music Library ]------");
                MusicLibrary = new();
                string JsonData = File.ReadAllText(Paths.AudioDefPath+"MusicDef.json");
                MusicLibrary = JsonConvert.DeserializeObject<Dictionary<string, MusicTrack>>(JsonData);
                Raylib.TraceLog(TraceLogLevel.LOG_INFO, "------[ Music Library Setup Done ]------");
            }
            catch (Exception e)
            {
                Raylib.TraceLog(TraceLogLevel.LOG_ERROR, e.Message);
            }
        }
        catch (Exception e)
        {
            Raylib.TraceLog(TraceLogLevel.LOG_ERROR, e.Message);
        }
    }
    static void LoadSoundEffectsLib()
    {
        try
        {
            SoundsLibrary = new();
            File.Exists(Paths.AudioDefPath+"SoundDef.json");
            try
            {
                if(Settings.cVars.Debugging) { Raylib.TraceLog(TraceLogLevel.LOG_INFO, "------[ Setting up SFX Library ]------"); }
                string JsonData = File.ReadAllText(Paths.AudioDefPath+"SoundDef.json");
                SoundsLibrary = JsonConvert.DeserializeObject<Dictionary<string, ISoundEffect>>(JsonData);
                if(Settings.cVars.Debugging) { Raylib.TraceLog(TraceLogLevel.LOG_INFO, "------[ SFX Library Setup Done ]------"); }
            }
            catch (Exception e)
            {
                Raylib.TraceLog(TraceLogLevel.LOG_ERROR, e.Message);
            }
        }
        catch (Exception e)
        {
            Raylib.TraceLog(TraceLogLevel.LOG_ERROR, e.Message);
        }
    }


    private static void CheckSoundInstances()
    {
        if(soundInstances != null)
        {
            foreach(KeyValuePair<string, List<Sound>> sfx in soundInstances)
            {
                sfx.Value.RemoveAll(snd => !IsSoundPlaying(snd));
            }
        }
    }

    static void Update()
    {
        CheckSoundInstances();
    }

    static void LateUpdate()
    {
        if(currentMusic != null)
        {
            UpdateMusicStream(currentMusic.Music);
        }
    }

    public static void Shutdown()
    {
        EngineSystems.dUpdate -= Update;
        EngineSystems.dLateUpdate -= LateUpdate;
        foreach(KeyValuePair<string, ISoundEffect> key in SoundsLibrary)
        {
            key.Value.Dispose();
        }
        CloseAudioDevice();
    }
    #region Music

    public static void PlayMusic(string name) 
    {
        MusicTrack musicTrack;
        if (MusicLibrary.TryGetValue(name, out musicTrack)) 
        {
            currentSongName = name;
            currentMusic = musicTrack;
            Raylib.SetMusicVolume(currentMusic.Music, currentMusic.Volume);
            PlayMusicStream(currentMusic.Music);
            Raylib.TraceLog(TraceLogLevel.LOG_INFO, $"Playing: {name}");
        }
        else
        {
            Raylib.TraceLog(TraceLogLevel.LOG_ERROR, $"Invalid song name: {name}");
        }
    }

    public static void FadeOutCurrentMusic()
    {
        
    }

    public static IEnumerable<Music> CrossFadeOut(Music music)
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

    public static IEnumerable<Music> CrossFadeIn(Music music)
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

    public static void ToggleMusic()
    {
        if(IsMusicStreamPlaying(currentMusic.Music))
        {
            PauseMusicStream(currentMusic.Music);
            Raylib.TraceLog(TraceLogLevel.LOG_INFO, "Music Paused");

        }
        else
        {
            ResumeMusicStream(currentMusic.Music);

            Raylib.TraceLog(TraceLogLevel.LOG_INFO, "Music Resumed");
        }
    }

    public static void StopMusic() 
    {
        StopMusicStream(currentMusic.Music);
        Raylib.TraceLog(TraceLogLevel.LOG_INFO, "Stopping Music: "+currentMusic.Music.ToString());
    }

    public static void SetMusicVolume(float volume) 
    {
        currentMusic.Volume = volume;
        SetMusicVolume(currentMusic.Volume);
        Raylib.TraceLog(TraceLogLevel.LOG_INFO, $"Current song volume set to: {volume}");
    }

    public static void UnloadMusic(string name) 
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

    public static void PlaySFX(Sound sfx, float minPitch, float maxPitch)
    {
        Raylib.SetSoundVolume(sfx, Settings.audioSettings.SfxVolume);
        SetSoundPitch(sfx, random.NextSingle() * (maxPitch - minPitch) + minPitch);
        PlaySound(sfx);
    }
    private static void PlaySFX(string name, MultiSoundEffect multiSoundEffect)
    {
        // If this sound effect is not in the dictionary yet, add it
        if (!soundInstances.ContainsKey(name))
        {
            soundInstances[name] = new List<Sound>();
        }

        // Only play the sound if the maximum number of instances has not been reached
        if (soundInstances[name].Count < multiSoundEffect.MaxInstances)
        {
            // Add this instance to the list
            soundInstances[name].Add(multiSoundEffect.Sound);
            Raylib.SetSoundVolume(multiSoundEffect.Sound, Settings.audioSettings.SfxVolume);
            Raylib.SetSoundPitch(multiSoundEffect.Sound, random.NextSingle() * (multiSoundEffect.MaxPitch - multiSoundEffect.MinPitch) + multiSoundEffect.MinPitch);
            Raylib.PlaySound(multiSoundEffect.Sound);
        }

    }

    private static void PlaySFX(string name, SingleSoundEffect singleSoundEffect)
    {
        // If this sound effect is not in the dictionary yet, add it
        if (!soundInstances.ContainsKey(name))
        {
            soundInstances[name] = new List<Sound>();
        }

        // Only play the sound if the maximum number of instances has not been reached
        if (soundInstances[name].Count < singleSoundEffect.MaxInstances)
        {
            // Add this instance to the list
            soundInstances[name].Add(singleSoundEffect.Sound);
            Raylib.SetSoundVolume(singleSoundEffect.Sound, Settings.audioSettings.SfxVolume);
            Raylib.SetSoundPitch(singleSoundEffect.Sound, random.NextSingle() * (singleSoundEffect.MaxPitch - singleSoundEffect.MinPitch) + singleSoundEffect.MinPitch);
            Raylib.PlaySound(singleSoundEffect.Sound);
        }
        else
        {
            Raylib.TraceLog(TraceLogLevel.LOG_INFO, $"SoundEffect {name} cannot be played. Too many instances playing");
        }
    }
    public static void PlaySFX(string name) 
    {
        if (SoundsLibrary!.TryGetValue(name, out ISoundEffect sound)) 
        {
            if(sound is MultiSoundEffect ms)
            {
                PlaySFX(name, ms);
            }
            else if(sound is SingleSoundEffect ss)
            {
                PlaySFX(name, ss);
            }
        }
        else
        {
            Raylib.TraceLog(TraceLogLevel.LOG_ERROR, $"No SoundEffect with name {name} found");
        }
    }

    public static void StopSFX(string name)
    {
        if(SoundsLibrary!.TryGetValue(name, out ISoundEffect sound))
        {
            if(sound is MultiSoundEffect ms)
            {
                StopSound(ms.Sound);
                if (soundInstances.ContainsKey(name))
                {
                    soundInstances[name].Remove(ms.Sound);
                }
            }
            else if(sound is SingleSoundEffect ss)
            {
                StopSound(ss.Sound);
                if (soundInstances.ContainsKey(name))
                {
                    soundInstances[name].Remove(ss.Sound);
                }
            }
        }
            // If this sound effect is in the dictionary, remove the sound instance
        else
        {
            Raylib.TraceLog(TraceLogLevel.LOG_INFO, $"No playing instance with name: {name}");
        }
    }
    #endregion
}
