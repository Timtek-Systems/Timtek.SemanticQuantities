using Timtek.SemanticQuantities.Units;
using Timtek.SemanticQuantities.Dimensions;

namespace Timtek.SemanticQuantities.Quantities.Domain;

/// <summary>
/// Mass factories.
/// </summary>
public static class Mass
{
    public static Quantity FromKilograms(double kg) => new Quantity(kg, new Kilogram());
}

/// <summary>
/// Acceleration factories.
/// </summary>
public static class Acceleration
{
    public static Quantity FromMetersPerSecondSquared(double mps2)
        => new Quantity(mps2, new MeterPerSecondSquared());
}

/// <summary>
/// Force factories.
/// </summary>
public static class Force
{
    public static Quantity FromNewtons(double n) => new Quantity(n, new Newton());
}
