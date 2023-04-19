namespace DonutEngine;
using DonutEngine.Backbone.Systems;
using Raylib_cs;
using Box2DX.Dynamics;
public class GameplayScene : Scene
{
    CollisionBox collbox1;

    public override void InitializeScene()
    {
        collbox1 = new(1000, 10, 0, 70, 1, 0.01f, 0.01f);
        //DonutSystems.SubscribeSystem(Sys.levelDataSystem);
        DonutSystems.Update += this.Update;
        DonutSystems.DrawUpdate += this.DrawUpdate;
        DonutSystems.LateUpdate += this.LateUpdate;
        //entityManager.CreateEntity(DonutFilePaths.entities+"Player.json");
        Sys.entityManager.CreateDirectory();
    }

    public override Scene UnloadScene()
    {
        DonutSystems.UnsubscribeSystem(Sys.levelDataSystem);
        DonutSystems.Update -= this.Update;
        DonutSystems.DrawUpdate -= this.DrawUpdate;
        DonutSystems.LateUpdate -= this.LateUpdate;
        return this;
    }

    public override void DrawUpdate()
    {

        Raylib.DrawCircle(100,100, 30, Raylib_cs.Color.BLACK);
        collbox1.DrawMe();
    }
    public override void LateUpdate()
    {
        
    }

    public override void Update()
    {
        
    }  

    private class CollisionBox
    {
        public CollisionBox(float width, float height, float x, float y, float density, float friction, float restitution)
        {
            bodyDef = new();
            bodyDef.Position.Set(x,y);
            body = Sys.physicsSystem.CreateBody(bodyDef);
            body.SetStatic();
            PolygonDef polygonDef = new PolygonDef();
            polygonDef.SetAsBox(width, height);
            polygonDef.Density = density;
            polygonDef.Friction = friction;
            polygonDef.Restitution = restitution;
            fixture = body.CreateFixture(polygonDef);
            rect = new(x, y, width, height);
        }
        Raylib_cs.Color color = Raylib_cs.Color.LIGHTGRAY;
        public Body body;
        Fixture fixture;
        BodyDef bodyDef;
        public Rectangle rect;

        public void DrawMe()
        {
            Raylib.DrawRectangleRec(rect, color);
        }
    }
}