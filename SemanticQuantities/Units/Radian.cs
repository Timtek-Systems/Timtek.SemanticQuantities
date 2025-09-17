using Timtek.SemanticQuantities.Dimensions;

namespace Timtek.SemanticQuantities.Units;

public sealed class Radian : IUnit
{
    public string             Name      => "Radian";
    public string             Symbol    => "rad";
    public DimensionSignature Signature => DimensionSignature.Angular;

    public double ToSI(double value) => value;
    public double FromSI(double siValue) => siValue;
}
