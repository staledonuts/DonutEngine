using Raylib_cs;
using DonutEngine.Backbone.Systems;
using System.IO;
namespace DonutEngine.Backbone.Textures;

public class TextureContainer
{
    Dictionary<string, Texture2D> textureLibrary = new Dictionary<string, Texture2D>();
    public void InitTextureContainer()
    {
        string emptyPath = DonutFilePaths.app + DonutSystems.settingsVars.texturesPath + "empty.png";
        if (File.Exists(emptyPath))
        {
            Texture2D tex = Raylib.LoadTexture(emptyPath);
            textureLibrary.Add("empty", tex);
        }
        else
        {
            // Create a new empty texture with a solid color 
            Image image = Raylib.GenImageColor(16, 16, Color.RED);
            // Save the new texture to a PNG file
            Raylib.ExportImage(image, emptyPath);
        }
        
        string pathToTextures = DonutFilePaths.app+DonutSystems.settingsVars.texturesPath;
        string[] texturePath = Directory.GetFiles(pathToTextures, "*.png");
        
        foreach(string pngFile in texturePath)
        {
            string name = Path.GetFileNameWithoutExtension(pngFile);
            Raylib.TraceLog(TraceLogLevel.LOG_DEBUG, "Adding: "+name+" to TexLib");
            textureLibrary.TryAdd(name, Raylib.LoadTexture(pngFile));
        }
    }
    
    public Texture2D GetTexture(string textureName)
    {
        Texture2D texture;
        try
        {
            textureLibrary.TryGetValue(textureName, out texture);
            return texture;
        }
        catch
        {
            textureLibrary.TryGetValue("empty", out texture);
            return texture;
        }
    }
    
    public void EmptyTextureLibrary()
    {
        foreach(KeyValuePair<string, Texture2D> keyValuePair in textureLibrary)
        {
            Raylib.UnloadTexture(keyValuePair.Value);
            textureLibrary.Remove(keyValuePair.Key);
        }
    }

}