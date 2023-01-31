using System;
using System.Numerics;
using System.Linq;
using System.Threading.Tasks;
using Raylib_cs;
using static Raylib_cs.Raylib;
using DonutEngine.MathD;

namespace DonutEngine
{
    public static class CameraHandler
    {
        public static Camera2D donutcam = new();

        public static void SetCameraTarget(Transform target)
        {
            donutcam.target = new((float)target.translation.X, (float)target.translation.Y);
        }

        public static void UpdateCamera(Transform target)
        {
            donutcam.target = new (target.translation.X,target.translation.Y);
        }
           
    }
}