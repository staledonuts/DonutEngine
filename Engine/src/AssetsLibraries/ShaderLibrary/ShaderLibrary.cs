using Engine.Systems;
using Raylib_CSharp;
using Raylib_CSharp.Logging;
using Raylib_CSharp.Shaders;
namespace Engine.Assets;

public static class ShaderLib
{
    static Dictionary<string, MaterialInstance> shaderLibrary = new();
    

    public static void Initialize()
    {
        string[] shaderFiles = Directory.GetFiles(Paths.ShaderPath);
        foreach(string file in shaderFiles)
        {
            string name = Path.GetFileNameWithoutExtension(file);
            string currentFile = Path.GetFileName(file);
            string compareExt = Path.GetExtension(currentFile);
            if(compareExt == ".fs")
            {
                if(Path.Exists(Path.ChangeExtension(Paths.ShaderPath+currentFile, ".vs")))
                {
                    string vs = Path.ChangeExtension(currentFile, ".vs");
                    shaderLibrary.TryAdd(name, new(Shader.Load(Paths.ShaderPath+vs, Paths.ShaderPath+currentFile), name));
                    Logger.TraceLog(TraceLogLevel.Info, "Adding: "+name+" Vertex and Fragment to Shader Library");
                }
                else
                {
                    Logger.TraceLog(TraceLogLevel.Info, "Adding: "+name+" Fragment to Shader Library");
                    shaderLibrary.TryAdd(name, new(Shader.Load(null, Paths.ShaderPath+currentFile), name));
                }
            }
            
        }
    }


    public static MaterialInstance UseShader(string name)
    {
        shaderLibrary.TryGetValue(name, out MaterialInstance shader);
        return shader;
    }
}