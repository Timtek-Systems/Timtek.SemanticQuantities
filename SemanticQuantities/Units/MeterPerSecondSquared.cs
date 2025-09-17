using Timtek.SemanticQuantities.Dimensions;

namespace Timtek.SemanticQuantities.Units;

public sealed class MeterPerSecondSquared : IUnit
{
    public string Name => "Meter per Second Squared";
    public string Symbol => "m/s^2";
    public DimensionSignature Signature => new(1, 0, -2, 0, 0, 0, 0, 0);

    public double ToSI(double value) => value;
    public double FromSI(double siValue) => siValue;
}
