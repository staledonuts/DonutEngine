using Box2DX.Common;

public static class MathD
{
    public const float Epsilon = 0.000001f;
    public const float GoldenRatio = 1.61803398875f;
	public const float Tau = 6.28318530717959f;
    public const float Sqrt2 = 1.41421356237f;
    public const float Rad2Deg = 360f / Tau;
    public const float Deg2Rad = Tau / 360f;
    public const float Pi = 3.14159265359f;
    public static float Distance(float value1, float value2)
    {
        return System.Math.Abs(value1 - value2);
    }

    public static float Lerp(float value1, float value2, float amount)
    {
        return value1 + (value2 - value1) * amount;
    }

    public static float Vec2Angle(Vec2 vec2)
    {
        return 360 - (MathF.Atan2(vec2.X, vec2.Y) * Rad2Deg * MathF.Sign(vec2.X));
    }
}