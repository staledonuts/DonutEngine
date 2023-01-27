using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Raylib_cs;

namespace DonutEngine
{
    public class Runtime
    {
        public static void Update()
        {
            Time.RunDeltaTime();
        }
    }
}