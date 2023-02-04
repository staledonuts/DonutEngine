using static DonutEngine.Backbone.ECS;
using static DonutEngine.FilePaths;
using DonutEngine.Physics;
using DonutEngine.Backbone;
using Raylib_cs;
using System.Numerics;

namespace DonutEngine
{
    public class PlayerBehaviour : Entity
    {
        public Transform2D transform;
        //public PhysicsObject physicsObject;
        //public PhysicsVars physicsVars;


        public SpriteRenderer spriteRenderer;
        Texture2D spriteTex = Raylib.LoadTexture(app+assets+"Sprites/Player/Player.png");

        public PlayerBehaviour()
        {
            Registry.AddEntity(this);
            transform = new(new Vector2(100,100),0f,Vector2.One);
            AddComponent(transform);
            spriteRenderer = new(spriteTex, transform);
            AddComponent(spriteRenderer);
            //physicsVars = new(1,1);
            //physicsObject = new(physicsVars, transform);
            //AddComponent(physicsObject);

        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }
    }
}