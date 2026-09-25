using OpenTK.Graphics.OpenGL4;

namespace Renderer.Buffers;

public class VertexArray : IDisposable
{
    public int Id { get; set; } = GL.GenVertexArray();

    public unsafe void ConfigureVertexAttributes<T>(int index, int size, VertexAttribPointerType type, bool normalized, int stride, int offset) where T : unmanaged
    {
        GL.VertexAttribPointer(index, size, type, normalized, stride * sizeof(T), offset * sizeof(T));
        GL.EnableVertexAttribArray(0);
    }
    
    public void Bind()
        => GL.BindVertexArray(Id);
    
    public void Unbind()
        => GL.BindVertexArray(0);

    private void ReleaseUnmanagedResources()
    {
        GL.DeleteVertexArray(Id);
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~VertexArray()
    {
        ReleaseUnmanagedResources();
    }
}