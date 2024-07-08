using System.Numerics;

using Engine.Systems.UI.Skeleton.Events;

namespace Engine.Systems.UI.Skeleton.Widgets;

// Should hold any number of widgets, not just two

public class HBox : Widget 
{
    public List<Widget> Children { get; private set; }

    public override float Width { get { return Children.Sum(c => c.Width); } }
    public override float Height 
    { 
        get 
        {
            float H = 0.0f;
            foreach (var C in Children) 
            {
                if (C.Height > H)
                {
                    H = C.Height;
                }
            }
            return H;
        } 
    }

    public HBox(List<Widget>? children=null, Style? style=null) : base(style) 
    {
        Children = children ?? new List<Widget>();
    }

    public void AddChild(Widget widget, int index=0) 
    {
        Children.Insert(index, widget);
    }

    public override bool FireEvent(Event e) 
    {
        foreach (var C in Children) 
        {
            if (C.FireEvent(e))
                return true;
        }

        return base.FireEvent(e);
	}

    public override void Draw() 
    {
        float Offset = 0.0f;
        foreach (var C in Children) 
        {
			float PX = Position.X + Offset;
			float PY = Position.Y + (Height / 2) - (C.Height / 2);

            C.Position = new Vector2(PX, PY);

            C.Draw();

            Offset += C.Width;
        }
    }
}