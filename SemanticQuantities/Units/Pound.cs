using Timtek.SemanticQuantities.Dimensions;

namespace Timtek.SemanticQuantities.Units;

public sealed class Pound : IUnit
{
    public string Name => "Pound";
    public string Symbol => "lb";
    public DimensionSignature Signature => DimensionSignature.Mass;
    public double ToSI(double value) => value * 0.45359237; // pounds mass to kg
    public double FromSI(double siValue) => siValue / 0.45359237;
}
