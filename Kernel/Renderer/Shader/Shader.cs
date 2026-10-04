using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Renderer.Exception;

namespace Renderer;

public class Shader : IDisposable
{
    public readonly int Id;
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

    public int GetUniformLocation(string name) => GL.GetUniformLocation(Id, name);
    public void SetUniform(int location, float value) => GL.Uniform1(location, value);
    public void SetUniform(int location, double value) => GL.Uniform1(location, value);
    public void SetUniform(int location, int value) => GL.Uniform1(location, value);
    public void SetUniform(int location, Vector2 value) => GL.Uniform2(location, value);
    
    public void SetUniform(int location, Vector3 value) => GL.Uniform3(location, value);
    public void SetUniform(int location, Vector4 value) => GL.Uniform4(location, value);
    public void SetUniform(int location, Matrix4 value) => GL.UniformMatrix4(location, true, ref value);
    
    
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