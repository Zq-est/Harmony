using OpenTK.Graphics.OpenGL4;
using Renderer.Exception;

namespace Renderer;

public class Shader : IDisposable
{
    private int Id { get; set; }
    private int VertexShader { get; set; }
    private int FragmentShader { get; set; }

    public Shader(int vertexShader, int fragmentShader)
    {
        VertexShader = vertexShader;
        FragmentShader = fragmentShader;
        
        Id = GL.CreateProgram();
        GL.AttachShader(Id, VertexShader);
        GL.AttachShader(Id, FragmentShader);
        GL.LinkProgram(Id);
        
        GL.GetProgram(Id, GetProgramParameterName.LinkStatus, out int status);
        CheckProgramStatus(status);
        
        GL.DetachShader(Id, VertexShader);
        GL.DetachShader(Id, FragmentShader);
    }
    
    public void Use() => GL.UseProgram(Id);

    private void CheckProgramStatus(int status)
    {
        if (status == 0)
            throw new ShaderLinkException($"Shader linking failed: {GL.GetProgramInfoLog(Id)}");
    }
    
    private void ReleaseUnmanagedResources()
    {
        GL.DeleteShader(VertexShader);
        GL.DeleteShader(FragmentShader);
        GL.DeleteProgram(Id);
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~Shader()
    {
        ReleaseUnmanagedResources();
    }
}