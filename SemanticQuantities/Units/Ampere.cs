using Timtek.SemanticQuantities.Dimensions;

namespace Timtek.SemanticQuantities.Units;

public sealed class Ampere : IUnit
{
    public string Name => "Ampere";
    public string Symbol => "A";
    public DimensionSignature Signature => DimensionSignature.ElectricCurrent;
    public double ToSI(double value) => value;
    public double FromSI(double siValue) => siValue;
}
