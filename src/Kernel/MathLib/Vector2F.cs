using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Kernel.MathLib;

/// <summary>
/// Represents a two-dimensional vector with single-precision floating-point components.
/// Provides common mathematical operations for 2D vectors used in game engine calculations.
/// </summary>
public struct Vector2F : IEquatable<Vector2F>
{
    /// <summary>
    /// Gets or sets the X component of the vector.
    /// </summary>
    public float X
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set;
    }
    
    /// <summary>
    /// Gets or sets the Y component of the vector.
    /// </summary>
    public float Y
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set;
    }
    
    /// <summary>
    /// A static readonly vector with both components set to 1.
    /// </summary>
    public static Vector2F One { get; } = new(1f, 1f);

    /// <summary>
    /// A static readonly vector with both components set to 0.
    /// </summary>
    public static Vector2F Zero { get; } = new(0f, 0f);

    /// <summary>
    /// A static readonly unit vector along the X axis (1, 0).
    /// </summary>
    public static Vector2F UnitX { get; } = new(1f, 0f);

    /// <summary>
    /// A static readonly unit vector along the Y axis (0, 1).
    /// </summary>
    public static Vector2F UnitY { get; } = new(0f, 1f);
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Vector2F"/> struct with zero components.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F() : this(0f, 0f)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Vector2F"/> struct with specified X and Y values.
    /// Validates that neither component is NaN or Infinity.
    /// </summary>
    /// <param name="x">The X component.</param>
    /// <param name="y">The Y component.</param>
    /// <exception cref="ArgumentException">Thrown when any component is NaN or Infinity.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F(float x, float y)
    {
#if DEBUG
        if (float.IsNaN(x) || float.IsNaN(y))
            throw new ArgumentException("Vector2F cannot have NaN components");
        
        if (float.IsInfinity(x) || float.IsInfinity(y))
            throw new ArgumentException("Vector2F cannot have Infinity components");
#endif
        
        this.X = x;
        this.Y = y;
    }
    
    /// <summary>
    /// Gets the Euclidean length (magnitude) of the vector.
    /// </summary>
    public float Length => (float)Math.Sqrt(LengthSquared);

    /// <summary>
    /// Gets the squared Euclidean length of the vector (avoids square root for performance).
    /// </summary>
    public float LengthSquared => X * X + Y * Y;
    
    /// <summary>
    /// Gets the aspect ratio (X / Y). Useful for screen-space calculations.
    /// </summary>
    public float Aspect => Y == 0 ? float.PositiveInfinity : X / Y;
    
    /// <summary>
    /// Determines whether two vectors are equal by comparing their components exactly.
    /// </summary>
    public static bool operator ==(Vector2F a, Vector2F b) => a.X == b.X && a.Y == b.Y;

    /// <summary>
    /// Determines whether two vectors are not equal.
    /// </summary>
    public static bool operator !=(Vector2F a, Vector2F b) => !(a == b);
    
    /// <summary>
    /// Checks if this vector is approximately equal to another vector within an epsilon tolerance.
    /// </summary>
    /// <param name="other">The other vector to compare.</param>
    /// <returns>True if the absolute difference in each component is less than <see cref="Constant.Epsilon"/>.</returns>
    public bool AlmostEquals(Vector2F other) => 
        Math.Abs(X - other.X) < Constant.Epsilon && 
        Math.Abs(Y - other.Y) < Constant.Epsilon;
    
    /// <summary>
    /// Adds two vectors component-wise.
    /// </summary>
    public static Vector2F operator +(Vector2F the, Vector2F other) 
        => new Vector2F(the.X + other.X, the.Y + other.Y);
    
    /// <summary>
    /// Subtracts one vector from another component-wise.
    /// </summary>
    public static Vector2F operator -(Vector2F the, Vector2F other) 
        => new Vector2F(the.X - other.X, the.Y - other.Y);
    
    public static Vector2F operator -(Vector2F the) 
        => new Vector2F(-the.X, -the.Y);
    
    /// <summary>
    /// Multiplies a vector by a scalar (scales all components).
    /// </summary>
    public static Vector2F operator *(Vector2F the, float scalar) 
        => new Vector2F(scalar * the.X, scalar * the.Y);
    
    
    /// <summary>
    /// Multiplies a vector by a scalar (scales all components).
    /// </summary>
    public static Vector2F operator *(float scalar, Vector2F the) 
        => new Vector2F(scalar * the.X, scalar * the.Y);

    /// <summary>
    /// Converts this vector to an angle (in radians) using Atan2(Y, X).
    /// The resulting angle is measured counterclockwise from the positive X axis.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float ToAngle() 
        => (float)Math.Atan2(Y, X);
    
    /// <summary>
    /// Creates a unit vector from an angle (in radians).
    /// This is an instance method but returns a new vector based on the given angle (does not use current state).
    /// </summary>
    /// <param name="angle">Angle in radians.</param>
    /// <returns>A unit vector pointing in the direction of the angle.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2F FromAngle(float angle) 
        => new Vector2F((float)Math.Cos(angle), (float)Math.Sin(angle));
    
    /// <summary>
    /// Returns a new vector with the absolute values of each component.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F Abs() => new Vector2F(Math.Abs(X), Math.Abs(Y));
    
    /// <summary>
    /// Returns a new vector where each component is the minimum of this vector's component and the corresponding component of the other vector.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F Min(in Vector2F other) =>  new Vector2F(Math.Min(X, other.X), Math.Min(Y, other.Y));
    
    /// <summary>
    /// Clamps each component of the given vector to be no greater than the specified minimum value.
    /// </summary>
    /// <param name="vector">Input vector.</param>
    /// <param name="min">The maximum allowed value for each component (acts as an upper clamp).</param>
    /// <returns>A vector with each component clamped to ≤ min.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public  static Vector2F ClampMinComponents(in Vector2F vector, float min) 
        => new Vector2F(
            Math.Min(vector.X, min),
            Math.Min(vector.Y, min)
        );
    
    /// <summary>
    /// Instance version of <see cref="ClampMinComponents(in Vector2F, float)"/>.
    /// Clamps each component of this vector to be no greater than the corresponding component of the given min vector.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F ClampMinComponents(in Vector2F min) 
        => new Vector2F(
            Math.Min(X, min.X),
            Math.Min(Y, min.Y)
        ); 
    
    /// <summary>
    /// Returns a new vector where each component is the maximum of this vector's component and the corresponding component of the other vector.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F Max(in Vector2F other) => new Vector2F(Math.Max(X, other.X), Math.Max(Y, other.Y));
    
    /// <summary>
    /// Clamps each component of this vector to be no less than the specified maximum value.
    /// </summary>
    /// <param name="max">The minimum allowed value for each component (acts as a lower clamp).</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F ClampMaxComponents(float max)
        => new Vector2F(
            Math.Max(X, max),
            Math.Max(Y, max)
        );
    
    /// <summary>
    /// Static version of <see cref="ClampMaxComponents(float)"/>.
    /// Clamps each component of the given vector to be no less than the specified max value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2F ClampMaxComponents(in Vector2F vector, float max) 
        => new Vector2F(
            Math.Max(vector.X, max),
            Math.Max(vector.Y, max)
        );
    
    /// <summary>
    /// Computes the Euclidean distance from this vector to another vector.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float DistanceTo(in Vector2F other)
        => (float)Math.Sqrt(DistanceSquaredTo(other));
    
    /// <summary>
    /// Computes the squared Euclidean distance from this vector to another vector (avoids square root).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float DistanceSquaredTo(in Vector2F other)
        => (X - other.X) * (X - other.X) + (Y - other.Y) * (Y - other.Y);
    
    /// <summary>
    /// Computes the dot product of this vector and another vector.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float Dot(in Vector2F other) 
        => X * other.X + Y * other.Y;
    
    /// <summary>
    /// Computes the 2D cross product (scalar) of this vector and another vector.
    /// The result is X*other.Y - Y*other.X, representing the signed area of the parallelogram.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float Cross(in Vector2F other)
        => X * other.Y - Y * other.X;
    
    /// <summary>
    /// Returns a new vector where each component is the sign of the original component (-1, 0, or 1).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F Sign() 
        => new Vector2F(Math.Sign(X), Math.Sign(Y));
    
    /// <summary>
    /// Returns a new vector where each component is floored to the nearest integer toward negative infinity.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F Floor() 
        => new Vector2F((float)Math.Floor(X), (float)Math.Floor(Y));

    /// <summary>
    /// Returns a new vector where each component is ceiled to the nearest integer toward positive infinity.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F Ceil() 
        => new Vector2F((float)Math.Ceiling(X), (float)Math.Ceiling(Y));

    /// <summary>
    /// Returns a new vector where each component is rounded to the nearest integer using "AwayFromZero" midpoint rounding.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F Round() 
        => new Vector2F(
            (float)Math.Round(X, MidpointRounding.AwayFromZero), 
            (float)Math.Round(Y, MidpointRounding.AwayFromZero)
        );
    
    /// <summary>
    /// Rotates this vector by the specified angle (in radians) around the origin.
    /// Uses standard 2D rotation matrix: [cos -sin; sin cos].
    /// </summary>
    /// <param name="angle">Rotation angle in radians.</param>
    /// <returns>The rotated vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F Rotate(float angle) 
    {
        float cos = (float)Math.Cos(angle);
        float sin = (float)Math.Sin(angle);
        return new Vector2F(
            X * cos - Y * sin,
            X * sin + Y * cos
        );
    }
    
    // Commented-out perpendicular method (left for reference):
    // public Vector2F Perpendicular() 
    //     => new Vector2F(-Y, X);
    
    /// <summary>
    /// Determines whether this vector is approximately equal to another vector,
    /// using an approximate equality check per component.
    /// </summary>
    /// <param name="other">The other vector to compare.</param>
    /// <returns>True if both X and Y components are approximately equal; otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEqualApproximate(Vector2F other)
        => Utils.IsEqualApproximate(X, other.X) && Utils.IsEqualApproximate(Y, other.Y);

    /// <summary>
    /// Determines whether this vector is exactly equal to another vector,
    /// using an exact (bitwise or tolerance‑based) comparison per component.
    /// </summary>
    /// <param name="other">The other vector to compare.</param>
    /// <returns>True if both X and Y components are considered identical; otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsSame(Vector2F other)
        => Utils.IsSame(X, other.X) && Utils.IsSame(Y, other.Y);

    /// <summary>
    /// Checks whether this vector is approximately the zero vector.
    /// </summary>
    /// <returns>True if both X and Y components are approximately zero; otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZeroApproximate()
        => Utils.IsZeroApproximate(X) && Utils.IsZeroApproximate(Y);

    /// <summary>
    /// Computes the reflection of this vector about a given normal vector.
    /// Formula: reflect = 2 * (dot(normal, this)) * normal - this
    /// </summary>
    /// <param name="other">The normal vector used as the mirror axis.</param>
    /// <returns>The reflected vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F Reflect(in Vector2F other)
        => 2.0f * other.Dot(this) * other - this;

    /// <summary>
    /// Computes the bounce direction by negating the reflection of this vector
    /// about the given normal. Equivalent to -Reflect(other).
    /// </summary>
    /// <param name="other">The normal vector.</param>
    /// <returns>The bounced (inverted reflection) vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F Bounce(in Vector2F other)
        => -Reflect(other);

    /// <summary>
    /// Projects this vector onto a plane defined by the given normal,
    /// effectively sliding it along the surface.
    /// Formula: slide = this - other * Dot(other)
    /// </summary>
    /// <param name="other">The normal vector of the surface.</param>
    /// <returns>The component of this vector parallel to the surface.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F Slide(in Vector2F other)
        => this - other * Dot(other);

    /// <summary>
    /// Clamps each component of this vector between the corresponding components
    /// of two other vectors.
    /// </summary>
    /// <param name="min">The minimum bounds vector.</param>
    /// <param name="max">The maximum bounds vector.</param>
    /// <returns>A new vector with X clamped to [min.X, max.X] and Y clamped to [min.Y, max.Y].</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F Clamp(in Vector2F min, in Vector2F max)
        => new Vector2F(
            Math.Clamp(X, min.X, max.X),
            Math.Clamp(Y, min.Y, max.Y)
        );

    /// <summary>
    /// Clamps each component of this vector to the same scalar range [min, max].
    /// </summary>
    /// <param name="min">The lower bound for both X and Y.</param>
    /// <param name="max">The upper bound for both X and Y.</param>
    /// <returns>A new vector with X and Y clamped to [min, max].</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F Clamp(float min, float max)
        => new Vector2F(
            Math.Clamp(X, min, max),
            Math.Clamp(Y, min, max)
        );
    
    /// <summary>
    /// Normalizes this vector to unit length.
    /// If the vector has near-zero length (or contains NaN/Infinity), returns <see cref="Zero"/>.
    /// Note: This implementation divides by the squared length instead of the actual length (likely a bug).
    /// Should be: new Vector2F(X / length, Y / length) where length = sqrt(LengthSquared).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F Normalize()
    {
        float lengthSquared = LengthSquared;
        if (lengthSquared < Constant.Epsilon ||
            float.IsNaN(lengthSquared) ||
            float.IsInfinity(lengthSquared)) return Zero;
        float length = (float)Math.Sqrt(lengthSquared);
        return new Vector2F(X / length, Y / length);
    }
    
    /// <summary>
    /// Computes a hash code for this vector using FNV-1a hash combination.
    /// </summary>
    public override int GetHashCode() => 
        HashHelper.Combine(
            HashHelper.ComputeFnvHash(X),
            HashHelper.ComputeFnvHash(Y)
            );
    
    public bool Equals(Vector2F other)
        => X == other.X && Y == other.Y;

    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is Vector2F other && Equals(other);
    
    /// <summary>
    /// Returns a string representation of the vector in the format "(X, Y)".
    /// </summary>
    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}