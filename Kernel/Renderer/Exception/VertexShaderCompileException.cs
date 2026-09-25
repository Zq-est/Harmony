namespace Renderer.Exception;

public class VertexShaderCompileException : ShaderCompileException
{
    public VertexShaderCompileException(string message)
        : base(message)
    {
    }

    public VertexShaderCompileException(string message, System.Exception innerException)
        : base(message, innerException)
    {
    }
}