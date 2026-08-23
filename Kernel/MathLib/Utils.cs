namespace Kernel.MathLib;

public class Utils
{
    public static bool IsEqualApproximate(double a, double b, double epsilon)
    {
        if (a == b)
            return true;
        
        return Math.Abs(a - b) < epsilon;
    }
    
    public static bool IsEqualApproximate(float a, float b, float epsilon)
    {
        if (a == b)
            return true;
        
        return Math.Abs(a - b) < epsilon;
    }
    
    public static bool IsEqualApproximate(double a, double b) 
        => Math.Abs(a - b) < Constant.Epsilon;

    public static bool IsEqualApproximate(float a, float b) 
        => Math.Abs(a - b) < Constant.Epsilon;

    public static bool IsZeroApproximate(double value)
        => Math.Abs(value) < Constant.Epsilon;
    
    public static bool IsZeroApproximate(float value)
        => Math.Abs(value) < Constant.Epsilon;
    
    public static bool IsSame(float a, float b) 
        => Math.Abs(a - b) < Constant.Epsilon;
    
    public static bool IsSame(double a, double b) 
        => Math.Abs(a - b) < Constant.Epsilon;
    
    public static bool IsSame(int a, int b) 
        => a == b;
}