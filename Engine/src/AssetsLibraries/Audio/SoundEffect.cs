using Raylib_CSharp;
using Raylib_CSharp.Audio;
using Newtonsoft.Json;
using Engine.Systems;
using Engine.Utils.Extensions;
using Raylib_CSharp.Logging;

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
        EngineSystems.GetSystem<AudioControl>().PlaySFX(this, new(0,0));
    }

    public async Task<Sound> InitSoundEffectAsync()
    {
        try
        {
            return await Task.Run(() =>
            {
                isLoaded = true;
                Sound.Load(Paths.SfxPath+FileName);
                return _sound;
            });
        }
        catch (Exception e)
        {
            isLoaded = false;
            #if DEBUG
            Logger.TraceLog(TraceLogLevel.Error, $"Failed to load sound. Exception: {e.Message}");
            #endif
            return default;
        }       
    }

    public void Play()
    {
        _sound.Play();
    }
    
    public void Stop()
    {
        _sound.Stop();
    }

    public void SetSoundPan(float pan)
    {
        _sound.SetPan(pan);
    }

    public void SetSoundPitch(float pitch)
    {
        
    }

    public void RandomizePitch(Random rand)
    {
        _sound.SetPitch(rand.NextSingle() * (MaxPitch - MinPitch) + MinPitch);
    }

    public void SetVolume(float volume)
    {
        _sound.SetVolume(volume);
    }

    public void Dispose()
    {
        this.Dispose(true);
    }

    public void Dispose(bool disposing)
    {
        if (isLoaded && disposing)
        {
            _sound.Unload();
        }
        GC.SuppressFinalize(this);
    }
}