using Raylib_cs;
using static DonutEngine.Backbone.ECS;
using System.Numerics;
using Box2D.NetStandard.Dynamics.Bodies;

namespace DonutEngine
{
    namespace Backbone
    {

        public class SpriteRenderer : Component
        {
            public SpriteRenderer(Texture2D spriteTex, Vector2 entityPosition, int frameWidth, int frameHeight)
            {
                currentTex = spriteTex;
                position = entityPosition;
                characterFrameRec = new Rectangle(0, 0, 0, 0);
                animator = new Animator("Player", 5, 5, 12);
                animator.AssignSprite(currentTex);
            }

            Texture2D currentTex;
            Vector2 position;
            Rectangle characterFrameRec;
            Animator animator;
            
            public override void Update(float deltaTime)
            {
                base.Update(deltaTime);
                animator.Play();
            }

            public override void DrawUpdate(float deltaTime)
            {
                base.DrawUpdate(deltaTime);
                Raylib.DrawTextureRec(animator.GetSprite(), animator.GetFrameRec(), position, Color.WHITE);
            }

            public void ChangeCurrentSprite(Texture2D inputSprite, int numOfFramesPerRow, int numOfRows, int speed, float delayInSeconds, bool bPlayInReverse, bool bContinuous, bool bLooping)
            {
                animator.ChangeSprite(inputSprite, numOfFramesPerRow, numOfRows, speed, delayInSeconds, bPlayInReverse, bContinuous, bLooping);
            }
        }
    }
}
