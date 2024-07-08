using System.Numerics;

namespace Engine.Systems.UI.Skeleton.Events;

public class MouseWheelEvent : Event {
    public float Amount { get; private set; }

    public MouseWheelEvent(float amount) {
        Amount = amount;
    }
}
