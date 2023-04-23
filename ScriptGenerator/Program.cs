using static Raylib_cs.Raylib;
using DonutEngine.Backbone.Systems;

static class Program
{
    
    public static void Main()
    {
        DonutSystems.InitSystems();
        MainLoopUpdate();
        Shutdown();
    }

    static void Shutdown()
    {
        DonutSystems.KillSystems();
        CloseWindow();
    }
    static void MainLoopUpdate()
    {
        while (!WindowShouldClose())
        {
            UpdateLoop();
            UpdateDraw();
            UpdateLate();
        }

        static void UpdateLoop()
        {
            DonutSystems.UpdateSystems();
            //ECS.ProcessUpdate();
        }

        static void UpdateDraw()
        {
            BeginDrawing();
            //ECS.ProcessDrawUpdate();
            DonutSystems.UpdateDraw();
            EndDrawing();
        }

        static void UpdateLate()
        {
            DonutSystems.UpdateLate();
            //ECS.ProcessLateUpdate();
        }
    }
}
