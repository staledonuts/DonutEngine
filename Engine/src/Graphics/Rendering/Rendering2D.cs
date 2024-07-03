using System.Diagnostics.Tracing;
using Engine.Assets;
using Engine.Enums;
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


    static List<LayerData> _layers;
    static string[] _layerShaders = new string[] {"main", "main", "bloom", "standardbloom", "main", "main", "main"};

    public static void InitializeLayers()
    {
        foreach(Layers _layersEnum in Enum.GetValues<Layers>())
        {
            _layers.Add(new(Raylib.LoadRenderTexture(Raylib.GetScreenWidth(), Raylib.GetScreenHeight()), ShaderLib.UseShader(_layerShaders[(int)_layersEnum])));
        }
    }

    public static void Shutdown()
    {
        foreach(LayerData rt in _layers)
        {
            Raylib.UnloadRenderTexture(rt.RenderTexture);
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
        foreach(LayerData layr in _layers)
        {
            Raylib.BeginTextureMode(layr.RenderTexture); 
            Raylib.ClearBackground(Color.Blank);
            RenderLayer(layr);
            Raylib.EndTextureMode();
        }   
    }

    static void RenderComposition()
    {
        
        foreach(LayerData l in _layers)
        {
            Raylib.BeginShaderMode(l.materialInstance.Shader);
            //Raylib.TraceLog(TraceLogLevel.Debug, "Rendering Layer: "+l+" with shader "+l.materialInstance.GetShaderName);
            Raylib.DrawTextureRec(l.RenderTexture.Texture, new Rectangle(0, 0, l.RenderTexture.Texture.Width, -l.RenderTexture.Texture.Height), new(0, 0), Color.White);
            Raylib.EndShaderMode();
        }
    }

    internal static void QueueAtLayer(Layers layerPos, IRenderSorting renderData)
    {
            LayerData l = _layers.ElementAt((int)layerPos);
            l.RenderBatch.Enqueue(renderData);
    }

    internal static void RenderLayer(LayerData layer)
    {
        layer.RenderBatch.TrimExcess();
        int length = layer.RenderBatch.Count;
        for (int i = 0; i < length; i++)
        {
            if(layer.RenderBatch.TryDequeue(out IRenderSorting r))
            {
                r.RenderMe();     
            }
        }
    }
}