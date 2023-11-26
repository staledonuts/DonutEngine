// MIT License - Copyright (C) The Mono.Xna Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Engine.Utils.Extensions;

namespace Engine.Utils;

/// <summary>
/// Describes a 2D-vector.
/// </summary>
[DataContract]
[DebuggerDisplay("{DebugDisplayString,nq}")]
public struct Vector2i : IEquatable<Vector2i>
{
    #region Private Fields

    private static readonly Vector2i zeroVector = new Vector2i(0, 0);
    private static readonly Vector2i unitVector = new Vector2i(1, 1);
    private static readonly Vector2i unitXVector = new Vector2i(1, 0);
    private static readonly Vector2i unitYVector = new Vector2i(0, 1);

    #endregion

    #region Public Fields

    /// <summary>
    /// The x coordinate of this <see cref="Vector2i"/>.
    /// </summary>
    [DataMember]
    public int X;

    /// <summary>
    /// The y coordinate of this <see cref="Vector2i"/>.
    /// </summary>
    [DataMember]
    public int Y;

    #endregion

    #region Properties

    /// <summary>
    /// Returns a <see cref="Vector2i"/> with components 0, 0.
    /// </summary>
    public static Vector2i Zero
    {
        get { return zeroVector; }
    }

    /// <summary>
    /// Returns a <see cref="Vector2i"/> with components 1, 1.
    /// </summary>
    public static Vector2i One
    {
        get { return unitVector; }
    }

    /// <summary>
    /// Returns a <see cref="Vector2i"/> with components 1, 0.
    /// </summary>
    public static Vector2i UnitX
    {
        get { return unitXVector; }
    }

    /// <summary>
    /// Returns a <see cref="Vector2i"/> with components 0, 1.
    /// </summary>
    public static Vector2i UnitY
    {
        get { return unitYVector; }
    }

    #endregion

    #region Internal Properties

    internal string DebugDisplayString
    {
        get
        {
            return string.Concat(
                this.X.ToString(), "  ",
                this.Y.ToString()
            );
        }
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Constructs a 2d vector with X and Y from two values.
    /// </summary>
    /// <param name="x">The x coordinate in 2d-space.</param>
    /// <param name="y">The y coordinate in 2d-space.</param>
    public Vector2i(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    /// <summary>
    /// Constructs a 2d vector with X and Y set to the same value.
    /// </summary>
    /// <param name="value">The x and y coordinates in 2d-space.</param>
    public Vector2i(int value)
    {
        this.X = value;
        this.Y = value;
    }

    #endregion

    #region Operators

    /// <summary>
    /// Converts a <see cref="System.Numerics.Vector2i"/> to a <see cref="Vector2i"/>.
    /// </summary>
    /// <param name="value">The converted value.</param>
    public static implicit operator Vector2i(System.Numerics.Vector2 value)
    {
        return new Vector2i((int)value.X, (int)value.Y);
    }

    /// <summary>
    /// Inverts values in the specified <see cref="Vector2i"/>.
    /// </summary>
    /// <param name="value">Source <see cref="Vector2i"/> on the right of the sub sign.</param>
    /// <returns>Result of the inversion.</returns>
    public static Vector2i operator -(Vector2i value)
    {
        value.X = -value.X;
        value.Y = -value.Y;
        return value;
    }

    /// <summary>
    /// Adds two vectors.
    /// </summary>
    /// <param name="value1">Source <see cref="Vector2i"/> on the left of the add sign.</param>
    /// <param name="value2">Source <see cref="Vector2i"/> on the right of the add sign.</param>
    /// <returns>Sum of the vectors.</returns>
    public static Vector2i operator +(Vector2i value1, Vector2i value2)
    {
        value1.X += value2.X;
        value1.Y += value2.Y;
        return value1;
    }

    /// <summary>
    /// Subtracts a <see cref="Vector2i"/> from a <see cref="Vector2i"/>.
    /// </summary>
    /// <param name="value1">Source <see cref="Vector2i"/> on the left of the sub sign.</param>
    /// <param name="value2">Source <see cref="Vector2i"/> on the right of the sub sign.</param>
    /// <returns>Result of the vector subtraction.</returns>
    public static Vector2i operator -(Vector2i value1, Vector2i value2)
    {
        value1.X -= value2.X;
        value1.Y -= value2.Y;
        return value1;
    }

    /// <summary>
    /// Multiplies the components of two vectors by each other.
    /// </summary>
    /// <param name="value1">Source <see cref="Vector2i"/> on the left of the mul sign.</param>
    /// <param name="value2">Source <see cref="Vector2i"/> on the right of the mul sign.</param>
    /// <returns>Result of the vector multiplication.</returns>
    public static Vector2i operator *(Vector2i value1, Vector2i value2)
    {
        value1.X *= value2.X;
        value1.Y *= value2.Y;
        return value1;
    }

    /// <summary>
    /// Multiplies the components of vector by a scalar.
    /// </summary>
    /// <param name="value">Source <see cref="Vector2i"/> on the left of the mul sign.</param>
    /// <param name="scaleFactor">Scalar value on the right of the mul sign.</param>
    /// <returns>Result of the vector multiplication with a scalar.</returns>
    public static Vector2i operator *(Vector2i value, int scaleFactor)
    {
        value.X *= scaleFactor;
        value.Y *= scaleFactor;
        return value;
    }

    /// <summary>
    /// Multiplies the components of vector by a scalar.
    /// </summary>
    /// <param name="scaleFactor">Scalar value on the left of the mul sign.</param>
    /// <param name="value">Source <see cref="Vector2i"/> on the right of the mul sign.</param>
    /// <returns>Result of the vector multiplication with a scalar.</returns>
    public static Vector2i operator *(int scaleFactor, Vector2i value)
    {
        value.X *= scaleFactor;
        value.Y *= scaleFactor;
        return value;
    }

    /// <summary>
    /// Divides the components of a <see cref="Vector2i"/> by the components of another <see cref="Vector2i"/>.
    /// </summary>
    /// <param name="value1">Source <see cref="Vector2i"/> on the left of the div sign.</param>
    /// <param name="value2">Divisor <see cref="Vector2i"/> on the right of the div sign.</param>
    /// <returns>The result of dividing the vectors.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2i operator /(Vector2i value1, Vector2i value2)
    {
        value1.X /= value2.X;
        value1.Y /= value2.Y;
        return value1;
    }

    /// <summary>
    /// Divides the components of a <see cref="Vector2i"/> by a scalar.
    /// </summary>
    /// <param name="value1">Source <see cref="Vector2i"/> on the left of the div sign.</param>
    /// <param name="divider">Divisor scalar on the right of the div sign.</param>
    /// <returns>The result of dividing a vector by a scalar.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2i operator /(Vector2i value1, int divider)
    {
        int factor = 1 / divider;
        value1.X *= factor;
        value1.Y *= factor;
        return value1;
    }

    /// <summary>
    /// Compares whether two <see cref="Vector2i"/> instances are equal.
    /// </summary>
    /// <param name="value1"><see cref="Vector2i"/> instance on the left of the equal sign.</param>
    /// <param name="value2"><see cref="Vector2i"/> instance on the right of the equal sign.</param>
    /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
    public static bool operator ==(Vector2i value1, Vector2i value2)
    {
        return value1.X == value2.X && value1.Y == value2.Y;
    }

    /// <summary>
    /// Compares whether two <see cref="Vector2i"/> instances are not equal.
    /// </summary>
    /// <param name="value1"><see cref="Vector2i"/> instance on the left of the not equal sign.</param>
    /// <param name="value2"><see cref="Vector2i"/> instance on the right of the not equal sign.</param>
    /// <returns><c>true</c> if the instances are not equal; <c>false</c> otherwise.</returns>	
    public static bool operator !=(Vector2i value1, Vector2i value2)
    {
        return value1.X != value2.X || value1.Y != value2.Y;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Performs vector addition on <paramref name="value1"/> and <paramref name="value2"/>.
    /// </summary>
    /// <param name="value1">The first vector to add.</param>
    /// <param name="value2">The second vector to add.</param>
    /// <returns>The result of the vector addition.</returns>
    public static Vector2i Add(Vector2i value1, Vector2i value2)
    {
        value1.X += value2.X;
        value1.Y += value2.Y;
        return value1;
    }

    /// <summary>
    /// Performs vector addition on <paramref name="value1"/> and
    /// <paramref name="value2"/>, storing the result of the
    /// addition in <paramref name="result"/>.
    /// </summary>
    /// <param name="value1">The first vector to add.</param>
    /// <param name="value2">The second vector to add.</param>
    /// <param name="result">The result of the vector addition.</param>
    public static void Add(ref Vector2i value1, ref Vector2i value2, out Vector2i result)
    {
        result.X = value1.X + value2.X;
        result.Y = value1.Y + value2.Y;
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains the cartesian coordinates of a vector specified in barycentric coordinates and relative to 2d-triangle.
    /// </summary>
    /// <param name="value1">The first vector of 2d-triangle.</param>
    /// <param name="value2">The second vector of 2d-triangle.</param>
    /// <param name="value3">The third vector of 2d-triangle.</param>
    /// <param name="amount1">Barycentric scalar <c>b2</c> which represents a weighting factor towards second vector of 2d-triangle.</param>
    /// <param name="amount2">Barycentric scalar <c>b3</c> which represents a weighting factor towards third vector of 2d-triangle.</param>
    /// <returns>The cartesian translation of barycentric coordinates.</returns>
    public static Vector2i Barycentric(Vector2i value1, Vector2i value2, Vector2i value3, int amount1, int amount2)
    {
        return new Vector2i(
            (int)GameMath.Barycentric(value1.X, value2.X, value3.X, amount1, amount2),
            (int)GameMath.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2));
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains the cartesian coordinates of a vector specified in barycentric coordinates and relative to 2d-triangle.
    /// </summary>
    /// <param name="value1">The first vector of 2d-triangle.</param>
    /// <param name="value2">The second vector of 2d-triangle.</param>
    /// <param name="value3">The third vector of 2d-triangle.</param>
    /// <param name="amount1">Barycentric scalar <c>b2</c> which represents a weighting factor towards second vector of 2d-triangle.</param>
    /// <param name="amount2">Barycentric scalar <c>b3</c> which represents a weighting factor towards third vector of 2d-triangle.</param>
    /// <param name="result">The cartesian translation of barycentric coordinates as an output parameter.</param>
    public static void Barycentric(ref Vector2i value1, ref Vector2i value2, ref Vector2i value3, int amount1, int amount2, out Vector2i result)
    {
        result.X = (int)GameMath.Barycentric(value1.X, value2.X, value3.X, amount1, amount2);
        result.Y = (int)GameMath.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2);
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains CatmullRom interpolation of the specified vectors.
    /// </summary>
    /// <param name="value1">The first vector in interpolation.</param>
    /// <param name="value2">The second vector in interpolation.</param>
    /// <param name="value3">The third vector in interpolation.</param>
    /// <param name="value4">The fourth vector in interpolation.</param>
    /// <param name="amount">Weighting factor.</param>
    /// <returns>The result of CatmullRom interpolation.</returns>
    public static Vector2i CatmullRom(Vector2i value1, Vector2i value2, Vector2i value3, Vector2i value4, int amount)
    {
        return new Vector2i(
            (int)GameMath.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
            (int)GameMath.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount));
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains CatmullRom interpolation of the specified vectors.
    /// </summary>
    /// <param name="value1">The first vector in interpolation.</param>
    /// <param name="value2">The second vector in interpolation.</param>
    /// <param name="value3">The third vector in interpolation.</param>
    /// <param name="value4">The fourth vector in interpolation.</param>
    /// <param name="amount">Weighting factor.</param>
    /// <param name="result">The result of CatmullRom interpolation as an output parameter.</param>
    public static void CatmullRom(ref Vector2i value1, ref Vector2i value2, ref Vector2i value3, ref Vector2i value4, int amount, out Vector2i result)
    {
        result.X = (int)GameMath.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount);
        result.Y = (int)GameMath.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount);
    }

    /// <summary>
    /// Round the members of this <see cref="Vector2i"/> towards positive infinity.
    /// </summary>
    public void Ceiling()
    {
        X = (int)MathF.Ceiling(X);
        Y = (int)MathF.Ceiling(Y);
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains members from another vector rounded towards positive infinity.
    /// </summary>
    /// <param name="value">Source <see cref="Vector2i"/>.</param>
    /// <returns>The rounded <see cref="Vector2i"/>.</returns>
    public static Vector2i Ceiling(Vector2i value)
    {
        value.X = (int)MathF.Ceiling(value.X);
        value.Y = (int)MathF.Ceiling(value.Y);
        return value;
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains members from another vector rounded towards positive infinity.
    /// </summary>
    /// <param name="value">Source <see cref="Vector2i"/>.</param>
    /// <param name="result">The rounded <see cref="Vector2i"/>.</param>
    public static void Ceiling(ref Vector2i value, out Vector2i result)
    {
        result.X = (int)MathF.Ceiling(value.X);
        result.Y = (int)MathF.Ceiling(value.Y);
    }

    /// <summary>
    /// Clamps the specified value within a range.
    /// </summary>
    /// <param name="value1">The value to clamp.</param>
    /// <param name="min">The min value.</param>
    /// <param name="max">The max value.</param>
    /// <returns>The clamped value.</returns>
    public static Vector2i Clamp(Vector2i value1, Vector2i min, Vector2i max)
    {
        return new Vector2i(
            (int)GameMath.Clamp(value1.X, min.X, max.X),
            (int)GameMath.Clamp(value1.Y, min.Y, max.Y));
    }

    /// <summary>
    /// Clamps the specified value within a range.
    /// </summary>
    /// <param name="value1">The value to clamp.</param>
    /// <param name="min">The min value.</param>
    /// <param name="max">The max value.</param>
    /// <param name="result">The clamped value as an output parameter.</param>
    public static void Clamp(ref Vector2i value1, ref Vector2i min, ref Vector2i max, out Vector2i result)
    {
        result.X = (int)GameMath.Clamp(value1.X, min.X, max.X);
        result.Y = (int)GameMath.Clamp(value1.Y, min.Y, max.Y);
    }

    /// <summary>
    /// Returns the distance between two vectors.
    /// </summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <returns>The distance between two vectors.</returns>
    public static int Distance(Vector2i value1, Vector2i value2)
    {
        int v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
        return (int)MathF.Sqrt((v1 * v1) + (v2 * v2));
    }

    /// <summary>
    /// Returns the distance between two vectors.
    /// </summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <param name="result">The distance between two vectors as an output parameter.</param>
    public static void Distance(ref Vector2i value1, ref Vector2i value2, out int result)
    {
        int v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
        result = (int)MathF.Sqrt((v1 * v1) + (v2 * v2));
    }

    /// <summary>
    /// Returns the squared distance between two vectors.
    /// </summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <returns>The squared distance between two vectors.</returns>
    public static int DistanceSquared(Vector2i value1, Vector2i value2)
    {
        int v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
        return (v1 * v1) + (v2 * v2);
    }

    /// <summary>
    /// Returns the squared distance between two vectors.
    /// </summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <param name="result">The squared distance between two vectors as an output parameter.</param>
    public static void DistanceSquared(ref Vector2i value1, ref Vector2i value2, out int result)
    {
        int v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
        result = (v1 * v1) + (v2 * v2);
    }

    /// <summary>
    /// Divides the components of a <see cref="Vector2i"/> by the components of another <see cref="Vector2i"/>.
    /// </summary>
    /// <param name="value1">Source <see cref="Vector2i"/>.</param>
    /// <param name="value2">Divisor <see cref="Vector2i"/>.</param>
    /// <returns>The result of dividing the vectors.</returns>
    public static Vector2i Divide(Vector2i value1, Vector2i value2)
    {
        value1.X /= value2.X;
        value1.Y /= value2.Y;
        return value1;
    }

    /// <summary>
    /// Divides the components of a <see cref="Vector2i"/> by the components of another <see cref="Vector2i"/>.
    /// </summary>
    /// <param name="value1">Source <see cref="Vector2i"/>.</param>
    /// <param name="value2">Divisor <see cref="Vector2i"/>.</param>
    /// <param name="result">The result of dividing the vectors as an output parameter.</param>
    public static void Divide(ref Vector2i value1, ref Vector2i value2, out Vector2i result)
    {
        result.X = value1.X / value2.X;
        result.Y = value1.Y / value2.Y;
    }

    /// <summary>
    /// Divides the components of a <see cref="Vector2i"/> by a scalar.
    /// </summary>
    /// <param name="value1">Source <see cref="Vector2i"/>.</param>
    /// <param name="divider">Divisor scalar.</param>
    /// <returns>The result of dividing a vector by a scalar.</returns>
    public static Vector2i Divide(Vector2i value1, int divider)
    {
        int factor = 1 / divider;
        value1.X *= factor;
        value1.Y *= factor;
        return value1;
    }

    /// <summary>
    /// Divides the components of a <see cref="Vector2i"/> by a scalar.
    /// </summary>
    /// <param name="value1">Source <see cref="Vector2i"/>.</param>
    /// <param name="divider">Divisor scalar.</param>
    /// <param name="result">The result of dividing a vector by a scalar as an output parameter.</param>
    public static void Divide(ref Vector2i value1, int divider, out Vector2i result)
    {
        int factor = 1 / divider;
        result.X = value1.X * factor;
        result.Y = value1.Y * factor;
    }

    /// <summary>
    /// Returns a dot product of two vectors.
    /// </summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <returns>The dot product of two vectors.</returns>
    public static int Dot(Vector2i value1, Vector2i value2)
    {
        return (value1.X * value2.X) + (value1.Y * value2.Y);
    }

    /// <summary>
    /// Returns a dot product of two vectors.
    /// </summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <param name="result">The dot product of two vectors as an output parameter.</param>
    public static void Dot(ref Vector2i value1, ref Vector2i value2, out int result)
    {
        result = (value1.X * value2.X) + (value1.Y * value2.Y);
    }

    /// <summary>
    /// Compares whether current instance is equal to specified <see cref="Object"/>.
    /// </summary>
    /// <param name="obj">The <see cref="Object"/> to compare.</param>
    /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
    public override bool Equals(object obj)
    {
        if (obj is Vector2i)
        {
            return Equals((Vector2i)obj);
        }

        return false;
    }

    /// <summary>
    /// Compares whether current instance is equal to specified <see cref="Vector2i"/>.
    /// </summary>
    /// <param name="other">The <see cref="Vector2i"/> to compare.</param>
    /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
    public bool Equals(Vector2i other)
    {
        return (X == other.X) && (Y == other.Y);
    }

    /// <summary>
    /// Round the members of this <see cref="Vector2i"/> towards negative infinity.
    /// </summary>
    public void Floor()
    {
        X = (int)MathF.Floor(X);
        Y = (int)MathF.Floor(Y);
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains members from another vector rounded towards negative infinity.
    /// </summary>
    /// <param name="value">Source <see cref="Vector2i"/>.</param>
    /// <returns>The rounded <see cref="Vector2i"/>.</returns>
    public static Vector2i Floor(Vector2i value)
    {
        value.X = (int)MathF.Floor(value.X);
        value.Y = (int)MathF.Floor(value.Y);
        return value;
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains members from another vector rounded towards negative infinity.
    /// </summary>
    /// <param name="value">Source <see cref="Vector2i"/>.</param>
    /// <param name="result">The rounded <see cref="Vector2i"/>.</param>
    public static void Floor(ref Vector2i value, out Vector2i result)
    {
        result.X = (int)MathF.Floor(value.X);
        result.Y = (int)MathF.Floor(value.Y);
    }

    /// <summary>
    /// Gets the hash code of this <see cref="Vector2i"/>.
    /// </summary>
    /// <returns>Hash code of this <see cref="Vector2i"/>.</returns>
    public override int GetHashCode()
    {
        unchecked
        {
            return (X.GetHashCode() * 397) ^ Y.GetHashCode();
        }
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains hermite spline interpolation.
    /// </summary>
    /// <param name="value1">The first position vector.</param>
    /// <param name="tangent1">The first tangent vector.</param>
    /// <param name="value2">The second position vector.</param>
    /// <param name="tangent2">The second tangent vector.</param>
    /// <param name="amount">Weighting factor.</param>
    /// <returns>The hermite spline interpolation vector.</returns>
    public static Vector2i Hermite(Vector2i value1, Vector2i tangent1, Vector2i value2, Vector2i tangent2, int amount)
    {
        return new Vector2i((int)GameMath.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount), (int)GameMath.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount));
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains hermite spline interpolation.
    /// </summary>
    /// <param name="value1">The first position vector.</param>
    /// <param name="tangent1">The first tangent vector.</param>
    /// <param name="value2">The second position vector.</param>
    /// <param name="tangent2">The second tangent vector.</param>
    /// <param name="amount">Weighting factor.</param>
    /// <param name="result">The hermite spline interpolation vector as an output parameter.</param>
    public static void Hermite(ref Vector2i value1, ref Vector2i tangent1, ref Vector2i value2, ref Vector2i tangent2, int amount, out Vector2i result)
    {
        result.X = (int)GameMath.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount);
        result.Y = (int)GameMath.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount);
    }

    /// <summary>
    /// Returns the length of this <see cref="Vector2i"/>.
    /// </summary>
    /// <returns>The length of this <see cref="Vector2i"/>.</returns>
    public int Length()
    {
        return (int)MathF.Sqrt((X * X) + (Y * Y));
    }

    /// <summary>
    /// Returns the squared length of this <see cref="Vector2i"/>.
    /// </summary>
    /// <returns>The squared length of this <see cref="Vector2i"/>.</returns>
    public int LengthSquared()
    {
        return (X * X) + (Y * Y);
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains linear interpolation of the specified vectors.
    /// </summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
    /// <returns>The result of linear interpolation of the specified vectors.</returns>
    public static Vector2i Lerp(Vector2i value1, Vector2i value2, int amount)
    {
        return new Vector2i(
            (int)GameMath.Lerp(value1.X, value2.X, amount),
            (int)GameMath.Lerp(value1.Y, value2.Y, amount));
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains linear interpolation of the specified vectors.
    /// </summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
    /// <param name="result">The result of linear interpolation of the specified vectors as an output parameter.</param>
    public static void Lerp(ref Vector2i value1, ref Vector2i value2, int amount, out Vector2i result)
    {
        result.X = (int)GameMath.Lerp(value1.X, value2.X, amount);
        result.Y = (int)GameMath.Lerp(value1.Y, value2.Y, amount);
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains linear interpolation of the specified vectors.
    /// Uses <see cref="GameMath.LerpPrecise"/> on GameMath for the interpolation.
    /// Less efficient but more precise compared to <see cref="Vector2i.Lerp(Vector2i, Vector2i, int)"/>.
    /// See remarks section of <see cref="GameMath.LerpPrecise"/> on GameMath for more info.
    /// </summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
    /// <returns>The result of linear interpolation of the specified vectors.</returns>
    public static Vector2i LerpPrecise(Vector2i value1, Vector2i value2, int amount)
    {
        return new Vector2i(
            (int)GameMath.LerpPrecise(value1.X, value2.X, amount),
            (int)GameMath.LerpPrecise(value1.Y, value2.Y, amount));
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains linear interpolation of the specified vectors.
    /// Uses <see cref="GameMath.LerpPrecise"/> on GameMath for the interpolation.
    /// Less efficient but more precise compared to <see cref="Vector2i.Lerp(ref Vector2i, ref Vector2i, int, out Vector2i)"/>.
    /// See remarks section of <see cref="GameMath.LerpPrecise"/> on GameMath for more info.
    /// </summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
    /// <param name="result">The result of linear interpolation of the specified vectors as an output parameter.</param>
    public static void LerpPrecise(ref Vector2i value1, ref Vector2i value2, int amount, out Vector2i result)
    { 
        result.X = (int)GameMath.LerpPrecise(value1.X, value2.X, amount);
        result.Y = (int)GameMath.LerpPrecise(value1.Y, value2.Y, amount);
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains a maximal values from the two vectors.
    /// </summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <returns>The <see cref="Vector2i"/> with maximal values from the two vectors.</returns>
    public static Vector2i Max(Vector2i value1, Vector2i value2)
    {
        return new Vector2i(value1.X > value2.X ? value1.X : value2.X,
                           value1.Y > value2.Y ? value1.Y : value2.Y);
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains a maximal values from the two vectors.
    /// </summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <param name="result">The <see cref="Vector2i"/> with maximal values from the two vectors as an output parameter.</param>
    public static void Max(ref Vector2i value1, ref Vector2i value2, out Vector2i result)
    {
        result.X = value1.X > value2.X ? value1.X : value2.X;
        result.Y = value1.Y > value2.Y ? value1.Y : value2.Y;
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains a minimal values from the two vectors.
    /// </summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <returns>The <see cref="Vector2i"/> with minimal values from the two vectors.</returns>
    public static Vector2i Min(Vector2i value1, Vector2i value2)
    {
        return new Vector2i(value1.X < value2.X ? value1.X : value2.X,
                           value1.Y < value2.Y ? value1.Y : value2.Y);
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains a minimal values from the two vectors.
    /// </summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <param name="result">The <see cref="Vector2i"/> with minimal values from the two vectors as an output parameter.</param>
    public static void Min(ref Vector2i value1, ref Vector2i value2, out Vector2i result)
    {
        result.X = value1.X < value2.X ? value1.X : value2.X;
        result.Y = value1.Y < value2.Y ? value1.Y : value2.Y;
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains a multiplication of two vectors.
    /// </summary>
    /// <param name="value1">Source <see cref="Vector2i"/>.</param>
    /// <param name="value2">Source <see cref="Vector2i"/>.</param>
    /// <returns>The result of the vector multiplication.</returns>
    public static Vector2i Multiply(Vector2i value1, Vector2i value2)
    {
        value1.X *= value2.X;
        value1.Y *= value2.Y;
        return value1;
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains a multiplication of two vectors.
    /// </summary>
    /// <param name="value1">Source <see cref="Vector2i"/>.</param>
    /// <param name="value2">Source <see cref="Vector2i"/>.</param>
    /// <param name="result">The result of the vector multiplication as an output parameter.</param>
    public static void Multiply(ref Vector2i value1, ref Vector2i value2, out Vector2i result)
    {
        result.X = value1.X * value2.X;
        result.Y = value1.Y * value2.Y;
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains a multiplication of <see cref="Vector2i"/> and a scalar.
    /// </summary>
    /// <param name="value1">Source <see cref="Vector2i"/>.</param>
    /// <param name="scaleFactor">Scalar value.</param>
    /// <returns>The result of the vector multiplication with a scalar.</returns>
    public static Vector2i Multiply(Vector2i value1, int scaleFactor)
    {
        value1.X *= scaleFactor;
        value1.Y *= scaleFactor;
        return value1;
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains a multiplication of <see cref="Vector2i"/> and a scalar.
    /// </summary>
    /// <param name="value1">Source <see cref="Vector2i"/>.</param>
    /// <param name="scaleFactor">Scalar value.</param>
    /// <param name="result">The result of the multiplication with a scalar as an output parameter.</param>
    public static void Multiply(ref Vector2i value1, int scaleFactor, out Vector2i result)
    {
        result.X = value1.X * scaleFactor;
        result.Y = value1.Y * scaleFactor;
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains the specified vector inversion.
    /// </summary>
    /// <param name="value">Source <see cref="Vector2i"/>.</param>
    /// <returns>The result of the vector inversion.</returns>
    public static Vector2i Negate(Vector2i value)
    {
        value.X = -value.X;
        value.Y = -value.Y;
        return value;
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains the specified vector inversion.
    /// </summary>
    /// <param name="value">Source <see cref="Vector2i"/>.</param>
    /// <param name="result">The result of the vector inversion as an output parameter.</param>
    public static void Negate(ref Vector2i value, out Vector2i result)
    {
        result.X = -value.X;
        result.Y = -value.Y;
    }

    /// <summary>
    /// Turns this <see cref="Vector2i"/> to a unit vector with the same direction.
    /// </summary>
    public void Normalize()
    {
        int val = 1 / (int)MathF.Sqrt((X * X) + (Y * Y));
        X *= val;
        Y *= val;
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains a normalized values from another vector.
    /// </summary>
    /// <param name="value">Source <see cref="Vector2i"/>.</param>
    /// <returns>Unit vector.</returns>
    public static Vector2i Normalize(Vector2i value)
    {
        int val = 1 / (int)MathF.Sqrt((value.X * value.X) + (value.Y * value.Y));
        value.X *= val;
        value.Y *= val;
        return value;
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains a normalized values from another vector.
    /// </summary>
    /// <param name="value">Source <see cref="Vector2i"/>.</param>
    /// <param name="result">Unit vector as an output parameter.</param>
    public static void Normalize(ref Vector2i value, out Vector2i result)
    {
        int val = 1 / (int)MathF.Sqrt((value.X * value.X) + (value.Y * value.Y));
        result.X = value.X * val;
        result.Y = value.Y * val;
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains reflect vector of the given vector and normal.
    /// </summary>
    /// <param name="vector">Source <see cref="Vector2i"/>.</param>
    /// <param name="normal">Reflection normal.</param>
    /// <returns>Reflected vector.</returns>
    public static Vector2i Reflect(Vector2i vector, Vector2i normal)
    {
        Vector2i result;
        int val = 2 * ((vector.X * normal.X) + (vector.Y * normal.Y));
        result.X = vector.X - (normal.X * val);
        result.Y = vector.Y - (normal.Y * val);
        return result;
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains reflect vector of the given vector and normal.
    /// </summary>
    /// <param name="vector">Source <see cref="Vector2i"/>.</param>
    /// <param name="normal">Reflection normal.</param>
    /// <param name="result">Reflected vector as an output parameter.</param>
    public static void Reflect(ref Vector2i vector, ref Vector2i normal, out Vector2i result)
    {
        int val = 2 * ((vector.X * normal.X) + (vector.Y * normal.Y));
        result.X = vector.X - (normal.X * val);
        result.Y = vector.Y - (normal.Y * val);
    }

    /// <summary>
    /// Round the members of this <see cref="Vector2i"/> to the nearest integer value.
    /// </summary>
    public void Round()
    {
        X = (int)MathF.Round(X);
        Y = (int)MathF.Round(Y);
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains members from another vector rounded to the nearest integer value.
    /// </summary>
    /// <param name="value">Source <see cref="Vector2i"/>.</param>
    /// <returns>The rounded <see cref="Vector2i"/>.</returns>
    public static Vector2i Round(Vector2i value)
    {
        value.X = (int)MathF.Round(value.X);
        value.Y = (int)MathF.Round(value.Y);
        return value;
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains members from another vector rounded to the nearest integer value.
    /// </summary>
    /// <param name="value">Source <see cref="Vector2i"/>.</param>
    /// <param name="result">The rounded <see cref="Vector2i"/>.</param>
    public static void Round(ref Vector2i value, out Vector2i result)
    {
        result.X = (int)MathF.Round(value.X);
        result.Y = (int)MathF.Round(value.Y);
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains cubic interpolation of the specified vectors.
    /// </summary>
    /// <param name="value1">Source <see cref="Vector2i"/>.</param>
    /// <param name="value2">Source <see cref="Vector2i"/>.</param>
    /// <param name="amount">Weighting value.</param>
    /// <returns>Cubic interpolation of the specified vectors.</returns>
    public static Vector2i SmoothStep(Vector2i value1, Vector2i value2, int amount)
    {
        return new Vector2i(
            (int)GameMath.SmoothStep(value1.X, value2.X, amount),
            (int)GameMath.SmoothStep(value1.Y, value2.Y, amount));
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains cubic interpolation of the specified vectors.
    /// </summary>
    /// <param name="value1">Source <see cref="Vector2i"/>.</param>
    /// <param name="value2">Source <see cref="Vector2i"/>.</param>
    /// <param name="amount">Weighting value.</param>
    /// <param name="result">Cubic interpolation of the specified vectors as an output parameter.</param>
    public static void SmoothStep(ref Vector2i value1, ref Vector2i value2, int amount, out Vector2i result)
    {
        result.X = (int)GameMath.SmoothStep(value1.X, value2.X, amount);
        result.Y = (int)GameMath.SmoothStep(value1.Y, value2.Y, amount);
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains subtraction of on <see cref="Vector2i"/> from a another.
    /// </summary>
    /// <param name="value1">Source <see cref="Vector2i"/>.</param>
    /// <param name="value2">Source <see cref="Vector2i"/>.</param>
    /// <returns>The result of the vector subtraction.</returns>
    public static Vector2i Subtract(Vector2i value1, Vector2i value2)
    {
        value1.X -= value2.X;
        value1.Y -= value2.Y;
        return value1;
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains subtraction of on <see cref="Vector2i"/> from a another.
    /// </summary>
    /// <param name="value1">Source <see cref="Vector2i"/>.</param>
    /// <param name="value2">Source <see cref="Vector2i"/>.</param>
    /// <param name="result">The result of the vector subtraction as an output parameter.</param>
    public static void Subtract(ref Vector2i value1, ref Vector2i value2, out Vector2i result)
    {
        result.X = value1.X - value2.X;
        result.Y = value1.Y - value2.Y;
    }

    /// <summary>
    /// Returns a <see cref="String"/> representation of this <see cref="Vector2i"/> in the format:
    /// {X:[<see cref="X"/>] Y:[<see cref="Y"/>]}
    /// </summary>
    /// <returns>A <see cref="String"/> representation of this <see cref="Vector2i"/>.</returns>
    public override string ToString()
    {
        return "{X:" + X + " Y:" + Y + "}";
    }

    /// <summary>
    /// Gets a <see cref="Point"/> representation for this object.
    /// </summary>
    /// <returns>A <see cref="Point"/> representation for this object.</returns>
    public Point ToPoint()
    {
        return new Point((int) X,(int) Y);
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains a transformation of 2d-vector by the specified <see cref="Matrix"/>.
    /// </summary>
    /// <param name="position">Source <see cref="Vector2i"/>.</param>
    /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
    /// <returns>Transformed <see cref="Vector2i"/>.</returns>
    public static Vector2i Transform(Vector2i position, Matrix4x4 matrix)
    {
        return new Vector2i((position.X * (int)matrix.M11) + (position.Y * (int)matrix.M21) + (int)matrix.M41, (position.X * (int)matrix.M12) + (position.Y * (int)matrix.M22) + (int)matrix.M42);
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains a transformation of 2d-vector by the specified <see cref="Matrix"/>.
    /// </summary>
    /// <param name="position">Source <see cref="Vector2i"/>.</param>
    /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
    /// <param name="result">Transformed <see cref="Vector2i"/> as an output parameter.</param>
    public static void Transform(ref Vector2i position, ref Matrix4x4 matrix, out Vector2i result)
    {
        var x = (position.X * matrix.M11) + (position.Y * matrix.M21) + matrix.M41;
        var y = (position.X * matrix.M12) + (position.Y * matrix.M22) + matrix.M42;
        result.X = (int)x;
        result.Y = (int)y;
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains a transformation of 2d-vector by the specified <see cref="Quaternion"/>, representing the rotation.
    /// </summary>
    /// <param name="value">Source <see cref="Vector2i"/>.</param>
    /// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
    /// <returns>Transformed <see cref="Vector2i"/>.</returns>
    public static Vector2i Transform(Vector2i value, Quaternion rotation)
    {
        Transform(ref value, ref rotation, out value);
        return value;
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains a transformation of 2d-vector by the specified <see cref="Quaternion"/>, representing the rotation.
    /// </summary>
    /// <param name="value">Source <see cref="Vector2i"/>.</param>
    /// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
    /// <param name="result">Transformed <see cref="Vector2i"/> as an output parameter.</param>
    public static void Transform(ref Vector2i value, ref Quaternion rotation, out Vector2i result)
    {
        var rot1 = new Vector3(rotation.X + rotation.X, rotation.Y + rotation.Y, rotation.Z + rotation.Z);
        var rot2 = new Vector3(rotation.X, rotation.X, rotation.W);
        var rot3 = new Vector3(1, rotation.Y, rotation.Z);
        var rot4 = rot1*rot2;
        var rot5 = rot1*rot3;

        var v = new Vector2i();
        v.X = (int)((double)value.X * (1.0 - (double)rot5.Y - (double)rot5.Z) + (double)value.Y * ((double)rot4.Y - (double)rot4.Z));
        v.Y = (int)((double)value.X * ((double)rot4.Y + (double)rot4.Z) + (double)value.Y * (1.0 - (double)rot4.X - (double)rot5.Z));
        result.X = v.X;
        result.Y = v.Y;
    }

    /// <summary>
    /// Apply transformation on vectors within array of <see cref="Vector2i"/> by the specified <see cref="Matrix"/> and places the results in an another array.
    /// </summary>
    /// <param name="sourceArray">Source array.</param>
    /// <param name="sourceIndex">The starting index of transformation in the source array.</param>
    /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
    /// <param name="destinationArray">Destination array.</param>
    /// <param name="destinationIndex">The starting index in the destination array, where the first <see cref="Vector2i"/> should be written.</param>
    /// <param name="length">The number of vectors to be transformed.</param>
    public static void Transform(
        Vector2i[] sourceArray,
        int sourceIndex,
        ref Matrix4x4 matrix,
        Vector2i[] destinationArray,
        int destinationIndex,
        int length)
    {
        if (sourceArray == null)
            throw new ArgumentNullException("sourceArray");
        if (destinationArray == null)
            throw new ArgumentNullException("destinationArray");
        if (sourceArray.Length < sourceIndex + length)
            throw new ArgumentException("Source array length is lesser than sourceIndex + length");
        if (destinationArray.Length < destinationIndex + length)
            throw new ArgumentException("Destination array length is lesser than destinationIndex + length");

        for (int x = 0; x < length; x++)
        {
            var position = sourceArray[sourceIndex + x];
            var destination = destinationArray[destinationIndex + x];
            destination.X = (position.X * (int)matrix.M11) + (position.Y * (int)matrix.M21) + (int)matrix.M41;
            destination.Y = (position.X * (int)matrix.M12) + (position.Y * (int)matrix.M22) + (int)matrix.M42;
            destinationArray[destinationIndex + x] = destination;
        }
    }

    /// <summary>
    /// Apply transformation on vectors within array of <see cref="Vector2i"/> by the specified <see cref="Quaternion"/> and places the results in an another array.
    /// </summary>
    /// <param name="sourceArray">Source array.</param>
    /// <param name="sourceIndex">The starting index of transformation in the source array.</param>
    /// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
    /// <param name="destinationArray">Destination array.</param>
    /// <param name="destinationIndex">The starting index in the destination array, where the first <see cref="Vector2i"/> should be written.</param>
    /// <param name="length">The number of vectors to be transformed.</param>
    public static void Transform
    (
        Vector2i[] sourceArray,
        int sourceIndex,
        ref Quaternion rotation,
        Vector2i[] destinationArray,
        int destinationIndex,
        int length
    )
    {
        if (sourceArray == null)
            throw new ArgumentNullException("sourceArray");
        if (destinationArray == null)
            throw new ArgumentNullException("destinationArray");
        if (sourceArray.Length < sourceIndex + length)
            throw new ArgumentException("Source array length is lesser than sourceIndex + length");
        if (destinationArray.Length < destinationIndex + length)
            throw new ArgumentException("Destination array length is lesser than destinationIndex + length");

        for (int x = 0; x < length; x++)
        {
            var position = sourceArray[sourceIndex + x];
            var destination = destinationArray[destinationIndex + x];

            Vector2i v;
            Transform(ref position,ref rotation,out v); 

            destination.X = v.X;
            destination.Y = v.Y;

            destinationArray[destinationIndex + x] = destination;
        }
    }

    /// <summary>
    /// Apply transformation on all vectors within array of <see cref="Vector2i"/> by the specified <see cref="Matrix"/> and places the results in an another array.
    /// </summary>
    /// <param name="sourceArray">Source array.</param>
    /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
    /// <param name="destinationArray">Destination array.</param>
    public static void Transform(
        Vector2i[] sourceArray,
        ref Matrix4x4 matrix,
        Vector2i[] destinationArray)
    {
        Transform(sourceArray, 0, ref matrix, destinationArray, 0, sourceArray.Length);
    }

    /// <summary>
    /// Apply transformation on all vectors within array of <see cref="Vector2i"/> by the specified <see cref="Quaternion"/> and places the results in an another array.
    /// </summary>
    /// <param name="sourceArray">Source array.</param>
    /// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
    /// <param name="destinationArray">Destination array.</param>
    public static void Transform
    (
        Vector2i[] sourceArray,
        ref Quaternion rotation,
        Vector2i[] destinationArray
    )
    {
        Transform(sourceArray, 0, ref rotation, destinationArray, 0, sourceArray.Length);
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains a transformation of the specified normal by the specified <see cref="Matrix"/>.
    /// </summary>
    /// <param name="normal">Source <see cref="Vector2i"/> which represents a normal vector.</param>
    /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
    /// <returns>Transformed normal.</returns>
    public static Vector2i TransformNormal(Vector2i normal, Matrix4x4 matrix)
    {
        return new Vector2i((normal.X * (int)matrix.M11) + (normal.Y * (int)matrix.M21),(normal.X * (int)matrix.M12) + (normal.Y * (int)matrix.M22));
    }

    /// <summary>
    /// Creates a new <see cref="Vector2i"/> that contains a transformation of the specified normal by the specified <see cref="Matrix"/>.
    /// </summary>
    /// <param name="normal">Source <see cref="Vector2i"/> which represents a normal vector.</param>
    /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
    /// <param name="result">Transformed normal as an output parameter.</param>
    public static void TransformNormal(ref Vector2i normal, ref Matrix4x4 matrix, out Vector2i result)
    {
        var x = (normal.X * matrix.M11) + (normal.Y * matrix.M21);
        var y = (normal.X * matrix.M12) + (normal.Y * matrix.M22);
        result.X = (int)x;
        result.Y = (int)y;
    }

    /// <summary>
    /// Apply transformation on normals within array of <see cref="Vector2i"/> by the specified <see cref="Matrix"/> and places the results in an another array.
    /// </summary>
    /// <param name="sourceArray">Source array.</param>
    /// <param name="sourceIndex">The starting index of transformation in the source array.</param>
    /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
    /// <param name="destinationArray">Destination array.</param>
    /// <param name="destinationIndex">The starting index in the destination array, where the first <see cref="Vector2i"/> should be written.</param>
    /// <param name="length">The number of normals to be transformed.</param>
    public static void TransformNormal
    (
        Vector2i[] sourceArray,
        int sourceIndex,
        ref Matrix4x4 matrix,
        Vector2i[] destinationArray,
        int destinationIndex,
        int length
    )
    {
        if (sourceArray == null)
            throw new ArgumentNullException("sourceArray");
        if (destinationArray == null)
            throw new ArgumentNullException("destinationArray");
        if (sourceArray.Length < sourceIndex + length)
            throw new ArgumentException("Source array length is lesser than sourceIndex + length");
        if (destinationArray.Length < destinationIndex + length)
            throw new ArgumentException("Destination array length is lesser than destinationIndex + length");

        for (int i = 0; i < length; i++)
        {
            var normal = sourceArray[sourceIndex + i];

            destinationArray[destinationIndex + i] = new Vector2i((normal.X * (int)matrix.M11) + (normal.Y * (int)matrix.M21),
                                                                 (normal.X * (int)matrix.M12) + (normal.Y * (int)matrix.M22));
        }
    }

    /// <summary>
    /// Apply transformation on all normals within array of <see cref="Vector2i"/> by the specified <see cref="Matrix"/> and places the results in an another array.
    /// </summary>
    /// <param name="sourceArray">Source array.</param>
    /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
    /// <param name="destinationArray">Destination array.</param>
    public static void TransformNormal
        (
        Vector2i[] sourceArray,
        ref Matrix4x4 matrix,
        Vector2i[] destinationArray
        )
    {
        if (sourceArray == null)
            throw new ArgumentNullException("sourceArray");
        if (destinationArray == null)
            throw new ArgumentNullException("destinationArray");
        if (destinationArray.Length < sourceArray.Length)
            throw new ArgumentException("Destination array length is lesser than source array length");

        for (int i = 0; i < sourceArray.Length; i++)
        {
            var normal = sourceArray[i];

            destinationArray[i] = new Vector2i((normal.X * (int)matrix.M11) + (normal.Y * (int)matrix.M21),
                                              (normal.X * (int)matrix.M12) + (normal.Y * (int)matrix.M22));
        }
    }

    /// <summary>
    /// Deconstruction method for <see cref="Vector2i"/>.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void Deconstruct(out int x, out int y)
    {
        x = X;
        y = Y;
    }

    /// <summary>
    /// Returns a <see cref="System.Numerics.Vector2i"/>.
    /// </summary>
    public System.Numerics.Vector2 ToNumerics()
    {
        return new System.Numerics.Vector2(this.X, this.Y);
    }

    #endregion
}