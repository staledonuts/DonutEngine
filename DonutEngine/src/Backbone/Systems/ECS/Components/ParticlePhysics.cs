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
        bodyDef.LinearDamping = 0.3f;
        bodyDef.MassData.Mass = 0.2f;
        bodyDef.LinearVelocity = new(Box2DX.Common.Math.Random(-100, 100), Box2DX.Common.Math.Random(200, 250));
        bodyDef.Position = entity.currentBody.GetPosition();
        Sys.physicsSystem.CreateBodyDef(bodyDef);
    }

    public Body body;
    public BodyDef bodyDef;
    internal void CreateBody()
    {
        body = Sys.physicsSystem.CreateBodyDef(bodyDef);
    }

    internal void DestroyBody()
    {
        Sys.physicsSystem.DestroyBody(body);
    }
}