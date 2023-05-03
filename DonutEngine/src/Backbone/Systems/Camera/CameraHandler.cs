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
        donutcam.offset = new (0,0);
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
					UpdateCameraCenterSmoothFollow(ref donutcam, currentTarget.position, Raylib.GetFrameTime(), Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
					break;
				}
				case CameraUpdateModes.Boundspush:
				{
					UpdateCameraPlayerBoundsPush(ref donutcam, currentTarget.position, Raylib.GetFrameTime(), Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
					break;
				}
				case CameraUpdateModes.Center:
				{
					UpdateCameraCenter(ref donutcam, currentTarget.position, Raylib.GetFrameTime(), Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
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
    }

	public enum CameraUpdateModes
	{
		Smooth,
		Boundspush,
		Center,
	}
}
