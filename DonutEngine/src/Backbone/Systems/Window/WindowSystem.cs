namespace DonutEngine.Backbone.Systems.Window;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static DonutEngine.Backbone.Systems.Debug.CustomLogging;

public unsafe static class WindowSystem
{
    static Color color = Color.MAGENTA;

    public static void DrawUpdate()
    {
        Raylib.ClearBackground(color);
    }

    public static void LateUpdate()
    {
        
    }

    public static void Shutdown()
    {
        
    }

    public static void Start()
    {
        InitWindow(Sys.settingsContainer.screenWidth, Sys.settingsContainer.screenHeight, "DonutEngine");
        SetTargetFPS(Sys.settingsContainer.targetFPS);  
        SetTraceLogLevel(Raylib_cs.TraceLogLevel.LOG_DEBUG);
        //Custom Logging
        SetTraceLogCallback(&LogCustom);

    }

    public static void Update()
    {
        Time.RunDeltaTime();
    }

    public static void SetBackgroundColor(Color color)
    {
        WindowSystem.color = color;
    }

    public static void SetWindowFlags()
    {
        if(Sys.settingsContainer.fullScreen)
        {
            SetConfigFlags(ConfigFlags.FLAG_FULLSCREEN_MODE);
        }
        if(Sys.settingsContainer.vSync)
        {
            SetConfigFlags(ConfigFlags.FLAG_VSYNC_HINT);
        }
        if(Sys.settingsContainer.windowResizable)
        {
            SetConfigFlags(ConfigFlags.FLAG_WINDOW_RESIZABLE);
        }

    }

    public static void ToggleVsync()
    {
        if(Sys.settingsContainer.vSync)
        {
            Sys.settingsContainer.vSync = false;
            Sys.settingsContainer.SaveSettings();
            Raylib.SetWindowState(ConfigFlags.FLAG_FULLSCREEN_MODE);
        }
        else
        {
            Sys.settingsContainer.vSync = true;
            Sys.settingsContainer.SaveSettings();
            Raylib.SetWindowState(ConfigFlags.FLAG_FULLSCREEN_MODE);
        }
    }
}
