
public static class Angle
{
    public static Quantity<Degree> FromDegrees(double degrees) => new(degrees);
    public static Quantity<Radian> FromRadians(double radians) => new(radians, true);
}
