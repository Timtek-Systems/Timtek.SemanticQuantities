using Timtek.SemanticQuantities.Dimensions;

namespace Timtek.SemanticQuantities.Units;

/// <summary>
/// Defines a physical unit with conversion to and from coherent SI, dimensional signature, and display metadata.
/// </summary>
public interface IUnit
{
    /// <summary>
    /// Human-friendly unit name (singular), e.g. "Meter", "Second", "Newton".
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Unit symbol for formatting, e.g. "m", "s", "N".
    /// </summary>
    string Symbol { get; }

    /// <summary>
    /// The dimensional signature for this unit (e.g., L=1, T=-2 for m/s^2).
    /// Angle is treated as a distinct base dimension.
    /// </summary>
    DimensionSignature Signature { get; }

    /// <summary>
    /// Converts a value in this unit to coherent SI value.
    /// SI_value = a * value + b (affine), but current implementations may use only scale (b = 0).
    /// </summary>
    double ToSI(double value);

    /// <summary>
    /// Converts a value from coherent SI to this unit.
    /// value = (SI_value - b) / a
    /// </summary>
    double FromSI(double siValue);
}
