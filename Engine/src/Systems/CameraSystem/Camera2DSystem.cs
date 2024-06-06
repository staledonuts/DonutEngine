namespace Engine.Systems;

using Engine.Exceptions;
using Engine.Systems;
using Engine.Utils;
using Raylib_cs;


public class Camera2DSystem : SystemClass, IUpdateSys
{
    Camera2D camera2D = new();

    public Rectangle Bounds = RaylibHelper.Viewport;

    Entity? currentTarget;

    public void SetCameraTarget(Entity entity)
    {
        currentTarget = entity;
        camera2D.Target = currentTarget.body.Position;
    }

    public void SetRotation(float ToAngle)
    {
        camera2D.Rotation = ToAngle;
    }

    public void LerpRotation(float ToAngle, float amount)
    {
        float current = camera2D.Rotation;
        camera2D.Rotation = GameMath.LerpPrecise(current, ToAngle, amount);
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
        camera2D.Target = new(0,0);
    }
}
