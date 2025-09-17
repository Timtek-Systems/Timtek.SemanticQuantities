
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class VelocityTests
{
    [TestMethod]
    public void ConvertKmhToMps()
    {
        var velocity = Velocity.FromKilometersPerHour(36);
        Assert.AreEqual(10.0, velocity.ValueSI, 1e-6);
    }
}
