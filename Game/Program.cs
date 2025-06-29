using Raylib_cs;
using System.Numerics;

namespace DonutEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            // --- Initialization ---
            const int screenWidth = 1280;
            const int screenHeight = 720;

            Raylib.InitWindow(screenWidth, screenHeight, "DonutEngine - LDtk Loader");
            Raylib.SetTargetFPS(60);

            // Initialize your modular system manager
            Systems.InitializeCoreSystems();

            Systems.Get<LdtkSystem>().LoadProject(AppDomain.CurrentDomain.BaseDirectory+"Json/Main.ldtk");

            // --- Game Loop ---
            while (!Raylib.WindowShouldClose())
            {
                // --- Logic Update ---
                Systems.Update();


                // --- Drawing ---
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.DarkGray);

                // Let the systems handle their own drawing.
                Systems.DrawUpdate();

                // Draw FPS counter for debugging
                Raylib.DrawFPS(10, 10);
                Raylib.EndDrawing();
                
                Systems.LateUpdate();
            }

            // --- Shutdown ---
            Systems.ShutdownSystems();
            Raylib.CloseWindow();
        }
    }
}