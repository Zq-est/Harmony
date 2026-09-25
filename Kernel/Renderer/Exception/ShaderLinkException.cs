namespace Renderer.Exception;

public class ShaderLinkException : System.Exception
{
    public ShaderLinkException(string message) 
        : base(message)
    {
    }

    public ShaderLinkException(string message, System.Exception innerException) 
        : base(message, innerException)
    {
    }
}