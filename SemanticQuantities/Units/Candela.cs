using Timtek.SemanticQuantities.Dimensions;

namespace Timtek.SemanticQuantities.Units;

public sealed class Candela : IUnit
{
    public string Name => "Candela";
    public string Symbol => "cd";
    public DimensionSignature Signature => DimensionSignature.LuminousIntensity;
    public double ToSI(double value) => value;
    public double FromSI(double siValue) => siValue;
}
