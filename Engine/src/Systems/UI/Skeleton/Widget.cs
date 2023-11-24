using System.Numerics;
using Raylib_cs;

namespace Engine.Systems.UI.Skeleton;

public abstract class Widget 
{
    public Container? Parent { get; set; }
    public Vector2 Position { get; set; }
    public Style Style { get; set; }
    public bool Active { get; set; }

    public bool IgnoreGlobalStyle { get; set; } = false;

    public virtual float Width { get { return 0.0f; } }
    public virtual float Height { get { return 0.0f; } }

    public Widget(Style? style=null) 
    {
        Style = style ?? new Style();
    }

    // Fire an event if the event meets the required conditions
    public virtual bool FireEvent(Event e) 
    {
        return false;
    }

    // Update the widget, if anything needs to be updated
    public virtual void Update() { }

    // Draw the widget, if anything needs to be drawn
    public virtual void Draw() { }
}