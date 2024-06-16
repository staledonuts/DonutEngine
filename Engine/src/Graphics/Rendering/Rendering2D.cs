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

    public static void Shutdown()
    {
        foreach(KeyValuePair<int, RenderTexture2D> rt in _renderTextures)
        {
            Raylib.UnloadRenderTexture(rt.Value);
        }
    }

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
        foreach(KeyValuePair<int, Layer> layr in _layers)
        {
            Raylib.BeginTextureMode(_renderTextures.GetValueOrDefault(layr.Key)); 
            Raylib.ClearBackground(Color.Blank);
            RenderLayer(layr.Value);
            Raylib.EndTextureMode();
        }
        
    }

    static void RenderComposition()
    {
        
        foreach(KeyValuePair<int, RenderTexture2D> rt in _renderTextures)
        {
            Raylib.DrawTextureRec(rt.Value.Texture, new Rectangle(0, 0, rt.Value.Texture.Width, -rt.Value.Texture.Height), new(0, 0), Color.White);
        }
    }

    internal static void QueueAtLayer(int layerPos, IRenderSorting renderData)
    {
        if(!_layers.ContainsKey(layerPos))
        {
            RenderTexture2D renderTexture = Raylib.LoadRenderTexture(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
            _renderTextures.Add(layerPos, renderTexture);
            Raylib.TraceLog(TraceLogLevel.Info, "Created RenderTexture with ID:"+renderTexture.Id);
            _layers.Add(layerPos, new Layer());
            Layer l = _layers.GetValueOrDefault(layerPos); 
            QueueAsShader(l, renderData);
        }
        else
        {
            Layer l = _layers.GetValueOrDefault(layerPos);
            QueueAsShader(l, renderData);
        }
            
    }

    static void QueueAsShader(Layer l, IRenderSorting renderData)
    {
        if(l.RenderBatch.ContainsKey(renderData.Shader))
        {
            l.RenderBatch.TryGetValue(renderData.Shader, out Queue<IRenderSorting> qu);
            qu.Enqueue(renderData);
        }
        else
        {
            l.RenderBatch.Add(renderData.Shader, new());
            l.RenderBatch.TryGetValue(renderData.Shader, out Queue<IRenderSorting> q);
            q.Enqueue(renderData);
        }
    }

    internal static void RenderLayer(Layer layer)
    {
        foreach(KeyValuePair<string, Queue<IRenderSorting>> l in layer.RenderBatch)
        {
            //Raylib.BeginShaderMode(ShaderLib.UseShader(l.Key));
            l.Value.TrimExcess();
            int length = l.Value.Count;
            for (int i = 0; i < length; i++)
            {
                if(l.Value.TryDequeue(out IRenderSorting r));
                {
                    
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
                        default:
                            break;
                    }
                    
                }
            }
            //Raylib.EndShaderMode();
        }
    }
}


