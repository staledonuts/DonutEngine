namespace DonutEngine
{
    public class Time
    {
        public static float deltaTime = 10;
        public static float lastTime = Raylib_cs.Raylib.GetFrameTime();

        public static void RunDeltaTime()
        {
            float now = Raylib_cs.Raylib.GetFrameTime();
            deltaTime = (now - lastTime) / Raylib_cs.Raylib.GetFrameTime();
            lastTime = now;
        }
    }
}