using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using Engine.Systems;
using Engine.Entities;
using Engine.Utils;
using Raylib_CSharp;
using Raylib_CSharp.Camera.Cam2D;
using Raylib_CSharp.Logging;
using Raylib_CSharp.Transformations;
using Raylib_CSharp.Windowing;
using Engine.Framework2D.Entities;

namespace Engine.Framework2D.RenderSystems;

public class Cam2D
{
    bool hasATarget;
    Camera2D _camera2D;
    public Vector2 Position 
    {
        get => _camera2D.Target;
        set => _camera2D.Target = value;
    }
    public Vector2 Offset 
    {
        get => _camera2D.Offset;
        set => _camera2D.Offset = value;
    }
    float MinZoom;
    float MaxZoom;
    float ZoomSpeed;
    float MinFollowSpeed;
    float MinFollowEffectLength;
    float FractionFollowSpeed;
    CameraMovement _cameraMovement;
    Rectangle _cameraRectangle;

    Entity2D currentEntity;
    public Cam2D()
    {
        _camera2D = new();
        _cameraMovement = CameraMovement.Normal;
        _cameraRectangle = new(0,0, Window.GetScreenWidth(), -Window.GetScreenHeight());
        hasATarget = false;
    }

    public void CameraTarget(Entity2D entity)
    {
        currentEntity = entity;
        if(!hasATarget)
        {
            EngineSystems.dMiscUpdate += UpdateCamera;
            hasATarget = true;
        }
    }

    private void UpdateCameraRectangle()
    {
        _cameraRectangle.Position = _camera2D.Target;
        _cameraRectangle.Size = new(Window.GetScreenWidth(), -Window.GetScreenHeight());
    }

    private void NormalMovement()
    {
        _camera2D.Offset = new Vector2(Window.GetScreenWidth() / 2, Window.GetScreenHeight() / 2);
        _camera2D.Target = currentEntity.Body.Position;
    }
    private void SmoothMovement() 
    {
        _camera2D.Offset = new Vector2(Window.GetScreenWidth() / 2, -Window.GetScreenHeight() / 2);
        Vector2 diff = _camera2D.Target - Position;
        float length = diff.Length();
        
        if (length > MinFollowEffectLength) 
        {
            float speed = Math.Max(FractionFollowSpeed * length, MinFollowSpeed);
            Position += diff * (speed * Time.GetFrameTime() / length);
        }
    }

    public Camera2D GetCamera2D()
    {
        return _camera2D;
    }

    public Rectangle GetCameraRect()
    {
        return _cameraRectangle;
    }

    public void SetCameraRotation(float ToAngle)
    {
        _camera2D.Rotation = ToAngle;
    }

    public void LerpCameraRotation(float ToAngle, float amount)
    {
        float current = _camera2D.Rotation;
        _camera2D.Rotation = GameMath.LerpPrecise(current, ToAngle, amount);
    }

    public void SetMovement(CameraMovement cameraMovement)
    {
        _cameraMovement = cameraMovement;
    }

    void UpdateCamera()
    {
        UpdateCameraRectangle();
        switch (_cameraMovement)
        {
            case CameraMovement.Normal:
            {
                NormalMovement();
                break;
            }
            case CameraMovement.Smooth:
            {
                SmoothMovement();
                break;
            }
            default:
            {
                break;
            }
        }
    }

    public enum CameraMovement
    {
        Normal,
        Smooth
    }
}