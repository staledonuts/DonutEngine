//#define Desktop
//#define Mobile
using Engine.Logging;
using Engine.Systems;
using Raylib_cs;


namespace Engine;

public static class App
{
    internal static void Initialize()
    {
        Raylib.InitWindow(Settings.graphicsSettings.ScreenWidth, Settings.graphicsSettings.ScreenHeight, "DonutEngine");
        Raylib.SetTargetFPS(Settings.graphicsSettings.TargetFPS);
        DonutLogging.SetLogging();
        Raylib.SetTraceLogLevel(TraceLogLevel.All);
    }
    internal static void UpdateApp()
    {
        #if OS_LINUX
            Desktop();
        #endif
    }
    public static void ToggleFullScreen() => Raylib.ToggleFullscreen();


    static void Desktop()
    {
        //Settings.cVars.Focused = Raylib.IsWindowFocused();
        if(Raylib.IsWindowResized())
        {
            Settings.graphicsSettings.ScreenWidth = Raylib.GetScreenWidth();
            Settings.graphicsSettings.ScreenHeight = Raylib.GetScreenHeight();
        }
    }




}