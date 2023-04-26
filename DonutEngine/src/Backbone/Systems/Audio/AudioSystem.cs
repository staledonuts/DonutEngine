using Raylib_cs;
using static Raylib_cs.Raylib;
using IniParser.Model;
using IniParser;
namespace DonutEngine.Backbone.Systems.Audio;
public partial class AudioControl : SystemsClass
{

    private List<AudioStream> audioStreams = new List<AudioStream>();
    private Dictionary<string, Sound> soundsLibrary = new Dictionary<string, Sound>();
    private Dictionary<string, Music> musicLibrary = new Dictionary<string, Music>();
    private Music currentMusic;
    private Random random = new();
    private string? currentSongName = null;

    public override void Start()
    {
        Raylib.TraceLog(TraceLogLevel.LOG_INFO, "------[ Setting up AudioSystem ]------");
        InitAudioDevice();
        InitAudioLibrary();
        Raylib.TraceLog(TraceLogLevel.LOG_INFO, "------[ AudioSystem Initialized ]------");
    }

    public void ReloadAudioLibrary()
    {
        Raylib.StopMusicStream(currentMusic);
        foreach(KeyValuePair<string, Sound> sfx in soundsLibrary)
        {
            Raylib.UnloadSound(sfx.Value);
        }
        foreach(KeyValuePair<string, Music> music in musicLibrary)
        {
            Raylib.UnloadMusicStream(music.Value);
        }
        soundsLibrary = new Dictionary<string, Sound>();
        musicLibrary = new Dictionary<string, Music>();
        InitAudioLibrary();
    }

    public void InitAudioLibrary()
    {
        string firstSong = "Intro";
        FileIniDataParser parser = new();
        IniData data = parser.ReadFile(DonutFilePaths.app+Sys.settingsContainer.audioDefPath);
        foreach (SectionData sectionName in data.Sections)
        {
            string audioType = sectionName.SectionName;
            switch(audioType)
            {
                case "SFX":
                {
                    foreach(KeyData keys in sectionName.Keys)
                    {
                        soundsLibrary.Add(keys.KeyName, LoadSound(DonutFilePaths.app+Sys.settingsContainer.sfxPath+keys.Value));
                    }
                }
                break;
                case "Music":
                {
                    firstSong = sectionName.Keys.First().KeyName;
                    foreach(KeyData keys in sectionName.Keys)
                    {
                        musicLibrary.Add(keys.KeyName, LoadMusicStream(DonutFilePaths.app+Sys.settingsContainer.musicPath+keys.Value));
                    }
                }
                break;
            }
            
        }
        if(currentSongName == null)
        {
            PlayMusic(firstSong);
        }
    }

    public override void Update()
    {
        
    }

    public override void DrawUpdate()
    {
        
    }

    public override void LateUpdate()
    {
        UpdateMusicStream(currentMusic);
    }

    public override void Shutdown()
    {
        CloseAudioDevice();
    }



    
}
