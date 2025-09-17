using Timtek.SemanticQuantities.Dimensions;

namespace Timtek.SemanticQuantities.Units;

public sealed class Kilometer : IUnit
{
    public string             Name      => "Kilometer";
    public string             Symbol    => "km";
    public DimensionSignature Signature => DimensionSignature.Length;

    public double ToSI(double value) => value * 1000.0;
    public double FromSI(double siValue) => siValue / 1000.0;
}
