using System.Numerics;
using Engine.Assets;
using Engine.Systems;
using Raylib_cs;

namespace Engine.RenderSystems;

public static partial class Rendering2D
{
    internal struct Layer
    {
        public Layer(RenderTexture2D rt)
        {
            RenderBatch = new();
            RenderTexture = rt;
        }
        public Dictionary<string, Queue<IRenderSorting>> RenderBatch;
        public RenderTexture2D RenderTexture;
    }



    public struct Texture2DData : IRenderSorting, IDisposable
    {
        public Texture2DData(Texture2D T, Rectangle R1, Rectangle R2, Vector2 V, float F, Color C, string shaderString)
        {
            _tex = T;
            _UVpos = R1;
            _rectTarget = R2;
            _origin = V;
            _orientation = F;
            _color = C;
            _shader  = shaderString;
        }
        public Texture2D _tex;
        public Rectangle _UVpos;
        public Rectangle _rectTarget;
        public Vector2 _origin;
        public float _orientation;
        public Color _color;
        public string _shader;
        string IRenderSorting.Shader
        { 
            get
            {
                return _shader;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void RenderMe()
        {
            Raylib.DrawTexturePro(_tex, _UVpos, _rectTarget, _origin, _orientation, _color);
        }
    }

    public struct ImageData : IRenderSorting
    {
        public ImageData(Texture2D tex, Vector2 pos, Color color, string shaderString)
        {
            _tex = tex;
            _color = color;
            _pos = pos;
            _shader  = shaderString;
        }

        public Texture2D _tex;
        public Vector2 _pos;
        public Color _color;
        public string _shader;
        string IRenderSorting.Shader
        { 
            get
            {
                return _shader;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void RenderMe()
        {
            Raylib.DrawTexture(_tex, (int)_pos.X, (int)_pos.Y, _color);
        }
    }

    public struct LineData : IRenderSorting, IDisposable
    {
        public LineData(Vector2 pos1, Vector2 pos2, float width, Color color, string shaderString)
        {
            _pos1 = pos1;
            _pos2 = pos2;
            _width = width;
            _color = color;
            _shader  = shaderString;
        }

        public Vector2 _pos1;
        public Vector2 _pos2;
        public float _width;
        public Color _color;
        public string _shader;
        string IRenderSorting.Shader
        { 
            get
            {
                return _shader;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void RenderMe()
        {
            Raylib.DrawLineEx(_pos1, _pos2, _width, _color);
        }
    }

    public interface IRenderSorting
    {
        public string Shader
        {
            get;
        }

        public void RenderMe();
    }

}