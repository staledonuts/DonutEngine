using Raylib_cs;
using static Raylib_cs.Raylib;

namespace DonutEngine
{
    namespace Backbone
    {
        public class DonutBehaviour
        {
            public Transform transform;
            public DonutBehaviour()
            {
                RuntimeUpdater.current.RegisterObserver(this);
            }


            

            public virtual void update()
            {

            }
            
            
        }

    }
    
    
}