namespace Engine;
using Raylib_cs;
public class GameTime
{
    double currentTime
    {
        get
        {
            return Raylib.GetTime();
        }
    }

    //gameTime.TotalGameTime.Milliseconds
    /*float systemTime
    {
        get
        {
            System.TimeOnly.
        }
    }*/

}