using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

using Engine.Systems.UI.Skeleton.Events;

namespace Engine.Systems.UI.Skeleton.Widgets;

public class Button : Widget {
    public string Text { get; set; }
    public Action? Action { get; set; }
    public bool Toggleable { get; set; }
    public float PaddingX { get; set; } = 20.0f;
    public float PaddingY { get; set; } = 20.0f;

    public Vector2 Size { get { return MeasureTextEx(Style.Font, Text, Style.FontSize, Style.FontSpacing) + new Vector2(PaddingX, PaddingY); } }
    public Rectangle ClickBox { get { return new Rectangle(Position.X, Position.Y, Size.X, Size.Y); } }

    public override float Width { get { return MeasureTextEx(Style.Font, Text, Style.FontSize, Style.FontSpacing).X + PaddingX; } }
    public override float Height { get { return MeasureTextEx(Style.Font, Text, Style.FontSize, Style.FontSpacing).Y + PaddingY; } }

    public bool Hovered { get { return CheckCollisionPointRec(GetMousePosition(), ClickBox);  } }
    public bool Clicked { get { return Hovered && IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT);  } }

    public bool Toggled { get; private set; } = false;

    public Button(string text, Action? action=null, bool? toggle=null, Style? style=null) : base(style) {
        Text = text;
        Action = action;
        Toggleable = toggle ?? false;
    }

    public override bool FireEvent(Event e) {
        if (e is MouseLeftEvent) {
            if (CheckCollisionPointRec(((MouseLeftEvent)e).Position, ClickBox)) {
                if (Action is not null) Action.Invoke();
                if (Toggleable) Toggled = !Toggled;
                return true;
	        }
	    }

        return base.FireEvent(e);
	}

    public override void Draw() {
        Color BG = Style.ButtonBackground;
        if (Hovered) BG = Style.ButtonBackgroundHover;
        if (Clicked || Toggled) BG = Style.ButtonBackgroundClick;

        Color FG = Style.ButtonForeground;
        if (Hovered) FG = Style.ButtonForegroundHover;
        if (Clicked) FG = Style.ButtonForegroundClick;

        DrawRectangleV(Position, Size, BG);
        DrawTextEx(Style.Font, Text, Position + new Vector2(PaddingX / 2, PaddingY / 2), Style.FontSize, Style.FontSpacing, FG);
    }
}