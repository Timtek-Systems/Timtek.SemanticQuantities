namespace Timtek.SemanticQuantities.Units;

/// <summary>
///     Angle unit measured in astronomical hours-of-time.
/// </summary>
/// <remarks>
///     By convention in astronomy, 24 hours correspond to a full circle (360°), so 1 hour equals 15 degrees.
///     Therefore, 1 hour = 15° = π/12 radians. This unit is commonly used for right ascension and hour angle.
/// </remarks>
public sealed class HourAngle : IUnit
{
    /// <summary>
    ///     Converts an angle value from hour-angle units (hours of time) to SI radians.
    /// </summary>
    /// <param name="value">Angle in hours of time.</param>
    /// <returns>Angle in radians.</returns>
    public double ToSI(double value) => value * (Math.PI / 12.0); // hours -> radians

    /// <summary>
    ///     Converts an angle value from SI radians to hour-angle units (hours of time).
    /// </summary>
    /// <param name="siValue">Angle in radians.</param>
    /// <returns>Angle in hours of time.</returns>
    public double FromSI(double siValue) => siValue * (12.0 / Math.PI); // radians -> hours
}