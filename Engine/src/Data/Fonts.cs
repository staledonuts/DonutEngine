using Engine.Logging;
using Raylib_cs;
namespace Engine.Data;

public static class Fonts
{
    static readonly Dictionary<string, Font> fontLibrary = new();

    public static void InitFontLibrary()
    {
        try
        {
            if (Directory.Exists(Paths.FontPath))  
            {
                string[] fontPath = Directory.GetFiles(Paths.FontPath, "*.ttf");
                
                foreach(string fontFile in fontPath)
                {
                    string name = Path.GetFileNameWithoutExtension(fontFile);
                    DonutLogging.Log(TraceLogLevel.LOG_DEBUG, "Adding: "+name+" to FontLib");
                    fontLibrary.TryAdd(name, Raylib.LoadFont(fontFile));
                }
            }
            else
            {
                Directory.CreateDirectory(Paths.FontPath);
            }
        }
        catch
        {
            Directory.CreateDirectory(Paths.TexturesPath);
            InitFontLibrary();
        }
    }

    public static Font GetFont(string fontName)
    {
        Font font;
        fontLibrary.TryGetValue(fontName, out font);
        Raylib.TraceLog(TraceLogLevel.LOG_INFO, "Using "+fontName);
        return font;
    }

    public static void FlushFontLibrary()
    {
        foreach(KeyValuePair<string, Font> pair in fontLibrary)
        {
            Raylib.UnloadFont(pair.Value);
            fontLibrary.Remove(pair.Key);
        }
    }
}