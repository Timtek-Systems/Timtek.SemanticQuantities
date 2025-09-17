using Timtek.SemanticQuantities.Dimensions;

namespace Timtek.SemanticQuantities.Units;

/// <summary>
/// Represents the SI base unit of length, the meter (symbol: m).
/// </summary>
/// <remarks>
/// Meter is the SI unit for distance/length, so conversions to and from SI are identity operations.
/// </remarks>
public sealed class Meter : IUnit
{
    public string             Name     => "Meter";
    public string             Symbol   => "m";
    public DimensionSignature Signature => DimensionSignature.Length;

    public double ToSI(double value) => value;
    public double FromSI(double siValue) => siValue;
}
