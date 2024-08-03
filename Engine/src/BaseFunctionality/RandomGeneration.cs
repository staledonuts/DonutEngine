using Engine.Utils;
using Engine.Utils.Extensions;
using System.Numerics;
namespace Engine;

/// <summary>
/// Random Generation Class
/// </summary>
public static class Gen
{
    static Gen()
    {
        Random = new();
        NoiseGen = new(Random.Next());
        NoiseGen.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
    }
    public static readonly Random Random;
    public static readonly FastNoiseLite NoiseGen;

    
}