namespace DonutEngine.Backbone;
using System.Numerics;
using DonutEngine.Backbone.Systems;
using Box2D.NetStandard.Dynamics.Bodies;
using Box2D.NetStandard.Collision;
using Raylib_cs;

public abstract partial class Entity
{
    public class SpriteRenderer
    {
        public SpriteRenderer(Texture2D spriteTex, Physics2D physics2D, int frameWidth, int frameHeight)
        {
            currentTex = spriteTex;
            currentPhysics2D = physics2D;
            characterFrameRec = new Rectangle(0, 0, 0, 0);
            animator = new Animator("Player", 3, 1, 12, false, false,true);
            animator.AssignSprite(currentTex);
        }

        Texture2D currentTex;
        Physics2D currentPhysics2D;
        Rectangle characterFrameRec;
        Animator animator;
        
        public void Update()
        {
            animator.Play();
        }

        public void DrawUpdate()
        {            
            Raylib.DrawTextureRec(animator.GetSprite(), animator.GetFrameRec(), currentPhysics2D.rigidbody2D.GetPosition(), Color.WHITE);
        }

        public void ChangeCurrentSprite(Texture2D inputSprite, int numOfFramesPerRow, int numOfRows, int speed, float delayInSeconds, bool bPlayInReverse, bool bContinuous, bool bLooping)
        {
            animator.ChangeSprite(inputSprite, numOfFramesPerRow, numOfRows, speed, delayInSeconds, bPlayInReverse, bContinuous, bLooping);
        }
    }
}

