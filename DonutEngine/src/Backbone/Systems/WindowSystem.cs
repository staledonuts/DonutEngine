namespace DonutEngine.Backbone.Systems;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static DonutSystems;

public class WindowSystem : SystemsClass
{
    

    public override void DrawUpdate()
    {
        Raylib.ClearBackground(Color.BLACK);
    }

    public override void LateUpdate()
    {
        
    }

    public override void Shutdown()
    {
        
    }

    public override void Start()
    {
        InitWindow(settingsVars.screenWidth, settingsVars.screenHeight, "DonutEngine");
        //Raylib.SetWindowState(ConfigFlags.FLAG_WINDOW_RESIZABLE | ConfigFlags.FLAG_WINDOW_MAXIMIZED);
        if(settingsVars.fullScreen)
        {
            ToggleFullscreen();
        }
        SetTargetFPS(settingsVars.targetFPS);
        
    }

    public override void Update()
    {
        Time.RunDeltaTime();
    }
}
