namespace DonutEngine.Backbone;
using System.Numerics;
using Raylib_cs;
using DonutEngine.Backbone.Systems;

public class CameraHandler
{
    static Camera2D donutcam;
    public void InitializeCamera()
    {
        donutcam = new();
        donutcam.zoom = 1.0f;
        donutcam.offset = new Vector2(Program.settingsVars.screenWidth / 2, Program.settingsVars.screenHeight / 2);
        donutcam.target = GameObjects.player.physics2D.rigidbody2D.Position;
        DonutSystems.Update += UpdateCamera;
    }

    public void UpdateCamera()
    {
        Raylib.BeginMode2D(donutcam);
        donutcam.target = GameObjects.player.physics2D.rigidbody2D.Position;
        Raylib.EndMode2D();
    }

    public void ChangeCameraTarget(Vector2 target)
    {
        
    }
}
