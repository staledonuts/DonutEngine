namespace DonutEngine.Backbone;
using System.Numerics;
using Raylib_cs;
using DonutEngine.Backbone.Systems;

public class CameraHandler
{
    public Camera2D donutcam;

    Vector2 currentTarget;
    public void InitializeCamera(Vector2 target)
    {
        donutcam = new();
        donutcam.zoom = 1.0f;
        donutcam.offset = new Vector2(Program.settingsVars.screenWidth / 2, Program.settingsVars.screenHeight / 2);
        donutcam.target = target;
        currentTarget = target;
        DonutSystems.Update += UpdateCamera;
    }

    public void UpdateCamera()
    {
        donutcam.target = currentTarget;
    }

    public void ChangeCameraTarget(Vector2 target)
    {
        currentTarget = target;
    }
}
