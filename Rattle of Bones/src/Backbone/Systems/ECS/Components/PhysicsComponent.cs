namespace DonutEngine.Backbone;
using System.Numerics;
using DonutEngine.Backbone.Systems;
using static DonutEngine.Backbone.Systems.DonutSystems;
using Box2D.NetStandard.Dynamics.Bodies;
using Box2D.NetStandard.Collision.Shapes;
using Raylib_cs;

public class PhysicsComponent : Component 
{
    public float Width { get; set; }
    public float Height { get; set; }
    public BodyType Type { get; set; }
    public float Density { get; set; }
    public float Friction { get; set; }
    public float Restitution { get; set; }
    public Body? Body { get; set; }

    private PositionComponent? position;
    public override void OnAddedToEntity(Entity entity)
    {
        position = entity.entityPos;
        Body = physicsSystem.CreateBody(this, entity);
        ECS.ecsUpdate += Update;
    }

    public override void OnRemovedFromEntity(Entity entity) 
    {
        physicsSystem.DestroyBody(Body);
    }

    public void Update(float deltaTime)
    {
        
        position.SetPosition(Body.GetPosition());

    }
}