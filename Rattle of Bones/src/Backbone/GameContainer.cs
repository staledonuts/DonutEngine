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

        public void InitializeGameContainer()
        {
            
        }

    }
        static class GameObjects
        {
            
            public static Player player = new();

        }
}