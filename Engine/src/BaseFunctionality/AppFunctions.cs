//#define Desktop
//#define Mobile
using Engine.Logging;
using Engine.Systems;
using Raylib_CSharp.Logging;
using Raylib_CSharp.Windowing;


namespace Engine;

public static class App
{
    internal static void Initialize()
    {
        Logger.Init();
        Logger.SetTraceLogLevel(TraceLogLevel.All);
        Window.Init(Settings.graphicsSettings.ScreenWidth, Settings.graphicsSettings.ScreenHeight, "DonutEngine");
        Window.SetState(ConfigFlags.ResizableWindow);
        //Window.SetTargetFPS(Settings.graphicsSettings.TargetFPS);
        //DonutLogging.SetLogging();
    }
    internal static void UpdateApp()
    {
        #if OS_LINUX
            Desktop();
        #endif
    }
    public static void ToggleFullScreen() => Window.ToggleFullscreen();


    static void Desktop()
    {
        //Settings.cVars.Focused = Raylib.IsWindowFocused();
        if(Window.IsResized())
        {
            Settings.graphicsSettings.ScreenWidth = Window.GetScreenWidth();
            Settings.graphicsSettings.ScreenHeight = Window.GetScreenHeight();
        }
    }




}