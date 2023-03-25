namespace DonutEngine.Backbone;
using System.Numerics;

public class GameCamera2D : DynamicComponent
{
	public bool IsActive { get; set; }
	EntityPhysics? entityPhysics;
    public override void OnAddedToEntity(Entity entity)
    {
		
		
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

    public override void OnAddedToEntity(DynamicEntity entity)
    {
        entityPhysics = entity.entityPhysics;
    }

    public override void OnRemovedFromEntity(DynamicEntity entity)
    {
		
    }
}