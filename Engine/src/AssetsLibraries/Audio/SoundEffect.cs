using Raylib_cs;
using static Raylib_cs.Raylib;
using Newtonsoft.Json;
using Engine.Systems;
using Engine.Utils.Extensions;

namespace Engine.Assets.Audio;

public class SoundEffect : IDisposable
{
    [JsonProperty] float Volume;
    [JsonProperty] public float MaxPitch { get; private set; }
    [JsonProperty] public float MinPitch { get; private set; }
    [JsonProperty] public int MaxInstances { get; private set; }
    [JsonProperty] string FileName { get; set; }
    
    Sound _sound = new();
    [JsonIgnore] bool isLoaded;

    [JsonIgnore] 
    public Sound Sound 
    {
        get
        {
            if (!isLoaded)
            {
                GetSoundEffect();
            }
            return _sound;
        }
    }

    public SoundEffect(float volume, float minPitch, float maxPitch, int maxInstances, string fileName)
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
        EngineSystems.GetSystem<AudioControl>().PlaySFX(_sound, MinPitch, MaxPitch);
    }

    public async Task<Sound> InitSoundEffectAsync()
    {
        try
        {
            return await Task.Run(() =>
            {
                isLoaded = true;
                _sound = LoadSound(Paths.SfxPath+FileName);
                return _sound;
            });
        }
        catch (Exception e)
        {
            isLoaded = false;
            Raylib.TraceLog(TraceLogLevel.Error, $"Failed to load sound. Exception: {e.Message}");
            return default;
        }       
    }

    public void Dispose()
    {
        this.Dispose(true);
    }

    public void Dispose(bool disposing)
    {
        if (isLoaded && disposing)
        {
            UnloadSound(_sound);
        }
        GC.SuppressFinalize(this);
    }
}