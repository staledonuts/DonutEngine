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
    [JsonProperty] List<string> FileNames { get; set; }
    
    List<Sound> sound = new();
    [JsonIgnore] List<bool> isLoaded;

    [JsonIgnore] 
    public Sound Sound 
    {
        get
        {
            int randomSound = Gen._random.RandomInteger(0, sound.Count);
            if (!isLoaded[randomSound])
            {
                GetSoundEffect(randomSound);
            }
            return sound[randomSound];
        }
    }

    public SoundEffect(float volume, float minPitch, float maxPitch, int maxInstances, List<string> fileNames)
    {
        this.Volume = volume;
        this.MaxPitch = maxPitch;
        this.MinPitch = minPitch;
        this.FileNames = fileNames;
        this.MaxInstances = maxInstances;
    }

    public async void GetSoundEffect(int randomInt)
    {
        if (!isLoaded[randomInt])
        {
            await InitSoundEffectAsync(randomInt);
        }
        EngineSystems.GetSystem<AudioControl>().PlaySFX(sound[randomInt], MinPitch, MaxPitch);
    }

    public async Task<Sound> InitSoundEffectAsync(int randomInt)
    {
        try
        {
            return await Task.Run(() =>
            {
                isLoaded[randomInt] = true;
                sound[randomInt] = LoadSound(Paths.SfxPath+FileNames[randomInt]);
                return sound[randomInt];
            });
        }
        catch (Exception e)
        {
            isLoaded[randomInt] = false;
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
        foreach(Sound _sound in sound)
        {
            int current = sound.IndexOf(_sound);
            if (isLoaded[current] && disposing)
            {
                UnloadSound(_sound);
            }
        }
        GC.SuppressFinalize(this);
    }
}