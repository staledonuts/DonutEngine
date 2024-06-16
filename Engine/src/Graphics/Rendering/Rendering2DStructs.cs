using System.Numerics;
using Engine.Assets;
using Engine.Systems;
using Raylib_cs;

namespace Engine.RenderSystems;

public static partial class Rendering2D
{
    public struct Layer
    {
        public Layer()
        {
            Shader = "main";
            RenderBatch = new();
        }

        public string Shader;

        public Queue<IRenderSorting> RenderBatch;
    }



    public struct Texture2DData : IRenderSorting, IDisposable
    {
        public Texture2DData(Texture2D T, Rectangle R1, Rectangle R2, Vector2 V, float F, Color C)
        {
            _tex = T;
            _UVpos = R1;
            _rectTarget = R2;
            _origin = V;
            _orientation = F;
            _color = C;
        }
        public Texture2D _tex;
        public Rectangle _UVpos;
        public Rectangle _rectTarget;
        public Vector2 _origin;
        public float _orientation;
        public Color _color;

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    public struct ImageData : IRenderSorting
    {
        public ImageData(Texture2D tex, Vector2 pos, Color color)
        {
            _tex = tex;
            _color = color;
            _pos = pos;
        }

            public Texture2D _tex;
            public Vector2 _pos;
            public Color _color;

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    public struct LineData : IRenderSorting, IDisposable
    {
        public LineData(Vector2 pos1, Vector2 pos2, float width, Color color)
        {
            _pos1 = pos1;
            _pos2 = pos2;
            _width = width;
            _color = color;
        }

        public Vector2 _pos1;
        public Vector2 _pos2;
        public float _width;
        public Color _color;

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    public interface IRenderSorting
    {
        
    }

}