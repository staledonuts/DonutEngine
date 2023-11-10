/*using Raylib_cs;
using DonutEngine.Backbone.Systems;
namespace DonutEngine.Backbone.DataContainers;

public class ShaderContainer
{
    readonly Dictionary<string, Shader> shaderLibrary = new();
    public void InitShaderContainer()
    {
        try
        {
            if (Directory.Exists(Paths.app + Engine.settings.texturesPath))  
            {
                string emptyPath = Paths.app + Engine.settings.texturesPath + "empty.png";
                if (File.Exists(emptyPath))
                {
                    Shader tex = Raylib.LoadShader(emptyPath);
                    shaderLibrary.Add("empty", tex);
                }
                else
                {
                    // Create a new empty texture with a solid color 
                    Image image = Raylib.GenImageColor(16, 16, Color.RED);
                    // Save the new texture to a PNG file
                    Raylib.ExportImage(image, emptyPath);
                }
                string pathToTextures = Paths.app+Engine.settings.texturesPath;
                string[] texturePath = Directory.GetFiles(pathToTextures, "*.png");
                
                foreach(string pngFile in texturePath)
                {
                    string name = Path.GetFileNameWithoutExtension(pngFile);
                    Raylib.TraceLog(TraceLogLevel.LOG_DEBUG, "Adding: "+name+" to TexLib");
                    textureLibrary.TryAdd(name, Raylib.LoadTexture(pngFile));
                }
            }
        }
        catch
        {
            Directory.CreateDirectory(Paths.app + Engine.settings.texturesPath);
            InitShaderContainer();
        }
    }
    
    public Texture2D GetShader(string textureName)
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
    
    public void EmptyShaderLibrary()
    {
        foreach(KeyValuePair<string, Shader> keyValuePair in shaderLibrary)
        {
            Raylib.UnloadShader(keyValuePair.Value);
            textureLibrary.Remove(keyValuePair.Key);
        }
    }
}*/