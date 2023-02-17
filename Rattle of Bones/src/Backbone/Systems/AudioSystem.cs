namespace DonutEngine.Backbone.Systems;
using Raylib_cs;
using static Raylib_cs.Raylib;

public class AudioSystem : SystemsClass
{
    public AudioSystem()
    {
        DonutSystems.SubscribeSystem(this);
    }
    public static Music music;

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

    public override void Start()
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
            music = LoadMusicStream(FilePaths.app+FilePaths.music+"lullaby.ogg");
            PlayMusicStream(music);
            SetMasterVolume(Program.settingsVars.currentMasterVolume);
        }
    }

    public override void Update()
    {
        UpdateMusicStream(music);
    }

    public override void DrawUpdate()
    {
        
    }

    public override void LateUpdate()
    {
        
    }

    public override void Shutdown()
    {
        UnloadMusicStream(music);
        CloseAudioDevice();
    }
}
