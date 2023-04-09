using Raylib_cs;
using static Raylib_cs.Raylib;
using IniParser.Model;
using IniParser;
using DonutEngine.Backbone.Systems;
using System.Collections;
namespace DonutEngine.Backbone;
public partial class AudioControl : Systems.SystemsClass
{

    private List<AudioStream> audioStreams = new List<AudioStream>();
    private Dictionary<string, Sound> soundsLibrary = new Dictionary<string, Sound>();
    private Dictionary<string, Music> musicLibrary = new Dictionary<string, Music>();
    private Music currentMusic;
    private Random random = new();
    private string? currentSongName = null;

    public override void Start()
    {
        InitAudioDevice();
        InitAudioLibrary();
        Raylib.TraceLog(TraceLogLevel.LOG_INFO, "------[ AudioSystem Initialized ]------");
    }

    public void InitAudioLibrary()
    {
        string firstSong = "Intro";
        FileIniDataParser parser = new();
        IniData data = parser.ReadFile(DonutFilePaths.app+DonutSystems.settingsVars.audioDefPath);
        foreach (SectionData sectionName in data.Sections)
        {
            string audioType = sectionName.SectionName;
            switch(audioType)
            {
                case "SFX":
                {
                    foreach(KeyData keys in sectionName.Keys)
                    {
                        //DonutSystems.uISystem.SetLoadingItem("Loading SFX: "+keys.KeyName);
                        soundsLibrary.Add(keys.KeyName, LoadSound(DonutFilePaths.app+DonutSystems.settingsVars.sfxPath+keys.Value));
                    }
                }
                break;
                case "Music":
                {
                    firstSong = sectionName.Keys.First().KeyName;
                    foreach(KeyData keys in sectionName.Keys)
                    {
                        //DonutSystems.uISystem.SetLoadingItem("Loading Music: "+keys.KeyName);
                        musicLibrary.Add(keys.KeyName, LoadMusicStream(DonutFilePaths.app+DonutSystems.settingsVars.musicPath+keys.Value));
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
