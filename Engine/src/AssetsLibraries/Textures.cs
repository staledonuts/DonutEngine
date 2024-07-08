
using Raylib_CSharp.Colors;
using Raylib_CSharp.Images;
using Raylib_CSharp.Logging;
using Raylib_CSharp.Textures;

namespace Engine.Assets;

/// <summary>
/// Texture Library. Uses the TexturesPath to load all image files from that folder.
/// To load a Texture use the correct string name without the extension. 
/// For example "donutengine-icon" which is a picture i choose to include in this engine.
/// </summary>
public static class Textures
{
    static Textures()
    {
        fileExts = new string[] { "*.png", "*.bmp", "*.tga", "*.jpg", "*.gif", "*.qoi*", "*.psd" };
        textureLibrary = new();
        filePaths = new();
    }
    static readonly string[] fileExts;
    static readonly Dictionary<string, Texture2D> textureLibrary;
    static readonly Dictionary<string, string> filePaths;

    /// <summary>
    /// Try to load all Textures into your library.
    /// </summary>
    public static void Initialize()
    {
        try
        {
            if (Directory.Exists(Paths.TexturesPath))  
            {
                string emptyPath = Paths.TexturesPath + "empty.png";
                if (File.Exists(emptyPath))
                {
                    Texture2D tex = Texture2D.Load(emptyPath);
                    textureLibrary.Add("empty", tex);
                }
                else
                {
                    // Create a new empty texture with a solid color 
                    Image image = Image.GenColor(16, 16, Color.Red);
                    // Save the new texture to a PNG file
                    image.Export(emptyPath);
                }
                string pathToTextures = Paths.TexturesPath;
                foreach(string s in fileExts)
                {
                    CheckforTextures(Directory.GetFiles(pathToTextures, s, SearchOption.AllDirectories));
                }   
            }
        }
        catch
        {
            Directory.CreateDirectory(Paths.TexturesPath);
            Initialize();
        }
    }

    private static void CheckforTextures(string[] paths)
    {
        foreach(string File in paths)
        {
            string name = Path.GetFileNameWithoutExtension(File);
            #if DEBUG
            Logger.TraceLog(TraceLogLevel.Debug, "Adding: "+name+" to TexLib");
            #endif
            textureLibrary.TryAdd(name, new());
            filePaths.TryAdd(name, File);
        }
    }
    
    public static Texture2D GetTexture(string textureName)
    {
        Texture2D texture;
        try
        {
            textureLibrary.TryGetValue(textureName, out texture);
            if(!texture.IsReady())
            {
                #if DEBUG
                Logger.TraceLog(TraceLogLevel.Debug, textureName+" was not loaded.");
                #endif
                filePaths.TryGetValue(textureName, out string toLoadString);
                textureLibrary.Remove(textureName);
                textureLibrary.Add(textureName, Texture2D.Load(toLoadString));
                textureLibrary.TryGetValue(textureName, out texture);
            }
            return texture;
        }
        catch
        {
            textureLibrary.TryGetValue("empty", out texture);
            return texture;
        }
    }

    public static void UnloadTexture(string textureName)
    {
        Texture2D texture;
                
        if(textureLibrary.TryGetValue(textureName, out texture))
        {
            texture.Unload();
            textureLibrary.Remove(textureName);
            #if DEBUG
            Logger.TraceLog(TraceLogLevel.Debug, "Unloaded: "+textureName+" from TexLib");
            #endif
        }
    }
    
    public static void FlushTextureLibrary()
    {
        foreach(KeyValuePair<string, Texture2D> pair in textureLibrary)
        {
            pair.Value.Unload();
            textureLibrary.Remove(pair.Key);
        }
    }

    public static void UnloadTextureLibrary()
    {
        foreach(KeyValuePair<string, Texture2D> pair in textureLibrary)
        {
            pair.Value.Unload();
        }
    }
}