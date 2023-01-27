using Raylib_cs;

namespace DonutEngine
{
    public class Player : DonutBehaviour
    {
        new public void update()
        {    
            Raylib.DrawCircle(100,100,20,Color.BLUE);
            Raylib.DrawText(Time.deltaTime.ToString(), 12, 12, 20, Color.BLACK);
        }
    }
}