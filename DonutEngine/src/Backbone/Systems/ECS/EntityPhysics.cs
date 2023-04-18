namespace DonutEngine.Backbone;
using Box2DX.Dynamics;
public class EntityPhysics
{
    public void InitEntityPhysics(Entity entity)
    {
        entity.bodyDef.Position.Set(entity.X, entity.Y);
        entity.polyDef.SetAsBox(entity.Width, entity.Height);
        entity.polyDef.Density = entity.Density;
        entity.polyDef.Friction = entity.Friction;
        entity.polyDef.Restitution = entity.Restitution;
        entity.bodyDef.IsSleeping = false;
        entity.bodyDef.AllowSleep = true;
        entity.bodyDef.LinearDamping = 0.2f;
        entity.bodyDef.AngularDamping= 0.1f;
        entity.bodyDef.FixedRotation = false;
        entity.body = Sys.physicsSystem.CreateBody(entity.bodyDef);
        entity.fixture = entity.body.CreateFixture(entity.polyDef);
        entity.fixture.ComputeMass(out entity.bodyDef.MassData);
        entity.body.SetMass(entity.bodyDef.MassData);
    }

    public void DestroyEntityPhysics(Entity entity) 
    {
        if(entity.body != null)
        {
            Sys.physicsSystem.DestroyBody(entity.body);
        }
    }
}