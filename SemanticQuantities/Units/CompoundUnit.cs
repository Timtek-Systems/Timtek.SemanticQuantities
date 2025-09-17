using Timtek.SemanticQuantities.Dimensions;

namespace Timtek.SemanticQuantities.Units;

/// <summary>
/// A unit representing a canonical coherent SI compound (product of SI base units with integer exponents).
/// Conversion to SI is identity.
/// </summary>
public sealed class CompoundUnit : IUnit
{
    public string Name { get; }
    public string Symbol { get; }
    public DimensionSignature Signature { get; }

    public CompoundUnit(string symbol, DimensionSignature signature)
    {
        Symbol = symbol;
        Name = symbol; // For unnamed compounds, reuse symbol as name
        Signature = signature;
    }

    public double ToSI(double value) => value;
    public double FromSI(double siValue) => siValue;
}
