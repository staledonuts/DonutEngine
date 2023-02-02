using static Raylib_cs.Raylib;
using Raylib_cs;

namespace DonutEngine
{
    public partial class Player : PlayerBehaviour
    {
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            Tasks();
        }

        public void Tasks()
        {
            if(Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN))
            {
                transform.position.X += 20;
            }
        }


    }
}