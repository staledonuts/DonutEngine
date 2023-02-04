using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Numerics;
using Raylib_cs;

namespace DonutEngine
{
    namespace Backbone
    {
        public partial class ECS
        {
            public class SpriteRenderer : Component
            {
                

                public SpriteRenderer(Texture2D spriteTex, Transform2D entityTransform)
                {
                    currentTex = spriteTex;
                    transform = entityTransform;
                }

                Texture2D currentTex;
                Transform2D transform;
                public override void Update(float deltaTime)
                {
                    base.Update(deltaTime);
                    Raylib.DrawTexture(currentTex, (int)transform.position.X, (int)transform.position.Y, Color.WHITE);
                }
            }
        }
    }
}