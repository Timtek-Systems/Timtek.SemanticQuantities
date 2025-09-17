using Timtek.SemanticQuantities.Dimensions;

namespace Timtek.SemanticQuantities.Units;

public sealed class Degree : IUnit
{
    public string             Name      => "Degree";
    public string             Symbol    => "°";
    public DimensionSignature Signature => DimensionSignature.Angular;

    public double ToSI(double value) => value * Math.PI / 180.0;
    public double FromSI(double siValue) => siValue * 180.0 / Math.PI;
}
