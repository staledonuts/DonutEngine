using Raylib_cs;
using static Raylib_cs.Raylib;
using DonutEngine.Backbone.Systems;
using System.Collections;
namespace DonutEngine.Backbone;

public partial class AudioControl
{
    public string[] GetMusicPlaylist()
    {
        return musicLibrary.Keys.ToArray<string>();
    }
    public int GetMusicPlaylistLength()
    {
        return musicLibrary.Count();
    }

    public float CurrentMusicTime()
    {
        return Raylib.GetMusicTimePlayed(currentMusic);
    }

    public float CurrentMusicLength()
    {
        return Raylib.GetMusicTimeLength(currentMusic);
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

    public void FadeOutCurrentMusic()
    {
        
        
    }

    public IEnumerable CrossFadeOut(Music music)
    {
        float elapsedTime = 0;
        float waitTime = 3f;
        while (elapsedTime < waitTime)
        {
            Raylib.SetMusicVolume(music, Raymath.Lerp(DonutSystems.settingsVars.currentMusicVolume, 0, 0.05f));
            elapsedTime += Time.deltaTime;
        }
        yield return null;
    }

    public IEnumerable CrossFadeIn(Music music)
    {
        float elapsedTime = 0;
        float waitTime = 3f;
        while (elapsedTime < waitTime)
        {
            Raylib.SetMusicVolume(music, Raymath.Lerp(0, DonutSystems.settingsVars.currentMusicVolume, 0.05f));
            elapsedTime += Time.deltaTime;
        }
        yield return null;
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
