using System.Numerics;
namespace DonutEngine.Backbone;
public class Transform2D
{
    public Transform2D(Vector2 inputPosition)
    {
        position = inputPosition;
    }

    public Transform2D(Vector2 inputPosition, float inputRotation)
    {
        position = inputPosition;
        rotation = inputRotation;
    }

    public Transform2D(Vector2 inputPosition, float inputRotation, Vector2 inputScale)
    {
        position = inputPosition;
        rotation = inputRotation;
        scale = inputScale;
    }
    public Vector2 position = Vector2.Zero;
    public float rotation = 0f;
    public Vector2 scale = Vector2.Zero;

    public static double Distance(Vector2 positionA, Vector2 positionB)
    {
        return Math.Sqrt(Math.Pow((positionA.X - positionA.X), 2f) + Math.Pow((positionA.Y - positionB.Y), 2f));
    }
}