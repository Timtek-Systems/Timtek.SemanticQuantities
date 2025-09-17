using Timtek.SemanticQuantities.Dimensions;

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
    public string             Name      => "Hour of Angle";
    public string             Symbol    => "h_angle";
    public DimensionSignature Signature => DimensionSignature.Angular;

    public double ToSI(double value) => value * (Math.PI / 12.0); // hours-of-angle -> radians
    public double FromSI(double siValue) => siValue * (12.0 / Math.PI); // radians -> hours-of-angle
}
