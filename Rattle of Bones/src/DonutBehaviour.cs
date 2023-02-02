using Raylib_cs;
using static Raylib_cs.Raylib;
using static DonutEngine.Backbone.ECS;

namespace DonutEngine
{
    namespace Backbone
    {
        public class DonutBehaviour : Entity
        {
            public Transform transform;
            public DonutBehaviour()
            {
                AddComponent();
                
            }


            

            public virtual void update()
            {

            }
            
            
        }

    }
    
    
}