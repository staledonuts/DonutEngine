using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonutEngine.Backbone
{
    public class InputEventSystem
    {
        public delegate void Notify();  // delegate
                        
        public class ProcessInput
        {
            public event Notify ProcessCompleted; // event

        }
    }
}