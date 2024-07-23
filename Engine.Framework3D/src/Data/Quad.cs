using System.Numerics;
using Engine.Utils;
using Raylib_CSharp.Geometry;
using Raylib_CSharp.Materials;

namespace Engine.Framework3D.Data;
public struct Quad
{
    public readonly Mesh Mesh
    {
        get;
        private init;
    }
    public Material Material
    {
        get;
        private init;
    }
    
    public Quad(Vector2 Size, Point Resolution, Material Material)
    {
        this.Mesh = Mesh.GenPlane(Size.X, Size.Y, Resolution.X, Resolution.Y);
        this.Material = Material;
    }

    
}