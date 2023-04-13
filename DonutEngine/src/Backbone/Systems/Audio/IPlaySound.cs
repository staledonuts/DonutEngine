using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Collections;
using DonutEngine.Backbone.Systems;
namespace DonutEngine.Backbone;
public interface IPlaySound
{

    public static void PlaySFX(string name)
    {
        DonutSystems.audioControl.PlaySFX(name);
    }

    public static void PlaySFX(string name, float minPitch, float maxPitch)
    {
        DonutSystems.audioControl.PlaySFX(name, minPitch, maxPitch);
    }

}