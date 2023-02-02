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
            public class Transform2D : Component
            {

                public Transform2D(Vector2 inputPosition)
                {
                    position = inputPosition;
                }

                public Transform2D(Vector2 inputPosition, Vector2 inputRotation)
                {
                    position = inputPosition;
                    rotation = inputRotation;
                }

                public Transform2D(Vector2 inputPosition, Vector2 inputRotation, Vector2 inputScale)
                {
                    position = inputPosition;
                    rotation = inputRotation;
                    rotation = inputScale;
                }
                public Vector2 position = Vector2.Zero;
                public Vector2 rotation = Vector2.Zero;
                public Vector2 scale = Vector2.Zero;
            }
        }
    }
}