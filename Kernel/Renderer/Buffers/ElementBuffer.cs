using OpenTK.Graphics.OpenGL4;

namespace Renderer.Buffers;

public class ElementBuffer : IDisposable
{
    public int Id { get; private set; } = GL.GenBuffer();
    
    public void Bind() => GL.BindBuffer(BufferTarget.ElementArrayBuffer, Id);
    
    public void Unbind() => GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
    
    public void UploadElementData(uint[] indices, BufferUsageHint usage = BufferUsageHint.StaticDraw)
    {
        Bind();
        GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, usage);
    }

    private void ReleaseUnmanagedResources()
    {
        GL.DeleteBuffer(Id);
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~ElementBuffer()
    {
        ReleaseUnmanagedResources();
    }
}