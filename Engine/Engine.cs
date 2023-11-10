using Raylib_cs;
using static Raylib_cs.Raylib;
using Engine.Systems;
using Engine.Data;
using Engine.Systems.Input;
using Engine.Logging;
using Engine.Systems.Audio;
using Engine.Systems.SceneSystem;
using Engine.Systems.UI.Skeleton;
using Engine.Systems.Particles;
using Engine.Utils;
namespace Engine;
public static class EngineRoot
{
    
    public static void InitEngine()
    {
        InitWindow(Settings.graphicsSettings.ScreenWidth, Settings.graphicsSettings.ScreenHeight, "DonutEngine");
        SetTargetFPS(Settings.graphicsSettings.TargetFPS);
        DonutLogging.SetLogging();
        Textures.InitTextureLibrary();
        Fonts.InitFontLibrary();
        InputEventSystem.Initialize();
        InitSystems();
    }

    private static void InitSystems()
    {
        EngineSystems.AddSystem(new AudioControl());
        EngineSystems.AddSystem(new ParticleManager<ParticleState>(1024 * Settings.graphicsSettings.MaximumParticles, ParticleState.UpdateParticle));
        EngineSystems.AddSystem(new SceneManager());
        EngineSystems.AddSystem(new Camera2DSystem());
        //EngineSystems.AddSystem(new SkeletonUISystem(new Style(Color.BLANK, Color.BLANK, Color.GRAY, Color.DARKBLUE, Color.DARKGRAY, Color.GRAY, Color.SKYBLUE, Color.DARKGRAY, Fonts.GetFont("PixelOperator"), 24, 1)));
    }
    public static void ShutdownEngine()
    {
        EngineSystems.ShutdownSystems();
        CloseWindow();
    }
}
