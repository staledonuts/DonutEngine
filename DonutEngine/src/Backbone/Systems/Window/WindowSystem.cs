namespace DonutEngine.Backbone.Systems.Window;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static DonutEngine.Backbone.Systems.Debug.CustomLogging;

public unsafe class WindowSystem : SystemsClass
{
    Color color = Color.MAGENTA;

    public override void DrawUpdate()
    {
        Raylib.ClearBackground(color);
    }

    public override void LateUpdate()
    {
        
    }

    public override void Shutdown()
    {
        
    }

    public override void Start()
    {
        InitWindow(Sys.settingsVars.screenWidth, Sys.settingsVars.screenHeight, "DonutEngine");
        SetTargetFPS(Sys.settingsVars.targetFPS);  
        SetTraceLogLevel(Raylib_cs.TraceLogLevel.LOG_DEBUG);
        //Custom Logging
        SetTraceLogCallback(&LogCustom);

    }

    public override void Update()
    {
        Time.RunDeltaTime();
    }

    public void SetBackgroundColor(Color color)
    {
        this.color = color;
    }

    public void SetWindowFlags()
    {
        if(Sys.settingsVars.fullScreen)
        {
            SetConfigFlags(ConfigFlags.FLAG_FULLSCREEN_MODE);
        }
        if(Sys.settingsVars.vSync)
        {
            SetConfigFlags(ConfigFlags.FLAG_VSYNC_HINT);
        }
        if(Sys.settingsVars.windowResizable)
        {
            SetConfigFlags(ConfigFlags.FLAG_WINDOW_RESIZABLE);
        }

    }

    public void ToggleVsync()
    {
        if(Sys.settingsVars.vSync)
        {
            Sys.settingsVars.vSync = false;
            Sys.settingsVars.SaveSettings();
            Raylib.SetWindowState(ConfigFlags.FLAG_FULLSCREEN_MODE);
        }
        else
        {
            Sys.settingsVars.vSync = true;
            Sys.settingsVars.SaveSettings();
            Raylib.SetWindowState(ConfigFlags.FLAG_FULLSCREEN_MODE);
        }
    }
}
