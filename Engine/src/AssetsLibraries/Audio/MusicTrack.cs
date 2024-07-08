using Raylib_CSharp;
using Raylib_CSharp.Audio;
using Newtonsoft.Json;
using Engine.Logging;
using Raylib_CSharp.Logging;
using System.Diagnostics.CodeAnalysis;
namespace Engine.Assets.Audio;

public class MusicTrack : IDisposable
{
    [JsonProperty] public float Volume { get; private set; }
    [JsonProperty] public bool FadeIn = true;
    [JsonProperty] public bool FadeOut = true;
    [JsonProperty] string FileName { get; set; }

    [JsonIgnore] public string Name
    {
        get
        {
            if(_music.IsReady())
            {
                return _music.ToString();
            }
            else
            {
                return "No name";
            }
        }
    }

    [AllowNull] Music _music;
    bool isLoaded = false;
    
    public MusicTrack(float volume, string fileName, bool fadeIn, bool fadeOut)
    {
        this.Volume = volume;
        this.FadeIn = fadeIn;
        this.FadeOut = fadeOut;
        this.FileName = fileName;
    }

    private void SetMusicStream()
    {
        try
        {
            isLoaded = true;
            Music.Load(Paths.MusicPath+FileName);
        }
        catch (Exception e)
        {
            isLoaded = false;
            #if DEBUG
            Logger.TraceLog(TraceLogLevel.Error, $"Failed to load Music stream. Exception: {e.Message}");
            #endif
            return;
        }
        _music.PlayStream();
    }

    public void PlayMusic()
    {
        if (!isLoaded)
        {
            SetMusicStream();
        }
    }

    public void StopMusic()
    {
        _music.StopStream();
    }

    public void PauseMusic()
    {
        _music.PauseStream();
    }

    public void ResumeMusic()
    {
        _music.ResumeStream();
    }

    public void SetVolume(float volume)
    {
        _music.SetVolume(volume);
    }

    public void Unload()
    {
        _music.UnloadStream();
    }

    public void UpdateMusicStream()
    {
        _music.UpdateStream();
    }

    public void Dispose()
    {
        this.Dispose(true);
    }

    public bool isPlaying()
    {
        return _music.IsStreamPlaying();
    }

    public void Dispose(bool disposing)
    {
        if (isLoaded && disposing)
        {
            _music.UnloadStream();
            isLoaded = false;
        }
        GC.SuppressFinalize(this);
    }
}