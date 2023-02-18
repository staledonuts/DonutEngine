using Raylib_cs;
using System.Numerics;
using static DonutEngine.Backbone.ECS;
namespace DonutEngine.Backbone;
public class DonutBehaviour : Entity
{
    public SpriteRenderer spriteRenderer;
    
    
    public DonutBehaviour()
    {
        EntityRegistry.SubscribeEntity(this);
    }

    public override void DrawUpdate(float deltaTime)
    {
        
    }

    public override void LateUpdate(float deltaTime)
    {
        ;
    }

    public override void Start()
    {
        
    }

    public override void Update(float deltaTime)
    {
        throw new NotImplementedException();
    }
}