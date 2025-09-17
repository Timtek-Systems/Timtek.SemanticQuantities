
public static class Angle
{
    public static Quantity<Degree> FromDegrees(double degrees) => new Quantity<Degree>(degrees);
    public static Quantity<Radian> FromRadians(double radians) => new Quantity<Radian>(radians, isSI: true);
}
