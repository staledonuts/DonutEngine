using System.Numerics;

namespace DonutEngine
{
    namespace Backbone
    {
        public partial class ECS
        {
            public class DonutTransform : Component
            {
                Vector2 position;
                Vector2 rotation;
                Vector2 scale;  

                public virtual void Update()
                {

                }
            }
        }

    }
}