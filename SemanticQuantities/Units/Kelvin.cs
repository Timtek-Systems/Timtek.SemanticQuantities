using Timtek.SemanticQuantities.Dimensions;

namespace Timtek.SemanticQuantities.Units;

public sealed class Kelvin : IUnit
{
    public string Name => "Kelvin";
    public string Symbol => "K";
    public DimensionSignature Signature => DimensionSignature.Temperature;
    public double ToSI(double value) => value;
    public double FromSI(double siValue) => siValue;
}
