using DonutEngine;
using DonutEngine.Backbone;
using DonutEngine.Backbone.Rendering;
using Raylib_cs;
using static Raylib_cs.Raylib;

static class Program
{
    public static void Main()
    {
        InitWindow(800, 480, "Rattle of Bones");
        SetTargetFPS(60);
        InitAudioDevice();
        GameContainer gameContainer = new();

        while (!WindowShouldClose())
        {
            BeginDrawing();
            ClearBackground(Color.WHITE);
            Time.RunDeltaTime();
            RuntimeUpdater.current.UpdateRuntimeObservers();
            DrawText(GetFPS().ToString(), 12, 24, 20, Color.BLACK);
            EndDrawing();
        }

        CloseWindow();
    }
}