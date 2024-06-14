using Microsoft.VisualBasic;
using Raylib_cs;
namespace Engine.Assets;

public class ShaderInstance
{
    Shader _shader;

    

    public ShaderInstance(Shader shader)
    {
        _shader = shader;
    }

    public Shader Shader 
    {
        get
        {
            return _shader;
        }
    }

}