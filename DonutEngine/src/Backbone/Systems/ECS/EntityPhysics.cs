namespace DonutEngine.Backbone;

public class EntityPhysics
{
    public void InitEntityPhysics(Entity entity)
    {
        entity.bodyDef.Position = new(entity.X, entity.Y);
        entity.polyDef.SetAsBox(entity.Width, entity.Height);
        entity.polyDef.Density = entity.Density;
        entity.polyDef.Friction = entity.Friction;
        entity.polyDef.Restitution = entity.Restitution;
        entity.bodyDef.IsSleeping = false;
        entity.bodyDef.AllowSleep = true;
        entity.bodyDef.LinearDamping = 0;
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