using static Raylib_cs.Raylib;
using Raylib_cs;
using DonutEngine.Backbone;


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
           MovePlayer();
        }

        public void MovePlayer()
        {
            transform.position += InputVars.dpad;
        }


    }
}