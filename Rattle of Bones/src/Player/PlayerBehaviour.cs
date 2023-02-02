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
        Texture2D spriteTex = Raylib.LoadTexture(Directory.GetCurrentDirectory()+assets+"Sprites/Player/Player.png");

        public PlayerBehaviour()
        {
            Registry.AddEntity(this);
            transform = new(new Vector2(100,100),Vector2.Zero,Vector2.One);
            AddComponent(transform);
            spriteRenderer = new(spriteTex);
            AddComponent(spriteRenderer);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }
    }
}