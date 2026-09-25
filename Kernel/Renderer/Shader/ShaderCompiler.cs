using OpenTK.Graphics.OpenGL4;
using Renderer.Exception;

namespace Renderer;

internal class ShaderCompiler
{
    public static int Compile(string shaderSource, ShaderType shaderType)
    {
        int shader = GL.CreateShader(shaderType);
        GL.ShaderSource(shader, shaderSource);
        GL.CompileShader(shader);
        
        GL.GetShader(shader, ShaderParameter.CompileStatus, out var statue);
        CheckCompileStatus(shader, statue, shaderType);

        return shader;
    }

    private static void CheckCompileStatus(int shader, int statue, ShaderType shaderType)
    {
        if (statue == 0 && shaderType == ShaderType.VertexShader)
        {
            string info = GL.GetShaderInfoLog(statue);
            GL.DeleteShader(shader);
            throw new VertexShaderCompileException($"Failed to compile {statue}: {info}");
        }

        if (statue == 0 && shaderType == ShaderType.FragmentShader)
        {
            string info = GL.GetShaderInfoLog(statue);
            GL.DeleteShader(shader);
            throw new FragmentShaderCompileException($"Failed to compile {statue}: {info}");
        }
    }
}