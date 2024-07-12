using Engine.Assets;
using Engine.Systems;

using Engine.RenderSystems;
using System.Numerics;
using Engine.Enums;
using Engine.Systems.UI.Skeleton;
using Raylib_CSharp.Textures;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Transformations;

namespace Engine;

public static partial class Draw2D
{
    public static void DrawTexture2D(Layers layer, Texture2D tex, Rectangle UVcoord, Rectangle targetRect, Vector2 origin, float orientation, Color color)
    {
        Rendering2D.QueueAtLayer(layer, new Rendering2D.Texture2DData(tex, UVcoord, targetRect, origin, orientation, color, layer, 1));
    }

    public static void DrawLine2D(Layers layer, Vector2 pos1, Vector2 pos2, float width, Color color)
    {
        Rendering2D.QueueAtLayer(layer, new Rendering2D.LineData(pos1, pos2, width, color, layer, 1));
    }
    
    public static void DrawCircle2D(Layers layer, Vector2 pos, float radius, Color color)
    {
        Rendering2D.QueueAtLayer(layer, new Rendering2D.CircleData(pos, radius, color, layer, 1));
    }

    public static void DrawRectangle2D(Layers layer, Vector2 pos, Vector2 size, Color color)
    {
        Rendering2D.QueueAtLayer(layer, new Rendering2D.RectangleData(pos, size, color, layer));
    }

    public static void DrawRectanglePro2D(Layers layer, Rectangle rectangle, Color color)
    {
        Rendering2D.QueueAtLayer(layer, new Rendering2D.RectangleProData(rectangle, color, layer));
    }

    public static void DrawText2D(Layers layer, Style style, string text, Vector2 pos, Vector2 padding, Color bgColor, Color fgColor)
    {
        Rendering2D.QueueAtLayer(layer, new Rendering2D.TextDrawData(style, text, pos, padding, bgColor, layer, fgColor));
    }

    public static void DrawImage(Layers layer, Texture2D tex, Vector2 pos, Color color)
    {
        Rendering2D.QueueAtLayer(layer, new Rendering2D.ImageData(tex, pos, color, layer, 1));
    }


}