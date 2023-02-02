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

                public SpriteRenderer(Texture2D spriteTex)
                {
                    currentTex = spriteTex;
                }

                Texture2D currentTex;
                int posX = 54;
                int posY = 54;


               
                public override void Update(float deltaTime)
                {
                    base.Update(deltaTime);
                    Raylib.DrawTexture(currentTex,posX,posY, Color.WHITE);
                }
            }
        }
    }
}