using static Raylib_cs.Raylib;
using Raylib_cs;
using System.Numerics;
using DonutEngine.Backbone.Systems;


namespace DonutEngine
{
    public class Player : PlayerBehaviour
    {
        public bool canJump = true;
        public float speed = 30;
        public const float playerJumpSpd = 350.0f;
		public const float playerHorSpd = 200.0f;
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            Tasks();
        }

        public void Tasks()
        {   
           
        }

        public override void Start()
        {
            InputEventSystem.JumpEvent += JumpButton;
            InputEventSystem.AttackEvent += AttackButton;
            InputEventSystem.DashEvent += DashButton;
            InputEventSystem.DpadEvent += Movement;
            base.Start();
        }
        public void AttackButton(CBool inputBool)
        {
            if(inputBool)
            {
                Console.Write("Slice Slice!");
            }
        }

        public void DashButton(CBool inputBool)
        {
            if(inputBool)
            {
                Console.Write("Dash Dash!");
            }
        }

        public void JumpButton(CBool inputBool)
        {
            if(inputBool && !canJump)
            {
                canJump = false;
            }
            else if (!inputBool && canJump)
            {

            }
        }

        public void Movement(Vector2 vector2)
        {
            Console.Write(vector2);
            physics2D.rigidbody2D?.SetLinearVelocity(vector2 * playerHorSpd);
        }

    }
}