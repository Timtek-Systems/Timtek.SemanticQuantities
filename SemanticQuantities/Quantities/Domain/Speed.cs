using Timtek.SemanticQuantities.Units;

namespace Timtek.SemanticQuantities.Quantities.Domain;

public static class Speed
{
    public static Quantity FromMetersPerSecond(double   mps) => new(mps, new MeterPerSecond());
    public static Quantity FromKilometersPerHour(double kmh) => new(kmh * 1000.0 / 3600.0, new MeterPerSecond());
}
