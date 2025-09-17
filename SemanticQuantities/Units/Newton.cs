using Timtek.SemanticQuantities.Dimensions;

namespace Timtek.SemanticQuantities.Units;

public sealed class Newton : IUnit
{
    public string Name => "Newton";
    public string Symbol => "N";
    // N = kg·m·s^-2 => [L=1, M=1, T=-2]
    public DimensionSignature Signature => new(1, 1, -2, 0, 0, 0, 0, 0);

    public double ToSI(double value) => value;
    public double FromSI(double siValue) => siValue;
}
