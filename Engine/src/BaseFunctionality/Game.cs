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
using Engine;

public class Game
{
    public virtual void InitGame(string[] args)
    {
        Settings.CheckSettings();
        EngineArgParser.ArgInput(args);
        InitWindow(Settings.graphicsSettings.ScreenWidth, Settings.graphicsSettings.ScreenHeight, "DonutEngine");
        SetTargetFPS(Settings.graphicsSettings.TargetFPS);
        DonutLogging.SetLogging();
        Raylib.SetTraceLogLevel(TraceLogLevel.LOG_ALL);
        Textures.InitTextureLibrary();
        Fonts.InitFontLibrary();
        InputEventSystem.Initialize();
        InitSystems();
        SetupGame();
        MainLoop();
        ShutdownEngine();
    }

    private void ShutdownEngine()
    {
        EngineSystems.ShutdownSystems();
        CloseWindow();
    }

    private void InitSystems()
    {
        EngineSystems.AddSystem(new AudioControl());
        EngineSystems.AddSystem(new ParticleManager<ParticleState>(1024 * Settings.graphicsSettings.MaximumParticles, ParticleState.UpdateParticle));
        EngineSystems.AddSystem(new SceneManager());
        EngineSystems.AddSystem(new Camera2DSystem());
        //EngineSystems.AddSystem(new SkeletonUISystem(new Style(Color.BLANK, Color.BLANK, Color.GRAY, Color.DARKBLUE, Color.DARKGRAY, Color.GRAY, Color.SKYBLUE, Color.DARKGRAY, Fonts.GetFont("PixelOperator"), 24, 1)));
    }

    /// <summary>
    /// This is where you create and initialize all your systems and scenes <see cref="Point"/>.
    /// </summary>
    public virtual void SetupGame()
    {
        //override me.
    }

    /// <summary>
    /// Main Loop of the Engine.
    /// The idea of there updaters is that you should be using Update() for game logic, DrawUpdate() for drawing and LateUpdate() for things like physics calculation and non-gameplay affecting systems.
    /// </summary>
    void MainLoop()
    {
        while (!WindowShouldClose())
        {
            //Physics, Player logic and so forth. Before draw.
            UpdateLoop();

            //All draw logic.
            DrawUpdate();
            
            //Camera and less priority nesseccary things go here.
            LateUpdate();
        }

        static void UpdateLoop()
        {
            EngineSystems.Update();
        }

        static void DrawUpdate()
        {
            BeginDrawing();
            Backgrounds.DrawBackground();
            EngineSystems.DrawUpdate();
            EndDrawing();
        }

        static void LateUpdate()
        {
            EngineSystems.LateUpdate();
        }
    }
}

