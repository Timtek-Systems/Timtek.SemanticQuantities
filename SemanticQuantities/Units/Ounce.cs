using Timtek.SemanticQuantities.Dimensions;

namespace Timtek.SemanticQuantities.Units;

public sealed class Ounce : IUnit
{
    public string Name => "Ounce";
    public string Symbol => "oz";
    public DimensionSignature Signature => DimensionSignature.Mass;
    public double ToSI(double value) => value * 0.028349523125; // avoirdupois ounce to kg
    public double FromSI(double siValue) => siValue / 0.028349523125;
}
