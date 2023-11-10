namespace Engine.Systems.UI;
using Raylib_cs;
using ImGuiNET;
using Engine;

public static class FontSetup
{
    public static void Init()
    {
        string pathToFonts = Paths.FontPath;
        string[] fontPath = Directory.GetFiles(pathToFonts, "*.ttf");
        
        foreach(string ttfFile in fontPath)
        {
            string name = Path.GetFileNameWithoutExtension(ttfFile);
            Raylib.TraceLog(TraceLogLevel.LOG_DEBUG, "Loading Font: "+name+"");
            ImGui.GetIO().Fonts.AddFontFromFileTTF(ttfFile, 8);
        }
    }
    
}