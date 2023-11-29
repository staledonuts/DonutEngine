using Raylib_cs;
namespace Engine.Assets;

public abstract class ShaderInstance
{
    Shader _shader;

    Shader Shader 
    {
        get
        {
            return _shader;
        }
    }

}