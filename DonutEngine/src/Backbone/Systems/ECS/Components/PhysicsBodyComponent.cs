using static DonutEngine.Backbone.Systems.DonutSystems;
using Box2DX.Dynamics;
namespace DonutEngine.Backbone;
internal class PhysicsBody
{
    public float X = 0;
    public float Y = 0;
    public float Width = 1;
    public float Height = 1;
    public float Density = 1;
    public float Friction = 1;
    public float Restitution = 1;
    public Body? currentBody = null;
    internal void CreateBody()
    {
        currentBody = physicsSystem.CreateBody(X,Y,Width,Height,Density,Friction,Restitution);
    }

    internal void DestroyBody()
    {
        physicsSystem.DestroyBody(currentBody);
    }
}