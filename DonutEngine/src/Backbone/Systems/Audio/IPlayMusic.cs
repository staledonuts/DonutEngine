namespace DonutEngine.Backbone;
public interface IPlayMusic
{
    public static void PlayMusic(string name)
    {
        Sys.audioControl.PlayMusic(name);
    }
}