namespace Renderer.Exception;

/// <summary>
/// Represents an exception that occurs during shader compilation.
/// </summary>
/// <example>
/// The following example demonstrates throwing a <see cref="ShaderCompileException"/> when compilation fails:
/// <code lang="csharp">
/// <![CDATA[
/// if (!CompileShader(source, out string errorLog))
/// {
///     throw new ShaderCompileException($"Failed to compile shader: {errorLog}");
/// }
/// ]]>
/// </code>
/// </example>
public class ShaderCompileException : ShaderException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ShaderCompileException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the compilation error.</param>
    public ShaderCompileException(string message) 
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ShaderCompileException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public ShaderCompileException(string message, System.Exception innerException)
        : base(message, innerException)
    {
    }
}