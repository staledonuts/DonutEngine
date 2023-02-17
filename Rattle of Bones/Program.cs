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
    
    public static void Main()
    {
        windowSystem.InitializeWindow();
        audioSystem.InitializeAudioSystem();
        inputEventSystem.InitializeInputSystem();
        SceneManager.InitScene();
        ECS.ProcessStart();
        MainLoopUpdate();
        Shutdown();
    }

    static void Shutdown()
    {
        DonutSystems.ShutdownSystems();
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
            DonutSystems.UpdateDraw();
            ECS.ProcessDrawUpdate();
            EndDrawing();
        }

        static void UpdateLate()
        {
            DonutSystems.UpdateLate();
            ECS.ProcessLateUpdate();
        }
    }
}
static class GameObjects
{
    
    public static Player player = new();

}