namespace Engine.Utils.Extensions;
using System.Numerics;

public static class Vector3Ext
{

    public static Vector3 Up(this Vector3 vector3)
    {
        return vector3 = new(0,1,0);
    }
}