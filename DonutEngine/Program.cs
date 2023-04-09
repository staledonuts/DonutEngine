using DonutEngine.Backbone;
using DonutEngine.Backbone.Systems;

using static Raylib_cs.Raylib;


static class Program
{
    public static bool isLoading = false;
    
    public static void Main()
    {
        //Lets set up the different nessecary systems!
        DonutSystems.InitSystems();
        //This is where all the update loops go.
        MainLoop();
        Shutdown();
    }

    static void Shutdown()
    {
        DonutSystems.KillSystems();
        CloseWindow();
    }
    static void MainLoop()
    {
        while (!WindowShouldClose())
        {
            //Physics, Player logic and so forth. Before draw.
            UpdateLoop();

            //All draw logic.
            UpdateDraw();
            
            //Camera and less priority nesseccary things go here.
            UpdateLate();
        }

        static void UpdateLoop()
        {
            //Input system first
            InputEventSystem.UpdateInputEvent();

            //DonutSystems is the backbone of the engine.
            //here is Physics, Sound and UI updated.
            DonutSystems.UpdateSystems();

            //Entity Component system.
            //most gamelogic is updated here.
            //this is updated after most other things.
            ECS.ProcessUpdate();
        }

        static void UpdateDraw()
        {
            BeginDrawing();
            BeginMode2D(DonutSystems.cameraHandler.donutcam);
            ECS.ProcessDrawUpdate();
            DonutSystems.UpdateDraw();
            EndMode2D();
            EndDrawing();
        }

        static void UpdateLate()
        {
            DonutSystems.UpdateLate();
            ECS.ProcessLateUpdate();
        }
    }
}
