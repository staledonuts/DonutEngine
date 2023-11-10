using Raylib_cs;
using System.Numerics;
namespace Engine;

public static class RaylibHelper
{
    public static Vector2 ScreenSize 
    { 
        get
        {
            return new(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
        } 
    }

    public static Rectangle Viewport
    {
        get
        {
            return new(0,0,Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
        }
    }

}