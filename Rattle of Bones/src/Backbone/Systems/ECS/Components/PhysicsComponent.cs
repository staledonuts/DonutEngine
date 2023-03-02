namespace DonutEngine.Backbone;
using System.Numerics;
using DonutEngine.Backbone.Systems;
using Box2D.NetStandard.Dynamics.Bodies;
using Box2D.NetStandard.Collision.Shapes;
using Raylib_cs;
using Newtonsoft.Json;
using DonutEngine.Backbone;

public class PhysicsComponent : Component 
{
    [JsonProperty("X")] 
    public float X { get; set; }
    [JsonProperty("Y")] 
    public float Y { get; set; }
    [JsonProperty("Width")] 
    public float Width { get; set; }
    [JsonProperty("Height")] 
    public float Height { get; set; }
    [JsonProperty("BodyType")] 
    public BodyType Type { get; set; }
    [JsonProperty("Density")] 
    public float Density { get; set; }
    [JsonProperty("Friction")] 
    public float Friction { get; set; }
    [JsonProperty("Restitution")] 
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