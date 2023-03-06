namespace DonutEngine.Backbone;
using System.Numerics;

public class GameCamera2D : Component
{
	public bool IsActive { get; set; }
	PositionComponent? position;
    public override void OnAddedToEntity(Entity entity)
    {
        position = entity.entityPos;
		
		//ECS.ecsUpdate += LateUpdate;
    }

    public override void OnRemovedFromEntity(Entity entity)
    {
        //throw new NotImplementedException();
    }

	public void LateUpdate(float deltaTime)
	{
		if(IsActive)
		{
			
		}
	}
}