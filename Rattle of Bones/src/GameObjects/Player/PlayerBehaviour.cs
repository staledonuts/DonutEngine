using static DonutEngine.Backbone.ECS;
using static DonutEngine.FilePaths;
using DonutEngine.Backbone;
using Raylib_cs;
using System.Numerics;

namespace DonutEngine
{
    public class PlayerBehaviour : Entity
    {
        public Transform2D transform;
        public SpriteRenderer spriteRenderer;
        Texture2D idleSpriteTex = Raylib.LoadTexture(app+assets+"Sprites/Player/Player-idle.png");
        Texture2D walkSpriteTex = Raylib.LoadTexture(app+assets+"Sprites/Player/Player-walk.png");
        Texture2D slash1SpriteTex = Raylib.LoadTexture(app+assets+"Sprites/Player/Player-slash1.png");
        Texture2D slash2SpriteTex = Raylib.LoadTexture(app+assets+"Sprites/Player/Player-slash2.png");
        Texture2D jumpSpriteTex = Raylib.LoadTexture(app+assets+"Sprites/Player/Player-jump.png");
        

        public PlayerBehaviour()
        {
            Registry.AddEntity(this);
            transform = new(new Vector2(100,100),0f,Vector2.One);
            AddComponent(transform);
            spriteRenderer = new(idleSpriteTex, transform);
            AddComponent(spriteRenderer);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
        }

        
    }
}