using DonutEngine;
using DonutEngine.Backbone;
using DonutEngine.Input;
using Raylib_cs;

static class Program
{
    public static int framesCounter = 0;
    public static void Main()
    {
        Raylib.InitWindow(SettingsContainer.screenWidth, SettingsContainer.screenHeight, "Rattle of Bones");
        Raylib.SetTargetFPS(60);
        Raylib.InitAudioDevice();
        ScreenManager.current.InitScreenManager();
        GameContainer.current.InitGameContainer();
        ECS.Registry.Start();
        MainLoopUpdate();
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
            InputSystem.UpdateInputVectorComposite();
            ScreenManager.current.CurrentScreen();
        }

        static void UpdateDraw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);
            ScreenManager.current.DrawCurrentScreen(SettingsContainer.screenWidth, SettingsContainer.screenHeight);
            Raylib.EndDrawing();
        }

        static void UpdateLate()
        {
            ScreenManager.current.LateCurrentScreen();
        }
    }
}