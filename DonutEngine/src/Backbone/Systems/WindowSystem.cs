namespace DonutEngine.Backbone.Systems;
using Raylib_cs;
using static Raylib_cs.Raylib;

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
        InitWindow(DonutSystems.settingsVars.screenWidth, DonutSystems.settingsVars.screenHeight, "DonutEngine");
        //Raylib.SetWindowState(ConfigFlags.FLAG_WINDOW_RESIZABLE | ConfigFlags.FLAG_WINDOW_MAXIMIZED);
        if(DonutSystems.settingsVars.fullScreen)
        {
            ToggleFullscreen();
        }
        SetTargetFPS(DonutSystems.settingsVars.targetFPS);
        
    }

    public override void Update()
    {
        Time.RunDeltaTime();
    }
}
