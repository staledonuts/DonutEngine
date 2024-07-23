using System.Numerics;
using Engine.Utils;
using Raylib_CSharp.Geometry;
using Raylib_CSharp.Materials;

namespace Engine.Framework3D.Data;
public struct Quad
{
    Mesh _mesh;
    Material _material;


    public Quad(Vector2 Size, Point Resolution, Material Material)
    {
        _mesh = Mesh.GenPlane(Size.X, Size.Y, Resolution.X, Resolution.Y);
        _material = Material;
    }

    public Material GetMaterials()
    {
        return _material;
    }

    public Mesh GetMesh()
    {
        return _mesh;
    }
}