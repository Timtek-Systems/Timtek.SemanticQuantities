using Timtek.SemanticQuantities.Dimensions;

namespace Timtek.SemanticQuantities.Units;

public sealed class Day : IUnit
{
    public string             Name      => "Day";
    public string             Symbol    => "d";
    public DimensionSignature Signature => DimensionSignature.Time;

    public double ToSI(double value) => value * 86400.0;
    public double FromSI(double siValue) => siValue / 86400.0;
}
