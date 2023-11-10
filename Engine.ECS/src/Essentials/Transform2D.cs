using System.Numerics;

namespace Engine.Systems.ECS;

public struct Transform2D
{
    public Transform2D()
    {
        Position = Vector2.Zero;
        Rotation = 0f;
        Scale = Vector2.One;
    }

    public Transform2D(Vector2 position)
    {
        Position = position;
        Rotation = 0f;
        Scale = Vector2.One;
    }
    public Vector2 Position { get; set; }
    public float Rotation { get; set; }
    public Vector2 Scale { get; set; }


    public void MoveTowards(Vector2 target, float maxDistance)
    {
        float toVector_x = target.X - Position.X;
        float toVector_y = target.Y - Position.Y;

        float squareDist = toVector_x * toVector_x + toVector_y * toVector_y;

        if (squareDist == 0 || (maxDistance >= 0 && squareDist <= maxDistance * maxDistance))
        {
            Position = target;
        }
        else
        {
            float dist = (float)Math.Sqrt(squareDist);
            Position = new Vector2(Position.X + toVector_x / dist * maxDistance, Position.Y + toVector_y / dist * maxDistance);
        }

    }
}