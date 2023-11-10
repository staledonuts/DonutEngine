using Engine;
using EngineTools;
using CommandLine;
static class Program
{
    static Program()
    {
        musicDef = new();
        sfxDef = new();
    }
    static MusicDefCreator musicDef;
    static SfxDefCreator sfxDef;

    public static void Main(string[] args)
    {
        musicDef.LoadFiles();
        musicDef.CreateMusicDef();
        sfxDef.LoadFiles();
        sfxDef.CreateSoundDef();
        
    }
}