using System.Numerics;

namespace Engine;

public struct Transform2D
{

    public Transform2D(Vector2 Translation, float Rotation, Vector2 Scale)
    {
        this.Translation = Translation;
        this.Rotation = Rotation;
        this.Scale = Scale;
    }

    public Transform2D(Vector2 Translation, float Rotation) : this(Translation, Rotation, Vector2.One) {}
    public Transform2D(Vector2 Translation) : this(Translation, 0, Vector2.One) {}
    public Transform2D() : this(Vector2.Zero, 0, Vector2.One) {}
    public static readonly Transform2D Zero = new(Vector2.Zero, 0, Vector2.One);
    //
    // Summary:
    //     Translation.
    public Vector2 Translation;

    //
    // Summary:
    //     Rotation.
    public float Rotation;

    //
    // Summary:
    //     Scale.
    public Vector2 Scale;


}
