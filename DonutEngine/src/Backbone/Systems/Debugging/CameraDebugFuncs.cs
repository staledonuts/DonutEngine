#if DEBUG
namespace DonutEngine.Backbone;
using System.Numerics;
using Raylib_cs;
using Box2DX.Common;
using static Raylib_cs.Raylib;
using DonutEngine.Backbone.Systems;

public partial class CameraHandler : SystemsClass
{
    public GameCamera2D returnCurrentCamTarget()
    {
        return currentTarget;
    }

    public CameraUpdateModes returnCameraModes()
    {
        return cameraUpdateModes;
    }

    public Camera2D returnCamera()
    {
        return donutcam;
    }
}
#endif