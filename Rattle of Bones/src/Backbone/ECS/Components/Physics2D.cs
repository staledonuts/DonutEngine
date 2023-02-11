namespace DonutEngine.Backbone;
using static DonutEngine.Backbone.ECS;

public class Physics2D : Component
{
    public Physics2D(float setGravity, Transform2D setTransform)
    {
        gravity = setGravity;
        transform = setTransform;
        
    }

    public float gravity = 1;
    Transform2D? transform;

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        transform.position.Y -= gravity * deltaTime; 
    }
}
