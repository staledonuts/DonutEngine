using Raylib_cs;
namespace Engine.Assets;

public class MaterialInstance
{
    Shader _shader;
    string _shaderName;
    public MaterialInstance(Shader shader, string name)
    {
        _shader = shader;
        _shaderName = name;
    }

    private void UpdateMaterial()
    {
        //Raylib.SetShaderValue(_shader, Raylib.GetShaderLocation(_shader, ), )
    }

    public Shader Shader 
    {
        get
        {
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

}