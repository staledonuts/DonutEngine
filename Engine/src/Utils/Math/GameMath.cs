namespace Engine.Utils;
using System.Numerics;
using Engine.Enums;
using Raylib_cs;

public static class GameMath
{
    public const float Epsilon = 0.000001f;
    public const float GoldenRatio = 1.61803398875f;
    public const float Tau = 6.28318530717959f;
    public const float Sqrt2 = 1.41421356237f;
    public const float Rad2Deg = 360f / Tau;
    public const float Deg2Rad = Tau / 360f;
    public const float Pi = (float)Math.PI;
    public const float PiOver2 = (float)(Math.PI / 2.0);
    public const float PiOver4 = (float)(Math.PI / 4.0);
    public const float TwoPi = (float)(Math.PI * 2.0);
    public const float E = (float)Math.E;
    public static float Abs(float value) => (float)Math.Abs(value);
    public static float Acos(float d) => (float)Math.Acos(d);
    public static float Asin(float d) => (float)Math.Asin(d);
    public static float Atan(float d) => (float)Math.Atan(d);
    public static float Atan2(float y, float x) => (float)Math.Atan2(y, x);
    public static float Ceiling(float a) => (float)Math.Ceiling(a);
    public static float Cos(float d) => (float)Math.Cos(d);
    public static float Cosh(float value) => (float)Math.Cosh(value);
    public static float Exp(float d) => (float)Math.Exp(d);
    public static float Floor(float d) => (float)Math.Floor(d);
    public static float IEEERemainder(float x, float y) => (float)Math.IEEERemainder(x, y);
    public static float Log(float a, float newBase) => (float)Math.Log(a, newBase);
    public static float Log(float d) => (float)Math.Log(d);
    public static float Log10(float d) => (float)Math.Log10(d);
    public static float Max(float val1, float val2) => (float)Math.Max(val1, val2);
    public static float Min(float val1, float val2) => (float)Math.Min(val1, val2);
    public static float Pow(float x, float y) => (float)Math.Pow(x, y);
    public static float Round(float value, int digits, MidpointRounding mode) => (float)Math.Round(value, digits, mode);
    public static float Round(float value, MidpointRounding mode) => (float)Math.Round(value, mode);
    public static float Round(float value, int digits) => (float)Math.Round(value, digits);
    public static float Round(float a) => (float)Math.Round(a);
    public static int Sign(float value) => Math.Sign(value);
    public static float Sin(float a) => (float)Math.Sin(a);
    public static float Sinh(float value) => (float)Math.Sinh(value);
    public static float Sqrt(float d) => (float)Math.Sqrt(d);
    public static float Tan(float a) => (float)Math.Tan(a);
    public static float Tanh(float value) => (float)Math.Tanh(value);
    public static float Truncate(this float d) => (float)Math.Truncate(d);
    public static float Magnitude(Vector3 vector) { return (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z); }
    public static float Distance(float value1, float value2) { return System.Math.Abs(value1 - value2); }
    public static float Lerp(float value1, float value2, float amount) { return value1 + (value2 - value1) * amount; }
    public static int IntLerp(byte value1, byte value2, int amount) { return value1 + (value2 - value1) * amount; }

    public static T Min<T>(T a, T b) where T : IComparable<T>
	{
		if (a.CompareTo(b) < 0)
			return a;
		return b;
	}
	public static T Min<T>(T a, T b, T c) where T : IComparable<T>
	{
		return Min(Min(a, b), c);
	}
	public static T Min<T>(T a, T b, T c, T d) where T : IComparable<T>
	{
		return Min(Min(Min(a, b), c), d);
	}

	public static T Max<T>(T a, T b) where T : IComparable<T>
	{
		if (a.CompareTo(b) > 0)
			return a;
		return b;
	}
	public static T Max<T>(T a, T b, T c) where T : IComparable<T>
	{
		return Max(Max(a, b), c);
	}
	public static T Max<T>(T a, T b, T c, T d) where T : IComparable<T>
	{
		return Max(Max(Max(a, b), c), d);
	}
    

    public static Vector3 Direction(float x, float y, float z) { return new Vector3(x, y, z); }

    public static Quaternion QuaternionLookAt(Vector3 sourcePoint, Vector3 destPoint)
    {
        Vector3 forwardVector = Vector3.Normalize(destPoint - sourcePoint);

        float dot = Vector3.Dot(Vector3.UnitZ, forwardVector);
        if (Math.Abs(dot - (-1.0f)) < 0.000001f)
        {
            // vector a and b point exactly in the opposite direction, 
            // so it is a 180 degrees turn around the up-axis
            return new Quaternion(Vector3.UnitY, (float)Math.PI);
        }
        if (Math.Abs(dot - (1.0f)) < 0.000001f)
        {
            // vector a and b point exactly in the same direction
            // so we return the identity quaternion
            return Quaternion.Identity;
        }

        float rotAngle = (float)Math.Acos(dot);
        Vector3 rotAxis = Vector3.Cross(Vector3.UnitZ, forwardVector);
        rotAxis = Vector3.Normalize(rotAxis);
        return Quaternion.CreateFromAxisAngle(rotAxis, rotAngle);
    }
    
    public static float CatmullRom(float value1, float value2, float value3, float value4, float amount)
    {
        // Using formula from http://www.mvps.org/directx/articles/catmull/
        // Internally using doubles not to lose precission
        double amountSquared = amount * amount;
        double amountCubed = amountSquared * amount;
        return (float)(0.5 * (2.0 * value2 +
            (value3 - value1) * amount +
            (2.0 * value1 - 5.0 * value2 + 4.0 * value3 - value4) * amountSquared +
            (3.0 * value2 - value1 - 3.0 * value3 + value4) * amountCubed));
    }

    public static float WrapAngle(float angle)
	{
		angle = (float)Math.IEEERemainder((double)angle, 6.2831854820251465);
		if (angle <= -3.14159274f)
		{
			angle += 6.28318548f;
		}
		else
		{
            if (angle > 3.14159274f)
            {
                angle -= 6.28318548f;
            }
		}
		return angle;
	}

    public static bool IsPowerOfTwo(int value)
    {
        return (value > 0) && ((value & (value - 1)) == 0);
    }

    public static float Barycentric(float value1, float value2, float value3, float amount1, float amount2)
    {
        return value1 + (value2 - value1) * amount1 + (value3 - value1) * amount2;
    }

    public static float Clamp(float value, float min, float max)
    { 
        value = (value > max) ? max : value; 
        value = (value < min) ? min : value; 
        return value;
    }

    public static float Hermite(float value1, float tangent1, float value2, float tangent2, float amount)
    {
        // All transformed to double not to lose precission
        // Otherwise, for high numbers of param:amount the result is NaN instead of Infinity
        double v1 = value1, v2 = value2, t1 = tangent1, t2 = tangent2, s = amount, result;
        double sCubed = s * s * s;
        double sSquared = s * s;

        if (amount == 0f)
            result = value1;
        else if (amount == 1f)
            result = value2;
        else
            result = (2 * v1 - 2 * v2 + t2 + t1) * sCubed +
                (3 * v2 - 3 * v1 - 2 * t1 - t2) * sSquared +
                t1 * s +
                v1;
        return (float)result;
    }

    public static Vector2 CatmullRom(Vector2 value1, Vector2 value2, Vector2 value3, Vector2 value4, float amount)
    {
        return new Vector2(
            GameMath.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
            GameMath.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount));
    }

    public static void CatmullRom(ref Vector2 value1, ref Vector2 value2, ref Vector2 value3, ref Vector2 value4, float amount, out Vector2 result)
    {
        result = new Vector2(
            GameMath.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
            GameMath.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount));
    }

    public static Vector2 FromPolar(float angle, float magnitude)
    {
        return magnitude * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
    }

    public static float SmoothStep(float value1, float value2, float amount)
    {
        // It is expected that 0 < amount < 1
        // If amount < 0, return value1
        // If amount > 1, return value2
        float result = Clamp(amount, 0f, 1f);
        result = Hermite(value1, 0f, value2, 0f, result);

        return result;
    }

    public static float LerpPrecise(float value1, float value2, float amount)
    {
        return ((1 - amount) * value1) + (value2 * amount);
    }

    public static float LengthSquared(float X, float Y)
    {
        return (X * X) + (Y * Y);
    }

    public static float CubicLerp(float firstFloat, float secondFloat, float by)
	{
		float smoothed = MathF.Pow(3*by, 2) - MathF.Pow(2*by, 3);

		return firstFloat * (1 - smoothed) + secondFloat * smoothed;
	}

    public static Point ToPoint(float X, float Y)
    {
        return new Point((int) X,(int) Y);
    }
	public static float[,] GetNoiseData(int seed, int width, int height, float size, float power)
	{
		FastNoiseLite noise = new FastNoiseLite(seed);
		noise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);

		float[,] noiseData = new float[width, height];
	
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				noiseData[x,y] = (noise.GetNoise(x*size,y*size)+1)/2*power;
			}
		}

		return noiseData;
	}

    public static float RadiansToDegrees(float radians)
    {
        return radians * (float)(180 / Math.PI);
    }

}