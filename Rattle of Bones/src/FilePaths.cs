using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Raylib_cs;

namespace DonutEngine
{
    public static class DonutFilePaths
    {
        
        public readonly static string app =  AppDomain.CurrentDomain.BaseDirectory;
        public readonly static string sprites = AppDomain.CurrentDomain.BaseDirectory+"Assets/Sprites/";
        public readonly static string sfx = AppDomain.CurrentDomain.BaseDirectory+"Assets/SFX/";
        public readonly static string music = AppDomain.CurrentDomain.BaseDirectory+"Assets/Music/";
        public readonly static string soundDef = AppDomain.CurrentDomain.BaseDirectory+"Scripts/Sound/SoundFileDef.ini";
        public readonly static string entities = AppDomain.CurrentDomain.BaseDirectory+"Scripts/Entities/";
        public readonly static string settings = AppDomain.CurrentDomain.BaseDirectory+"Scripts/Settings/Settings.ini";
    }
}