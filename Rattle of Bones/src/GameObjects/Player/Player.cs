using static Raylib_cs.Raylib;
using Raylib_cs;

namespace DonutEngine
{
    public class Player : PlayerBehaviour
    {
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            Tasks();
        }

        public void Tasks()
        {   
            /*UpdatePlayer(this,,3);

            void UpdatePlayer(ref Player player, EnvItem[] envItems, float delta)
            {
                if (IsKeyDown(KEY_LEFT))
                    player.position.X -= PLAYER_HOR_SPD * delta;
                if (IsKeyDown(KEY_RIGHT))
                    player.position.X += PLAYER_HOR_SPD * delta;
                if (IsKeyDown(KEY_SPACE) && player.canJump)
                {
                    player.speed = -PLAYER_JUMP_SPD;
                    player.canJump = false;
                }

                int hitObstacle = 0;
                for (int i = 0; i < envItems.Length; i++)
                {
                    EnvItem ei = envItems[i];
                    Vector2 p = player.position;
                    if (ei.blocking != 0 &&
                        ei.rect.x <= p.X &&
                        ei.rect.x + ei.rect.width >= p.X &&
                        ei.rect.y >= p.Y &&
                        ei.rect.y < p.Y + player.speed * delta)
                    {
                        hitObstacle = 1;
                        player.speed = 0.0f;
                        player.position.Y = ei.rect.y;
                    }
                }
            Raylib.DrawCircle((int)transform.position.X,(int)transform.position.Y,10f,Color.BLACK);
            if(Raylib.IsGamepadButtonDown(0,GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_DOWN))
            {
                transform.position.Y -= 3;
            }*/


        }


    }
}