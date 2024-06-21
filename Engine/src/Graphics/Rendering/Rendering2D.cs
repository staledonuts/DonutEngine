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
    }

    static Dictionary<int, Layer> _layers;

    public static void Shutdown()
    {
        foreach(KeyValuePair<int, Layer> rt in _layers)
        {
            Raylib.UnloadRenderTexture(rt.Value.RenderTexture);
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
            Raylib.BeginTextureMode(layr.Value.RenderTexture); 
            Raylib.ClearBackground(Color.Blank);
            RenderLayer(layr.Value);
            Raylib.EndTextureMode();
        }
        
    }

    static void RenderComposition()
    {
        Raylib.BeginShaderMode(ShaderLib.UseShader("bloom"));
        foreach(KeyValuePair<int, Layer> rt in _layers)
        {
            Raylib.DrawTextureRec(rt.Value.RenderTexture.Texture, new Rectangle(0, 0, rt.Value.RenderTexture.Texture.Width, -rt.Value.RenderTexture.Texture.Height), new(0, 0), Color.White);
        }
        Raylib.EndShaderMode();
    }

    internal static void QueueAtLayer(int layerPos, IRenderSorting renderData)
    {
        if(!_layers.ContainsKey(layerPos))
        {
            _layers.Add(layerPos, new Layer(Raylib.LoadRenderTexture(Raylib.GetScreenWidth(), Raylib.GetScreenHeight())));
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
            l.Value.TrimExcess();
            int length = l.Value.Count;
            for (int i = 0; i < length; i++)
            {
                if(l.Value.TryDequeue(out IRenderSorting r))
                {
                    r.RenderMe();                    
                }
            }
        }
    }
}


