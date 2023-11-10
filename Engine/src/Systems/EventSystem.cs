using System;
namespace Engine.Events;
public static class GameEvents
{
    public delegate void GameEvent();

    public static event GameEvent? trigger;

    public static void ProcessEvent()
    {
        trigger?.Invoke();
    }
}