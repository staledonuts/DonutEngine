namespace Engine.Utils;
using Raylib_cs;
using System.Numerics;

public static class Vector2Ext
{
    /// <summary>
    /// Creates a new <see cref="Vector2"/> that contains cubic interpolation of the specified vectors.
    /// </summary>
    /// <param name="value1">Source <see cref="Vector2"/>.</param>
    /// <param name="value2">Source <see cref="Vector2"/>.</param>
    /// <param name="amount">Weighting value.</param>
    /// <returns>Cubic interpolation of the specified vectors.</returns>
    public static Vector2 SmoothStep(this Vector2 value1, Vector2 value2, float amount)
    {
        return new Vector2( GameMath.SmoothStep(value1.X, value2.X, amount), GameMath.SmoothStep(value1.Y, value2.Y, amount) );
    }

    /// <summary>
    /// Creates a new <see cref="Vector2"/> that contains cubic interpolation of the specified vectors but inverted.
    /// </summary>
    /// <param name="value1">Source <see cref="Vector2"/>.</param>
    /// <param name="value2">Source <see cref="Vector2"/>.</param>
    /// <param name="amount">Weighting value.</param>
    /// <returns>Cubic interpolation of the specified vectors.</returns>
    public static Vector2 InvSmoothStep(this Vector2 value1, Vector2 value2, float amount)
    {
        return new Vector2( GameMath.SmoothStep(value2.X, value1.X, amount), GameMath.SmoothStep(value2.Y, value1.Y, amount) );
    }

    /// <summary>
    /// Creates a new <see cref="Vector2"/> that contains cubic interpolation of the specified vectors.
    /// </summary>
    /// <param name="value1">Source <see cref="Vector2"/>.</param>
    /// <param name="value2">Source <see cref="Vector2"/>.</param>
    /// <param name="amount">Weighting value.</param>
    /// <param name="result">Cubic interpolation of the specified vectors as an output parameter.</param>
    public static void SmoothStep(this ref Vector2 value1, ref Vector2 value2, float amount, out Vector2 result)
    {
        result.X = GameMath.SmoothStep(value1.X, value2.X, amount);
        result.Y = GameMath.SmoothStep(value1.Y, value2.Y, amount);
    }

    /// <summary>
    /// Creates a new <see cref="Vector2"/> that contains cubic interpolation of the specified vectors but inverted.
    /// </summary>
    /// <param name="value1">Source <see cref="Vector2"/>.</param>
    /// <param name="value2">Source <see cref="Vector2"/>.</param>
    /// <param name="amount">Weighting value.</param>
    /// <returns>Cubic interpolation of the specified vectors.</returns>
    public static void InvSmoothStep(this ref Vector2 value1, ref Vector2 value2, float amount, out Vector2 result)
    {
        result.X = GameMath.SmoothStep(value2.X, value1.X, amount); 
        result.Y = GameMath.SmoothStep(value2.Y, value1.Y, amount);
    }

    public static Vector2 ScaleTo(this Vector2 vector, float length)
    {
        return vector * (length / vector.Length());
    }

    public static float ToAngle(this Vector2 vector)
    {
        return (float)Math.Atan2(vector.Y, vector.X);
    }

    public static float ToDegrees(this Vector2 vector2) 
    { 
        return 360 - (GameMath.Atan2(vector2.X, vector2.Y) * GameMath.Rad2Deg * GameMath.Sign(vector2.X)); 
    }
}