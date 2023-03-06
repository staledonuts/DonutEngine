namespace DonutEngine.Backbone.Systems;
using Raylib_cs;
using static Raylib_cs.Raylib;

public class WindowSystem : SystemsClass
{

    public override void DrawUpdate()
    {
        
    }

    public override void LateUpdate()
    {
        
    }

    public override void Shutdown()
    {
        
    }

    public override void Start()
    {
        InitWindow(Program.settingsVars.screenWidth, Program.settingsVars.screenHeight, "Rattle of Bones");
        if(Program.settingsVars.fullScreen)
        {
            ToggleFullscreen();
        }
        SetTargetFPS(60);
    }

    public override void Update()
    {
        
    }
}
