// MIT License - Copyright (C) The Mono.xna Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System.Numerics;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Engine.Utils;
/// <summary>
/// Describes a 2D-point.
/// </summary>
[DataContract]
[DebuggerDisplay("{DebugDisplayString,nq}")]
public struct Point : IEquatable<Point>
{
    #region Private Fields

    private static readonly Point zeroPoint = new Point();

    #endregion

    #region Public Fields

    /// <summary>
    /// The x coordinate of this <see cref="Point"/>.
    /// </summary>
    [DataMember]
    public int x;

    /// <summary>
    /// The y coordinate of this <see cref="Point"/>.
    /// </summary>
    [DataMember]
    public int y;

    #endregion

    #region Properties

    /// <summary>
    /// Returns a <see cref="Point"/> with coordinates 0, 0.
    /// </summary>
    public static Point Zero
    {
        get { return zeroPoint; }
    }

    #endregion

    #region Internal Properties

    internal string DebugDisplayString
    {
        get
        {
            return string.Concat(
                this.x.ToString(), "  ",
                this.y.ToString()
            );
        }
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Constructs a point with x and y from two values.
    /// </summary>
    /// <param name="x">The x coordinate in 2d-space.</param>
    /// <param name="y">The y coordinate in 2d-space.</param>
    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    /// <summary>
    /// Constructs a point with x and y set to the same value.
    /// </summary>
    /// <param name="value">The x and y coordinates in 2d-space.</param>
    public Point(int value)
    {
        this.x = value;
        this.y = value;
    }

    #endregion

    #region Operators

    /// <summary>
    /// Adds two points.
    /// </summary>
    /// <param name="value1">Source <see cref="Point"/> on the left of the add sign.</param>
    /// <param name="value2">Source <see cref="Point"/> on the right of the add sign.</param>
    /// <returns>Sum of the points.</returns>
    public static Point operator +(Point value1, Point value2)
    {
        return new Point(value1.x + value2.x, value1.y + value2.y);
    }

    /// <summary>
    /// Subtracts a <see cref="Point"/> from a <see cref="Point"/>.
    /// </summary>
    /// <param name="value1">Source <see cref="Point"/> on the left of the sub sign.</param>
    /// <param name="value2">Source <see cref="Point"/> on the right of the sub sign.</param>
    /// <returns>Result of the subtraction.</returns>
    public static Point operator -(Point value1, Point value2)
    {
        return new Point(value1.x - value2.x, value1.y - value2.y);
    }

    /// <summary>
    /// Multiplies the components of two points by each other.
    /// </summary>
    /// <param name="value1">Source <see cref="Point"/> on the left of the mul sign.</param>
    /// <param name="value2">Source <see cref="Point"/> on the right of the mul sign.</param>
    /// <returns>Result of the multiplication.</returns>
    public static Point operator *(Point value1, Point value2)
    {
        return new Point(value1.x * value2.x, value1.y * value2.y);
    }

    /// <summary>
    /// Divides the components of a <see cref="Point"/> by the components of another <see cref="Point"/>.
    /// </summary>
    /// <param name="source">Source <see cref="Point"/> on the left of the div sign.</param>
    /// <param name="divisor">Divisor <see cref="Point"/> on the right of the div sign.</param>
    /// <returns>The result of dividing the points.</returns>
    public static Point operator /(Point source, Point divisor)
    {
        return new Point(source.x / divisor.x, source.y / divisor.y);
    }

    /// <summary>
    /// Compares whether two <see cref="Point"/> instances are equal.
    /// </summary>
    /// <param name="a"><see cref="Point"/> instance on the left of the equal sign.</param>
    /// <param name="b"><see cref="Point"/> instance on the right of the equal sign.</param>
    /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
    public static bool operator ==(Point a, Point b)
    {
        return a.Equals(b);
    }

    /// <summary>
    /// Compares whether two <see cref="Point"/> instances are not equal.
    /// </summary>
    /// <param name="a"><see cref="Point"/> instance on the left of the not equal sign.</param>
    /// <param name="b"><see cref="Point"/> instance on the right of the not equal sign.</param>
    /// <returns><c>true</c> if the instances are not equal; <c>false</c> otherwise.</returns>	
    public static bool operator !=(Point a, Point b)
    {
        return !a.Equals(b);
    }

    #endregion

    #region Public methods

    /// <summary>
    /// Compares whether current instance is equal to specified <see cref="Object"/>.
    /// </summary>
    /// <param name="obj">The <see cref="Object"/> to compare.</param>
    /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
    public override bool Equals(object obj)
    {
        return (obj is Point) && Equals((Point)obj);
    }

    /// <summary>
    /// Compares whether current instance is equal to specified <see cref="Point"/>.
    /// </summary>
    /// <param name="other">The <see cref="Point"/> to compare.</param>
    /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
    public bool Equals(Point other)
    {
        return ((x == other.x) && (y == other.y));
    }

    /// <summary>
    /// Gets the hash code of this <see cref="Point"/>.
    /// </summary>
    /// <returns>Hash code of this <see cref="Point"/>.</returns>
    public override int GetHashCode()
    {
        unchecked
        {
            var hash = 17;
            hash = hash * 23 + x.GetHashCode();
            hash = hash * 23 + y.GetHashCode();
            return hash;
        }

    }

    /// <summary>
    /// Returns a <see cref="String"/> representation of this <see cref="Point"/> in the format:
    /// {x:[<see cref="x"/>] y:[<see cref="y"/>]}
    /// </summary>
    /// <returns><see cref="String"/> representation of this <see cref="Point"/>.</returns>
    public override string ToString()
    {
        return "{x:" + x + " y:" + y + "}";
    }

    /// <summary>
    /// Gets a <see cref="Vector2"/> representation for this object.
    /// </summary>
    /// <returns>A <see cref="Vector2"/> representation for this object.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2 ToVector2()
    {
        return new Vector2(x, y);
    }

    /// <summary>
    /// Deconstruction method for <see cref="Point"/>.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void Deconstruct(out int X, out int Y)
    {
        X = x;
        Y = y;
    }

    #endregion
}