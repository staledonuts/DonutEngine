using Raylib_cs;
using static Raylib_cs.Raylib;
using IniParser.Model;
using IniParser;
using DonutEngine.Backbone.Systems;
namespace DonutEngine.Backbone;
public class AudioControl : Systems.SystemsClass
{

    private List<AudioStream> audioStreams = new List<AudioStream>();
    private Dictionary<string, Sound> soundsLibrary = new Dictionary<string, Sound>();
    private Dictionary<string, Music> musicLibrary = new Dictionary<string, Music>();

    private Music currentMusic;
    private string? currentSongName = null;

    public override void Start()
    {
        InitAudioDevice();
        InitAudioLibrary();
    }

    public void InitAudioLibrary()
    {
        string firstSong = "Intro";
        FileIniDataParser parser = new();
        IniData data = parser.ReadFile(DonutFilePaths.audioDef);
        foreach (SectionData sectionName in data.Sections)
        {
            string audioType = sectionName.SectionName;
            switch(audioType)
            {
                case "SFX":
                {
                    foreach(KeyData keys in sectionName.Keys)
                    {
                        soundsLibrary.Add(keys.KeyName, LoadSound(DonutFilePaths.sfx+keys.Value));
                    }
                }
                break;
                case "Music":
                {
                    firstSong = sectionName.Keys.First().KeyName;
                    foreach(KeyData keys in sectionName.Keys)
                    {
                        musicLibrary.Add(keys.KeyName, LoadMusicStream(DonutFilePaths.music+keys.Value));
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
        UpdateMusicStream(currentMusic);
    }

    public override void DrawUpdate()
    {
        //throw new NotImplementedException();
    }

    public override void LateUpdate()
    {
        //throw new NotImplementedException();
    }

    public override void Shutdown()
    {
        CloseAudioDevice();
    }

    public float CurrentMusicTime()
    {
        return Raylib.GetMusicTimePlayed(currentMusic);
    }


    public string[] GetMusicPlaylist()
    {
        return musicLibrary.Keys.ToArray<string>();
    }
    public int GetMusicPlaylistLength()
    {
        return musicLibrary.Count();
    }

    public int GetSFXPlaylistLength()
    {
        return soundsLibrary.Count();
    }

    public string[] GetSFXPlaylist()
    {
        return soundsLibrary.Keys.ToArray<string>();
    }



    public void LoadSFX(string name, string filename) 
    {
        Sound sound = LoadSound(filename);
        soundsLibrary.Add(name, sound);
    }

    public void PlaySFX(string name) 
    {
        Sound sound;
        if (soundsLibrary.TryGetValue(name, out sound)) 
        {
            Raylib.SetSoundVolume(sound, DonutSystems.settingsVars.currentSFXVolume);
            PlaySoundMulti(sound);
        }
    }

    public void StopSFX(string name) 
    {
        Sound sound;
        if (soundsLibrary.TryGetValue(name, out sound)) 
        {
            StopSound(sound);
        }
    }

    public void UnloadSFX(string name) 
    {
        Sound sound;
        if (soundsLibrary.TryGetValue(name, out sound)) 
        {
            UnloadSound(sound);
            soundsLibrary.Remove(name);
        }
    }

    public void LoadMusic(string name, string filename) 
    {
        Music music = LoadMusicStream(filename);
        musicLibrary.Add(name, music);
    }

    public void PlayMusic(string name) 
    {
        Music music;
        if (musicLibrary.TryGetValue(name, out music)) 
        {
            currentSongName = name;
            currentMusic = music;
            Raylib.SetMusicVolume(currentMusic, DonutSystems.settingsVars.currentMusicVolume);
            PlayMusicStream(currentMusic);
        }
    }

    public void PauseMusic()
    {
        if(IsMusicStreamPlaying(currentMusic))
        {
            PauseMusicStream(currentMusic);
        }
        else
        {
            ResumeMusicStream(currentMusic);
        }
    }

    public void StopMusic() 
    {
        StopMusicStream(currentMusic);
    }

    public void SetMusicVolume(float volume) 
    {
        SetMusicVolume(volume);
    }

    public void UnloadMusic(string name) 
    {
        Music music;
        if (musicLibrary.TryGetValue(name, out music)) 
        {
            UnloadMusicStream(music);
            musicLibrary.Remove(name);
        }
    }    
}
