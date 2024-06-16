using System.Numerics;
using Engine.Assets;
using Engine.Systems;
using Raylib_cs;

namespace Engine.RenderSystems;

public static partial class Rendering2D
{
    internal struct Layer
    {
        public Layer()
        {
            RenderBatch = new();
        }

        internal Dictionary<string, Queue<IRenderSorting>> RenderBatch;
    }



    internal struct Texture2DData : IRenderSorting, IDisposable
    {
        internal Texture2DData(Texture2D T, Rectangle R1, Rectangle R2, Vector2 V, float F, Color C, string shaderString)
        {
            _tex = T;
            _UVpos = R1;
            _rectTarget = R2;
            _origin = V;
            _orientation = F;
            _color = C;
            _shader  = shaderString;
        }
        internal Texture2D _tex;
        internal Rectangle _UVpos;
        internal Rectangle _rectTarget;
        internal Vector2 _origin;
        internal float _orientation;
        internal Color _color;
        internal string _shader;
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
    }

    internal struct ImageData : IRenderSorting
    {
        internal ImageData(Texture2D tex, Vector2 pos, Color color, string shaderString)
        {
            _tex = tex;
            _color = color;
            _pos = pos;
            _shader  = shaderString;
        }

        internal Texture2D _tex;
        internal Vector2 _pos;
        internal Color _color;
        internal string _shader;
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
    }

    internal struct LineData : IRenderSorting, IDisposable
    {
        internal LineData(Vector2 pos1, Vector2 pos2, float width, Color color, string shaderString)
        {
            _pos1 = pos1;
            _pos2 = pos2;
            _width = width;
            _color = color;
            _shader  = shaderString;
        }

        internal Vector2 _pos1;
        internal Vector2 _pos2;
        internal float _width;
        internal Color _color;
        internal string _shader;
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
    }

    internal interface IRenderSorting
    {
        internal string Shader
        {
            get;
        }
    }

}