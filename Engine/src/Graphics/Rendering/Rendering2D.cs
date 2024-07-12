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
using System.Numerics;
using Raylib_CSharp.Camera.Cam2D;
using System.Diagnostics.CodeAnalysis;
using Engine.Utils;
using Raylib_CSharp.Logging;

namespace Engine.RenderSystems;
public static partial class Rendering2D
{
    static Rendering2D()
    {
        _layers = new();
        _camera2D = new()
        {
            Offset = new(Window.GetScreenWidth() / 2.0f, Window.GetScreenWidth() / 2.0f)
        };
    }

    static Camera2D _camera2D;
    static List<LayerData> _layers;
    static string[] _layerShaders = new string[] {"main", "main", "bloom", "standardbloom", "main", "main", "main"};
    [AllowNull] static Entity currentTarget;

    public static void SetCameraTarget(Entity entity)
    {
        currentTarget = entity;
    }

    public static void SetCameraRotation(float ToAngle)
    {
        _camera2D.Rotation = ToAngle;
    }

    public static void LerpCameraRotation(float ToAngle, float amount)
    {
        float current = _camera2D.Rotation;
        _camera2D.Rotation = GameMath.LerpPrecise(current, ToAngle, amount);
    }

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
    static bool skipFrame = false;
    static bool lastFrameResized = false;
    public static void Render()
    {
        if(Window.IsResized() && !lastFrameResized)
        {
            skipFrame = true;
            lastFrameResized = true;
            foreach(LayerData layr in _layers)
            {
                layr.Dispose();
            }
            _layers = new();
        }
        if(!skipFrame)
        {
            Graphics.BeginDrawing();
            
            Backgrounds.DrawBackground();
            DrawToRenderTextures();
            RenderComposition();
            Graphics.EndDrawing();
            lastFrameResized = false;
        }
        else
        {
            InitializeLayers();
            skipFrame = false;
        }
    }

    static void DrawToRenderTextures()
    {
        
        foreach(LayerData layr in _layers)
        {
            Graphics.BeginTextureMode(layr.RenderTexture); 
            Graphics.BeginMode2D(_camera2D);
            Graphics.ClearBackground(Color.Blank);
            RenderLayer(layr);
            Graphics.EndMode2D();
            Graphics.EndTextureMode();
        }   
        
    }
    static Rectangle _screenRect = new();
    static readonly Vector2 _screenPos = new(0,0);
    static void RenderComposition()
    {
        _screenRect.Position = _screenPos;
        _screenRect.Width = Window.GetScreenWidth();
        _screenRect.Height = -Window.GetScreenHeight();
        
        foreach(LayerData l in _layers)
        {
            Graphics.BeginShaderMode(l.materialInstance.Shader);
            Graphics.DrawTextureRec(l.RenderTexture.Texture, _screenRect, _screenPos, Color.White);
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

    public static void UpdateCamera()
    {
        if(currentTarget != null)
        {
            _camera2D.Target.X = currentTarget.body.Position.X + 20;
            _camera2D.Target.Y = currentTarget.body.Position.Y + 20;
            Logger.TraceLog(TraceLogLevel.Info, _camera2D.Target.ToString());
        }
    }
}