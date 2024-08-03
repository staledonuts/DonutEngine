using System.Numerics;
using Raylib_CSharp.Shaders;
namespace Engine.Assets;

public class ShaderData
{
    Shader _shader;
    string _shaderName;
    string _vsPath;
    string _fsPath;

    public ShaderData(string vsPath, string fsPath)
    {
        _shaderName = Path.GetFileName(fsPath);
        _fsPath = fsPath;
        _vsPath = vsPath;
    }

    private void UpdateMaterial()
    {
        //Raylib.SetShaderValue(_shader, Raylib.GetShaderLocation(_shader, ), )
    }

    public Shader Shader 
    {
        get
        {
            if(!_shader.IsReady())
            {
                if(_vsPath == "empty")
                {
                    _shader = Shader.Load(null, _fsPath);
                }
                else if(_fsPath == "empty")
                {
                    _shader = Shader.Load(_vsPath, null);
                }
                else
                {
                    _shader = Shader.Load(_vsPath, _fsPath);
                }
            }
            return _shader;
        }
    }

    public string GetShaderName
    {
        get
        {
            return _shaderName;
        }
    }

    public uint GetShaderID
    {
        get
        {
            return _shader.Id;
        }
    }

    public Span<int> GetShaderLocs
    {
        get
        {
            
            return _shader.Locs;
        }
    }

    public void SetShaderValue(string propertyName, ShaderUniformDataType uniformDataType, Vector4 vec4)
    {

    }

    public void UnloadShader()
    {
        _shader.Unload();
    }

}