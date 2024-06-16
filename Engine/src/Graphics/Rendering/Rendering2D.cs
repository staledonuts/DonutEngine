using System.Diagnostics.Tracing;
using Engine.Assets;
using Engine.Logging;
using Engine.Systems;
using Raylib_cs;

namespace Engine.RenderSystems;
public static partial class Rendering2D
{
    static Rendering2D()
    {
        _layers = new();
        _renderTextures = new();
    }

    static Dictionary<int, Layer> _layers;
    static Dictionary<int, RenderTexture2D> _renderTextures;

    public static void Render()
    {
        Raylib.BeginDrawing();
        Backgrounds.DrawBackground();
        DrawToRenderTextures();
        RenderComposition();
        Raylib.EndDrawing();
    }

    static void DrawToRenderTextures()
    {
        foreach(KeyValuePair<int, RenderTexture2D> rt in _renderTextures)
        {
            Raylib.BeginTextureMode(rt.Value);
            foreach(KeyValuePair<int, Layer> layr in _layers)
            {
                RenderLayer(layr.Value);
            }
            Raylib.EndTextureMode();
        }
    }

    static void RenderComposition()
    {
        Raylib.BeginShaderMode(ShaderLib.UseShader("main"));
        foreach(KeyValuePair<int, RenderTexture2D> rt in _renderTextures)
        {
            Raylib.DrawTextureRec(rt.Value.Texture, new Rectangle(0, 0, rt.Value.Texture.Width, -rt.Value.Texture.Height), new(0, 0), Color.White);
        }
        Raylib.EndShaderMode();
    }

    internal static void QueueAtLayer(int layerPos, IRenderSorting renderData)
    {
        if(!_layers.ContainsKey(layerPos))
        {
            RenderTexture2D renderTexture = new();
            _renderTextures.Add(layerPos, renderTexture);
            Raylib.TraceLog(TraceLogLevel.Info, "Created RenderTexture with ID:"+renderTexture.Id);
            _layers.Add(layerPos, new Layer());
        }
        _layers.GetValueOrDefault(layerPos).RenderBatch.Enqueue(renderData);
            
    }

    internal static void RenderLayer(Layer layer)
    {
        layer.RenderBatch.TrimExcess();
        int length = layer.RenderBatch.Count;
        for (int i = 0; i < length; i++)
        {
            IRenderSorting r = layer.RenderBatch.Dequeue();
            switch (r)
            {
                case Texture2DData t2d:
                   Raylib.DrawTexturePro(t2d._tex, t2d._UVpos, t2d._rectTarget, t2d._origin, t2d._orientation, t2d._color);
                   t2d.Dispose();
                   break;
                case LineData lineD:
                    Raylib.DrawLineEx(lineD._pos1, lineD._pos2, lineD._width, lineD._color);
                    lineD.Dispose();
                    break;
                case ImageData imgData:
                    Raylib.DrawTexture(imgData._tex, (int)imgData._pos.X, (int)imgData._pos.Y, imgData._color);
                    imgData.Dispose();
                    break;
            }
        }
    }
}


