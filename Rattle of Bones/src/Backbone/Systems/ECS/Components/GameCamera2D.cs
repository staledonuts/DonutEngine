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
    public override void OnAddedToEntity(Entity entity)
    {
        this.entity = entity;
    }

    public override void OnRemovedFromEntity(Entity entity)
    {
        //throw new NotImplementedException();
        Dispose();
    }
}