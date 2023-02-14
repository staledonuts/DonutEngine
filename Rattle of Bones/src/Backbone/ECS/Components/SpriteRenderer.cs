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
            public SpriteRenderer(Texture2D spriteTex, Physics2D physics2D, int frameWidth, int frameHeight)
            {
                currentTex = spriteTex;
                currentPhysics2D = physics2D;
                characterFrameRec = new Rectangle(0, 0, 0, 0);
                animator = new Animator("Player", 5, 5, 12);
                animator.AssignSprite(currentTex);
            }

            Texture2D currentTex;
            Physics2D currentPhysics2D;
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
                Raylib.DrawCircle((int)currentPhysics2D.rigidbody2D.Position.X, (int)currentPhysics2D.rigidbody2D.Position.Y,4 , Color.DARKGREEN);
                Raylib.DrawTextureRec(animator.GetSprite(), animator.GetFrameRec(), currentPhysics2D.rigidbody2D.Position, Color.WHITE);
            }

            public void ChangeCurrentSprite(Texture2D inputSprite, int numOfFramesPerRow, int numOfRows, int speed, float delayInSeconds, bool bPlayInReverse, bool bContinuous, bool bLooping)
            {
                animator.ChangeSprite(inputSprite, numOfFramesPerRow, numOfRows, speed, delayInSeconds, bPlayInReverse, bContinuous, bLooping);
            }
        }
    }
}
