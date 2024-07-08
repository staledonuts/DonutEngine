#nullable disable
using Raylib_CSharp;
using Raylib_CSharp.Audio;
using Newtonsoft.Json;
using Engine.Assets.Audio;
using Engine.Systems;
using System.Collections;
using System.Numerics;
using Engine.Utils;
using Raylib_CSharp.Logging;
namespace Engine.Assets;

public class AudioControl : SystemClass, IUpdateSys, ILateUpdateSys
{
    
    private Dictionary<string, SoundEffect> SoundsLibrary { get; set; }
    private Dictionary<string, MusicTrack> MusicLibrary { get; set; }
    private MusicTrack currentMusic;
    private readonly Random random;
    private string currentSongName;

    public AudioControl()
    {
        SoundsLibrary = new();
        MusicLibrary = new();
        random = new();
    }
    public override void Initialize()
    {
        #if DEBUG
        Logger.TraceLog(TraceLogLevel.Info, "------[ Setting up AudioSystem ]------");
        #endif
        AudioDevice.Init();
        InitAudioLibrary();
        #if DEBUG
        Logger.TraceLog(TraceLogLevel.Info, "------[ AudioSystem Initialized ]------");
        #endif
    }
    public void ReloadAudioLibrary()
    {
        currentMusic.Unload();
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
        AudioDevice.SetMasterVolume(Settings.audioSettings.MasterVolume);
    }
    void LoadMusicTrackLib()
    {
        try
        {
            MusicLibrary = new();
            File.Exists(Paths.AudioDefPath+"MusicDef.json");
            try
            {
                #if DEBUG
                Logger.TraceLog(TraceLogLevel.Info, "------[ Setting up Music Library ]------");
                #endif
                MusicLibrary = new();
                string JsonData = File.ReadAllText(Paths.AudioDefPath+"MusicDef.json");
                MusicLibrary = JsonConvert.DeserializeObject<Dictionary<string, MusicTrack>>(JsonData);
                #if DEBUG
                Logger.TraceLog(TraceLogLevel.Info, "------[ Music Library Setup Done ]------");
                #endif
            }
            catch (Exception e)
            {
                Logger.TraceLog(TraceLogLevel.Error, e.Message);
            }
        }
        catch (Exception e)
        {
            Logger.TraceLog(TraceLogLevel.Error, e.Message);
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
                #if DEBUG
                if(Settings.cVars.Debugging) { Logger.TraceLog(TraceLogLevel.Info, "------[ Setting up SFX Library ]------"); }
                #endif
                string JsonData = File.ReadAllText(Paths.AudioDefPath+"SoundDef.json");
                SoundsLibrary = JsonConvert.DeserializeObject<Dictionary<string, SoundEffect>>(JsonData);
                #if DEBUG
                if(Settings.cVars.Debugging) { Logger.TraceLog(TraceLogLevel.Info, "------[ SFX Library Setup Done ]------"); }
                #endif
            }
            catch (Exception e)
            {
                Logger.TraceLog(TraceLogLevel.Error, e.Message);
            }
        }
        catch (Exception e)
        {
            Logger.TraceLog(TraceLogLevel.Error, e.Message);
        }
    }
    
    public void Update()
    {
        
    }
    public void LateUpdate()
    {
        if(currentMusic != null)
        {
            currentMusic.UpdateMusicStream();
        }
    }
    public override void Shutdown()
    {
        foreach(KeyValuePair<string, SoundEffect> key in SoundsLibrary)
        {
            key.Value.Dispose();
        }
        AudioDevice.Close();
    }
    #region Music
    public void PlayMusic(string name) 
    {
        MusicTrack musicTrack;
        if (MusicLibrary.TryGetValue(name, out musicTrack)) 
        {
            currentSongName = name;
            currentMusic = musicTrack;
            currentMusic.SetVolume(currentMusic.Volume);
            currentMusic.PlayMusic();
            #if DEBUG
            Logger.TraceLog(TraceLogLevel.Info, $"Playing: {name}");
            #endif
        }
        else
        {
            Logger.TraceLog(TraceLogLevel.Error, $"Invalid song name: {name}");
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
            music.SetVolume(RayMath.Lerp(Settings.audioSettings.MusicVolume, 0, 0.05f));
            elapsedTime += Raylib_CSharp.Time.GetFrameTime();
        }
        yield return music;
    }
    public IEnumerable<Music> CrossFadeIn(Music music)
    {
        float elapsedTime = 0;
        float waitTime = 3f;
        while (elapsedTime < waitTime)
        {
            music.SetVolume(RayMath.Lerp(0, Settings.audioSettings.MusicVolume, 0.05f));
            elapsedTime += Raylib_CSharp.Time.GetFrameTime();
        }
        yield return music;
    }
    public void ToggleMusic()
    {
        if(currentMusic.isPlaying())
        {
            currentMusic.PauseMusic();
            #if DEBUG
            Logger.TraceLog(TraceLogLevel.Info, "Music Paused");
            #endif

        }
        else
        {
            currentMusic.ResumeMusic();
            #if DEBUG
            Logger.TraceLog(TraceLogLevel.Info, "Music Resumed");
            #endif
        }
    }
    public void StopMusic() 
    {
        currentMusic.StopMusic();
        Logger.TraceLog(TraceLogLevel.Info, "Stopping Music: "+currentMusic.Name);
    }
    public void SetMusicVolume(float volume) 
    {
        currentMusic.SetVolume(volume);
        SetMusicVolume(currentMusic.Volume);
        Logger.TraceLog(TraceLogLevel.Info, $"Current song volume set to: {volume}");
    }
    public void UnloadMusic(string name) 
    {
        MusicTrack music;
        if (MusicLibrary.TryGetValue(name, out music)) 
        {
            music.Unload();
            MusicLibrary.Remove(name);
        }
    }
    #endregion

    #region SFX

    public void PlaySFX(Sound sfx, float pitch, Vector2 position)
    {
        sfx.SetVolume(Settings.audioSettings.SfxVolume);
        sfx.SetPitch(pitch);
        sfx.SetPan(SoundPannerCalc2D(position));
        sfx.Play();
    }
    public void PlaySFX(Sound sfx, float minPitch, float maxPitch, Vector2 position)
    {
        sfx.SetVolume(Settings.audioSettings.SfxVolume);
        sfx.SetPitch(random.NextSingle() * (maxPitch - minPitch) + minPitch);
        sfx.SetPan(SoundPannerCalc2D(position));
        sfx.Play();
    }
    
    public void PlaySFX(SoundEffect soundEffect, Vector2 position)
    {
            soundEffect.SetVolume(Settings.audioSettings.SfxVolume);
            soundEffect.RandomizePitch(random);
            soundEffect.SetSoundPan(SoundPannerCalc2D(position));
            soundEffect.Play();
    }

    float SoundPannerCalc2D(Vector2 position)
    {
        return (EngineSystems.GetSystem<Camera2DSystem>().Bounds.Width - position.X) / EngineSystems.GetSystem<Camera2DSystem>().Bounds.Width;
    }

    public void PlaySFX(string name, Vector2 position) 
    {
        if (SoundsLibrary!.TryGetValue(name, out SoundEffect sound)) 
        {
            PlaySFX(sound, position);
        }
        else
        {
            #if DEBUG
            Logger.TraceLog(TraceLogLevel.Error, $"No SoundEffect with name {name} found");
            #endif
        }
    }
    public void StopSFX(string name)
    {
        if(SoundsLibrary!.TryGetValue(name, out SoundEffect sound))
        {          
            sound.Sound.Stop();
        }
        else
        {
            #if DEBUG
            Logger.TraceLog(TraceLogLevel.Info, $"No playing instance with name: {name}");
            #endif
        }
    }
    #endregion
}
