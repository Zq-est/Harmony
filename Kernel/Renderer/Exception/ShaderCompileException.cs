namespace Renderer.Exception;

public class ShaderCompileException : ShaderException
{
    public ShaderCompileException(string message) 
        : base(message)
    {
    }

    public ShaderCompileException(string message, System.Exception innerException)
        : base(message, innerException)
    {
    }
}