using DonutEngine;
using DonutEngine.Backbone;
using DonutEngine.Backbone.Systems;
using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raylib;

static class Program
{
    public static readonly SettingsVars settingsVars = new();
    public static readonly AudioSystem audioSystem = new();
    public static readonly WindowSystem windowSystem = new();
    public static Camera2D donutCamera = new()
    {
        zoom = 1.0f,
        offset = new Vector2(settingsVars.screenWidth / 2, Program.settingsVars.screenHeight / 2),
        target = new Vector2(settingsVars.screenWidth / 2, Program.settingsVars.screenHeight / 2)
    };

    
    public static void Main()
    {
        
        windowSystem.InitializeWindow();
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
            BeginDrawing();
            BeginMode2D(donutCamera);
            DonutSystems.UpdateDraw();
            ECS.ProcessDrawUpdate();
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
