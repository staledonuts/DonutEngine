using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Collections;
using DonutEngine.Backbone.Systems;
namespace DonutEngine.Backbone;
public interface IPlayMusic
{
    public static void PlayMusic(string name)
    {
        DonutSystems.audioControl.PlayMusic(name);
    }
}