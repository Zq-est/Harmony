using Kernel.MathLib;

namespace Tests;

[TestClass]
public class KernelVector2FTest
{
    private Vector2F _vector2F1;
    private Vector2F _vector2F2;
    private Vector2F _vector2F3;
    private Vector2F _vector2F4;
    private Vector2F _vector2F5;
    private Vector2F _vector2F6;
    private Vector2F _vector2F7;

    private Vector2F _origin;

    [TestInitialize]
    public void KernelVector2FTestInitialize()
    {
        _vector2F1 = new Vector2F(1, 2);
        _vector2F2 = new Vector2F(3, 4);
        _vector2F3 = new Vector2F(5, 6);
        _vector2F4 = new Vector2F(-1, -2);
        _vector2F5 = new Vector2F(-3, -4);
        _vector2F6 = new Vector2F(-5, -6);
        _vector2F7 = new Vector2F(-2, 2);

        _origin = new Vector2F(0, 0);
    }

    [TestMethod]
    public void KernelVector2FConstructorDefaultTest()
    {
        var vector2F = new Vector2F();
        Assert.AreEqual(0, vector2F.X);
        Assert.AreEqual(0, vector2F.Y);
    }

    [TestMethod]
    public void KernelVector2FConstructorTest()
    {
        Assert.AreEqual(1, _vector2F1.X);
        Assert.AreEqual(2, _vector2F1.Y);
    }

    [TestMethod]
    public void KernelVector2FConstructorNanTest()
    {
        Assert.Throws<ArgumentException>(() => new Vector2F(float.NaN, float.NaN));
    }

    [TestMethod]
    public void KernelVector2FConstructorInfinityTest()
    {
        Assert.Throws<ArgumentException>(() => new Vector2F(float.PositiveInfinity, float.PositiveInfinity));
    }

    [TestMethod]
    public void KernelVector2FAddTest()
    {
        var vector2F = _vector2F1 + _vector2F2;
        Assert.AreEqual(4, vector2F.X);
        Assert.AreEqual(6, vector2F.Y);
    }

    [TestMethod]
    public void KernelVector2FSubTest()
    {
        var vector2F = _vector2F2 - _vector2F1;
        Assert.AreEqual(2, vector2F.X);
        Assert.AreEqual(2, vector2F.Y);
    }

    [TestMethod]
    public void KernelVector2FMulOfScalarTest()
    {
        var vector2F = _vector2F2 * 2;
        Assert.AreEqual(6, vector2F.X);
        Assert.AreEqual(8, vector2F.Y);
    }

    [TestMethod]
    public void KernelVector2FMulOfProductTest()
    {
        var vector2F = _vector2F1 * _vector2F2;
        Assert.AreEqual(11, vector2F);
    }

    [TestMethod]
    public void KernelVector2FDivTest()
    {
        var vector2F = _vector2F2 / 2;
        Assert.AreEqual(1.5f, vector2F.X);
        Assert.AreEqual(2, vector2F.Y);
    }

    [TestMethod]
    public void KernelVector2FDivOfZeroTest()
    {
        Assert.Throws<DivideByZeroException>(() => _vector2F2 / 0);
    }
    
    [TestMethod]
    public void KernelVector2FToAngleTest1()
    {
        var vector2F = new Vector2F(1, 0);
        Assert.AreEqual(0, vector2F.ToAngle());
    }

    [TestMethod]
    public void KernelVector2FToAngleTest2()
    {
        var vector2F = new Vector2F(1, 1);
        Assert.AreEqual(Constant.Pi / 4, vector2F.ToAngle(), Constant.Epsilon);
    }

    [TestMethod]
    public void KernelVector2FToAngleTest3()
    {
        var vector2F = new Vector2F(0, 1);
        Assert.AreEqual(Constant.Pi / 2, vector2F.ToAngle(), Constant.Epsilon);
    }

    public void KernelVector2FToAngleTest4()
    {
        var vector2F = new Vector2F(-1, 1);
        Assert.AreEqual(Constant.Pi * 3 / 4, vector2F.ToAngle(), Constant.Epsilon);
    }

    public void KernelVector2FToAngleTest5()
    {
        var vector2F = new Vector2F(-1, 0);
        Assert.AreEqual(Constant.Pi, vector2F.ToAngle(), Constant.Epsilon);
    }

    [TestMethod]
    public void KernelVector2FToAngleTest6()
    {
        var vector2F = new Vector2F(-1, -1);
        Assert.AreEqual(-Constant.Pi * 3 / 4, vector2F.ToAngle(), Constant.Epsilon);
    }

    [TestMethod]
    public void KernelVector2FToAngleTest7()
    {
        var vector2F = new Vector2F(0, -1);
        Assert.AreEqual(-Constant.Pi / 2, vector2F.ToAngle(), Constant.Epsilon);
    }

    [TestMethod]
    public void KernelVector2FToAngleTest8()
    {
        var vector2F = new Vector2F(1, -1);
        Assert.AreEqual(-Constant.Pi / 4, vector2F.ToAngle(), Constant.Epsilon);
    }
    
    [TestMethod]
    public void KernelVector2FFromAngleTest1()
    {
        var vector2F = Vector2F.FromAngleRadian(0);
        Assert.AreEqual(1, vector2F.X);
        Assert.AreEqual(0, vector2F.Y);
    }

    [TestMethod]
    public void KernelVector2FFromAngleTest2()
    {
        var vector2F = Vector2F.FromAngleRadian(Constant.Pi / 4);
        Assert.AreEqual(Constant.Sqrt2 / 2, vector2F.X, Constant.Epsilon);
        Assert.AreEqual(Constant.Sqrt2 / 2, vector2F.Y, Constant.Epsilon);
    }

    [TestMethod]
    public void KernelVector2FFromAngleTest3()
    {
        var vector2F = Vector2F.FromAngleRadian(Constant.Pi / 2);
        Assert.AreEqual(0, vector2F.X, Constant.Epsilon);
        Assert.AreEqual(1, vector2F.Y, Constant.Epsilon);
    }

    [TestMethod]
    public void KernelVector2FFromAngleTest4()
    {
        var vector2F = Vector2F.FromAngleRadian(Constant.Pi * 3 / 4);
        Assert.AreEqual(-Constant.Sqrt2 / 2, vector2F.X, Constant.Epsilon);
        Assert.AreEqual(Constant.Sqrt2 / 2, vector2F.Y, Constant.Epsilon);
    }

    [TestMethod]
    public void KernelVector2FFromAngleTest5()
    {
        var vector2F = Vector2F.FromAngleRadian(Constant.Pi);
        Assert.AreEqual(-1, vector2F.X, Constant.Epsilon);
        Assert.AreEqual(0, vector2F.Y, Constant.Epsilon);
    }

    [TestMethod]
    public void KernelVector2FFromAngleTest6()
    {
        var vector2F = Vector2F.FromAngleRadian(-Constant.Pi * 3 / 4);
        Assert.AreEqual(-Constant.Sqrt2 / 2, vector2F.X, Constant.Epsilon);
        Assert.AreEqual(-Constant.Sqrt2 / 2, vector2F.Y, Constant.Epsilon);
    }

    [TestMethod]
    public void KernelVector2FFromAngleTest7()
    {
        var vector2F = Vector2F.FromAngleRadian(-Constant.Pi / 2);
        Assert.AreEqual(0, vector2F.X, Constant.Epsilon);
        Assert.AreEqual(-1, vector2F.Y, Constant.Epsilon);
    }

    [TestMethod]
    public void KernelVector2FFromAngleTest8()
    {
        var vector2F = Vector2F.FromAngleRadian(-Constant.Pi / 4);
        Assert.AreEqual(Constant.Sqrt2 / 2, vector2F.X, Constant.Epsilon);
        Assert.AreEqual(-Constant.Sqrt2 / 2, vector2F.Y, Constant.Epsilon);
    }

    [TestMethod]
    public void KernelVector2FAbsTest()
    {
        var vector2F = _vector2F4.Abs();
        Assert.AreEqual(1, vector2F.X);
        Assert.AreEqual(2, vector2F.Y);
    }

    [TestMethod]
    public void KernelVector2FMinTest()
    {
        var vector2F = _vector2F1.Min(_vector2F7);
        Assert.AreEqual(-2, vector2F.X);
        Assert.AreEqual(2, vector2F.Y);
    }

    [TestMethod]
    public void KernelVector2FClamMinComponentsWithVectorTest()
    {
        var vector2F = _vector2F1.ClampMinComponents(_vector2F7);
        Assert.AreEqual(1, vector2F.X);
        Assert.AreEqual(2, vector2F.Y);
        
        vector2F = _vector2F7.ClampMinComponents(_vector2F1);
        Assert.AreEqual(1, vector2F.X);
        Assert.AreEqual(2, vector2F.Y);
    }

    [TestMethod]
    public void KernelVector2FClamMinComponentsWithVectorAndMinTest()
    {
        var vector2F = Vector2F.ClampMinComponents(_vector2F1, 1);
        Assert.AreEqual(1, vector2F.X);
        Assert.AreEqual(2, vector2F.Y);
        
        vector2F = Vector2F.ClampMinComponents(_vector2F1, 2);
        Assert.AreEqual(2, vector2F.X);
        Assert.AreEqual(2, vector2F.Y);
    }
    
    // TODO: add clampMaxComponents Test

    [TestMethod]
    public void KernelVector2FDistanceToTest()
    {
        var vector2F1 = new Vector2F(1, 2);
        var vector2F2 = new Vector2F(4, 6);
        Assert.AreEqual(5, vector2F1.DistanceTo(vector2F2));
    }

    [TestMethod]
    public void KernelVector2FDistanceSquaredToTest()
    {
        var vector2F1 = new Vector2F(1, 2);
        var vector2F2 = new Vector2F(4, 6);
        Assert.AreEqual(25, vector2F2.DistanceSquaredTo(vector2F1));
    }

    [TestMethod]
    public void KernelVector2FDotTest()
    {
        var vector2F = _vector2F1.Dot(_vector2F2);
        Assert.AreEqual(11, vector2F);
    }

    [TestMethod]
    public void KernelVector2FCrossTest()
    {
        var vector2F = _vector2F1.Cross(_vector2F2);
        Assert.AreEqual(-2, vector2F);
    }
    
    // TODO: Add Rotate IsEqualApproximate IsZeroApproximate IsSame Reflect Bounce Slide Clamp Test
    
    [TestMethod]
    public void KernelVector2FIsEqualApproximateTest()
    {
        var v1 = new Vector2F(1.0f, 2.0f);
        var v2 = new Vector2F(1.0f + (float)Constant.Epsilon / 2, 2.0f);
        var v3 = new Vector2F(1.1f, 2.0f);
        Assert.IsTrue(v1.IsEqualApproximate(v2));
        Assert.IsFalse(v1.IsEqualApproximate(v3));
    }
    
    [TestMethod]
    public void KernelVector2FIsZeroApproximateTest()
    {
        Assert.IsTrue(_origin.IsZeroApproximate());
        Assert.IsFalse(_vector2F1.IsZeroApproximate());
    }

    [TestMethod]
    public void KernelVector2FNormalizeTest()
    {
        var vector2F = _vector2F4.Normalize();
        Assert.AreEqual(0.4472135954999579, vector2F.X);
        Assert.AreEqual(0.8944271909999159, vector2F.Y);
    }

    [TestMethod]
    public void KernelVector2FIsNormalizeTest()
    {
        var vector2F = _vector2F4.Normalize();
        Assert.IsTrue(vector2F.IsNormalized());
    }
}