using Raylib_cs;
using DonutEngine.Backbone;

namespace DonutEngine
{
    public static class CameraHandler
    {
        public static Camera2D donutcam = new();

        public static void SetCameraTarget(ECS.Transform2D target)
        {
            donutcam.target = target.position;
        }

        public static void UpdateCamera(ECS.Transform2D target)
        {
            donutcam.target = target.position;
        }
           
    }
}