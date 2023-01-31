using Raylib_cs;

namespace DonutEngine
{
    public class GameContainer
    {
        public GameContainer()
        {
            CameraHandler.SetCameraTarget(player.transform);
        }
        
        public Player player = new();

        
    }
}