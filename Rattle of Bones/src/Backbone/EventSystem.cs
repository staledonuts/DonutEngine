using System;
namespace DonutEngine.Events;
public class EventSystem
{
    public delegate void GameEvent();

    public static event GameEvent? trigger;

    public static void ProcessEvent()
    {
        trigger?.Invoke();
    }


}