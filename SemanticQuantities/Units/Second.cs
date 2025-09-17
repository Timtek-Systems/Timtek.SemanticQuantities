using Timtek.SemanticQuantities.Dimensions;

namespace Timtek.SemanticQuantities.Units;

public sealed class Second : IUnit
{
    public string             Name      => "Second";
    public string             Symbol    => "s";
    public DimensionSignature Signature => DimensionSignature.Time;

    public double ToSI(double value) => value;
    public double FromSI(double siValue) => siValue;
}
