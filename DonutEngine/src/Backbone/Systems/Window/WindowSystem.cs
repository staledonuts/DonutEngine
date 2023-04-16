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
        //Raylib.SetWindowState(ConfigFlags.FLAG_WINDOW_RESIZABLE | ConfigFlags.FLAG_WINDOW_MAXIMIZED);
        if(Sys.settingsVars.fullScreen)
        {
            ToggleFullscreen();
        }
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
}
