using Raylib_cs;
using static Raylib_cs.Raylib;
using Newtonsoft.Json;

namespace Engine.Assets.Audio;

public class SingleSoundEffect : IDisposable
{
    [JsonProperty] float Volume;
    [JsonProperty] public float MaxPitch { get; private set; }
    [JsonProperty] public float MinPitch { get; private set; }
    [JsonProperty] public int MaxInstances { get; private set; }
    [JsonProperty] string FileName { get; set; }

    Sound sound;
    [JsonIgnore] bool isLoaded = false;

    [JsonIgnore]
    public Sound Sound 
    {
        get
        {
            if (!isLoaded)
            {
                GetSoundEffect();
            }
            return sound;
        }
    }

    public SingleSoundEffect(float volume, float minPitch, float maxPitch, int maxInstances, string fileName)
    {
        this.Volume = volume;
        this.MaxPitch = maxPitch;
        this.MinPitch = minPitch;
        this.FileName = fileName;
        this.MaxInstances = maxInstances;
    }

    public async void GetSoundEffect()
    {
        if (!isLoaded)
        {
            await InitSoundEffectAsync();
        }
        AudioControl.PlaySFX(sound, MinPitch, MaxPitch);
    }

    public async Task<Sound> InitSoundEffectAsync()
    {
        try
        {
            return await Task.Run(() =>
            {
                isLoaded = true;
                sound = LoadSound(Paths.SfxPath+FileName);
                return sound;
            });
        }
        catch (Exception e)
        {
            isLoaded = false;
            Raylib.TraceLog(TraceLogLevel.LOG_ERROR, $"Failed to load sound. Exception: {e.Message}");
            return default;
        }       
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
            UnloadSound(sound);
            isLoaded = false;
        }
        GC.SuppressFinalize(this);
    }
}