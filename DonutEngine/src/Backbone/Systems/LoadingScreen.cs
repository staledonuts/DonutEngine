namespace DonutEngine;
using ImGuiNET;
using Raylib_cs;


public static class LoadingScreen
{

    public static void FadeIn()
    {

    }
    public static void FadeOut()
    {

    }
    public static void DrawLoadingScreen(string LoadingItem)
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.BLACK);
        Raylib.DrawText(LoadingItem, Raylib.GetScreenWidth() / 4, Raylib.GetScreenHeight() / 2, 4, Color.WHITE);
        Raylib.EndDrawing();
    }


}