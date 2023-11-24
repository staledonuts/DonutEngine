using System.Numerics;
using System;
using Raylib_cs;
using static Raylib_cs.Raylib;

using Engine.Systems.UI.Skeleton.Events;

namespace Engine.Systems.UI.Skeleton;

public class Container {
	public SkeletonUISystem? Parent { get; set; }
	public Vector2 Position { get; private set; }
	public Vector2 Size { get; private set; }
	public float Margin { get; private set; }
	public float Padding { get; private set; } // Unimplemented
	public float Spacing { get; private set; }
	public Style? Style { get; set; }

	public bool Active { get; set; } = true;

	public Vector2 Origin { get { return new Vector2(Position.X + Margin, Position.Y + Margin); } }

	public List<Widget> Widgets { get; private set; }

	public bool MatchContentSize { get; set; } = false;
	public bool KeepMinimumSize { get; set; } = false;
	public bool IgnoreGlobalStyle { get; set; } = false;


	public Container(Vector2 position, Vector2 size, Style style, bool active = true, float? margin=null, float? padding=null, float? spacing=null) 
	{
		Position = position;
		Size = size;
		Margin = margin ?? 25.0f;
		Padding = padding ?? 5.0f;
		Spacing = spacing ?? 5.0f;
		Style = style;
		Active = active;

		Widgets = new List<Widget>();
	}

	// Add a widget to this container if the widget isn't already a child of this container
	public bool AddWidget(Widget w, bool active = true) 
	{
		if (Widgets.Contains(w))
		{
			Raylib.TraceLog(TraceLogLevel.LOG_INFO , "The widget "+w.GetType().Name+" already exists in this Container!");
			return false;
		}
		
		Raylib.TraceLog(TraceLogLevel.LOG_INFO , $"Added "+w.GetType().Name+" widget!");
		w.Parent = this;
		w.Active = active;
		Widgets.Add(w);
		return true;
    }

    // Fire an event if the event meets the required conditions, otherwise pass the event to all contained widgets
	public bool FireEvent(Event e)
	{
		if (e is MouseWheelEvent) 
		{
			Raylib.TraceLog(TraceLogLevel.LOG_INFO , $"Mouse was moved! {((MouseWheelEvent)e).Amount}");
			return true;
		}

		foreach (var W in Widgets) 
		{
			if (W.FireEvent(e))
			{
				return true;
			}
		}

		return false;
	}

	public virtual void Initialize()
	{
		
	}

	// Update the container and all child elements
	public void Update() 
	{
		foreach (var W in Widgets)
		{
			W.Update();
		}
    }

	// Draw the container and all child elements
	public void Draw() 
	{
		var S = Size;

		// Calculate size if FitContent is enabled
		if (MatchContentSize) 
		{
			float MinWidth = KeepMinimumSize ? Size.X : 0.0f;
			float MinHeight = 0.0f;

			for (int i = 0; i < Widgets.Count(); i++) 
			{
				var W = Widgets[i];
				if (W.Width > MinWidth) MinWidth = W.Width;
				MinHeight += W.Height;
			}

			if (KeepMinimumSize && MinHeight < Size.Y) MinHeight = Size.Y;

			S = new Vector2(MinWidth + (Margin * 2), MinHeight + (Margin * 2) + (Spacing * (Widgets.Count() - 1)));
		}

		DrawRectangleV(Position, S, Style.Background);

		float Offset = 0;
		//foreach (var W in Widgets) {
		for (int i = 0; i < Widgets.Count(); i++) 
		{
			var W = Widgets[i];

			var Pos = new Vector2(Origin.X, Origin.Y + Offset + (Spacing * i));

			Offset += W.Height;

			W.Position = Pos;
			W.Draw();
		}
    }
}
