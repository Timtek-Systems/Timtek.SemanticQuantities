using Timtek.SemanticQuantities.Dimensions;

namespace Timtek.SemanticQuantities.Units;

public sealed class Minute : IUnit
{
    public string             Name      => "Minute";
    public string             Symbol    => "min";
    public DimensionSignature Signature => DimensionSignature.Time;

    public double ToSI(double value) => value * 60.0;
    public double FromSI(double siValue) => siValue / 60.0;
}
