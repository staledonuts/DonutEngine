namespace DonutEngine.Backbone.Systems;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static DonutSystems;

public class WindowSystem : SystemsClass
{
    

    public override void DrawUpdate()
    {
        Raylib.ClearBackground(Color.GRAY);
        //Raylib.DrawText(Raylib.GetFPS().ToString(), (int) + 12, (int)DonutSystems.cameraHandler.donutcam.offset.Y + 24, 20, Color.BLACK);
        //Raylib.DrawRectangleV(new(0.0f, 100.0f),new(500,30), Raylib_cs.Color.BLACK);
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
