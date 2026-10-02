namespace Renderer.Exception;

/// <summary>
/// Represents an exception that occurs during the linking of shader programs.
/// </summary>
/// <example>
/// The following example demonstrates throwing a <see cref="ShaderLinkException"/> when program linking fails:
/// <code lang="csharp">
/// <![CDATA[
/// if (!LinkProgram(shaderProgram, out string linkError))
/// {
///     throw new ShaderLinkException($"Shader program linking failed: {linkError}");
/// }
/// ]]>
/// </code>
/// </example>
public class ShaderLinkException : System.Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ShaderLinkException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the shader linking error.</param>
    public ShaderLinkException(string message) 
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ShaderLinkException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public ShaderLinkException(string message, System.Exception innerException) 
        : base(message, innerException)
    {
    }
}