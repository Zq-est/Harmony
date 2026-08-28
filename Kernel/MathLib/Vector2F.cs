using System.Diagnostics;
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
    public float Aspect
    {
        get
        {
            if (Y == 0f) return X > 0 ? float.PositiveInfinity : float.NegativeInfinity;
            return X / Y;
        }
    }
    
    /// <summary>
    /// Determines whether two vectors are equal by comparing their components exactly.
    /// </summary>
    public static bool operator ==(Vector2F left, Vector2F right) => left.X == right.X && left.Y == right.Y;

    /// <summary>
    /// Determines whether two vectors are not equal.
    /// </summary>
    public static bool operator !=(Vector2F left, Vector2F right) => !(left == right);
    
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
    public static Vector2F operator +(Vector2F left, Vector2F right) 
        => new Vector2F(left.X + right.X, left.Y + right.Y);
    
    /// <summary>
    /// Subtracts one vector from another component-wise.
    /// </summary>
    public static Vector2F operator -(Vector2F left, Vector2F right) 
        => new Vector2F(left.X - right.X, left.Y - right.Y);
    
    public static Vector2F operator -(Vector2F the) 
        => new Vector2F(-the.X, -the.Y);
    
    /// <summary>
    /// Multiplies a vector by a scalar (scales all components).
    /// </summary>
    public static Vector2F operator *(Vector2F left, float scalar) 
        => new Vector2F(scalar * left.X, scalar * left.Y);
    
    
    /// <summary>
    /// Multiplies a vector by a scalar (scales all components).
    /// </summary>
    public static Vector2F operator *(float scalar, Vector2F right) 
        => new Vector2F(scalar * right.X, scalar * right.Y);

    public static float operator *(Vector2F left, Vector2F right)
        => left.X * right.X + left.Y * right.Y;

    /// <summary>
    /// Divides a vector by a scalar (scales all components).
    /// </summary>
    /// <exception cref="DivideByZeroException">Thrown if the scalar is zero.</exception>
    public static Vector2F operator /(Vector2F left, float scalar)
    {
        if (scalar == 0f) 
            throw new DivideByZeroException();
        return new Vector2F(left.X / scalar, left.Y / scalar);
    }

    /// <summary>
    /// Converts this vector to an angle (in radians) using <see cref="Math.Atan2(double, double)"/>.
    /// The resulting angle is measured counterclockwise from the positive X axis.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The angle is computed as <c>Atan2(Y, X)</c>, which returns a value in the range [-π, π].
    /// A positive angle indicates a counter‑clockwise rotation from the positive X axis.
    /// This method is the inverse of <see cref="FromAngleRadian"/>: for any unit vector,
    /// <c>FromAngleRadian(v.ToAngle())</c> yields a vector approximately equal to <paramref name="v"/>.
    /// </para>
    /// <para>
    /// The method is marked with <see cref="MethodImplOptions.AggressiveInlining"/> to reduce call overhead
    /// in performance‑sensitive contexts.
    /// </para>
    /// </remarks>
    /// <returns>The angle in radians corresponding to the direction of this vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float ToAngle()
        => (float)Math.Atan2(Y, X);

    /// <summary>
    /// Creates a unit vector from an angle (in radians).
    /// This is a static factory method that returns a new vector pointing in the direction specified by the angle.
    /// </summary>
    /// <param name="angle">The angle in radians. Measured counterclockwise from the positive X axis.</param>
    /// <remarks>
    /// <para>
    /// The returned vector has components <c>(Cos(angle), Sin(angle))</c> and is guaranteed to be a unit vector
    /// (within floating‑point precision). This method is the inverse of <see cref="ToAngle()"/>.
    /// </para>
    /// <para>
    /// Example usage:
    /// <code lang="csharp">
    /// Vector2F direction = Vector2F.FromAngle(Math.PI / 4); // Points at 45°
    /// </code>
    /// </para>
    /// <para>
    /// The method is aggressively inlined for performance.
    /// </para>
    /// </remarks>
    /// <returns>A unit <see cref="Vector2F"/> pointing in the direction of the given angle.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2F FromAngleRadian(float angle)
        => new Vector2F((float)Math.Cos(angle), (float)Math.Sin(angle));
    
    /// <summary>
    /// Creates a unit vector from an angle (in radians).
    /// This is a static factory method that returns a new vector pointing in the direction specified by the angle.
    /// </summary>
    /// <param name="angle">The angle in radians. Measured counterclockwise from the positive X axis.</param>
    /// <remarks>
    /// <para>
    /// The returned vector has components <c>(Cos(angle), Sin(angle))</c> and is guaranteed to be a unit vector
    /// (within floating‑point precision). This method is the inverse of <see cref="ToAngle()"/>.
    /// </para>
    /// <para>
    /// Example usage:
    /// <code lang="csharp">
    /// Vector2F direction = Vector2F.FromAngle(Math.PI / 4); // Points at 45°
    /// </code>
    /// </para>
    /// <para>
    /// The method is aggressively inlined for performance.
    /// </para>
    /// </remarks>
    /// <returns>A unit <see cref="Vector2F"/> pointing in the direction of the given angle.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2F FromAngleRadian(double angle)
        => new Vector2F((float)Math.Cos(angle), (float)Math.Sin(angle));

    /// <summary>
    /// Returns a new vector with the absolute values of each component.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method applies <see cref="Math.Abs(float)"/> to both the X and Y components independently.
    /// The resulting vector has non‑negative components.
    /// </para>
    /// <para>
    /// It is useful for obtaining the magnitude of each axis separately, e.g., when calculating
    /// bounding box extents or removing directional signs.
    /// </para>
    /// <para>
    /// The method is marked with <see cref="MethodImplOptions.AggressiveInlining"/> to minimize overhead.
    /// </para>
    /// </remarks>
    /// <returns>A new <see cref="Vector2F"/> where each component is the absolute value of the original component.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F Abs() => new Vector2F(Math.Abs(X), Math.Abs(Y));
    
    /// <summary>
    /// Returns a new vector where each component is the minimum of this vector's component and the corresponding component of the other vector.
    /// </summary>
    /// <param name="other">The vector to compare with.</param>
    /// <remarks>
    /// <para>
    /// This method performs component-wise minimization: <c>result.X = Min(this.X, other.X)</c>, <c>result.Y = Min(this.Y, other.Y)</c>.
    /// It is useful for computing the lower bounds of a set of vectors or for clamping to an upper limit when combined with <see cref="Max(in Vector2F)"/>.
    /// </para>
    /// <para>
    /// The method is marked with <see cref="MethodImplOptions.AggressiveInlining"/> to reduce call overhead.
    /// </para>
    /// </remarks>
    /// <returns>A new <see cref="Vector2F"/> where each component is the smaller of the two input components.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F Min(in Vector2F other) => new Vector2F(Math.Min(X, other.X), Math.Min(Y, other.Y));
    
    /// <summary>
    /// Clamps each component of the given vector so that it is no less than the specified minimum value.
    /// Effectively ensures every component ≥ <paramref name="min"/>.
    /// </summary>
    /// <param name="vector">The input vector.</param>
    /// <param name="min">The lower bound for each component.</param>
    /// <remarks>
    /// <para>
    /// Despite the name containing "Min", this method actually raises components that are too low.
    /// It uses <see cref="Math.Max(float, float)"/> to enforce a lower bound: <c>result.X = Max(vector.X, min)</c>, <c>result.Y = Max(vector.Y, min)</c>.
    /// </para>
    /// <para>
    /// This is the static counterpart of <see cref="ClampMinComponents(in Vector2F)"/>.
    /// </para>
    /// </remarks>
    /// <returns>A new <see cref="Vector2F"/> with each component clamped to be at least <paramref name="min"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2F ClampMinComponents(in Vector2F vector, float min)
        => new Vector2F(
            Math.Max(vector.X, min),
            Math.Max(vector.Y, min)
        );

    /// <summary>
    /// Clamps the components of the vector to a specified minimum value, returning a new <see cref="Vector2F"/> instance.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method evaluates each component (X and Y) of the current vector.
    /// If a component's value is less than the specified <paramref name="min"/>, it is replaced by <paramref name="min"/> using <see cref="Math.Max"/>.
    /// Otherwise, the original value is retained. This method applies the <see cref="MethodImplOptions.AggressiveInlining"/> attribute to suggest JIT inlining for optimal performance.
    /// </para>
    /// </remarks>
    /// <param name="min">The minimum threshold value to clamp each component to.</param>
    /// <returns>A new <see cref="Vector2F"/> where each component is at least <paramref name="min"/>.</returns>
    /// <example>
    /// <code lang="csharp">
    /// <![CDATA[
    /// Vector2F vector = new Vector2F(-5.0f, 10.0f);
    /// Vector2F clampedVector = vector.ClampMinComponents(0.0f);
    /// // clampedVector.X is 0.0f, clampedVector.Y is 10.0f
    /// ]]>
    /// </code>
    /// </example>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F ClampMinComponents(float min)
        => new Vector2F(
            Math.Max(X, min),
            Math.Max(Y, min)
        );
    
    /// <summary>
    /// Instance version of <see cref="ClampMinComponents(in Vector2F, float)"/>.
    /// Clamps each component of this vector so that it is no less than the corresponding component of the given min vector.
    /// </summary>
    /// <param name="min">The vector whose components define the lower bounds.</param>
    /// <remarks>
    /// <para>
    /// This method enforces: <c>result.X = Max(this.X, min.X)</c>, <c>result.Y = Max(this.Y, min.Y)</c>.
    /// It is useful for ensuring a position stays above a certain threshold region.
    /// </para>
    /// </remarks>
    /// <returns>A new <see cref="Vector2F"/> with each component clamped to be at least the corresponding component of <paramref name="min"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F ClampMinComponents(in Vector2F min)
        => new Vector2F(
            Math.Max(X, min.X),
            Math.Max(Y, min.Y)
        );
    
    /// <summary>
    /// Returns a new vector where each component is the maximum of this vector's component and the corresponding component of the other vector.
    /// </summary>
    /// <param name="other">The vector to compare with.</param>
    /// <remarks>
    /// <para>
    /// Performs component-wise maximization: <c>result.X = Max(this.X, other.X)</c>, <c>result.Y = Max(this.Y, other.Y)</c>.
    /// Useful for computing upper bounds or for clamping to a lower limit when combined with <see cref="Min(in Vector2F)"/>.
    /// </para>
    /// </remarks>
    /// <returns>A new <see cref="Vector2F"/> where each component is the larger of the two input components.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F Max(in Vector2F other) => new Vector2F(Math.Max(X, other.X), Math.Max(Y, other.Y));
    
    /// <summary>
    /// Clamps each component of this vector so that it is not greater than the specified maximum value.
    /// Effectively ensures every component ≤ <paramref name="max"/>.
    /// </summary>
    /// <param name="max">The upper bound for each component.</param>
    /// <remarks>
    /// <para>
    /// Despite the name containing "Max", this method actually lowers components that are too high.
    /// It uses <see cref="Math.Min(float, float)"/> to enforce an upper bound: <c>result.X = Min(this.X, max)</c>, <c>result.Y = Min(this.Y, max)</c>.
    /// </para>
    /// <para>
    /// This is the instance counterpart of <see cref="ClampMaxComponents(in Vector2F, float)"/>.
    /// </para>
    /// </remarks>
    /// <returns>A new <see cref="Vector2F"/> with each component clamped to be at most <paramref name="max"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F ClampMaxComponents(float max)
        => new Vector2F(
            Math.Min(X, max),
            Math.Min(Y, max)
        );
    
    /// <summary>
    /// Static version of <see cref="ClampMaxComponents(float)"/>.
    /// Clamps each component of the given vector so that it is not greater than the specified maximum value.
    /// </summary>
    /// <param name="vector">The input vector.</param>
    /// <param name="max">The upper bound for each component.</param>
    /// <remarks>
    /// <para>
    /// Uses <see cref="Math.Min(float, float)"/> to enforce: <c>result.X = Min(vector.X, max)</c>, <c>result.Y = Min(vector.Y, max)</c>.
    /// </para>
    /// </remarks>
    /// <returns>A new <see cref="Vector2F"/> with each component clamped to be at most <paramref name="max"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2F ClampMaxComponents(in Vector2F vector, float max)
        => new Vector2F(
            Math.Min(vector.X, max),
            Math.Min(vector.Y, max)
        );

    /// <summary>
    /// Restricts the components of the current <see cref="Vector2F"/> to a specified maximum value.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method performs a component-wise upper-bound clamp operation. 
    /// It compares each component (X, Y) of the current vector with the corresponding component of the <paramref name="max"/> vector, 
    /// and returns a new <see cref="Vector2F"/> where each component is the minimum of the two values.
    /// </para>
    /// <para>
    /// The method is marked with <see cref="MethodImplOptions.AggressiveInlining"/> to minimize call overhead in performance-critical 
    /// game engine scenarios (e.g., math libraries using Vortice.DirectX11 or opnetk). The <paramref name="max"/> parameter is passed 
    /// as <see langword="in"/> to avoid struct copying while ensuring immutability.
    /// </para>
    /// </remarks>
    /// <param name="max">The vector containing the maximum allowed values for each component.</param>
    /// <returns>A new <see cref="Vector2F"/> with components clamped to not exceed the corresponding components in <paramref name="max"/>.</returns>
    /// <example>
    /// This example demonstrates how to use <c>ClampMaxComponents</c> to clamp a vector's values.
    /// <code lang="csharp">
    /// <![CDATA[
    /// Vector2F vector = new Vector2F(10.0f, -5.0f);
    /// Vector2F maxValues = new Vector2F(0.0f, 0.0f);
    /// Vector2F result = vector.ClampMaxComponents(in maxValues);
    /// // result will be (0.0f, -5.0f) because X is clamped to 0.0f, while Y remains -5.0f.
    /// ]]>
    /// </code>
    /// </example>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F ClampMaxComponents(in Vector2F max)
        => new Vector2F(
            Math.Min(X, max.X),
            Math.Min(Y, max.Y)
        );
    
    /// <summary>
    /// Computes the Euclidean distance from this vector to another vector.
    /// </summary>
    /// <param name="other">The target vector to measure distance to.</param>
    /// <remarks>
    /// <para>
    /// The Euclidean distance is calculated as the square root of the sum of squared differences of corresponding components:
    /// <c>√[(X₁−X₂)² + (Y₁−Y₂)²]</c>.
    /// </para>
    /// <para>
    /// This method internally calls <see cref="DistanceSquaredTo(in Vector2F)"/> and then takes the square root.
    /// If you only need to compare distances (e.g., find the closest point), prefer using
    /// <see cref="DistanceSquaredTo(in Vector2F)"/> to avoid the expensive square‑root operation.
    /// </para>
    /// <para>
    /// The method is marked with <see cref="MethodImplOptions.AggressiveInlining"/> to reduce call overhead
    /// in performance‑critical scenarios such as collision detection or pathfinding.
    /// </para>
    /// </remarks>
    /// <returns>The Euclidean distance between this vector and <paramref name="other"/> as a <see langword="float"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float DistanceTo(in Vector2F other)
        => (float)Math.Sqrt(DistanceSquaredTo(other));

    /// <summary>
    /// Computes the squared Euclidean distance from this vector to another vector.
    /// Avoids the square‑root operation for efficiency.
    /// </summary>
    /// <param name="other">The target vector to measure squared distance to.</param>
    /// <remarks>
    /// <para>
    /// The squared distance is computed as <c>(X₁−X₂)² + (Y₁−Y₂)²</c>.
    /// </para>
    /// <para>
    /// This method is preferred over <see cref="DistanceTo(in Vector2F)"/> when only relative comparisons are needed,
    /// because it eliminates the computationally expensive <see cref="Math.Sqrt(double)"/> call.
    /// Common use cases include:
    /// <list type="bullet">
    ///   <item><description>Finding the nearest object among many candidates.</description></item>
    ///   <item><description>Checking if a point lies within a radius (compare squared distance against squared radius).</description></item>
    ///   <item><description>Sorting by distance.</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// The method is aggressively inlined for maximum performance.
    /// </para>
    /// </remarks>
    /// <returns>The squared Euclidean distance between this vector and <paramref name="other"/> as a <see langword="float"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float DistanceSquaredTo(in Vector2F other)
        => (X - other.X) * (X - other.X) + (Y - other.Y) * (Y - other.Y);
    
    /// <summary>
    /// Computes the dot product of this vector and another vector.
    /// </summary>
    /// <param name="other">The other vector to compute the dot product with.</param>
    /// <remarks>
    /// <para>
    /// The dot product is defined as <c>X · other.X + Y · other.Y</c>.
    /// It measures the projection of one vector onto another and is commonly used
    /// to determine angles (via <c>cosθ = Dot / (|this|·|other|)</c>) or to test orthogonality
    /// (dot product of zero means perpendicular vectors).
    /// </para>
    /// <para>
    /// This method is marked with <see cref="MethodImplOptions.AggressiveInlining"/> to reduce overhead
    /// in performance‑sensitive contexts such as physics or rendering loops.
    /// </para>
    /// </remarks>
    /// <returns>A scalar value representing the dot product of the two vectors.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float Dot(in Vector2F other) 
        => X * other.X + Y * other.Y;

    /// <summary>
    /// Computes the 2D cross product (scalar) of this vector and another vector.
    /// The result is <c>X · other.Y - Y · other.X</c>, representing the signed area of the parallelogram spanned by the two vectors.
    /// </summary>
    /// <param name="other">The other vector to compute the cross product with.</param>
    /// <remarks>
    /// <para>
    /// In 2D, the cross product yields a scalar (often called the “perp dot product” or “wedge product”).
    /// Its magnitude equals the area of the parallelogram formed by the two vectors.
    /// The sign indicates orientation: positive if <paramref name="other"/> is counter‑clockwise from this vector,
    /// negative if clockwise, and zero if they are collinear.
    /// </para>
    /// <para>
    /// This operation is equivalent to treating the vectors as 3D with Z = 0 and taking the Z component of the full 3D cross product.
    /// It is widely used in collision detection, winding order tests, and torque calculations.
    /// </para>
    /// <para>
    /// The method is aggressively inlined for performance.
    /// </para>
    /// </remarks>
    /// <returns>A scalar representing the 2D cross product (signed area).</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float Cross(in Vector2F other)
        => X * other.Y - Y * other.X;

    /// <summary>
    /// Returns a new vector where each component is the sign of the original component.
    /// Possible values are -1, 0, or 1.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method applies <see cref="Math.Sign(float)"/> to both X and Y independently.
    /// <list type="bullet">
    ///   <item><description>Positive values become 1.</description></item>
    ///   <item><description>Negative values become -1.</description></item>
    ///   <item><description>Zero remains 0.</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// The resulting vector can be used to extract directional information, e.g., determining octants
    /// or creating unit step vectors aligned to the axes.
    /// </para>
    /// <para>
    /// The method is aggressively inlined for performance.
    /// </para>
    /// </remarks>
    /// <returns>A new <see cref="Vector2F"/> with components being -1, 0, or 1.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F Sign() 
        => new Vector2F(Math.Sign(X), Math.Sign(Y));

    /// <summary>
    /// Returns a new vector where each component is rounded down to the nearest integer toward negative infinity.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method applies <see cref="Math.Floor(double)"/> to both X and Y independently.
    /// The result is a new <see cref="Vector2F"/> whose components are integers (represented as <see langword="float"/>)
    /// that are less than or equal to the original components.
    /// </para>
    /// <para>
    /// For example, a vector with components (1.8, -0.3) becomes (1.0, -1.0).
    /// </para>
    /// <para>
    /// Flooring is useful for grid‑based operations, tile mapping, or quantizing positions downward.
    /// </para>
    /// <para>
    /// The method is aggressively inlined for performance.
    /// </para>
    /// </remarks>
    /// <returns>A new <see cref="Vector2F"/> with each component floored to the previous whole number.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F Floor() 
        => new Vector2F((float)Math.Floor(X), (float)Math.Floor(Y));

    /// <summary>
    /// Returns a new vector where each component is rounded up to the nearest integer toward positive infinity.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method applies <see cref="Math.Ceiling(double)"/> to both the X and Y components independently.
    /// The result is a new <see cref="Vector2F"/> whose components are integers (represented as <see langword="float"/>)
    /// that are greater than or equal to the original components.
    /// </para>
    /// <para>
    /// For example, a vector with components (1.2, -0.8) becomes (2.0, 0.0).
    /// </para>
    /// <para>
    /// The method is marked with <see cref="MethodImplOptions.AggressiveInlining"/> to reduce overhead
    /// in performance‑sensitive contexts.
    /// </para>
    /// </remarks>
    /// <returns>A new <see cref="Vector2F"/> with each component ceiled to the next whole number.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F Ceil() 
        => new Vector2F((float)Math.Ceiling(X), (float)Math.Ceiling(Y));

    /// <summary>
    /// Returns a new vector where each component is rounded to the nearest integer using
    /// <see cref="MidpointRounding.AwayFromZero"/> midpoint rounding rule.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method applies <see cref="Math.Round(double, MidpointRounding)"/> to both the X and Y components independently.
    /// The <see cref="MidpointRounding.AwayFromZero"/> strategy rounds numbers at the halfway point to the nearest integer
    /// that is farther from zero (e.g., 1.5 → 2, -1.5 → -2).
    /// </para>
    /// <para>
    /// If you require banker's rounding (to even), use <see cref="Math.Round(double)"/> directly instead.
    /// </para>
    /// <para>
    /// The method is aggressively inlined for performance.
    /// </para>
    /// </remarks>
    /// <returns>A new <see cref="Vector2F"/> with each component rounded to the nearest integer.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F Round() 
        => new Vector2F(
            (float)Math.Round(X, MidpointRounding.AwayFromZero), 
            (float)Math.Round(Y, MidpointRounding.AwayFromZero)
        );

    /// <summary>
    /// Rotates this vector by the specified angle (in radians) around the origin.
    /// Uses the standard 2D rotation matrix: <c>[cos θ, -sin θ; sin θ, cos θ]</c>.
    /// </summary>
    /// <param name="angle">The rotation angle measured in radians. Positive values indicate counter‑clockwise rotation.</param>
    /// <remarks>
    /// <para>
    /// The rotation is performed by applying the following linear transformation:
    /// <list type="bullet">
    ///   <item><description>newX = X·cos(θ) − Y·sin(θ)</description></item>
    ///   <item><description>newY = X·sin(θ) + Y·cos(θ)</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// The angle is assumed to be in radians. If you have degrees, convert using <c>radians = degrees * (Math.PI / 180.0)</c>.
    /// </para>
    /// <para>
    /// The method computes sine and cosine once via <see cref="Math.Cos(double)"/> and <see cref="Math.Sin(double)"/>,
    /// then reuses them for both components. It is aggressively inlined for performance‑critical loops.
    /// </para>
    /// </remarks>
    /// <returns>A new <see cref="Vector2F"/> representing the rotated vector.</returns>
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
    /// <remarks>
    /// <para>
    /// This method delegates to <see cref="Utils.IsEqualApproximate(float, float)"/> for each component.
    /// The comparison is tolerant to floating‑point rounding errors, making it suitable for scenarios
    /// where exact equality is unlikely (e.g., after multiple arithmetic operations).
    /// </para>
    /// <para>
    /// The method is marked with <see cref="MethodImplOptions.AggressiveInlining"/> to reduce call overhead
    /// in performance‑critical code paths.
    /// </para>
    /// </remarks>
    /// <returns>
    /// <see langword="true"/> if both the X and Y components are considered approximately equal according to
    /// the tolerance defined in <see cref="Utils.IsEqualApproximate(float, float)"/>; otherwise <see langword="false"/>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEqualApproximate(Vector2F other)
        => Utils.IsEqualApproximate(X, other.X) && Utils.IsEqualApproximate(Y, other.Y);
    
    /// <summary>
    /// Checks whether this vector is approximately the zero vector.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method delegates to <see cref="Utils.IsZeroApproximate(float)"/> for each component.
    /// It returns <see langword="true"/> when both X and Y are close enough to zero within the tolerance
    /// defined by the utility method. This is useful for detecting degenerate vectors after calculations.
    /// </para>
    /// <para>
    /// The method is marked with <see cref="MethodImplOptions.AggressiveInlining"/> for performance.
    /// </para>
    /// </remarks>
    /// <returns>
    /// <see langword="true"/> if both the X and Y components are approximately zero according to
    /// <see cref="Utils.IsZeroApproximate(float)"/>; otherwise <see langword="false"/>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZeroApproximate()
        => Utils.IsZeroApproximate(X) && Utils.IsZeroApproximate(Y);

    /// <summary>
    /// Determines whether this vector is exactly equal to another vector,
    /// using a strict (possibly bitwise or tolerance‑free) comparison per component.
    /// </summary>
    /// <param name="other">The other vector to compare.</param>
    /// <remarks>
    /// <para>
    /// This method delegates to <see cref="Utils.IsSame(float, float)"/> for each component.
    /// Unlike <see cref="IsEqualApproximate(Vector2F)"/>, this method performs an exact equality check,
    /// typically without tolerance. Use this when you require precise identity (e.g., for caching or hashing).
    /// </para>
    /// <para>
    /// The method is marked with <see cref="MethodImplOptions.AggressiveInlining"/> to minimize overhead.
    /// </para>
    /// </remarks>
    /// <returns>
    /// <see langword="true"/> if both the X and Y components are considered identical according to
    /// the definition of <see cref="Utils.IsSame(float, float)"/>; otherwise <see langword="false"/>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsSame(Vector2F other)
        => Utils.IsSame(X, other.X) && Utils.IsSame(Y, other.Y);

    /// <summary>
    /// Computes the reflection of this vector about a given normal vector.
    /// Formula: <c>reflect = this - 2 · Dot(other) · other</c>.
    /// </summary>
    /// <param name="other">The normal vector used as the mirror axis.</param>
    /// <remarks>
    /// <para>
    /// The reflection formula assumes the normal vector <paramref name="other"/> is a unit vector for correct geometric results.
    /// A debug assertion verifies that the normal is normalized; if the assertion fails, ensure the provided normal is normalized before calling this method.
    /// </para>
    /// <para>
    /// The method is marked with <see cref="MethodImplOptions.AggressiveInlining"/> to minimize overhead in performance-critical paths.
    /// </para>
    /// </remarks>
    /// <returns>A new <see cref="Vector2F"/> representing the reflected direction.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F Reflect(in Vector2F other)
    {
        Debug.Assert(IsNormalized(other), "Normal vector must be normalized");
        return this - 2 * Dot(other) * other;
    }

    /// <summary>
    /// Computes the bounce direction by negating the reflection of this vector about the given normal.
    /// Equivalent to <c>-Reflect(other)</c>.
    /// </summary>
    /// <param name="other">The normal vector used as the mirror axis.</param>
    /// <remarks>
    /// <para>
    /// Bounce is commonly used in collision response to reverse the velocity component perpendicular to a surface.
    /// It is simply the negative of the reflected vector, which flips the outgoing direction entirely.
    /// </para>
    /// <para>
    /// The same caution regarding the normal’s normalization applies as in <see cref="Reflect(in Vector2F)"/>.
    /// </para>
    /// </remarks>
    /// <returns>A new <see cref="Vector2F"/> representing the bounced (inverted reflection) direction.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F Bounce(in Vector2F other)
        => -Reflect(other);

    /// <summary>
    /// Projects this vector onto a plane defined by the given normal,
    /// effectively computing the component parallel to the surface.
    /// Formula: <c>slide = this - Dot(other) · other</c>.
    /// </summary>
    /// <param name="other">The normal vector of the surface.</param>
    /// <remarks>
    /// <para>
    /// This operation removes the component of the vector that is perpendicular to the normal,
    /// leaving only the tangential part. It is often used to make a character "slide" along a wall.
    /// </para>
    /// <para>
    /// As with <see cref="Reflect(in Vector2F)"/>, the normal should be normalized for physically accurate results.
    /// A debug assertion verifies this condition.
    /// </para>
    /// </remarks>
    /// <returns>A new <see cref="Vector2F"/> representing the component of this vector parallel to the surface.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F Slide(in Vector2F other)
    {
        Debug.Assert(IsNormalized(other), "Normal vector must be normalized");
        return this - Dot(other) * other;
    }

    /// <summary>
    /// Returns a new <see cref="Vector2F"/> whose each component is clamped to the inclusive range defined by the corresponding components of two boundary vectors.
    /// </summary>
    /// <param name="min">The minimum boundary vector. Each component of the result will not be less than the corresponding component of this vector.</param>
    /// <param name="max">The maximum boundary vector. Each component of the result will not exceed the corresponding component of this vector.</param>
    /// <remarks>
    /// <para>
    /// The clamping is performed per component:
    /// <list type="bullet">
    ///   <item><description>Result.X = <see cref="Math.Clamp(float, float, float)"/></description></item>
    ///   <item><description>Result.Y = <see cref="Math.Clamp(float, float, float)"/></description></item>
    /// </list>
    /// Both parameters are passed by reference (<see langword="in"/>) to avoid copying and improve performance.
    /// </para>
    /// <para>
    /// This method is marked with <see cref="MethodImplOptions.AggressiveInlining"/> to reduce call overhead
    /// in performance‑sensitive contexts such as physics or rendering loops.
    /// </para>
    /// <para>
    /// Note: If <paramref name="min"/> contains components greater than the corresponding components of <paramref name="max"/>,
    /// the behavior is undefined because <see cref="Math.Clamp(float, float, float)"/> requires <c>min ≤ max</c>.
    /// </para>
    /// </remarks>
    /// <returns>A new <see cref="Vector2F"/> with each component independently clamped to [min.Component, max.Component].</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F Clamp(in Vector2F min, in Vector2F max)
        => new Vector2F(
            Math.Clamp(X, min.X, max.X),
            Math.Clamp(Y, min.Y, max.Y)
        );

    /// <summary>
    /// Returns a new <see cref="Vector2F"/> whose each component is clamped to the same scalar range [<paramref name="min"/>, <paramref name="max"/>].
    /// </summary>
    /// <param name="min">The lower bound for both components.</param>
    /// <param name="max">The upper bound for both components.</param>
    /// <remarks>
    /// <para>
    /// This overload applies the same clamping interval to both axes:
    /// <list type="bullet">
    ///   <item><description>Result.X = <see cref="Math.Clamp(float, float, float)"/></description></item>
    ///   <item><description>Result.Y = <see cref="Math.Clamp(float, float, float)"/></description></item>
    /// </list>
    /// It is useful when you need to restrict the entire vector within a uniform bounding box (e.g., screen coordinates or grid limits).
    /// </para>
    /// <para>
    /// Like the vector‑based overload, this method is aggressively inlined for performance.
    /// </para>
    /// <para>
    /// Ensure that <paramref name="min"/> ≤ <paramref name="max"/>; otherwise the result is undefined due to the contract of <see cref="Math.Clamp(float, float, float)"/>.
    /// </para>
    /// </remarks>
    /// <returns>A new <see cref="Vector2F"/> with both components clamped to [<paramref name="min"/>, <paramref name="max"/>].</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2F Clamp(float min, float max)
        => new Vector2F(
            Math.Clamp(X, min, max),
            Math.Clamp(Y, min, max)
        );
    
    /// <summary>
    /// Returns a normalized copy of the current vector (unit vector with length 1).
    /// If the vector is zero, NaN, or infinite, returns <see cref="Zero"/> instead.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Normalization divides each component by the Euclidean length of the vector.
    /// To avoid unnecessary square‑root calculations, the method first checks the squared length (<see cref="LengthSquared"/>)
    /// against <see cref="Constant.Epsilon"/> to detect near‑zero vectors. It also guards against <see cref="float.NaN"/>
    /// and <see cref="float.PositiveInfinity"/> / <see cref="float.NegativeInfinity"/> values.
    /// </para>
    /// <para>
    /// When any of these conditions are met, the method returns <see cref="Zero"/> (a vector with all components set to 0)
    /// rather than attempting a division that would produce undefined results.
    /// </para>
    /// <para>
    /// This method is marked with <see cref="MethodImplOptions.AggressiveInlining"/> to encourage the JIT compiler
    /// to inline the call, reducing overhead in performance‑critical paths such as game loops.
    /// </para>
    /// </remarks>
    /// <returns>
    /// A new <see cref="Vector2F"/> representing the normalized direction of the current vector,
    /// or <see cref="Zero"/> if the vector cannot be normalized.
    /// </returns>
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
    /// Determines whether the current vector is normalized, i.e., its length is approximately equal to 1.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Normalization is checked by comparing the squared length (<see cref="LengthSquared"/>) to 1.
    /// The comparison uses a tolerance defined by <see cref="Constant.Epsilon"/> to account for floating-point imprecision.
    /// Using squared length avoids the computational cost of a square root operation.
    /// </para>
    /// <para>
    /// This is an instance method that operates on the current vector object.
    /// </para>
    /// </remarks>
    /// <returns>
    /// <see langword="true"/> if the absolute difference between <see cref="LengthSquared"/> and 1 is less than <see cref="Constant.Epsilon"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsNormalized() => Math.Abs(LengthSquared - 1f) < Constant.Epsilon;

    /// <summary>
    /// Determines whether the specified vector is normalized.
    /// </summary>
    /// <param name="other">The vector to test. Passed by reference (<see langword="in"/>) to avoid copying and improve performance.</param>
    /// <remarks>
    /// <para>
    /// This overload allows checking the normalization state of another <see cref="Vector2F"/> instance without affecting the current one.
    /// It follows the same logic as the parameterless version: compares the squared length of <paramref name="other"/> to 1 using <see cref="Constant.Epsilon"/> as tolerance.
    /// </para>
    /// <para>
    /// The <see langword="in"/> modifier indicates that the argument is passed by reference but cannot be modified inside the method,
    /// which is efficient for large structures like vectors.
    /// </para>
    /// </remarks>
    /// <returns>
    /// <see langword="true"/> if the absolute difference between <paramref name="other"/>.LengthSquared and 1 is less than <see cref="Constant.Epsilon"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsNormalized(in Vector2F other) => Math.Abs(other.LengthSquared - 1f) < Constant.Epsilon;
    
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