using System.Numerics;
using Engine.Assets;
using Engine.Enums;
using Engine.Systems;
using Raylib_cs;

namespace Engine.RenderSystems;

public static partial class Rendering2D
{
    internal struct LayerData
    {
        public LayerData(RenderTexture2D rt, MaterialInstance matInstance)
        {
            RenderBatch = new();
            RenderTexture = rt;
            materialInstance = matInstance;
        }
        public Queue<IRenderSorting> RenderBatch;
        public RenderTexture2D RenderTexture;
        public MaterialInstance materialInstance;
    }



    public struct Texture2DData : IRenderSorting, IDisposable
    {
        public Texture2DData(Texture2D T, Rectangle R1, Rectangle R2, Vector2 V, float F, Color C, Layers lay, int frameBuffer)
        {
            _tex = T;
            _UVpos = R1;
            _rectTarget = R2;
            _origin = V;
            _orientation = F;
            _color = C;
            _layer = lay;
            _frameBuffer = frameBuffer;
        }
        public Texture2D _tex;
        public Rectangle _UVpos;
        public Rectangle _rectTarget;
        public Vector2 _origin;
        public float _orientation;
        public Color _color;
        private string _shader;
        private Layers _layer;
        private int _frameBuffer;
        private MaterialInstance _mat;
        public string Shader
        { 
            get
            {
                return _shader;
            }
            set
            {
                _shader = value;
            }
        }

        public int Framebuffer
        {
            get
            {
                return _frameBuffer;
            }
            set
            {
                _frameBuffer = value;
            }
        }

        public Layers Layer
        {
            get
            {
                return _layer;
            }
            set
            {
                _layer = value;
            }
        }

        public MaterialInstance Mat
        {
            get
            {
                return _mat;
            }
            set
            {
                _mat = value;
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
        public ImageData(Texture2D tex, Vector2 pos, Color color, Layers lay, int frameBuffer)
        {
            _tex = tex;
            _color = color;
            _pos = pos;
            _layer = lay;
            _frameBuffer = frameBuffer;
        }

        public Texture2D _tex;
        public Vector2 _pos;
        public Color _color;
        private string _shader;
        private Layers _layer;
        private int _frameBuffer;

        public int Framebuffer
        {
            get
            {
                return _frameBuffer;
            }
            set
            {
                _frameBuffer = value;
            }
        }

        public Layers Layer
        {
            get
            {
                return _layer;
            }
            set
            {
                _layer = value;
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


    public struct CircleData : IRenderSorting, IDisposable
    {
        public CircleData(Vector2 pos, float radius, Color color, Layers lay, int frameBuffer)
        {
            _pos = pos;
            _radius = radius;
            _color = color;
            _layer = lay;
            _frameBuffer = frameBuffer;
        }



        public Vector2 _pos;
        public float _radius;
        public Color _color;
        private Layers _layer;
        private int _frameBuffer;

        public int Framebuffer
        {
            get
            {
                return _frameBuffer;
            }
            set
            {
                _frameBuffer = value;
            }
        }

        public Layers Layer
        {
            get
            {
                return _layer;
            }
            set
            {
                _layer = value;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void RenderMe()
        {
            Raylib.DrawCircleV(_pos, _radius, _color);
        }

    }

    public struct LineData : IRenderSorting, IDisposable
    {
        public LineData(Vector2 pos1, Vector2 pos2, float width, Color color, Layers lay, int frameBuffer)
        {
            _pos1 = pos1;
            _pos2 = pos2;
            _width = width;
            _color = color;
            _layer = lay;
            _frameBuffer = frameBuffer;
        }



        public Vector2 _pos1;
        public Vector2 _pos2;
        public float _width;
        public Color _color;
        private Layers _layer;
        private int _frameBuffer;

        public int Framebuffer
        {
            get
            {
                return _frameBuffer;
            }
            set
            {
                _frameBuffer = value;
            }
        }

        public Layers Layer
        {
            get
            {
                return _layer;
            }
            set
            {
                _layer = value;
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
        public int Framebuffer
        {
            internal get;
            set;
        }

        public Layers Layer
        {
            internal get;
            set;
        }

        public void RenderMe();
    }

}