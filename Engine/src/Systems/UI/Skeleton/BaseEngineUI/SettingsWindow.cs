namespace Engine.Systems.UI.Skeleton;

using System.Numerics;
using Engine.Assets;
using Engine.Systems.UI.Skeleton.Widgets;
using Raylib_cs;
using Skeleton;

public class SettingsWindow : Container
{
    Style style1;
    public SettingsWindow
    (
        Vector2 position, 
        Vector2 size, 
        Style style, 
        float? margin = null, 
        float? padding = null, 
        float? spacing = null
    ):
    base
    (
        position,
        size,
        style,
        margin,
        padding,
        spacing 
    )
    { style1 = style; Active = true; }

    public override void Initialize()
    {
        AddWidget(new Label("Settings", style1));
        base.Initialize();
    }
}