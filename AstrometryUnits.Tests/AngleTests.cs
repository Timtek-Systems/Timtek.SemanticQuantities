
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class AngleTests
{
    [TestMethod]
    public void ConvertDegreesToRadians()
    {
        var angleDeg = Angle.FromDegrees(180);
        var radians = angleDeg.As<Radian>();
        Assert.AreEqual(Math.PI, radians, 1e-6);
    }
}
