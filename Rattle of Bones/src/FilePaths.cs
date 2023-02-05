using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Raylib_cs;

namespace DonutEngine
{
    public static class FilePaths
    {
        
        public readonly static string app =  AppDomain.CurrentDomain.BaseDirectory;
        public const string assets = "Assets/";
        public const string sounds = "Sounds/";
    }
}