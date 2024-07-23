using System.Numerics;
using Engine.Transformations;
using Engine.Utils;
using Raylib_CSharp.Geometry;
using Raylib_CSharp.Materials;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Transformations;

namespace Engine.Framework3D.Data;
public struct SpriteRenderData : IRenderData
{
    Mesh _mesh;
    Material _material;
    Matrix4x4 _transform;

    int _depthPosition = 0;

    public SpriteRenderData(Vector2 Size, Point Resolution, int Depth, Material Material)
    {
        _mesh = Mesh.GenPlane(Size.X, Size.Y, Resolution.X, Resolution.Y);
        _material = Material;
        _depthPosition = Depth;
    }

    public Material GetMaterial()
    {
        return _material;
    }

    public Mesh GetMesh()
    {
        return _mesh;
    }

    public void RenderMe()
    {
        Graphics.DrawMesh(_mesh, _material, _transform);
    }

    public void UpdateData(Transform2D transform)
    {
        
        _transform.M11 = transform.Translation.X;
        _transform.M12 = transform.Translation.Y;
        _transform.M13 = _depthPosition;
        _transform.M21 = transform.Rotation;
        _transform.M31 = transform.Scale.X;
        _transform.M32 = transform.Scale.Y;

    }
}