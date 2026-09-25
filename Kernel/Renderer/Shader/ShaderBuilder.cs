using OpenTK.Graphics.OpenGL4;

namespace Renderer;

public class ShaderBuilder
{
    private int _vertexShader;
    private int _fragmentShader;

    public ShaderBuilder WithVertexShader(string vertexPath)
    {
        string vertexShaderSource = ShaderLoader.FromFiles(vertexPath);
        _vertexShader = ShaderCompiler.Compile(vertexShaderSource, ShaderType.VertexShader);
        return this;
    }

    public ShaderBuilder WithFragmentShader(string fragmentPath)
    {
        string fragmentShaderSource = ShaderLoader.FromFiles(fragmentPath);
        _fragmentShader = ShaderCompiler.Compile(fragmentShaderSource, ShaderType.FragmentShader);
        return this;
    }

    private Shader WithShader()
    {
        return new Shader(_vertexShader, _fragmentShader);
    }

    public Shader Build()
    {
        return WithShader();
    }

}

