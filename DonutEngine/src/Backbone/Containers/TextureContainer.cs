using Raylib_cs;
using DonutEngine.Backbone.Systems;
namespace DonutEngine.Backbone.Textures;

public static class TextureContainer
{
    public static void InitTextureContainer()
    {
        textureLibrary = new();
        string emptyPath = DonutFilePaths.app + DonutSystems.settingsVars.texturesPath + "empty.png";
        if (File.Exists(emptyPath))
        {
            textureLibrary.Add("empty", Raylib.LoadTexture(emptyPath));
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
            Add(pngFile);
        }

        void Add(string str)
        {
            textureLibrary.TryAdd(str, Raylib.LoadTexture(pathToTextures+str));
        }
    }



    static Dictionary<string, Texture2D> textureLibrary;
    
    public static Texture2D GetTexture(string textureName)
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
    
    public static void EmptyTextureLibrary()
    {
        foreach(KeyValuePair<string, Texture2D> keyValuePair in textureLibrary)
        {
            Raylib.UnloadTexture(keyValuePair.Value);
            textureLibrary.Remove(keyValuePair.Key);
        }
    }

}