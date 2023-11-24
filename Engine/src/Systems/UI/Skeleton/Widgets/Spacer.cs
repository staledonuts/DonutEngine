using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Engine.Systems.UI.Skeleton.Widgets;

public class Spacer : Widget 
{
    public float Size { get; set; }
    public override float Width { get { return Size; } }
    public Spacer(float width, Style? style=null) : base(style) 
    {
        Size = width;
    }
}