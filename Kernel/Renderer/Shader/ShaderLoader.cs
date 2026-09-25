using OpenTK.Graphics.OpenGL4;

namespace Renderer;

public static class ShaderLoader
{
    public static string FromFiles(string shaderPath)
    {
        return File.ReadAllText(shaderPath);
    }
}