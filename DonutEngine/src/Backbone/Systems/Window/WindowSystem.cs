namespace DonutEngine.Backbone.Systems.Window;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static DonutEngine.Backbone.Systems.Debug.CustomLogging;

public unsafe class WindowSystem : SystemsClass
{
    

    public override void DrawUpdate()
    {
        Raylib.ClearBackground(Color.DARKGRAY);
    }

    public override void LateUpdate()
    {
        
    }

    public override void Shutdown()
    {
        
    }

    public override void Start()
    {
        InitWindow(DonutSystems.settingsVars.screenWidth, DonutSystems.settingsVars.screenHeight, "DonutEngine");
        //Raylib.SetWindowState(ConfigFlags.FLAG_WINDOW_RESIZABLE | ConfigFlags.FLAG_WINDOW_MAXIMIZED);
        if(DonutSystems.settingsVars.fullScreen)
        {
            ToggleFullscreen();
        }
        SetTargetFPS(DonutSystems.settingsVars.targetFPS);  
        SetTraceLogLevel(Raylib_cs.TraceLogLevel.LOG_ALL);
        //Custom Logging
        SetTraceLogCallback(&LogCustom);      
    }

    public override void Update()
    {
        Time.RunDeltaTime();
    }
}
