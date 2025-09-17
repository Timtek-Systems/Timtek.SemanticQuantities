using Timtek.SemanticQuantities.Dimensions;

namespace Timtek.SemanticQuantities.Units;

public sealed class JulianDay : IUnit
{
    public string             Name      => "Julian Day";
    public string             Symbol    => "d_julian";
    public DimensionSignature Signature => DimensionSignature.Time;

    public double ToSI(double value) => value * 86400.0;
    public double FromSI(double siValue) => siValue / 86400.0;
}
