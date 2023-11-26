using Engine.Utils.Extensions;
using System.Numerics;
namespace Engine;

/// <summary>
/// Raylib Extensions to make your life easier.
/// </summary>
public static class Gen
{
    internal static System.Random _random = new();
    public static int RandomInt(int minValue, int maxValue)
    {
        return _random.RandomInteger(minValue, maxValue);
    }

    public static Vector2 NextVector2(float minLength, float maxLength)
    {
        double theta = _random.NextDouble() * 2 * Math.PI;
        float length = _random.NextFloat(minLength, maxLength);
        return new Vector2(length * (float)Math.Cos(theta), length * (float)Math.Sin(theta));
    }

    public static float NextFloat(float minValue, float maxValue)
    {
        return (float)_random.NextDouble() * (maxValue - minValue) + minValue;
    }

    public static int RandomInteger()
    {
        return _random.Next();
    }

    public static bool RandomBooleon()
    {
        int value = _random.RandomInteger(0, 2);

        if(value == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    
    public static int RandomInteger(int min, int max)
    {
        if(min == max)
        {
            return min;
        }

        if (min > max)
        {
            Swap(ref min, ref max);
        }

        int result = min + _random.Next() % (max - min);
        return result;
    }

    public static void Swap<T>(ref T a, ref T b) where T : struct
    {
        T t = a;
        a = b;
        b = t;
    }


}