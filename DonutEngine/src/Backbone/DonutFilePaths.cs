namespace DonutEngine
{
    public static class DonutFilePaths
    {
        public readonly static string app =  AppDomain.CurrentDomain.BaseDirectory;
        public readonly static string winSettings = AppDomain.CurrentDomain.BaseDirectory+"Win_Settings.ini";
        public readonly static string linuxSettings = AppDomain.CurrentDomain.BaseDirectory+"Linux_Settings.ini";
        public readonly static string bsdSettings = AppDomain.CurrentDomain.BaseDirectory+"Bsd_Settings.ini";
    }
}