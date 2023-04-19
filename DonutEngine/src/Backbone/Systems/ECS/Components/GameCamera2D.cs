namespace DonutEngine.Backbone;
using Box2DX.Common;
using DonutEngine.Backbone.Systems;
using System.Numerics;

public class GameCamera2D : Component
{
    
	public bool IsActive { get; set; }
	Entity? entity;
    Vector2 ConvertVector;

    public Vector2 GetPosition()
    {
        ConvertVector.X = entity.body.GetPosition().X;
        ConvertVector.Y = entity.body.GetPosition().Y;
        return ConvertVector;
    }

	public void LateUpdate(float deltaTime)
	{
        
	}
    public override void OnAddedToEntity(Entity entity)
    {
        this.entity = entity;
        Sys.cameraHandler.SetCameraTarget(entity);
        Sys.cameraHandler.SetCameraUpdateMode(CameraHandler.CameraUpdateModes.Smooth);
        ECS.ecsLateUpdate += LateUpdate;
    }
    
    public void ToggleActive()
    {
        if(IsActive)
        {
            IsActive = false;
            ECS.ecsLateUpdate -= LateUpdate;
        }
        else
        {
            IsActive = true;
            ECS.ecsLateUpdate += LateUpdate;
        }
    }

    public override void OnRemovedFromEntity(Entity entity)
    {
        ECS.ecsLateUpdate -= LateUpdate;
        Dispose();
    }
}