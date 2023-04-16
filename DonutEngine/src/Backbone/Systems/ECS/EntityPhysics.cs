using Box2DX.Dynamics;
namespace DonutEngine.Backbone;

public class EntityPhysics
{
    public void InitEntityPhysics(Entity entity)
    {
        BodyDef bodyDef = new();
        bodyDef.Position = new(entity.X, entity.Y);
        PolygonDef polygonDef = new PolygonDef();
        polygonDef.SetAsBox(entity.Width, entity.Height);
        polygonDef.Density = entity.Density;
        polygonDef.Friction = entity.Friction;
        polygonDef.Restitution = entity.Restitution;
        if(entity.Type == Body.BodyType.Dynamic)
        {
            bodyDef.AllowSleep = true;
            bodyDef.LinearDamping = 0.3f;
            bodyDef.MassData.Mass = 1.0f;
            entity.currentBody = Sys.physicsSystem.CreateBody(bodyDef);
        }
        entity.currentBody = Sys.physicsSystem.CreateBody(bodyDef);
        if(entity.Type == Body.BodyType.Static)
        {
            entity.currentBody.SetStatic();
        }
        entity.currentBody.CreateFixture(polygonDef);
    }

    public void DestroyEntityPhysics(Entity entity) 
    {
        if(entity.currentBody != null)
        {
            Sys.physicsSystem.DestroyBody(entity.currentBody);
        }
    }
}