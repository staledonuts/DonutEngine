using static DonutEngine.Backbone.ECS;
using static DonutEngine.FilePaths;
using DonutEngine.Backbone.FSM;
using DonutEngine.Backbone;
using Raylib_cs;
using System.Numerics;

namespace DonutEngine
{
    public class PlayerBehaviour : Entity
    {
        public Transform2D transform;
        public SpriteRenderer spriteRenderer;
        //public Physics2D physics2D;
        public Texture2D idleSpriteTex = Raylib.LoadTexture(app+assets+"Sprites/Player/Player-idle.png");
        public Texture2D walkSpriteTex = Raylib.LoadTexture(app+assets+"Sprites/Player/Player-walk.png");
        public Texture2D slash1SpriteTex = Raylib.LoadTexture(app+assets+"Sprites/Player/Player-slash1.png");
        public Texture2D slash2SpriteTex = Raylib.LoadTexture(app+assets+"Sprites/Player/Player-slash2.png");
        public Texture2D jumpSpriteTex = Raylib.LoadTexture(app+assets+"Sprites/Player/Player-jump.png");
        

        public PlayerBehaviour()
        {
            EntityRegistry.SubscribeEntity(this);
            transform = new(new Vector2(100,100),0f,Vector2.One);
            spriteRenderer = new(idleSpriteTex, transform, 80, 57);
            SubscribeComponent(spriteRenderer);
            //physics2D = new(9, transform);
            //SubscribeComponent(physics2D);
            
        }

        public override void Start()
        {
            
        }
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
        }
        

        
    }
}