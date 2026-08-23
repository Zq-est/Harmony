namespace Kernel.MathLib;

public class HashHelper
{
    private const uint FnvOffsetBasis = 2166136261;
    private const uint FnvPrime = 16777619;

    /// <summary>
    /// Computes the FNV-1a hash for the given byte data.
    /// </summary>
    /// <remarks>
    /// This implementation uses the FNV-1a hash algorithm (Fowler–Noll–Vo),
    /// starting from the FNV offset basis and performing an XOR operation
    /// followed by multiplication with the FNV prime for each byte.
    /// The final result is cast to a signed 32-bit integer in an unchecked context,
    /// preventing arithmetic overflow exceptions.
    /// </remarks>
    /// <param name="data">The read-only span of bytes to hash.</param>
    /// <returns>The computed 32-bit signed integer hash value.</returns>
    public static int ComputeFnvHash(ReadOnlySpan<byte> data)
    {
        uint hash = FnvOffsetBasis;
        foreach (var b in data)
        {
            hash ^= b;
            hash *= FnvPrime;
        }

        unchecked
        {
            return (int)hash;
        }
    }

    /// <summary>
    /// Computes the FNV-1a hash of the byte representation of a single-precision floating-point number.
    /// </summary>
    /// <param name="data">The float value to hash.</param>
    /// <returns>A 32-bit signed integer representing the FNV hash of the float's underlying bytes.</returns>
    public static int ComputeFnvHash(float data)
    {
        // Convert the float to its 4-byte representation and delegate to the byte array overload
        return ComputeFnvHash(BitConverter.GetBytes(data));
    }

    /// <summary>
    /// Combines two integer hash values into a new hash value.
    /// Uses core steps of the FNV-1a algorithm: XOR the two hashes,
    /// then multiply by the FNV prime constant (<see cref="FnvPrime"/>),
    /// mixing their bit distributions for a more uniform composite result.
    /// </summary>
    /// <param name="hash1">The first hash value.</param>
    /// <param name="hash2">The second hash value.</param>
    /// <returns>The combined hash value (32-bit signed integer).</returns>
    /// <remarks>
    /// This method is commonly used in custom <see cref="object.GetHashCode"/>
    /// implementations to combine hash codes of multiple fields sequentially.
    /// Since multiplication may overflow, the entire computation is performed
    /// inside an <c>unchecked</c> block to ignore overflow exceptions and
    /// retain only the lower 32 bits — a standard practice in hashing.
    /// </remarks>
    public static int Combine(int hash1, int hash2)
    {
        uint hash = (uint)hash1;
        hash ^= (uint)hash2;          // XOR the two hash values together
        hash *= FnvPrime;             // Multiply by the FNV prime to spread bit patterns
        unchecked
        {
            return (int)hash;         // Convert unsigned result back to signed integer
        }
    }

    /// <summary>
    /// Combines three hash codes using the FNV-1a hashing algorithm.
    /// </summary>
    /// <param name="hash1">The first hash code.</param>
    /// <param name="hash2">The second hash code.</param>
    /// <param name="hash3">The third hash code.</param>
    /// <returns>A combined hash code as a signed 32-bit integer.</returns>
    public static int Combine(int hash1, int hash2, int hash3)
    {
        // Start with the first hash, cast to unsigned for bitwise operations
        uint hash = (uint)hash1;

        // XOR with the second hash, then multiply by the FNV prime
        hash ^= (uint)hash2;
        hash *= FnvPrime;

        // XOR with the third hash, then multiply by the FNV prime again
        hash ^= (uint)hash3;
        hash *= FnvPrime;

        // Return the final hash as a signed integer (overflow is allowed)
        unchecked
        {
            return (int)hash;
        }
    }
    
    /// <summary>
    /// Combines four hash codes using the FNV-1a hashing algorithm.
    /// </summary>
    /// <param name="hash1">The first hash code.</param>
    /// <param name="hash2">The second hash code.</param>
    /// <param name="hash3">The third hash code.</param>
    /// <param name="hash4">The fourth hash code.</param>
    /// <returns>A combined hash code as a signed 32-bit integer.</returns>
    public static int Combine(int hash1, int hash2, int hash3, int hash4)
    {
        // Initialize with the first hash value, cast to unsigned for bitwise operations
        uint hash = (uint)hash1;

        // XOR with the second hash, then multiply by the FNV prime
        hash ^= (uint)hash2;
        hash *= FnvPrime;

        // XOR with the third hash, then multiply by the FNV prime
        hash ^= (uint)hash3;
        hash *= FnvPrime;

        // XOR with the fourth hash, then multiply by the FNV prime
        hash ^= (uint)hash4;
        hash *= FnvPrime;

        // Return the result as a signed integer, allowing overflow
        unchecked
        {
            return (int)hash;
        }
    }
}