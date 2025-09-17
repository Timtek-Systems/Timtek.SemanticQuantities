using Timtek.SemanticQuantities.Units;

namespace Timtek.SemanticQuantities.Quantities.Domain;

public static class Angle
{
    public static Quantity FromDegrees(double degrees) => new(degrees, new Degree());
    public static Quantity FromRadians(double radians) => new(radians, new Radian());
}
