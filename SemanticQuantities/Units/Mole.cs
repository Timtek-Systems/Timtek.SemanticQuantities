using Timtek.SemanticQuantities.Dimensions;

namespace Timtek.SemanticQuantities.Units;

public sealed class Mole : IUnit
{
    public string Name => "Mole";
    public string Symbol => "mol";
    public DimensionSignature Signature => DimensionSignature.Amount;
    public double ToSI(double value) => value;
    public double FromSI(double siValue) => siValue;
}
