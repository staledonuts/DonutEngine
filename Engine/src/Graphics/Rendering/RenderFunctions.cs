using Engine.Assets;
using Engine.Systems;
using Raylib_cs;
using Engine.RenderSystems;
using System.Numerics;
using Engine.Systems.LDtk;

namespace Engine;

public static class Draw2D
{
    public static void DrawTexture2D(int layer, Texture2D tex, Rectangle UVcoord, Rectangle targetRect, Vector2 origin, float orientation, Color color, MaterialInstance mat)
    {
        Rendering2D.QueueAtLayer(layer, new Rendering2D.Texture2DData(tex, UVcoord, targetRect, origin, orientation, color, layer, 1, mat));
    }

    public static void DrawLine2D(int layer, Vector2 pos1, Vector2 pos2, float width, Color color, MaterialInstance mat)
    {
        Rendering2D.QueueAtLayer(layer, new Rendering2D.LineData(pos1, pos2, width, color, layer, 1, mat));
    }

    public static void DrawImage(int layer, Texture2D tex, Vector2 pos, Color color, MaterialInstance mat)
    {
        Rendering2D.QueueAtLayer(layer, new Rendering2D.ImageData(tex, pos, color, layer, 1, mat));
    }


}