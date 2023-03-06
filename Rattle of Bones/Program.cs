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
    public static readonly PhysicsSystem physicsSystem = new();
    public static CameraHandler cameraHandler = new();
    
    public static void Main()
    {
        DonutSystems.InitSystems();
        MainLoopUpdate();
        Shutdown();
    }

    static void Shutdown()
    {
        DonutSystems.KillSystems();
        CloseWindow();
    }
    static void MainLoopUpdate()
    {
        while (!WindowShouldClose())
        {
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
            Time.RunDeltaTime();
            BeginDrawing();
            BeginMode2D(cameraHandler.donutcam);
            ECS.ProcessDrawUpdate();
            DonutSystems.UpdateDraw();
            EndMode2D();
            EndDrawing();
        }

        static void UpdateLate()
        {
            DonutSystems.UpdateLate();
            ECS.ProcessLateUpdate();
        }
    }
}
