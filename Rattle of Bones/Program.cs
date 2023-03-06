using DonutEngine;
using DonutEngine.Backbone;
using DonutEngine.Backbone.Systems;
using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raylib;

static class Program
{
    public static readonly SettingsVars settingsVars = new();
    public static readonly AudioControl audioControl = new();
    public static readonly WindowSystem windowSystem = new();
    public static readonly EntityManager entityManager = new();

    public static CameraHandler cameraHandler = new();
    
    public static void Main()
    {
        windowSystem.InitializeWindow();
        cameraHandler.InitializeCamera(new(0,0));
        SceneManager.InitScene();
        MainLoopUpdate();
        Shutdown();
    }

    static void Shutdown()
    {
        //DonutSystems.ShutdownSystems();
        CloseWindow();
    }
    static void MainLoopUpdate()
    {
        while (!WindowShouldClose())
        {
            Time.RunDeltaTime();
            UpdateLoop();
            UpdateDraw();
            UpdateLate();
        }

        static void UpdateLoop()
        {
            InputEventSystem.UpdateInputEvent();
            DonutSystems.UpdateSystems();
            ECS.ProcessUpdate();
        }

        static void UpdateDraw()
        {
            BeginMode2D(cameraHandler.donutcam);
            BeginDrawing();
            DonutSystems.UpdateDraw();
            ECS.ProcessDrawUpdate();
            EndDrawing();
            EndMode2D();
        }

        static void UpdateLate()
        {
            DonutSystems.UpdateLate();
            ECS.ProcessLateUpdate();
        }
    }
}
