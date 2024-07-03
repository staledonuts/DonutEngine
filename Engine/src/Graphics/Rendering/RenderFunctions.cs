using Engine.Assets;
using Engine.Systems;
using Raylib_cs;
using Engine.RenderSystems;
using System.Numerics;
using Engine.Systems.LDtk;
using Engine.Enums;

namespace Engine;

public static class Draw2D
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

    public static void DrawImage(Layers layer, Texture2D tex, Vector2 pos, Color color)
    {
        Rendering2D.QueueAtLayer(layer, new Rendering2D.ImageData(tex, pos, color, layer, 1));
    }


}