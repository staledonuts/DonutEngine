using System.Numerics;
using Engine.Enums;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Engine.Systems.UI.Skeleton.Widgets;

public class Label : Widget 
{
    public string Text { get; set; }
    public override float Width { get { return MeasureTextEx(Style.Font, Text, Style.FontSize, Style.FontSpacing).X; } }
    public override float Height { get { return MeasureTextEx(Style.Font, Text, Style.FontSize, Style.FontSpacing).Y; } }

    public Label(string text, Style? style=null) : base(style) 
    {
        Text = text;
    }

    public override void Draw() 
    {
        Draw2D.DrawText2D(Layers.UI, Style, Text, Position, new(),  Style.Background, Style.Foreground);
    }
}

