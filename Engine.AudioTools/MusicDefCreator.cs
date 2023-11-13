namespace EngineTools;
using Raylib_cs;
using Newtonsoft.Json;
using Engine;
using Engine.Assets.Audio;

public class MusicDefCreator
{
    static Dictionary<string, MusicTrack> musicLibrary = new();

    public void LoadFiles()
    {
        string[] musicPath = Directory.GetFiles(Paths.MusicPath);
        foreach(string file in musicPath)
        {
            string name = Path.GetFileNameWithoutExtension(file);
            string currentFile = Path.GetFileName(file);
            Raylib.TraceLog(TraceLogLevel.LOG_INFO, "Adding: "+name+" to Music Library");
            musicLibrary.TryAdd(name, new MusicTrack(1, currentFile, true, true));
        }
    }

    public void CreateMusicDef()
    {
        string jsonString = JsonConvert.SerializeObject(musicLibrary, Formatting.Indented);
        File.WriteAllText(Paths.AudioDefPath+"MusicDef.json", jsonString);
    }
}