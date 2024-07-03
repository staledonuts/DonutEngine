using Raylib_cs;
namespace Engine.Assets;
/// <summary>
/// Font loader and library. Uses the FontPath to load all .ttf files from that folder.
/// To load a Font use the correct string name without the extension. 
/// For example "PixelOperator" which is a font i choose to include in this engine.
/// </summary>
public static class Fonts
{
    static readonly Dictionary<string, Font> fontLibrary = new();
    /// <summary>
    /// Try to load all your fonts into the libray.
    /// </summary>
    public static void Initialize()
    {
        try
        {
            if (Directory.Exists(Paths.FontPath))  
            {
                string[] fontPath = Directory.GetFiles(Paths.FontPath, "*.ttf");
                
                foreach(string fontFile in fontPath)
                {
                    string name = Path.GetFileNameWithoutExtension(fontFile);
                    #if DEBUG
                    Raylib.TraceLog(TraceLogLevel.Debug, "Adding: "+name+" to FontLib");
                    #endif
                    fontLibrary.TryAdd(name, Raylib.LoadFont(fontFile));
                }
            }
            else
            {
                #if DEBUG
                Raylib.TraceLog(TraceLogLevel.Warning, "There is no font folder. Creating...");
                #endif
                Directory.CreateDirectory(Paths.FontPath);
            }
        }
        catch
        {
            Directory.CreateDirectory(Paths.TexturesPath);
            Initialize();
        }
    }
    /// <summary>
    /// Use this method to find your font.
    /// If font cannot be found, return Raylibs default font.
    /// </summary>
    /// <param name="fontName">should be the string name of the font. For example "PixelOperator". No file extension.</param>
    /// <returns>Font for usage.</returns>
    public static Font GetFont(string fontName)
    {
        if(fontLibrary.TryGetValue(fontName, out Font font))
        {
            #if DEBUG
            Raylib.TraceLog(TraceLogLevel.Info, "Using "+fontName);
            #endif
            return font;
        }
        else
        {
            return Raylib.GetFontDefault();
        }
    }

    /// <summary>
    /// Empty the library of fonts. Unload everything.
    /// </summary>
    public static void FlushFontLibrary()
    {
        foreach(KeyValuePair<string, Font> pair in fontLibrary)
        {
            Raylib.UnloadFont(pair.Value);
            fontLibrary.Remove(pair.Key);
        }
    }
}