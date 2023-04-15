namespace DonutEngine.Backbone;
public interface IPlaySound
{

    public static void PlaySFX(string name)
    {
        Sys.audioControl.PlaySFX(name);
    }

    public static void PlaySFX(string name, float minPitch, float maxPitch)
    {
        Sys.audioControl.PlaySFX(name, minPitch, maxPitch);
    }

}