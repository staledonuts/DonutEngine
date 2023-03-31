namespace DonutEngine.Backbone;
using System.Numerics;

public class GameCamera2D : Component
{
	public bool IsActive { get; set; }
	Entity? entity;

	public void LateUpdate(float deltaTime)
	{
		if(IsActive)
		{
			
		}
	}

    public override void OnAddedToEntity(DynamicEntity entity)
    {
        this.entity = entity;
    }

    public override void OnRemovedFromEntity(DynamicEntity entity)
    {
		
    }

    public override void OnAddedToEntity(StaticEntity entity)
    {
        //throw new NotImplementedException();
    }

    public override void OnRemovedFromEntity(StaticEntity entity)
    {
        //throw new NotImplementedException();
    }
}