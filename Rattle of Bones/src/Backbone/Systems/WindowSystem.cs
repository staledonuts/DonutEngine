namespace DonutEngine.Backbone.Systems;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static DonutSystems;

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
        InitWindow(settingsVars.screenWidth, settingsVars.screenHeight, "Rattle of Bones");
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
