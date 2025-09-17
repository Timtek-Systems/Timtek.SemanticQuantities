using Timtek.SemanticQuantities.Units;

namespace Timtek.SemanticQuantities.Units;

/// <summary>
/// Represents the SI base unit of length, the meter (symbol: m).
/// </summary>
/// <remarks>
/// Meter is the SI unit for distance/length, so conversions to and from SI are identity operations.
/// </remarks>
public class Meter : IUnit
{
    /// <summary>
    /// Converts a value in meters to the SI value.
    /// </summary>
    /// <param name="value">The value in meters.</param>
    /// <returns>The value expressed in SI units (meters).</returns>
    public double ToSI(double value) => value;

    /// <summary>
    /// Converts a value from SI (meters) to meters.
    /// </summary>
    /// <param name="siValue">The value in SI units (meters).</param>
    /// <returns>The value in meters.</returns>
    public double FromSI(double siValue) => siValue;
}
