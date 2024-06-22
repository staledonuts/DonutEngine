using Engine.Assets;
using Raylib_cs;

namespace Engine.RenderSystems;
public static partial class Rendering2D
{
    static Rendering2D()
    {
        _renderSortingPool = new();
        _frameBuffer = new();
        _layers = new();
        _materials = new();
        _renderTextures = new()
        {
            Raylib.LoadRenderTexture(Raylib.GetScreenWidth(), Raylib.GetScreenHeight())
        };

    }

    static List<IRenderSorting> _renderSortingPool;
    static List<IRenderSorting> _frameBuffer;
    static List<IRenderSorting> _layers;
    static List<IRenderSorting> _materials;
    static List<RenderTexture2D> _renderTextures;


    public static void Shutdown()
    {
        foreach(RenderTexture2D rt in _renderTextures)
        {
            Raylib.UnloadRenderTexture(rt);
        }
    }

    public static void Render()
    {
        SortByFrameBuffer();
        SortByLayer();
        SortByShader();
        Raylib.BeginDrawing();
        Raylib.BeginTextureMode(_renderTextures.ElementAt(0));
        Backgrounds.DrawBackground();
        Raylib.BeginShaderMode(ShaderLib.UseShader("main").Shader);
        Raylib.ClearBackground(Color.Blank);
        RenderComposition();
        Raylib.EndShaderMode();
        Raylib.EndTextureMode();
        foreach(RenderTexture2D rt in _renderTextures)
        {
            Raylib.DrawTextureRec(rt.Texture, new Rectangle(0, 0, rt.Texture.Width, -rt.Texture.Height), new(0, 0), Color.White);
        }
        Raylib.EndDrawing();
    }

    static int currentSorting = 0;
    static void SortByFrameBuffer()
    { 
        _frameBuffer.Clear();
        while(_frameBuffer.Count != _renderSortingPool.Count )
        {
            currentSorting++;
            foreach(IRenderSorting r in _renderSortingPool)
            {
                if(r.Framebuffer == currentSorting)
                {
                    _frameBuffer.Add(r);
                }
            }    
        }
        currentSorting = 0;
    }
    static void SortByLayer()
    {
        _layers.Clear();
        while(_layers.Count != _renderSortingPool.Count )
        {
            currentSorting++;
            foreach(IRenderSorting r in _renderSortingPool)
            {
                if(r.Framebuffer == currentSorting)
                {
                    _layers.Add(r);
                }
            }
        }
        currentSorting = 0;
    }


    static void SortByShader()
    { 
        _materials.Clear();
        while(_materials.Count != _renderSortingPool.Count)
        {
            currentSorting++;
            foreach(IRenderSorting r in _renderSortingPool)
            {
                if(r.Mat.Shader.Id == currentSorting)
                {
                    _materials.Add(r);
                }
            }

        }
        currentSorting = 0;
    }

    static void RenderComposition()
    {
        int currentFrameBuffer = -1;
        int currentLayer = -1;
        int currentMaterial = -1;      
        int amount = _renderSortingPool.Count;

        for (int i = 0; i < amount; i++)
        {
            IRenderSorting r = _frameBuffer.ElementAt(i);
            if(currentFrameBuffer != r.Framebuffer)
            {
                Raylib.EndTextureMode();
                try
                {
                    Raylib.BeginTextureMode(_renderTextures.ElementAt(r.Framebuffer));
                    Raylib.ClearBackground(Color.Blank);
                }
                catch
                {
                    _renderTextures.Add(Raylib.LoadRenderTexture(Raylib.GetScreenWidth(), Raylib.GetScreenHeight()));
                    Raylib.BeginTextureMode(_renderTextures.ElementAt(r.Framebuffer));
                    Raylib.ClearBackground(Color.Blank);
                }
                
            }

            if(currentMaterial != r.Mat.Shader.Id)
            {
                Raylib.EndShaderMode();
                Raylib.BeginShaderMode(r.Mat.Shader);
            }

            r.RenderMe();

        }

        _renderSortingPool.Clear();
    }



    internal static void AddToRenderPool(IRenderSorting rs)
    {
        //incoming Renderdata
        _renderSortingPool.Add(rs);
    }

    
}


