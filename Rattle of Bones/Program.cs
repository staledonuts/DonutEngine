using DonutEngine;
using DonutEngine.Backbone;
using DonutEngine.Backbone.Systems;
using Raylib_cs;
using static Raylib_cs.Raylib;

static class Program
{
    public static SettingsVars settingsVars = new();
    public static AudioSystem audioSystem = new();
    public static WindowSystem windowSystem = new();
    public static InputEventSystem inputEventSystem = new();
    public static CameraHandler cameraHandler = new();

    
    public static void Main()
    {
        windowSystem.InitializeWindow();
        inputEventSystem.InitializeInputSystem();
        cameraHandler.InitializeCamera(new(Program.settingsVars.screenWidth / 2, Program.settingsVars.screenHeight / 2));
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
            DonutSystems.UpdateSystems();
            ECS.ProcessUpdate();
        }

        static void UpdateDraw()
        {
            BeginDrawing();
            Raylib.BeginMode2D(cameraHandler.donutcam);
            DonutSystems.UpdateDraw();
            ECS.ProcessDrawUpdate();
            Raylib.EndMode2D();
            EndDrawing();
        }

        static void UpdateLate()
        {
            
            DonutSystems.UpdateLate();
            ECS.ProcessLateUpdate();

        }
    }
}
