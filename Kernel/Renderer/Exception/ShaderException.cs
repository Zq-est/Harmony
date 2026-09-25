namespace Renderer.Exception;

public class ShaderException : System.Exception
{
    public ShaderException(string message) 
        : base(message)
    {
    }

    public ShaderException(string message, System.Exception innerException) 
        : base(message, innerException)
    {
    }
}