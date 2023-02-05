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
        Texture2D spriteTex = Raylib.LoadTexture(app+assets+"Sprites/Player/Player.png");

        public PlayerBehaviour()
        {
            Registry.AddEntity(this);
            transform = new(new Vector2(100,100),0f,Vector2.One);
            AddComponent(transform);
            spriteRenderer = new(spriteTex, transform);
            AddComponent(spriteRenderer);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            InputHandler();
        }

        public void InputHandler()
        {
            if(Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_ALT) && Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
            {
            Raylib.ToggleFullscreen();
            }
            if(Raylib.IsGamepadAvailable(0))
            {

            }
            if(Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
            {
                transform.position.X += 3f;
            }
        }
    }
}