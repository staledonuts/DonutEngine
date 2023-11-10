namespace Engine.Systems.Audio;
using Raylib_cs;

public interface ISoundEffect : IDisposable
{
    public Task<Sound> InitSoundEffectAsync();

    public void GetSoundEffect();

}