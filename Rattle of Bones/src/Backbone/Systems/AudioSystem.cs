namespace DonutEngine.Backbone.Systems;
using Raylib_cs;
using static Raylib_cs.Raylib;

public class AudioSystem
{
    public static Music music;
    public void InitializeAudioSystem()
    {
        InitAudioDevice();
        if(!IsAudioDeviceReady())
        {
            Console.Write("Cannot Init Sound!\n");
            CloseWindow();
        }
        else
        {
            Console.Write("SoundInit Successful\n");
            DonutSystems.Update += UpdateAudioSystem;
            DonutSystems.ShutdownEvent += ShutdownAudioSystem;
            music = LoadMusicStream(FilePaths.app+FilePaths.music+"deer.mp3");
            PlayMusicStream(music);
            SetMasterVolume(Program.settingsVars.currentMasterVolume);
        }
    }

    public void ShutdownAudioSystem()
    {
        DonutSystems.ShutdownEvent -= ShutdownAudioSystem;
        UnloadMusicStream(music);
        CloseAudioDevice();
    }

    public void PlaySound()
    {
        
    }

    public void PlayMusic(Music music)
    {
        //MusicVolume(music, Program.settingsVars.currentMusicVolume);
        PlayMusicStream(music);
    }

    public void MusicVolume(Music music, float volume)
    {
        SetMusicVolume(music, volume);
    }

    public static void UpdateAudioSystem()
    {
        UpdateMusicStream(music);
    }

}
