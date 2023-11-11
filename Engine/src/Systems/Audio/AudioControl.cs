#nullable disable
using Raylib_cs;
using static Raylib_cs.Raylib;
using Newtonsoft.Json;
using Engine.Data;
namespace Engine.Systems.Audio;

public class AudioControl : SystemClass
{
    
    private Dictionary<string, List<Sound>> soundInstances;
    private Dictionary<string, ISoundEffect> SoundsLibrary { get; set; }
    private Dictionary<string, MusicTrack> MusicLibrary { get; set; }
    private MusicTrack currentMusic = null;
    private Random random;
    private string currentSongName = null;

    public AudioControl()
    {
        soundInstances = new Dictionary<string, List<Sound>>();
        SoundsLibrary = new();
        MusicLibrary = new();
        random = new();
    }

    public override void Initialize()
    {
        Raylib.TraceLog(TraceLogLevel.LOG_INFO, "------[ Setting up AudioSystem ]------");
        InitAudioDevice();
        InitAudioLibrary();
        EngineSystems.dUpdate += Update;
        EngineSystems.dLateUpdate += LateUpdate;
        Raylib.TraceLog(TraceLogLevel.LOG_INFO, "------[ AudioSystem Initialized ]------");
    }

    public void ReloadAudioLibrary()
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

    public void InitAudioLibrary()
    {
        LoadSoundEffectsLib();
        LoadMusicTrackLib();
    }
    void LoadMusicTrackLib()
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
    void LoadSoundEffectsLib()
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


    private void CheckSoundInstances()
    {
        if(soundInstances != null)
        {
            foreach(KeyValuePair<string, List<Sound>> sfx in soundInstances)
            {
                sfx.Value.RemoveAll(snd => !IsSoundPlaying(snd));
            }
        }
    }

    public override void Update()
    {
        CheckSoundInstances();
    }

    public override void DrawUpdate()
    {
        
    }

    public override void LateUpdate()
    {
        if(currentMusic != null)
        {
            UpdateMusicStream(currentMusic.Music);
        }
    }

    public override void Shutdown()
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

    public void PlayMusic(string name) 
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

    public void FadeOutCurrentMusic()
    {
        
    }

    public static IEnumerable<Music> CrossFadeOut(Music music)
    {
        float elapsedTime = 0;
        float waitTime = 3f;
        while (elapsedTime < waitTime)
        {
            Raylib.SetMusicVolume(music, Raymath.Lerp(Data.Settings.audioSettings.MusicVolume, 0, 0.05f));
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
            Raylib.SetMusicVolume(music, Raymath.Lerp(0, Data.Settings.audioSettings.MusicVolume, 0.05f));
            elapsedTime += Raylib.GetFrameTime();
        }
        yield return music;
    }

    public void ToggleMusic()
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

    public void StopMusic() 
    {
        StopMusicStream(currentMusic.Music);
        Raylib.TraceLog(TraceLogLevel.LOG_INFO, "Stopping Music: "+currentMusic.Music.ToString());
    }

    public void SetMusicVolume(float volume) 
    {
        currentMusic.Volume = volume;
        SetMusicVolume(currentMusic.Volume);
        Raylib.TraceLog(TraceLogLevel.LOG_INFO, $"Current song volume set to: {volume}");
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
    private void PlaySFX(string name, MultiSoundEffect multiSoundEffect)
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

    private void PlaySFX(string name, SingleSoundEffect singleSoundEffect)
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
    public void PlaySFX(string name) 
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

    public void StopSFX(string name)
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
