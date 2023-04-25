namespace DonutEngine.Backbone;
using System.Numerics;
using Raylib_cs;
using Box2DX.Common;
using static Raylib_cs.Raylib;
using DonutEngine.Backbone.Systems;

public partial class CameraHandler : SystemsClass
{
    public Camera2D donutcam = new();
    GameCamera2D? currentTarget;
	CameraUpdateModes cameraUpdateModes = new();
    public override void Start()
    {
        donutcam.zoom = 1.0f;
        donutcam.offset = new Vector2(Sys.settingsVars.screenWidth / 2, Sys.settingsVars.screenHeight / 2);
        currentTarget = null;
		cameraUpdateModes = CameraUpdateModes.Center;
    }

    public override void Update()
    {
        
    }

    public override void DrawUpdate()
    {
        
    }

    public override void LateUpdate()
    {
		if(currentTarget != null)
		{
			switch(cameraUpdateModes)
			{
				case CameraUpdateModes.Smooth:
				{
					UpdateCameraCenterSmoothFollow(ref donutcam, currentTarget.GetPosition(), Raylib.GetFrameTime(), Sys.settingsVars.screenWidth, Sys.settingsVars.screenHeight);
					break;
				}
				case CameraUpdateModes.Boundspush:
				{
					UpdateCameraPlayerBoundsPush(ref donutcam, currentTarget.GetPosition(), 1f, Sys.settingsVars.screenWidth, Sys.settingsVars.screenHeight);
					break;
				}
				case CameraUpdateModes.Center:
				{
					UpdateCameraCenter(ref donutcam, currentTarget.GetPosition(), Raylib.GetFrameTime(), Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
					break;
				}
			}
		}
    }

    public override void Shutdown()
    {
        
    }

	public void SetCameraUpdateMode(CameraUpdateModes cameraUpdateModes)
	{
		this.cameraUpdateModes = cameraUpdateModes;
	}

	public Camera2D GetCurrentCamera()
	{
		return donutcam;
	}

    public void SetCameraTarget(Entity entity)
    {
        currentTarget = entity.GetComponent<GameCamera2D>();
		donutcam.target = currentTarget.GetPosition();
    }

	public enum CameraUpdateModes
	{
		Smooth,
		Boundspush,
		Center,
	}
}
