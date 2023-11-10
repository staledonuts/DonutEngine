using Engine.Logging;
using Raylib_cs;
namespace Engine.Data
{
    public static class Textures
    {
        static readonly Dictionary<string, Texture2D> textureLibrary = new();
        public static void InitTextureLibrary()
        {
            try
            {
                if (Directory.Exists(Paths.TexturesPath))  
                {
                    string emptyPath = Paths.TexturesPath + "empty.png";
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
                    string pathToTextures = Paths.TexturesPath;
                    string[] texturePath = Directory.GetFiles(pathToTextures, "*.png");
                    
                    foreach(string pngFile in texturePath)
                    {
                        string name = Path.GetFileNameWithoutExtension(pngFile);
                        DonutLogging.Log(TraceLogLevel.LOG_DEBUG, "Adding: "+name+" to TexLib");
                        textureLibrary.TryAdd(name, Raylib.LoadTexture(pngFile));
                    }
                }
            }
            catch
            {
                Directory.CreateDirectory(Paths.TexturesPath);
                InitTextureLibrary();
            }
        }
        
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

        public static bool TryGetTexture(string textureName, out Texture2D tex)
        {
            try
            {
                textureLibrary.TryGetValue(textureName, out tex);
                return true;
            }
            catch
            {
                textureLibrary.TryGetValue("empty", out tex);
                return false;
            }
        }

        public static void UnloadTexture(string textureName)
        {
            Texture2D texture;
                    
            if(textureLibrary.TryGetValue(textureName, out texture))
            {
                Raylib.UnloadTexture(texture);
                textureLibrary.Remove(textureName);
                DonutLogging.Log(TraceLogLevel.LOG_DEBUG, "Unloaded: "+textureName+" from TexLib");
            }
        }
        
        public static void FlushTextureLibrary()
        {
            foreach(KeyValuePair<string, Texture2D> pair in textureLibrary)
            {
                Raylib.UnloadTexture(pair.Value);
                textureLibrary.Remove(pair.Key);
            }
        }
    }
}