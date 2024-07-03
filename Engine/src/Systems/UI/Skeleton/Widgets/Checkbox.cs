using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

using Engine.Systems.UI.Skeleton.Events;
using Engine.Enums;

namespace Engine.Systems.UI.Skeleton.Widgets;

public class Checkbox : Widget 
{
    public Action? Action { get; set; }

    public Vector2 Size { get; private set; } = new Vector2(16, 16);
    public Rectangle ClickBox { get { return new Rectangle(Position.X, Position.Y, Size.X, Size.Y); } }

    public override float Width { get { return 20.0f; } }
    public override float Height { get { return 20.0f; } }

    public bool Checked { get; private set; }

    public Checkbox(Action? action=null, bool? check_state=null, Style? style=null) : base(style) 
    {
        Action = action;
        Checked = check_state ?? false;
    }

    public override bool FireEvent(Event e) 
    {
        if (e is MouseLeftEvent) 
        {
            if (CheckCollisionPointRec(((MouseLeftEvent)e).Position, ClickBox)) 
            {
                if (Action is not null) Action.Invoke();
                Checked = !Checked;
                return true;
	        }
	    }

        return base.FireEvent(e);
	}

    public override void Draw() 
    {
        Draw2D.DrawRectangle2D(Layers.UI, Position, Size, Style.Background);
        if (Checked) 
        {
            Draw2D.DrawRectangle2D(Layers.UI, Position, Size, Style.ButtonBackgroundClick);
        }
    }
}