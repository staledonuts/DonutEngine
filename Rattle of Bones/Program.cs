using Raylib_cs;
using DonutEngine;
using DonutEngine.Backbone;

    static class Program
    {
        public static void Main()
        {
            RuntimeUpdater runtimeUpdater = new();
            Raylib.InitWindow(800, 480, "Rattle of Bones");
            Raylib.SetTargetFPS(60);
            Raylib.InitAudioDevice();
            //Player player = new();
            
            

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);
                
                runtimeUpdater.UpdateObservers();
                
                Raylib.DrawText(Raylib_cs.Raylib.GetFPS().ToString(), 12, 24, 20, Color.BLACK);
                
                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }
    }