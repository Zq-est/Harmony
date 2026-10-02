namespace Renderer.Exception;

/// <summary>
/// Represents an exception that occurs during the compilation of a vertex shader.
/// </summary>
/// <example>
/// The following example demonstrates throwing a <see cref="VertexShaderCompileException"/> with an inner exception:
/// <code lang="csharp">
/// <![CDATA[
/// try
/// {
///     CompileVertexShader(vertexSource);
/// }
/// catch (Exception ex)
/// {
///     throw new VertexShaderCompileException("Vertex shader compilation error.", ex);
/// }
/// ]]>
/// </code>
/// </example>
public class VertexShaderCompileException : ShaderCompileException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VertexShaderCompileException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the vertex shader compilation error.</param>
    public VertexShaderCompileException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="VertexShaderCompileException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public VertexShaderCompileException(string message, System.Exception innerException)
        : base(message, innerException)
    {
    }
}