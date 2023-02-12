using DonutEngine;
using DonutEngine.Backbone;
using DonutEngine.Backbone.Systems;
using Raylib_cs;

static class Program
{
    public static int framesCounter = 0;
    public static SettingsVars settingsVars = new();
    public static AudioSystem audioSystem = new();
    public static WindowSystem windowSystem = new();
    public static ScreenManager screenManager = new();
    public static void Main()
    {
        windowSystem.InitializeWindow();
        audioSystem.InitializeAudioSystem();
        screenManager.InitScreenManager();
        InputEventSystem.InitInputSystem();
        GameContainer.current.InitGameContainer();
        ECS.ProcessStart();
        MainLoopUpdate();
        Shutdown();
    }

    static void Shutdown()
    {
        DonutSystems.ShutdownSystems();
        Raylib.CloseWindow();
    }
    static void MainLoopUpdate()
    {
        while (!Raylib.WindowShouldClose())
        {
            Time.RunDeltaTime();
            UpdateLoop();
            UpdateDraw();
            UpdateLate();
            
        }

        static void UpdateLoop()
        {
            DonutSystems.UpdateSystems();
            screenManager.CurrentScreen();
        }

        static void UpdateDraw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);
            screenManager.DrawCurrentScreen(Program.settingsVars.screenWidth, Program.settingsVars.screenHeight);
            Raylib.EndDrawing();
        }

        static void UpdateLate()
        {
            screenManager.LateCurrentScreen();
        }
    }
}