using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Numerics;

namespace DonutEngine
{
    namespace Backbone
    {
        public partial class ECS
        {
            public partial class Transform
            {
                public Transform()
                {
                    
                }

                public Transform(Vector2 position)
                {
                    currentPosition = position;
                }

                public Transform(Vector2 position, Vector2 rotation)
                {
                    currentPosition = position;
                    currentRotation = rotation;
                }

                public Transform(Vector2 position, Vector2 rotation, Vector2 scale)
                {
                    currentPosition = position;
                    currentRotation = rotation;
                    currentRotation = scale;
                }
                Vector2 currentPosition = Vector2.Zero;
                Vector2 currentRotation = Vector2.Zero;
                Vector2 currentScale = Vector2.Zero;
            }
        }
    }
}