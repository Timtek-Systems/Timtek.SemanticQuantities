using Timtek.SemanticQuantities.Dimensions;

namespace Timtek.SemanticQuantities.Units;

public sealed class MeterPerSecond : IUnit
{
    public string             Name      => "Meter per Second";
    public string             Symbol    => "m/s";
    public DimensionSignature Signature => new(1, 0, -1, 0, 0, 0, 0, 0);

    public double ToSI(double value) => value;
    public double FromSI(double siValue) => siValue;
}
