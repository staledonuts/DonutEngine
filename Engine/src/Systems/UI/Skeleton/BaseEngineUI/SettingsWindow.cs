namespace Engine.Systems.UI.Skeleton;

using System.Numerics;
using Engine.Assets;
using Engine.Systems.UI.Skeleton.Widgets;
using Skeleton;

public class SettingsWindow : Container
{
    Style style;
    public SettingsWindow
    (
        Vector2 position, 
        Vector2 size, 
        Style style,
        bool active = true, 
        float? margin = null, 
        float? padding = null, 
        float? spacing = null
    ):
    base
    (
        position,
        size,
        style,
        active,
        margin,
        padding,
        spacing 
    )
    { this.style = style; Active = true; }

    public override void Initialize()
    {
        AddWidget(new Label("Settings", style));
        base.Initialize();
    }
}