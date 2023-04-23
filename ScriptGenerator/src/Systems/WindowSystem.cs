namespace DonutEngine.Backbone.Systems;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static DonutSystems;

public class WindowSystem : SystemsClass
{

    public override void DrawUpdate()
    {
        Raylib.ClearBackground(Raylib_cs.Color.GRAY);
    }

    public override void LateUpdate()
    {
        
    }

    public override void Shutdown()
    {
        
    }

    public override void Start()
    {
        InitWindow(Sys.settingsVars.screenWidth, Sys.settingsVars.screenHeight, "Rattle of Bones");
        if(Sys.settingsVars.fullScreen)
        {
            ToggleFullscreen();
        }
        SetTargetFPS(Sys.settingsVars.targetFPS);
    }

    public override void Update()
    {
        
    }
}
