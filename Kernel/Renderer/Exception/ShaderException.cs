namespace Renderer.Exception;

/// <summary>
/// Represents a base exception for shader-related errors in the rendering engine.
/// </summary>
/// <example>
/// The following example demonstrates how to catch a <see cref="ShaderException"/>:
/// <code lang="csharp">
/// <![CDATA[
/// try
/// {
///     // Code that might throw a shader exception
///     renderer.CompileShader(shaderCode);
/// }
/// catch (ShaderException ex)
/// {
///     Console.WriteLine($"Shader error: {ex.Message}");
/// }
/// ]]>
/// </code>
/// </example>
public class ShaderException : System.Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ShaderException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public ShaderException(string message) 
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ShaderException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public ShaderException(string message, System.Exception innerException) 
        : base(message, innerException)
    {
    }
}