using static DonutEngine.Backbone.Systems.DonutSystems;
using Box2DX.Dynamics;
using Box2DX.Common;
using DonutEngine.Backbone.Systems;
namespace DonutEngine.Backbone;
internal class ParticlePhysics
{

    public ParticlePhysics(Entity entity)
    {
        bodyDef = new();
        polyDef = new();
        bodyDef.LinearDamping = 0.3f;
        bodyDef.MassData.Mass = 0.2f;
        polyDef.SetAsBox(1,1);
        bodyDef.LinearVelocity = new(Box2DX.Common.Math.Random(-100, 100), Box2DX.Common.Math.Random(200, 250));
        bodyDef.Position = entity.body.GetPosition();
        body = Sys.physicsSystem.CreateBody(bodyDef);
        fixture = body.CreateFixture(polyDef);
        fixture.ComputeMass(out bodyDef.MassData);
        body.SetMass(bodyDef.MassData);
    }

    public Body body;
    public BodyDef bodyDef;
    public Fixture fixture;
    public PolygonDef polyDef;

    internal void DestroyBody()
    {
        Sys.physicsSystem.DestroyBody(body);
    }
}