using System;
using System.Numerics;
using System.Linq;
using System.Threading.Tasks;
using Raylib_cs;
using DonutEngine.MathD;

namespace DonutEngine
{
    public static class CameraHandler
    {
        public static Camera2D donutcam = new();

        public static void SetCameraTarget(Transform target)
        {
            donutcam.target = new((float)target.position.x, (float)target.position.y);
        }

        public static void UpdateCamera(Transform target)
        {
            
        }
           
    }
}