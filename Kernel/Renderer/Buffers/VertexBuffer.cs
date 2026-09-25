using OpenTK.Graphics.OpenGL4;

namespace Renderer.Buffers;

public class VertexBuffer : IDisposable
{
    public int Id { get; set; } = GL.GenBuffer();

    public unsafe void UploadVertexData<T>(T[] data, BufferUsageHint usageHint) where T : unmanaged
    {
        Bind();
        GL.BufferData(BufferTarget.ArrayBuffer, (int)(data.Length * sizeof(T)), data, usageHint);
    }
    
    public void Bind()
        => GL.BindBuffer(BufferTarget.ArrayBuffer, Id);

    public void Unbind()
        => GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

    private void ReleaseUnmanagedResources()
    {
        GL.DeleteBuffer(Id);
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~VertexBuffer()
    {
        ReleaseUnmanagedResources();
    }
}