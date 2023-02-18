namespace DonutEngine.Backbone;
using System.Numerics;
using DonutEngine.Backbone.Systems;
using Box2D.NetStandard.Dynamics.Bodies;
using Box2D.NetStandard.Collision;
using Raylib_cs;

public abstract partial class Entity
{
    public Physics2D? physics2D;
 
    public abstract void Start();
    public abstract void Update(float deltaTime);
    public abstract void DrawUpdate(float deltaTime);
    public abstract void LateUpdate(float deltaTime);

}



