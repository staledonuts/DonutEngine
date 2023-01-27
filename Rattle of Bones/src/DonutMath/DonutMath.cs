using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace DonutEngine.MathD
{
    public class Donutmath
    {
        public static float InverseLerp(float firstFloat, float secondFloat, float by)
        {
            return firstFloat * by + secondFloat * (1 - by);
        }

        public static float Lerp(float firstFloat, float secondFloat, float by)
        {
            return firstFloat * (1 - by) + secondFloat * by;
        }

        public static DVector2 DVector2Lerp(DVector2 position, DVector2 target, double by)
        {
            DVector2 temp = new(0,0);

            temp.x = position.x * (1 - by) + target.x * by;
            temp.y = position.y * (1 - by) + target.y * by;
            return temp;
        }

        public static Vector2 Vector2Lerp(Vector2 position, Vector2 target, float by)
        {
            Vector2 temp = new(0,0);

            temp.X = position.X * (1 - by) + target.X * by;
            temp.Y = position.Y * (1 - by) + target.Y * by;
            return temp;
        }

        public static float CubicInterpolation(float v0, float v1, float v2, float v3, float t) 
        {
        float p = (v3 - v2) - (v0 - v1);
        float q = (v0 - v1) - p;
        float r = v2 - v0;
        float s = v1;
        return (p * t * 3) + (q * t * 2) + (r * t) + s;
    }
    
    public static float QuadraticInterpolation(float v0, float v1, float v2, float t) 
    {
        float v01 = Lerp( v0, v1, t );
        float v12 = Lerp( v1, v2, t );
        return Lerp( v01, v12, t );
    }


    public static float CosInterpolation(float target) 
    {
        target = (float) -Math.Cos( target * Math.PI ); // [-1, 1]
        return (target + 1) / 2; // [0, 1]
    }
    public static float PerlinSmoothStep(float t) 
    {
        // Ken Perlin's version
        return t * t * t * ((t * ((6 * t) - 15)) + 10);
    }
    public static float SmoothStep(float t) 
    {
        return t * t * (3 - (2 * t));
    }
    }
}