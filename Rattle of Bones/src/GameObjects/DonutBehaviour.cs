using Raylib_cs;
using System.Numerics;
using static DonutEngine.Backbone.ECS;

namespace DonutEngine
{
    namespace Backbone
    {
        public class DonutBehaviour : Entity
        {
            public Transform2D transform;
            public SpriteRenderer spriteRenderer;
            
            
            public DonutBehaviour()
            {
                EntityRegistry.SubscribeEntity(this);
                transform = new(new Vector2(100,100),0f,Vector2.One);
            }


            

            public virtual void update()
            {

            }
            
            
        }

    }
    
    
}