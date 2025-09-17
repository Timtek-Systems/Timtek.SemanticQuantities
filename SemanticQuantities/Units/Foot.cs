using Timtek.SemanticQuantities.Dimensions;

namespace Timtek.SemanticQuantities.Units;

public sealed class Foot : IUnit
{
    public string Name => "Foot";
    public string Symbol => "ft";
    public DimensionSignature Signature => DimensionSignature.Length;
    public double ToSI(double value) => value * 0.3048;
    public double FromSI(double siValue) => siValue / 0.3048;
}
