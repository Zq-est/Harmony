# Feature: Vector2F

The `Vector2F` is a structure located in the `Kernel.MathLib` namespace that represents a two-dimensional vector with single-precision floating-point components. It provides a comprehensive set of common mathematical operations for 2D vectors used throughout the Harmony Engine project (which utilizes technologies like Fody, opnetk, nlog, and Vortice.DirectX11). The struct implements `IEquatable<Vector2F>` for efficient equality comparison and leverages `MethodImplOptions.AggressiveInlining` extensively to minimize call overhead in performance-critical game engine loops. It also integrates with the `HashHelper` class for FNV-1a-based hashing and uses `Constant.Epsilon` for floating-point tolerance checks.

# Components of Vector2F

The `Vector2F` struct is composed of the following components:

- **Fields & Predefined Vectors**: A set of static readonly vectors including `One` (1,1), `Zero` (0,0), `UnitX` (1,0), and `UnitY` (0,1) for common directional and scaling constants.
- **Properties**: Component accessors (`X`, `Y`), computed properties for magnitude (`Length`, `LengthSquared`), and aspect ratio (`Aspect`).
- **Constructors**: A parameterless constructor initializing to zero, and a parameterized constructor with optional debug validation against NaN/Infinity.
- **Operators**: Full set of arithmetic (`+`, `-`, `*`, `/`) and equality (`==`, `!=`) operators for intuitive vector manipulation.
- **Hash & Equality**: `GetHashCode`, `Equals`, `AlmostEquals`, `IsEqualApproximate`, and `IsSame` for various comparison strategies.
- **Length & Distance**: Euclidean length, squared length, and distance computation (both exact and squared) to another vector.
- **Normalization**: `Normalize` with safety guards and `IsNormalized` checks.
- **Angle Operations**: Convert vector to angle via `ToAngle` and create a unit vector from an angle via `FromAngle`.
- **Component-wise Math**: `Abs`, `Min`, `Max`, `Sign`, `Floor`, `Ceil`, `Round` for per-component scalar operations.
- **Clamping**: Multiple overloads of `ClampFloor`, `ClampCeil`, and `Clamp` for bounding vectors within ranges.
- **Vector Products**: Dot product (instance and static) and 2D cross product (scalar perpendicular dot product).
- **Projections & Reflections**: `Project`, `Reflect`, `Bounce`, `Slide`, and `Orthogonal` for geometric transformations.
- **Transformation**: `Rotate` by angle and `MoveToward` for smooth interpolation toward a target.

## Detailed API Reference

### Class: `Vector2F`

A struct representing a 2D vector with single-precision floating-point components. Implements `IEquatable<Vector2F>`.

---

### Fields / Static Properties

- **`One`** (`public static Vector2F`)
  - **Summary:** A static readonly vector with both components set to 1.
- **`Zero`** (`public static Vector2F`)
  - **Summary:** A static readonly vector with both components set to 0.
- **`UnitX`** (`public static Vector2F`)
  - **Summary:** A static readonly unit vector along the X axis (1, 0).
- **`UnitY`** (`public static Vector2F`)
  - **Summary:** A static readonly unit vector along the Y axis (0, 1).

### Instance Properties

- **`X`** (`public float`)
  - **Summary:** Gets or sets the X component of the vector.
- **`Y`** (`public float`)
  - **Summary:** Gets or sets the Y component of the vector.
- **`Length`** (`public float`)
  - **Summary:** Gets the Euclidean length (magnitude) of the vector. Computed as `√(X² + Y²)`.
- **`LengthSquared`** (`public float`)
  - **Summary:** Gets the squared Euclidean length of the vector (`X² + Y²`). Avoids the square root for performance-critical comparisons.
- **`Aspect`** (`public float`)
  - **Summary:** Gets the aspect ratio (`X / Y`). Returns `PositiveInfinity` if Y > 0 and Y = 0, or `NegativeInfinity` if Y < 0 and Y = 0. Useful for screen-space calculations.

---

### Constructors

#### `Vector2F()`
- **Summary:** Initializes a new instance with zero components (0, 0).

#### `Vector2F(float x, float y)`
- **Summary:** Initializes a new instance with specified X and Y values. Validates in DEBUG builds that neither component is NaN or Infinity.
- **Parameters:**
  - `x` (`float`): The X component.
  - `y` (`float`): The Y component.
- **Exceptions:**
  - `ArgumentException`: Thrown in DEBUG when any component is NaN or Infinity.

---

### Operators

| Operator | Summary |
|----------|---------|
| `==` | Determines whether two vectors are equal by comparing their components exactly. |
| `!=` | Determines whether two vectors are not equal. |
| `+` | Adds two vectors component-wise. |
| `-` (binary) | Subtracts one vector from another component-wise. |
| `-` (unary) | Negates both components of the vector. |
| `*` (vector × scalar) | Multiplies a vector by a scalar, scaling all components. |
| `*` (scalar × vector) | Multiplies a scalar by a vector, scaling all components. |
| `/` | Divides a vector by a scalar. Throws `DivideByZeroException` if scalar is zero. |

---

### Method: `AlmostEquals(Vector2F vector)`

- **Summary:** Checks if this vector is approximately equal to another vector within an epsilon tolerance.
- **Remarks:** Uses `Constant.Epsilon` as the tolerance threshold. Compares the absolute difference of each component.
- **Parameters:**
  - `vector` (`Vector2F`): The vector to compare.
- **Returns:** `true` if `|X - vector.X| < Epsilon` and `|Y - vector.Y| < Epsilon`; otherwise `false`.

---

### Method: `ToAngle()`

- **Summary:** Converts this vector to an angle (in radians) using `Math.Atan2(Y, X)`. The resulting angle is measured counterclockwise from the positive X axis.
- **Remarks:** Returns a value in the range `[-π, π]`. This method is the inverse of `FromAngle(float)`: for any unit vector, `FromAngle(v.ToAngle())` yields a vector approximately equal to `v`. Marked with `AggressiveInlining`.
- **Returns:** The angle in radians corresponding to the direction of this vector.

---

### Method: `FromAngle(float angle)` (Static)

- **Summary:** Creates a unit vector from an angle (in radians). Returns a new vector pointing in the direction specified by the angle.
- **Remarks:** Components are `(Cos(angle), Sin(angle))`. This is the inverse of `ToAngle()`. Example: `Vector2F.FromAngle(Math.PI / 4)` points at 45°. Marked with `AggressiveInlining`.
- **Parameters:**
  - `angle` (`float`): The angle in radians, measured counterclockwise from the positive X axis.
- **Returns:** A unit `Vector2F` pointing in the direction of the given angle.

---

### Method: `Abs()`

- **Summary:** Returns a new vector with the absolute values of each component.
- **Remarks:** Applies `Math.Abs(float)` to both X and Y independently. Useful for bounding box extents or removing directional signs. Marked with `AggressiveInlining`.
- **Returns:** A new `Vector2F` where each component is the absolute value of the original.

---

### Method: `Min(in Vector2F vector)`

- **Summary:** Returns a new vector where each component is the minimum of this vector's component and the corresponding component of the argument.
- **Remarks:** Performs component-wise minimization: `result.X = Min(X, vector.X)`, `result.Y = Min(Y, vector.Y)`. Marked with `AggressiveInlining`.
- **Parameters:**
  - `vector` (`in Vector2F`): The vector to compare with.
- **Returns:** A new `Vector2F` with the smaller of the two input components per axis.

---

### Method: `Max(in Vector2F vector)`

- **Summary:** Returns a new vector where each component is the maximum of this vector's component and the corresponding component of the argument.
- **Remarks:** Performs component-wise maximization. Marked with `AggressiveInlining`.
- **Parameters:**
  - `vector` (`in Vector2F`): The vector to compare with.
- **Returns:** A new `Vector2F` with the larger of the two input components per axis.

---

### Method: `ClampFloor` (3 Overloads)

#### `ClampFloor(in Vector2F min)` (Instance)
- **Summary:** Clamps each component so that it is no less than the corresponding component of the given min vector.
- **Remarks:** Uses `Math.Max` per component. Enforces a lower bound per axis.
- **Returns:** A new `Vector2F` with each component ≥ `min.X` and ≥ `min.Y` respectively.

#### `ClampFloor(float min)` (Instance)
- **Summary:** Clamps each component to be no less than the specified scalar minimum.
- **Returns:** A new `Vector2F` with both components ≥ `min`.

#### `ClampFloor(in Vector2F vector, float min)` (Static)
- **Summary:** Static version. Clamps each component of the given vector so that it is no less than the specified minimum value.
- **Returns:** A new `Vector2F` with each component ≥ `min`.

---

### Method: `ClampCeil` (3 Overloads)

#### `ClampCeil(in Vector2F max)` (Instance)
- **Summary:** Clamps each component to be no greater than the corresponding component of the specified maximum vector.
- **Remarks:** Uses `Math.Min` per component. Enforces an upper bound per axis.
- **Returns:** A new `Vector2F` with each component ≤ `max.X` and ≤ `max.Y` respectively.

#### `ClampCeil(float max)` (Instance)
- **Summary:** Clamps each component to be no greater than the specified scalar maximum.
- **Returns:** A new `Vector2F` with both components ≤ `max`.

#### `ClampCeil(in Vector2F vector, float max)` (Static)
- **Summary:** Static version. Clamps each component of the given vector so that it is no greater than the specified maximum value.
- **Returns:** A new `Vector2F` with each component ≤ `max`.

---

### Method: `Clamp` (2 Overloads)

#### `Clamp(in Vector2F min, in Vector2F max)` (Instance)
- **Summary:** Returns a new vector with each component clamped to the inclusive range `[min.component, max.component]`.
- **Remarks:** Uses `Math.Clamp` per component. Both parameters passed by `in` reference for performance. Requires `min ≤ max` per component.
- **Returns:** A new `Vector2F` with components independently clamped.

#### `Clamp(float min, float max)` (Instance)
- **Summary:** Returns a new vector with both components clamped to the same scalar range `[min, max]`.
- **Returns:** A new `Vector2F` with both X and Y clamped to `[min, max]`.

---

### Method: `DistanceTo(in Vector2F vector)`

- **Summary:** Computes the Euclidean distance from this vector to another vector.
- **Remarks:** Uses `DistanceSquaredTo` and applies `Math.Sqrt`. For relative comparisons, prefer `DistanceSquaredTo` to avoid the square root cost. Marked with `AggressiveInlining`.
- **Parameters:**
  - `vector` (`in Vector2F`): The target vector.
- **Returns:** The Euclidean distance as a `float`.

---

### Method: `DistanceSquaredTo(in Vector2F vector)`

- **Summary:** Computes the squared Euclidean distance, avoiding the square root for efficiency.
- **Remarks:** Formula: `(X - vector.X)² + (Y - vector.Y)²`. Preferred for nearest-object searches or radius checks. Marked with `AggressiveInlining`.
- **Parameters:**
  - `vector` (`in Vector2F`): The target vector.
- **Returns:** The squared Euclidean distance as a `float`.

---

### Method: `Dot` (Instance & Static)

#### `Dot(in Vector2F vector)` (Instance)
- **Summary:** Computes the dot product of this vector and another.
- **Remarks:** `X * vector.X + Y * vector.Y`. Measures projection and angle between vectors. Marked with `AggressiveInlining`.
- **Returns:** Scalar dot product.

#### `Dot(in Vector2F a, in Vector2F b)` (Static)
- **Summary:** Static version computing the dot product of two vectors.
- **Remarks:** The `in` modifier avoids copying for performance.
- **Returns:** Scalar dot product.

---

### Method: `Cross(in Vector2F vector)`

- **Summary:** Computes the 2D cross product (scalar): `X * vector.Y - Y * vector.X`. Represents the signed area of the parallelogram spanned by the two vectors.
- **Remarks:** Positive if `vector` is counter-clockwise from this vector; negative if clockwise; zero if collinear. Equivalent to the Z component of a 3D cross product. Marked with `AggressiveInlining`.
- **Returns:** A scalar representing the signed area.

---

### Method: `Project` (Instance & Static)

#### `Project(in Vector2F onto)` (Instance)
- **Summary:** Projects this vector onto another non-zero vector.
- **Remarks:** Formula: `((this · onto) / (onto · onto)) * onto`. Includes a debug assertion that the target is not approximately zero. Marked with `AggressiveInlining`.
- **Parameters:**
  - `onto` (`in Vector2F`): The vector to project onto.
- **Returns:** A new `Vector2F` representing the projection.

#### `Project(in Vector2F vector, in Vector2F onto)` (Static)
- **Summary:** Static version projecting one vector onto another.
- **Returns:** A new `Vector2F` representing the projection.

---

### Method: `Orthogonal()`

- **Summary:** Computes a vector orthogonal (perpendicular) to this vector: `(Y, -X)`.
- **Remarks:** Returns a clockwise 90° rotation (right-hand normal). For counter-clockwise, use `(-Y, X)`.

---

### Method: `Sign()`

- **Summary:** Returns a new vector where each component is the sign of the original (-1, 0, or 1).
- **Remarks:** Uses `Math.Sign`. Useful for extracting directional information. Marked with `AggressiveInlining`.
- **Returns:** A new `Vector2F` with signed components.

---

### Method: `Floor()`

- **Summary:** Returns a new vector with each component rounded down to the nearest integer toward negative infinity.
- **Remarks:** Uses `Math.Floor`. Example: (1.8, -0.3) → (1.0, -1.0). Useful for grid/tile operations. Marked with `AggressiveInlining`.
- **Returns:** A new `Vector2F` with floored components.

---

### Method: `Ceil()`

- **Summary:** Returns a new vector with each component rounded up to the nearest integer toward positive infinity.
- **Remarks:** Uses `Math.Ceiling`. Example: (1.2, -0.8) → (2.0, 0.0). Marked with `AggressiveInlining`.
- **Returns:** A new `Vector2F` with ceiled components.

---

### Method: `Round()`

- **Summary:** Returns a new vector with each component rounded to the nearest integer using `MidpointRounding.AwayFromZero`.
- **Remarks:** 1.5 → 2, -1.5 → -2. Marked with `AggressiveInlining`.
- **Returns:** A new `Vector2F` with rounded components.

---

### Method: `Rotate(float angle)`

- **Summary:** Rotates this vector by the specified angle (in radians) around the origin using the 2D rotation matrix.
- **Remarks:** `newX = X·cos(θ) - Y·sin(θ)`, `newY = X·sin(θ) + Y·cos(θ)`. Positive angle = counter-clockwise. Computes sin/cos once. Marked with `AggressiveInlining`.
- **Parameters:**
  - `angle` (`float`): Rotation angle in radians.
- **Returns:** A new `Vector2F` representing the rotated vector.

---

### Method: `IsEqualApproximate(Vector2F vector)`

- **Summary:** Determines whether this vector is approximately equal to another, using `Utils.IsEqualApproximate` per component.
- **Remarks:** Tolerant to floating-point rounding errors. Marked with `AggressiveInlining`.
- **Returns:** `true` if both components are approximately equal; otherwise `false`.

---

### Method: `IsZeroApproximate()`

- **Summary:** Checks whether this vector is approximately the zero vector.
- **Remarks:** Delegates to `Utils.IsZeroApproximate` for each component. Marked with `AggressiveInlining`.
- **Returns:** `true` if both components are approximately zero; otherwise `false`.

---

### Method: `IsSame(Vector2F other)`

- **Summary:** Determines whether this vector is exactly equal to another, using `Utils.IsSame` per component.
- **Remarks:** Strict comparison without tolerance. Use for caching or hashing. Marked with `AggressiveInlining`.
- **Returns:** `true` if both components are identical; otherwise `false`.

---

### Method: `Reflect(in Vector2F normal)`

- **Summary:** Computes the reflection of this vector about a given normal: `this - 2 · Dot(normal) · normal`.
- **Remarks:** Assumes the normal is a unit vector. Includes a debug assertion for normalization. Marked with `AggressiveInlining`.
- **Parameters:**
  - `normal` (`in Vector2F`): The normal vector (mirror axis).
- **Returns:** A new `Vector2F` representing the reflected direction.

---

### Method: `Bounce(in Vector2F normal)`

- **Summary:** Computes the bounce direction by negating the reflection: `-Reflect(normal)`.
- **Remarks:** Commonly used in collision response. Marked with `AggressiveInlining`.
- **Returns:** A new `Vector2F` representing the bounced direction.

---

### Method: `Slide(in Vector2F normal)`

- **Summary:** Projects this vector onto a plane defined by the normal, computing the component parallel to the surface: `this - Dot(normal) · normal`.
- **Remarks:** Used for sliding along walls. Requires normalized normal (debug-asserted). Marked with `AggressiveInlining`.
- **Returns:** A new `Vector2F` representing the tangential component.

---

### Method: `Normalize()`

- **Summary:** Returns a normalized copy (unit vector with length 1). If the vector is zero, NaN, or infinite, returns `Zero`.
- **Remarks:** Guards against near-zero vectors using `Constant.Epsilon` and checks for NaN/Infinity. Avoids division by zero. Marked with `AggressiveInlining`.
- **Returns:** A normalized `Vector2F`, or `Zero` if normalization is impossible.

---

### Method: `IsNormalized()` / `IsNormalized(in Vector2F vector)`

- **Summary:** Determines whether a vector is normalized (length ≈ 1).
- **Remarks:** Compares `LengthSquared` to 1 within `Constant.Epsilon` tolerance. The static overload accepts a vector by `in` reference.
- **Returns:** `true` if approximately normalized; otherwise `false`.

---

### Method: `MoveToward(Vector2F to, float delta)`

- **Summary:** Moves this vector towards a target by a maximum distance `delta`.
- **Remarks:** If within `delta` or negligible distance, returns the target directly. Otherwise advances by `delta` along the direction. Prevents overshoot.
- **Parameters:**
  - `to` (`Vector2F`): The target vector.
  - `delta` (`float`): Maximum distance to move (non-negative).
- **Returns:** A new `Vector2F` moved toward the target, or the target if reached.

---

### Method: `GetHashCode()`

- **Summary:** Computes a hash code using FNV-1a hash combination via `HashHelper`.
- **Returns:** A 32-bit integer hash code combining the hashes of X and Y.

---

### Method: `Equals(Vector2F other)` / `Equals(object? obj)`

- **Summary:** Determines equality by comparing X and Y components exactly.
- **Returns:** `true` if components match; otherwise `false`.

---

### Method: `ToString()`

- **Summary:** Returns a string representation in the format `"(X, Y)"`.
- **Returns:** A formatted string.