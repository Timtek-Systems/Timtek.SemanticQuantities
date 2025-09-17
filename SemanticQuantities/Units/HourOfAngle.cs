namespace Timtek.SemanticQuantities.Units;

/// <summary>
///     Angle unit measured in hours-of-angle (astronomical hours on a 24h = 360° circle).
/// </summary>
/// <remarks>
///     By convention in astronomy, 24 hours of angle correspond to a full circle (360°), so 1 hour-of-angle equals 15 degrees.
///     Therefore, 1 h_angle = 15° = π/12 radians. This unit is commonly used for right ascension, hour angle, and sidereal time when expressed as an angle.
///     Note: This is a unit of Angle, distinct from the Time unit "hour".
/// </remarks>
public sealed class HourOfAngle : IUnit
{
    /// <summary>
    ///     Converts an angle value from hours-of-angle to SI radians.
    /// </summary>
    /// <param name="value">Angle in hours-of-angle.</param>
    /// <returns>Angle in radians.</returns>
    public double ToSI(double value) => value * (Math.PI / 12.0); // hours-of-angle -> radians

    /// <summary>
    ///     Converts an angle value from SI radians to hours-of-angle.
    /// </summary>
    /// <param name="siValue">Angle in radians.</param>
    /// <returns>Angle in hours-of-angle.</returns>
    public double FromSI(double siValue) => siValue * (12.0 / Math.PI); // radians -> hours-of-angle
}
