namespace Engine.Utils;
using Raylib_cs;
using System.Numerics;
using System.Runtime.CompilerServices;

public static class RectangleExt
{
    /// <summary>
    /// Gets whether or not the provided <see cref="Point"/> lies within the bounds of this <see cref="Rectangle"/>.
    /// </summary>
    /// <param name="value">The coordinates to check for inclusion in this <see cref="Rectangle"/>.</param>
    /// <returns><c>true</c> if the provided <see cref="Point"/> lies inside this <see cref="Rectangle"/>; <c>false</c> otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains(this Rectangle rect, Point value)
    {
        return (rect.X <= value.x) && (value.x < (rect.X + rect.Width)) && (rect.Y <= value.y) && (value.y < (rect.Y + rect.Height));
    }

    /// <summary>
    /// Gets whether or not the provided <see cref="Point"/> lies within the bounds of this <see cref="Rectangle"/>.
    /// </summary>
    /// <param name="value">The coordinates to check for inclusion in this <see cref="Rectangle"/>.</param>
    /// <param name="result"><c>true</c> if the provided <see cref="Point"/> lies inside this <see cref="Rectangle"/>; <c>false</c> otherwise. As an output parameter.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Contains(this Rectangle rect, ref Point value, out bool result)
    {
        result = (rect.X <= value.x) && (value.x < (rect.X + rect.Width)) && (rect.Y <= value.y) && (value.y < (rect.Y + rect.Height));
    }

    /// <summary>
    /// Gets whether or not the provided <see cref="Vector2"/> lies within the bounds of this <see cref="Rectangle"/>.
    /// </summary>
    /// <param name="value">The coordinates to check for inclusion in this <see cref="Rectangle"/>.</param>
    /// <returns><c>true</c> if the provided <see cref="Vector2"/> lies inside this <see cref="Rectangle"/>; <c>false</c> otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains(this Rectangle rect, Vector2 value)
    {
        return (rect.X <= value.X) && (value.X < (rect.X + rect.Width)) && (rect.Y <= value.Y) && (value.Y < (rect.Y + rect.Height));
    }

    /// <summary>
    /// Gets whether or not the provided <see cref="Vector2"/> lies within the bounds of this <see cref="Rectangle"/>.
    /// </summary>
    /// <param name="value">The coordinates to check for inclusion in this <see cref="Rectangle"/>.</param>
    /// <param name="result"><c>true</c> if the provided <see cref="Vector2"/> lies inside this <see cref="Rectangle"/>; <c>false</c> otherwise. As an output parameter.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Contains(this Rectangle rect, ref Vector2 value, out bool result)
    {
        result = (rect.X <= value.X) && (value.X < (rect.X + rect.Width)) && (rect.Y <= value.Y) && (value.Y < (rect.Y + rect.Height));
    }

    /// <summary>
    /// Gets whether or not the provided <see cref="Rectangle"/> lies within the bounds of this <see cref="Rectangle"/>.
    /// </summary>
    /// <param name="value">The <see cref="Rectangle"/> to check for inclusion in this <see cref="Rectangle"/>.</param>
    /// <returns><c>true</c> if the provided <see cref="Rectangle"/>'s bounds lie entirely inside this <see cref="Rectangle"/>; <c>false</c> otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains(this Rectangle rect, Rectangle value)
    {
        return (rect.X <= value.X) && ((value.X + value.Width) <= (rect.X + rect.Width)) && (rect.Y <= value.Y) && ((value.Y + value.Height) <= (rect.Y + rect.Height));
    }

    /// <summary>
    /// Gets whether or not the provided coordinates lie within the bounds of this <see cref="Rectangle"/>.
    /// </summary>
    /// <param name="x">The x coordinate of the point to check for containment.</param>
    /// <param name="y">The y coordinate of the point to check for containment.</param>
    /// <returns><c>true</c> if the provided coordinates lie inside this <see cref="Rectangle"/>; <c>false</c> otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains(this Rectangle rect ,int x, int y)
    {
        return (rect.X <= x) && (x < (rect.X + rect.Width)) && (rect.Y <= y) && (y < (rect.Y + rect.Height));
    }

    /// <summary>
    /// Gets whether or not the provided coordinates lie within the bounds of this <see cref="Rectangle"/>.
    /// </summary>
    /// <param name="x">The x coordinate of the point to check for containment.</param>
    /// <param name="y">The y coordinate of the point to check for containment.</param>
    /// <returns><c>true</c> if the provided coordinates lie inside this <see cref="Rectangle"/>; <c>false</c> otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains(this Rectangle rect, float x, float y)
    {
        return (rect.X <= x) && (x < (rect.X + rect.Width)) && (rect.Y <= y) && (y < (rect.Y + rect.Height));
    }

    /// <summary>
    /// Gets whether or not the provided <see cref="Rectangle"/> lies within the bounds of this <see cref="Rectangle"/>.
    /// </summary>
    /// <param name="value">The <see cref="Rectangle"/> to check for inclusion in this <see cref="Rectangle"/>.</param>
    /// <param name="result"><c>true</c> if the provided <see cref="Rectangle"/>'s bounds lie entirely inside this <see cref="Rectangle"/>; <c>false</c> otherwise. As an output parameter.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Contains(this Rectangle rect, ref Rectangle value,out bool result)
    {
        result = (rect.X <= value.X) && ((value.X + value.Width) <= (rect.X + rect.Width)) && (rect.Y <= value.Y) && ((value.Y + value.Height) <= (rect.Y + rect.Height));
    }

    /// <summary>
    /// Adjusts the edges of this <see cref="Rectangle"/> by specified horizontal and vertical amounts. 
    /// </summary>
    /// <param name="horizontalAmount">Value to adjust the left and right edges.</param>
    /// <param name="verticalAmount">Value to adjust the top and bottom edges.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Inflate(this Rectangle rect, int horizontalAmount, int verticalAmount)
    {
        rect.X -= horizontalAmount;
        rect.Y -= verticalAmount;
        rect.Width += horizontalAmount * 2;
        rect.Height += verticalAmount * 2;
    }

    /// <summary>
    /// Adjusts the edges of this <see cref="Rectangle"/> by specified horizontal and vertical amounts. 
    /// </summary>
    /// <param name="horizontalAmount">Value to adjust the left and right edges.</param>
    /// <param name="verticalAmount">Value to adjust the top and bottom edges.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Inflate(this Rectangle rect, float horizontalAmount, float verticalAmount)
    {
        rect.X -= (int)horizontalAmount;
        rect.Y -= (int)verticalAmount;
        rect.Width += (int)horizontalAmount * 2;
        rect.Height += (int)verticalAmount * 2;
    }
}