using static DonutEngine.Backbone.ECS;
using static DonutEngine.FilePaths;
using DonutEngine.Backbone.FSM;
using DonutEngine.Backbone;
using Raylib_cs;
using System.Numerics;
using Box2D.NetStandard.Dynamics.Bodies;

namespace DonutEngine
{
    public class PlayerBehaviour : Entity
    {
        
        public SpriteRenderer? spriteRenderer;
        Vector2 setPosition;
        public Texture2D idleSpriteTex = Raylib.LoadTexture(app+sprites+"Player/Player-idle.png");
        public Texture2D walkSpriteTex = Raylib.LoadTexture(app+sprites+"Player/Player-walk.png");
        public Texture2D slash1SpriteTex = Raylib.LoadTexture(app+sprites+"Player/Player-slash1.png");
        public Texture2D slash2SpriteTex = Raylib.LoadTexture(app+sprites+"Player/Player-slash2.png");
        public Texture2D jumpSpriteTex = Raylib.LoadTexture(app+sprites+"Player/Player-jump.png");
        

        public PlayerBehaviour()
        {
            EntityRegistry.SubscribeEntity(this);
            setPosition = new(100,100);
            physics2D = new(setPosition);
            spriteRenderer = new(idleSpriteTex, physics2D, 80, 57);
        }

        public override void Start()
        {
            
        }
        public override void Update(float deltaTime)
        {
            
        }

        public override void DrawUpdate(float deltaTime)
        {
            
        }

        public override void LateUpdate(float deltaTime)
        {
            
        }
    }
}