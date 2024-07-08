namespace Engine;
using System;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Textures;
using Raylib_CSharp.Rendering;
using System.Numerics;
using Engine.Utils;
/// <summary>
/// This is where you control the background color of the window.
/// </summary>
public static class Backgrounds
{
    static Backgrounds()
    {
        clearColor = new Color(0, 0, 0, 255);
        renderBackground = false;
    }

    static bool renderBackground;
    static Texture2D background;
    static Texture2D midground;
    static Texture2D foreground;
    static float scrollingBack = 0.0f;
    static float scrollingMid = 0.0f;
    static float scrollingFore = 0.0f;
    public static Color clearColor;

    public static void LerpBackground(Color ToColor, float lerpAmount)
    {
        clearColor = ColorUtil.ColorLerp(clearColor, ToColor, lerpAmount);
    }

    public static void DrawBackground()
    {
        Graphics.ClearBackground(clearColor);
        /*if(renderBackground)
        {    
            if (scrollingBack <= -background.Width * 2)
            {
                scrollingBack = 0;
            }
            if (scrollingMid <= -midground.Width * 2)
            {
                scrollingMid = 0;
            }
            if (scrollingFore <= -foreground.Width * 2)
            {
                scrollingFore = 0;
            }
            // Draw background image twice
            // NOTE: Texture is scaled twice its size
            Raylib.DrawTextureEx(background, new Vector2(scrollingBack, 20), 0.0f, 2.0f, Color.WHITE);
            Raylib.DrawTextureEx(background, new Vector2(background.Width * 2 + scrollingBack, 20), 0.0f, 2.0f, Color.WHITE );

            // Draw midground image twice
            Raylib.DrawTextureEx(midground, new Vector2(scrollingMid, 20), 0.0f, 2.0f, Color.WHITE);
            Raylib.DrawTextureEx(midground, new Vector2(midground.Width * 2 + scrollingMid, 20), 0.0f, 2.0f, Color.WHITE);

            // Draw foreground image twice
            Raylib.DrawTextureEx(foreground, new Vector2(scrollingFore, 70), 0.0f, 2.0f, Color.WHITE);
            Raylib.DrawTextureEx(foreground, new Vector2(foreground.Width * 2 + scrollingFore, 70), 0.0f, 2.0f, Color.WHITE);
        }*/
    }
}