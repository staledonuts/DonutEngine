namespace DonutEngine.Backbone;
using System.Numerics;
using DonutEngine.Backbone.Systems;
using Box2D.NetStandard.Dynamics.Bodies;
using Box2D.NetStandard.Collision.Shapes;
using Raylib_cs;
using DonutEngine.Backbone;

public class PhysicsComponent : Component 
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public BodyType Type { get; set; }
    public float Density { get; set; }
    public float Friction { get; set; }
    public float Restitution { get; set; }
    public Body Body { get; set; }

    private PositionComponent? position;
    public override void OnAddedToEntity(Entity entity)
    {
        position = entity.GetComponent<PositionComponent>();
        Body = PhysicsSystem.Instance.CreateBody(this);
        ECS.ecsUpdate += Update;
    }

    public override void OnRemovedFromEntity(Entity entity) 
    {
        PhysicsSystem.Instance.DestroyBody(Body);
    }

    public void Update(float deltaTime)
    {
        position.SetPosition(Body.GetPosition());
    }
}