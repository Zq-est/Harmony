namespace Renderer.Exception;

/// <summary>
/// Represents an exception that occurs during the compilation of a fragment shader.
/// </summary>
/// <example>
/// The following example demonstrates handling a <see cref="FragmentShaderCompileException"/> in a rendering context:
/// <code lang="csharp">
/// <![CDATA[
/// try
/// {
///     shaderCompiler.CompileFragmentShader(fragmentSource);
/// }
/// catch (FragmentShaderCompileException ex)
/// {
///     LogError($"Fragment shader compilation failed: {ex.Message}");
///     throw;
/// }
/// ]]>
/// </code>
/// </example>
public class FragmentShaderCompileException : ShaderCompileException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FragmentShaderCompileException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the fragment shader compilation error.</param>
    public FragmentShaderCompileException(string message) 
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FragmentShaderCompileException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public FragmentShaderCompileException(string message, System.Exception innerException) 
        : base(message, innerException)
    {
    }
}