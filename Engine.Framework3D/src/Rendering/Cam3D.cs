using System.Numerics;
using Engine.Systems;
using Raylib_CSharp.Transformations;
using Raylib_CSharp.Camera.Cam3D;
using Engine.Framework3D.Entities;

namespace Engine.Framework3D.RenderSystems;

public class Cam3D : SystemClass, ILateUpdateSys
{
    bool hasATarget;
    Camera3D _camera3D;
    public Vector3 Position 
    {
        get => _camera3D.Target;
        set => _camera3D.Target = value;
    }
    public float Fov 
    {
        get => _camera3D.FovY;
        set => _camera3D.FovY = value;
    }

    public float Zoom
    {
        get => _camera3D.FovY;
    }


    float MinZoom;
    float MaxZoom;
    float ZoomSpeed;
    float MinFollowSpeed;
    float MinFollowEffectLength;
    float FractionFollowSpeed;
    Rectangle _cameraRectangle;
    Entity3D currentEntity;
    public Cam3D()
    {
        _camera3D = new();
        hasATarget = false;
    }

    public void CameraTarget(Entity3D entity)
    {
        currentEntity = entity;
        if(!hasATarget)
        {
            
            EngineSystems.dMiscUpdate += UpdateCamera;
            hasATarget = true;
        }
    }

    
    public Camera3D GetCamera()
    {
        return _camera3D;
    }

    public Rectangle GetCameraRect()
    {
        return _cameraRectangle;
    }

    public void SetCameraRotation(float ToAngle)
    {
        //_camera3D.Rotation = ToAngle;
    }

    public void LerpCameraRotation(float ToAngle, float amount)
    {
        //float current = _camera3D.GetMatrix;
        //_camera3D.RotateRoll(GameMath.LerpPrecise(current, ToAngle, amount));
    }

    void UpdateCamera()
    {
        //_camera3D.UpdatePro();
    }

    public void LateUpdate()
    {
        
    }

    public override void Initialize()
    {
        
    }

    public override void Shutdown()
    {
        
    }
}