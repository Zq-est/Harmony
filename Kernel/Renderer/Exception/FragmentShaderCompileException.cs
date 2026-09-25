namespace Renderer.Exception;

public class FragmentShaderCompileException : ShaderCompileException
{
    public FragmentShaderCompileException(string message) 
        : base(message)
    {
    }

    public FragmentShaderCompileException(string message, System.Exception innerException) 
        : base(message, innerException)
    {
    }
}