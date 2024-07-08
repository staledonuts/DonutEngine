using System.Numerics;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Shaders;
namespace Engine.RenderSystems;

public struct Light
{
    public bool enabled;
    public LightType type;
    public Vector3 position;
    public Vector3 target;
    public Color color;

    public int enabledLoc;
    public int typeLoc;
    public int posLoc;
    public int targetLoc;
    public int colorLoc;
}

public enum LightType
{
    Directorional,
    Point
}

public static class Rlights
{
    public static Light CreateLight(
        int lightsCount,
        LightType type,
        Vector3 pos,
        Vector3 target,
        Color color,
        Shader shader
    )
    {
        Light light = new Light();

        light.enabled = true;
        light.type = type;
        light.position = pos;
        light.target = target;
        light.color = color;

        string enabledName = "lights[" + lightsCount + "].enabled";
        string typeName = "lights[" + lightsCount + "].type";
        string posName = "lights[" + lightsCount + "].position";
        string targetName = "lights[" + lightsCount + "].target";
        string colorName = "lights[" + lightsCount + "].color";

        light.enabledLoc = shader.GetLocation(enabledName);
        light.typeLoc = shader.GetLocation(typeName);
        light.posLoc = shader.GetLocation(posName);
        light.targetLoc = shader.GetLocation(targetName);
        light.colorLoc = shader.GetLocation(colorName);

        UpdateLightValues(shader, light);

        return light;
    }

    public static void UpdateLightValues(Shader shader, Light light)
    {
        // Send to shader light enabled state and type
        shader.SetValue(light.enabledLoc, light.enabled ? 1 : 0, ShaderUniformDataType.Int);
        shader.SetValue(light.typeLoc, (int)light.type, ShaderUniformDataType.Int);

        // Send to shader light target position values
        shader.SetValue(light.posLoc, light.position, ShaderUniformDataType.Vec3);

        // Send to shader light target position values
        shader.SetValue(light.targetLoc, light.target, ShaderUniformDataType.Vec3);

        // Send to shader light color values
        Vector4 colour = new Vector4(light.color.R / 255f, light.color.G / 255f, light.color.B / 255f, light.color.A / 255f);
        shader.SetValue(light.colorLoc, colour, ShaderUniformDataType.Vec4);
    }
}
