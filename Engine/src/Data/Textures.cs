using Engine.Logging;
using Raylib_cs;
namespace Engine.Data
{
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
        }
        static readonly string[] fileExts;
        static readonly Dictionary<string, Texture2D> textureLibrary;

        /// <summary>
        /// Try to load all Textures into your library.
        /// </summary>
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
                    foreach(string s in fileExts)
                    {
                        CheckforTextures(Directory.GetFiles(pathToTextures, s, SearchOption.AllDirectories));
                    }   
                }
            }
            catch
            {
                Directory.CreateDirectory(Paths.TexturesPath);
                InitTextureLibrary();
            }
        }

        private static void CheckforTextures(string[] paths)
        {
            foreach(string File in paths)
            {
                string name = Path.GetFileNameWithoutExtension(File);
                Raylib.TraceLog(TraceLogLevel.LOG_DEBUG, "Adding: "+name+" to TexLib");
                textureLibrary.TryAdd(name, Raylib.LoadTexture(File));
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
                Raylib.TraceLog(TraceLogLevel.LOG_DEBUG, "Unloaded: "+textureName+" from TexLib");
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