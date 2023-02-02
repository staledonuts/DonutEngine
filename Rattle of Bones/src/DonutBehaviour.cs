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
            
            
            public DonutBehaviour()
            {
                Registry.AddEntity(this);
                transform = new(new Vector2(100,100),Vector2.Zero,Vector2.One);
                AddComponent(transform);
            }


            

            public virtual void update()
            {

            }
            
            
        }

    }
    
    
}