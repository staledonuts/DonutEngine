/*namespace DonutEngine.Backbone.Systems.Shaders;
using Raylib_cs;
using DonutEngine.Camera;
using static Raylib_cs.Raylib;
using static Raylib_cs.ShaderLocationIndex;


public unsafe static class DefaultShader
{
    const int GLSL_VERSION = 330;
    static Shader shader;
    static int ambientLoc = GetShaderLocation(shader, "ambient");
    static float[] ambient = new[] { 0.1f, 0.1f, 0.1f, 1.0f };

    public static void InitDefaultShader()
    {
        shader = LoadShader(Paths.ShaderPath+"glsl330/base_lighting.vs", Paths.ShaderPath+"glsl330/lighting.fs");
        shader.locs[(int)SHADER_LOC_VECTOR_VIEW] = GetShaderLocation(shader, "viewPos");
        SetShaderValue(shader, ambientLoc, ambient, ShaderUniformDataType.SHADER_UNIFORM_VEC4);
        
    }

    public static void UpdateLight()
    {
        Raylib.SetShaderValue(shader, shader.locs[(int)SHADER_LOC_VECTOR_VIEW],  CameraHandler.cam3D, ShaderUniformDataType.SHADER_UNIFORM_VEC3);
    }
}*/