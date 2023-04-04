namespace DonutEngine.Backbone;
using System.Numerics;
using static DonutEngine.Backbone.Systems.DonutSystems;
using DonutEngine.Backbone.Systems;

public class GameCamera2D : Component
{
	public bool IsActive { get; set; }
	Entity? entity;

	public void LateUpdate(float deltaTime)
	{
        cameraHandler.UpdateCameraPlayerBoundsPush(ref cameraHandler.donutcam, entity.currentBody.GetPosition(), 1f, settingsVars.screenWidth, settingsVars.screenHeight);
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