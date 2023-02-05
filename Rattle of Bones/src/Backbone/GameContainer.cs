using Raylib_cs;
using System.Numerics;
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
            GameObjects.donutcam = new();
            GameObjects.donutcam.zoom = 1.0f;
            GameObjects.donutcam.offset = new Vector2(SettingsContainer.screenWidth / 2, SettingsContainer.screenHeight / 2);
            GameObjects.donutcam.target = GameObjects.player.transform.position;
        }

    }
        static class GameObjects
        {
            public static Camera2D donutcam;
            public static Player player = new();

        }
}