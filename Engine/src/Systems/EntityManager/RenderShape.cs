using System.Numerics;
using Raylib_CSharp.Geometry;

namespace Engine.RenderSystems;

internal struct RenderShape
{
    public RenderShape(RenderShapeEnum shapeEnum)
    {
        switch (shapeEnum)
        {
            case RenderShapeEnum.Quad:
                plane = new Plane();
                
                break;
            case RenderShapeEnum.Triangle:
                break;
            case RenderShapeEnum.Custom:
                break;
        }
    }
    public readonly Mesh Shape;
    public readonly Plane plane;
}


internal enum RenderShapeEnum
{
    Quad,
    Triangle,
    Custom
}