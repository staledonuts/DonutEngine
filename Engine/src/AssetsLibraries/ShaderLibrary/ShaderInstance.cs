using Raylib_cs;
namespace Engine.Assets;

public class MaterialInstance
{
    Shader _shader;
    public MaterialInstance(Shader shader)
    {
        _shader = shader;
    }

    private void UpdateMaterial()
    {
        //Raylib.SetShaderValue(_shader, Raylib.GetShaderLocation(_shader, ), )
    }

    public Shader Shader 
    {
        get
        {
            UpdateMaterial();
            return _shader;
        }
    }

}