using Raylib_cs;
using System;
using DonutEngine.Backbone;

namespace DonutEngine
{
    public class GameContainer
    {        
        private static GameContainer? _current;
        public static GameContainer current
        {
            get
            {
                if(_current == null)
                {
                    _current = new GameContainer();
                }
                return _current;
            }
            set
            {
                if(value != null)
                {
                    _current = value;
                }
            }
        }

        public void InitGameContainer()
        {
            Raylib.BeginMode2D(GameObjects.donutcam);
            GameContainer.current.SetCameraTarget(GameObjects.player.transform);
        }

        /*public static bool LoadLevel()
        {

        }*/

        public void SetCameraTarget(ECS.Transform2D target)
        {
            GameObjects.donutcam.target = target.position;
        }

        public void UpdateCamera(ECS.Transform2D target)
        {
            GameObjects.donutcam.target = target.position;
        }
            
    

    }
        static class GameObjects
        {
            public static Camera2D donutcam;
            public static Player player = new();

        }
}