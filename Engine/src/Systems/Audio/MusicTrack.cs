using Raylib_cs;
using static Raylib_cs.Raylib;
using Newtonsoft.Json;
using Engine.Logging;
namespace Engine.Systems.Audio;

public class MusicTrack : IDisposable
{
    [JsonProperty] public float Volume = 0;
    [JsonProperty] public bool FadeIn = true;
    [JsonProperty] public bool FadeOut = true;
    [JsonProperty] string FileName { get; set; }

    private Music music;
    private bool isLoaded = false;

    [JsonIgnore]
    public Music Music 
    {
        get
        {
            if (!isLoaded)
            {
                SetMusicStream();
            }
            return music;
        }
    }

    public MusicTrack(float volume, string fileName, bool fadeIn, bool fadeOut)
    {
        this.Volume = volume;
        this.FadeIn = fadeIn;
        this.FadeOut = fadeOut;
        this.FileName = fileName;
    }

    public Music SetMusicStream()
    {
        try
        {
            isLoaded = true;
            Task.Run(() =>
            {
                music = LoadMusicStream(Paths.MusicPath+FileName);
                return music;
            });
        }
        catch (Exception e)
        {
            isLoaded = false;
            DonutLogging.Log(TraceLogLevel.LOG_ERROR, $"Failed to load Music stream. Exception: {e.Message}");
        }
        return music;        
    }

    public void Dispose()
    {
        this.Dispose(true);
    }

    public void Dispose(bool disposing)
    {
        Dispose(disposing: true);
        if (isLoaded)
        {
            UnloadMusicStream(music);
            isLoaded = false;
        }
        GC.SuppressFinalize(this);
    }
}