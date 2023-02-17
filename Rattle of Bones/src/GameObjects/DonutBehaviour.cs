using Raylib_cs;
using System.Numerics;
using static DonutEngine.Backbone.ECS;

namespace DonutEngine
{
    namespace Backbone
    {
        public class DonutBehaviour : Entity
        {
            public Vector2 position;
            public SpriteRenderer spriteRenderer;
            
            
            public DonutBehaviour()
            {
                EntityRegistry.SubscribeEntity(this);
                //new(new Vector2(100,100),0f,Vector2.One);
            }

            public override void DrawUpdate(float deltaTime)
            {
                
            }

            public override void LateUpdate(float deltaTime)
            {
                
            }

            public override void Update(float deltaTime)
            {

            }
        }

    }
    
    
}