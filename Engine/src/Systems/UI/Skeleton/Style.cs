using Engine.Assets;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Engine.Systems.UI.Skeleton;

public class Style 
{
    // Main
    public Color Background;
    public Color Foreground;

    // Button
    public Color ButtonBackground;
    public Color ButtonBackgroundHover;
    public Color ButtonBackgroundClick;

    public Color ButtonForeground;
    public Color ButtonForegroundHover;
    public Color ButtonForegroundClick;

    // Font
    public Font Font;
    public int FontSize;
    public int FontSpacing;

    public Style(
	    Color? foreground = null,
	    Color? background = null,
        Color? button_background = null,
        Color? button_background_hover = null,
        Color? button_background_click = null,
        Color? button_foreground = null,
        Color? button_foreground_hover = null,
        Color? button_foreground_click = null,
        Font? font = null,
        int? font_size = null,
        int? font_spacing = null
	) {
        Foreground = foreground ?? Color.White;
        Background = background ?? Color.DarkGray;

        ButtonBackground = button_background ?? Color.Gray;
        ButtonBackgroundHover = button_background_hover ?? Color.Red;
        ButtonBackgroundClick = button_background_click ?? Color.Maroon;
        ButtonForeground = button_foreground ?? Color.White;
        ButtonForegroundHover = button_foreground_hover ?? Color.White;
        ButtonForegroundClick = button_foreground_click ?? Color.White;

		//Font = font ?? LoadFont("Resources/pixantiqua.png");
		Font = Fonts.GetFont("PixelOperator");
        FontSize = font_size ?? Font.BaseSize;
        FontSpacing = font_spacing ?? 0;
    }
}
