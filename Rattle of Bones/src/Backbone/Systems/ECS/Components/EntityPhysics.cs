namespace DonutEngine.Backbone;
using System.Numerics;
using DonutEngine.Backbone.Systems;
using static DonutEngine.Backbone.Systems.DonutSystems;
using Box2DX.Dynamics;
using Box2DX.Collision;
using Raylib_cs;

public class EntityPhysics 
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public Body.BodyType Type { get; set; }
    public float Density { get; set; }
    public float Friction { get; set; }
    public float Restitution { get; set; }
    public Body? currentBody;



    public void InitEntityPhysics(Entity entity)
    {
        currentBody = physicsSystem.CreateBody(this, entity);
        ECS.ecsUpdate += Update;
    }


    public void DestroyEntityPhysics() 
    {
        physicsSystem.DestroyBody(currentBody);
        ECS.ecsUpdate -= Update;
    }

    public void Update(float deltaTime)
    {
        X = currentBody.GetPosition().X;
        Y = currentBody.GetPosition().Y;
        System.Console.WriteLine(currentBody.GetPosition().X+"-"+currentBody.GetPosition().Y);

    }

    public Vector2 GetVector2Pos()
    {
        Vector2 vector2 = new(X,Y);
        return vector2;
    }
}