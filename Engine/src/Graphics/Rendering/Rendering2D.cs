using System.Diagnostics.Tracing;
using Engine.Assets;
using Engine.Enums;
using Engine.Logging;
using Engine.Systems;
using Raylib_CSharp.Windowing;
using Raylib_CSharp.Textures;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Transformations;

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
            _layers.Add(new(RenderTexture2D.Load(Window.GetScreenWidth(), Window.GetScreenHeight()), ShaderLib.UseShader(_layerShaders[(int)_layersEnum])));
        }
    }

    public static void Shutdown()
    {
        foreach(LayerData rt in _layers)
        {
            rt.RenderTexture.Unload();
        }
    }

    public static void Render()
    {
        Graphics.BeginDrawing();
        Backgrounds.DrawBackground();
        DrawToRenderTextures();
        RenderComposition();
        Graphics.EndDrawing();
    }

    static void DrawToRenderTextures()
    {
        foreach(LayerData layr in _layers)
        {
            Graphics.BeginTextureMode(layr.RenderTexture); 
            Graphics.ClearBackground(Color.Blank);
            RenderLayer(layr);
            Graphics.EndTextureMode();
        }   
    }

    static void RenderComposition()
    {
        
        foreach(LayerData l in _layers)
        {
            Graphics.BeginShaderMode(l.materialInstance.Shader);
            Graphics.DrawTextureRec(l.RenderTexture.Texture, new Rectangle(0, 0, l.RenderTexture.Texture.Width, -l.RenderTexture.Texture.Height), new(0, 0), Color.White);
            Graphics.EndShaderMode();
        }
    }

    public static void QueueAtLayer(Layers layerPos, IRenderSorting renderData)
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