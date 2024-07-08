using Raylib_CSharp.Fonts;
using Raylib_CSharp.Logging;
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
                    Logger.TraceLog(TraceLogLevel.Debug, "Adding: "+name+" to FontLib");
                    #endif
                    fontLibrary.TryAdd(name, Font.Load(fontFile));
                }
            }
            else
            {
                #if DEBUG
                Logger.TraceLog(TraceLogLevel.Warning, "There is no font folder. Creating...");
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
            Logger.TraceLog(TraceLogLevel.Info, "Using "+fontName);
            #endif
            return font;
        }
        else
        {
            return Font.GetDefault();
        }
    }

    /// <summary>
    /// Empty the library of fonts. Unload everything.
    /// </summary>
    public static void FlushFontLibrary()
    {
        foreach(KeyValuePair<string, Font> pair in fontLibrary)
        {
            pair.Value.Unload();
            fontLibrary.Remove(pair.Key);
        }
    }
}