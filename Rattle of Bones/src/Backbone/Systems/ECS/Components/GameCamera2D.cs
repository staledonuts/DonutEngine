namespace DonutEngine.Backbone;
using System.Numerics;

public class GameCamera2D : Component
{
	public bool IsActive { get; set; }
	EntityPhysics? entityPhysics;
    public override void OnAddedToEntity(Entity entity)
    {
        entityPhysics = entity.entityPhysics;
		
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