using DonutEngine;
using DonutEngine.Backbone;
using DonutEngine.Backbone.Systems;
using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raylib;

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
            
            InputEventSystem.UpdateInputEvent();
            DonutSystems.UpdateSystems();
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
