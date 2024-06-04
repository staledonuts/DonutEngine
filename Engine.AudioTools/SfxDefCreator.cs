namespace EngineTools;
using System.Numerics;
using Raylib_cs;
using Newtonsoft.Json;
using Engine;
using Engine.Assets.Audio;
using Engine.Assets;
using System.Collections.Generic;
using System.IO;

public class SfxDefCreator
{
    static Dictionary<string, SoundEffect> soundsLibrary = new();
    public void CreateSoundDef()
    {
        string jsonString = JsonConvert.SerializeObject(soundsLibrary, Formatting.Indented);
        File.WriteAllText(Paths.AudioDefPath+"SoundDef.json", jsonString);
    }

    public void LoadFiles()
    {
        string[] sfxPath = Directory.GetFiles(Paths.SfxPath);
        foreach(string file in sfxPath)
        {
            string name = Path.GetFileNameWithoutExtension(file);
            string currentFile = Path.GetFileName(file);
            Raylib.TraceLog(TraceLogLevel.Info, "Adding: "+name+" to Sound Library");
            //soundsLibrary.TryAdd(name, new SoundEffect(1, 1f, 1f, 5, currentFile));
        }
    }
}