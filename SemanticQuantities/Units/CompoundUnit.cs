using Timtek.SemanticQuantities.Dimensions;

namespace Timtek.SemanticQuantities.Units;

/// <summary>
/// A unit representing a canonical coherent SI compound (product of SI base units with integer exponents).
/// Conversion to SI is identity.
/// </summary>
using Timtek.SemanticQuantities.Registry;

public sealed class CompoundUnit : IUnit
{
    public string Name { get; }
    public string Symbol { get; }
    public DimensionSignature Signature { get; }
    public UnitSystem System { get; }

    public CompoundUnit(string symbol, DimensionSignature signature, UnitSystem system = UnitSystem.SI)
    {
        Symbol = symbol;
        Name = symbol; // For unnamed compounds, reuse symbol as name
        Signature = signature;
        System = system;
    }

    public double ToSI(double value) => value;
    public double FromSI(double siValue) => siValue;
}
