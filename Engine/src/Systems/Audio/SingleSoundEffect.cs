using Raylib_cs;
using static Raylib_cs.Raylib;
using Newtonsoft.Json;
using Engine.Data;
using Engine.Logging;

namespace Engine.Systems.Audio;

public class SingleSoundEffect : IDisposable
{
    [JsonProperty] public float Volume = 0;
    [JsonProperty] public float MaxPitch = 0;
    [JsonProperty] public float MinPitch = 0;
    [JsonProperty] public int MaxInstances = 0;
    [JsonProperty] string FileName { get; set; }

    private Sound sound;
    [JsonIgnore] public bool isLoaded = false;

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
        EngineSystems.GetSystem<AudioControl>().PlaySFX(sound, MinPitch, MaxPitch);
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
            DonutLogging.Log(TraceLogLevel.LOG_ERROR, $"Failed to load sound. Exception: {e.Message}");
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