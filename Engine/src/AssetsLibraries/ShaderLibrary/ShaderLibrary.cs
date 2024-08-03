using Engine.Systems;
using Raylib_CSharp;
using Raylib_CSharp.Logging;
using Raylib_CSharp.Shaders;
namespace Engine.Assets;

public static class ShaderLib
{
    static Dictionary<string, ShaderData> shaderLibrary = new();
    

    public static void Initialize()
    {
        string[] shaderFiles = Directory.GetFiles(Paths.ShaderPath);
        foreach(string file in shaderFiles)
        {
            string name = Path.GetFileNameWithoutExtension(file);
            string currentFile = Path.GetFileName(file);
            string fsFile;
            string vsFile;
            string compareExt = Path.GetExtension(currentFile);
            if(compareExt == ".fs")
            {
                fsFile = file;
                string vsTest = Path.ChangeExtension(Paths.ShaderPath+currentFile, ".vs");
                if(Path.Exists(vsTest))
                {
                    vsFile = vsTest;
                    Logger.TraceLog(TraceLogLevel.Info, "Adding: "+name+" Vertex and Fragment to Shader Library");
                }
                else
                {
                    vsFile = "empty";
                    Logger.TraceLog(TraceLogLevel.Info, "Adding: "+name+" Fragment to Shader Library");
                }
                shaderLibrary.TryAdd(name, new(vsFile, fsFile));
            }
            else if(!shaderLibrary.ContainsKey(name))
            {
                vsFile = file;
                string fsTest = Path.ChangeExtension(Paths.ShaderPath+currentFile, ".fs");
                if(Path.Exists(fsTest))
                {
                    fsFile = fsTest;
                    Logger.TraceLog(TraceLogLevel.Info, "Adding: "+name+" Vertex and Fragment to Shader Library");
                }
                else
                {
                    fsFile = "empty";
                    Logger.TraceLog(TraceLogLevel.Info, "Adding: "+name+" Fragment to Shader Library");
                }
                shaderLibrary.TryAdd(name, new(vsFile, fsFile));
            }
            
        }
    }


    public static ShaderData UseShader(string name)
    {
        if(shaderLibrary.TryGetValue(name, out ShaderData shader))
        {
            return shader;
        }
        else
        {
            return null;
        }
    }
}