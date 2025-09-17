using Timtek.SemanticQuantities.Dimensions;

namespace Timtek.SemanticQuantities.Units;

public sealed class Kilogram : IUnit
{
    public string Name => "Kilogram";
    public string Symbol => "kg";
    public DimensionSignature Signature => DimensionSignature.Mass;
    public double ToSI(double value) => value; // kg is SI base for mass
    public double FromSI(double siValue) => siValue;
}
