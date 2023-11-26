namespace Engine.Utils.Extensions;
using Raylib_cs;
using System.Numerics;
using System.Runtime.CompilerServices;

public static class RandomExt
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 NextVector2(this System.Random rand, float minLength, float maxLength)
    {
        double theta = rand.NextDouble() * 2 * Math.PI;
        float length = rand.NextFloat(minLength, maxLength);
        return new Vector2(length * (float)Math.Cos(theta), length * (float)Math.Sin(theta));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float NextFloat(this System.Random rand, float minValue, float maxValue)
    {
        return (float)rand.NextDouble() * (maxValue - minValue) + minValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int RandomInteger(this System.Random rand)
    {
        return rand.Next();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool RandomBooleon(this System.Random rand)
    {
        int value = RandomInteger(rand, 0, 2);

        if(value == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int RandomInteger(this System.Random rand, int min, int max)
    {
        if(min == max)
        {
            return min;
        }

        if (min > max)
        {
            Swap(ref min, ref max);
        }

        int result = min + rand.Next() % (max - min);
        return result;
    }

    public static void Swap<T>(ref T a, ref T b) where T : struct
    {
        T t = a;
        a = b;
        b = t;
    }

}