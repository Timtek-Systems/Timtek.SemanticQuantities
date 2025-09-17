namespace Timtek.SemanticQuantities.Quantities.Domain;

public readonly struct Direction
{
    public double X { get; }
    public double Y { get; }
    public double Z { get; }

    private Direction(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static Direction FromXYZ(double x, double y, double z)
    {
        var mag = Math.Sqrt(x * x + y * y + z * z);
        if (mag == 0) throw new ArgumentException("Direction vector cannot be zero.");
        return new Direction(x / mag, y / mag, z / mag);
    }
}