﻿using DonutEngine;
using DonutEngine.Backbone;
using Raylib_cs;

static class Program
{
    public static void Main()
    {
        Raylib.InitWindow(800, 480, "Rattle of Bones");
        Raylib.SetTargetFPS(60);
        Raylib.InitAudioDevice();
        GameContainer gameContainer = new();

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);
            Time.RunDeltaTime();
            RuntimeUpdater.current.UpdateRuntimeObservers();
            Raylib.DrawText(Raylib.GetFPS().ToString(), 12, 24, 20, Color.BLACK);
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}