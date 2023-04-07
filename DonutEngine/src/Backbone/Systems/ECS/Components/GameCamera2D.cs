namespace DonutEngine.Backbone;
using System.Numerics;
using DonutEngine.Backbone.Systems;

public class GameCamera2D : Component
{
	public bool IsActive { get; set; }
	Entity? entity;

	public void LateUpdate(float deltaTime)
	{
        DonutSystems.cameraHandler.UpdateCameraPlayerBoundsPush(ref DonutSystems.cameraHandler.donutcam, entity.currentBody.GetPosition(), 1f, DonutSystems.settingsVars.screenWidth, DonutSystems.settingsVars.screenHeight);
	}
    public override void OnAddedToEntity(Entity entity)
    {
        this.entity = entity;
        ECS.ecsLateUpdate += LateUpdate;
        
    }

    public override void OnRemovedFromEntity(Entity entity)
    {
        ECS.ecsLateUpdate -= LateUpdate;
        Dispose();
    }
}