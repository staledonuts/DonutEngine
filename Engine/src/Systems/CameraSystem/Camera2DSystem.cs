namespace Engine.Systems;

using Engine.Exceptions;
using Engine.Systems;
using Engine.Utils;
using Raylib_cs;


public class Camera2DSystem : SystemClass, IUpdateSys
{
    static Camera2D _camera2D = new();
    public static Camera2D camera
    {
        get
        {
            return _camera2D;
        }
    }

    public Rectangle Bounds = RaylibHelper.Viewport;

    Entity? currentTarget;

    public void SetCameraTarget(Entity entity)
    {
        currentTarget = entity;
        _camera2D.Target = currentTarget.body.Position;
    }

    public void SetRotation(float ToAngle)
    {
        _camera2D.Rotation = ToAngle;
    }

    public void LerpRotation(float ToAngle, float amount)
    {
        float current = _camera2D.Rotation;
        _camera2D.Rotation = GameMath.LerpPrecise(current, ToAngle, amount);
    }

    public override void Shutdown()
    {

    }

    public void Update()
    {
        if(Raylib.IsWindowResized())
        {

        }
    }


    public override void Initialize()
    {
        _camera2D.Target = new(0,0);
    }

}
