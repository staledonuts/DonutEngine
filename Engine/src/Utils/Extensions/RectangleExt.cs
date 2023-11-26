namespace Engine.Utils.Extensions;
using Raylib_cs;
using System.Numerics;
using System.Runtime.CompilerServices;

public static class RectangleExt
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 GetCenter(this Rectangle rect)
    {
        return new(rect.Width / 2, rect.Height / 2);
    }
    public static void FlipHorizontal(this Rectangle rect)
    {        
        rect.Width = rect.Width * -1;
    }
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
    public static bool Contains(this Rectangle rect, System.Numerics.Vector2 value)
    {
        return (rect.X <= value.X) && (value.X < (rect.X + rect.Width)) && (rect.Y <= value.Y) && (value.Y < (rect.Y + rect.Height));
    }

    /// <summary>
    /// Gets whether or not the provided <see cref="Vector2"/> lies within the bounds of this <see cref="Rectangle"/>.
    /// </summary>
    /// <param name="value">The coordinates to check for inclusion in this <see cref="Rectangle"/>.</param>
    /// <param name="result"><c>true</c> if the provided <see cref="Vector2"/> lies inside this <see cref="Rectangle"/>; <c>false</c> otherwise. As an output parameter.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Contains(this Rectangle rect, ref System.Numerics.Vector2 value, out bool result)
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

     /// <summary>
    /// Changes the <see cref="Location"/> of this <see cref="Rectangle"/>.
    /// </summary>
    /// <param name="offsetX">The x coordinate to add to this <see cref="Rectangle"/>.</param>
    /// <param name="offsetY">The y coordinate to add to this <see cref="Rectangle"/>.</param>
    public static void Offset(this Rectangle rect, int offsetX, int offsetY)
    {
        rect.X += offsetX;
        rect.Y += offsetY;
    }

    /// <summary>
    /// Changes the <see cref="Location"/> of this <see cref="Rectangle"/>.
    /// </summary>
    /// <param name="offsetX">The x coordinate to add to this <see cref="Rectangle"/>.</param>
    /// <param name="offsetY">The y coordinate to add to this <see cref="Rectangle"/>.</param>
    public static void Offset(this Rectangle rect, float offsetX, float offsetY)
    {
        rect.X += (int)offsetX;
        rect.Y += (int)offsetY;
    }

    /// <summary>
    /// Changes the <see cref="Location"/> of this <see cref="Rectangle"/>.
    /// </summary>
    /// <param name="amount">The x and y components to add to this <see cref="Rectangle"/>.</param>
    public static void Offset(this Rectangle rect, Point amount)
    {
        rect.X += amount.x;
        rect.Y += amount.y;
    }

    /// <summary>
    /// Changes the <see cref="Location"/> of this <see cref="Rectangle"/>.
    /// </summary>
    /// <param name="amount">The x and y components to add to this <see cref="Rectangle"/>.</param>
    public static void Offset(this Rectangle rect, System.Numerics.Vector2 amount)
    {
        rect.X += (int)amount.X;
        rect.Y += (int)amount.Y;
    }
}