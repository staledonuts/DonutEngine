using System.Numerics;
using Engine.Transformations;
using Raylib_CSharp.Materials;
using Raylib_CSharp.Transformations;

namespace Engine.Framework3D.Data;
public interface IRenderData
{
    public void RenderMe();
    public Material GetMaterial();
    public void UpdateData(Transform2D transform);
}