namespace DonutEngine.Backbone.Systems;
using Raylib_cs;
using static Raylib_cs.Raylib;

public class WindowSystem
{
    public WindowSystem()
    {
        //DonutSystems.InitEvent += InitializeWindow;
        //DonutSystems.current.ShutdownEvent += ShutdownAudioSystem;
    }
    public void InitializeWindow()
    {
        InitWindow(Program.settingsVars.screenWidth, Program.settingsVars.screenHeight, "Rattle of Bones");
        SetTargetFPS(60);
    }

}
