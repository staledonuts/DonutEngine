using Engine.Systems;
using Raylib_cs;
namespace Engine.Assets;

public class ShaderLib : SystemClass , IUpdateSys
{
    Dictionary<string, ShaderInstance> shaderLibrary = new();
    

    public override void Initialize()
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
                    shaderLibrary.TryAdd(name, new(Raylib.LoadShader(Paths.ShaderPath+vs, Paths.ShaderPath+currentFile)));
                    Raylib.TraceLog(TraceLogLevel.Info, "Adding: "+name+" Vertex and Fragment to Shader Library");
                }
                else
                {
                    Raylib.TraceLog(TraceLogLevel.Info, "Adding: "+name+" Fragment to Shader Library");
                    shaderLibrary.TryAdd(name, new(Raylib.LoadShader(null, Paths.ShaderPath+currentFile)));
                }
            }
            
        }
    }

    public void Update()
    {
        
    }

    public Shader UseShader(string name)
    {
        shaderLibrary.TryGetValue(name, out ShaderInstance shader);
        
        return shader.Shader;
    }
}