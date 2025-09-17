using Timtek.SemanticQuantities.Units;

namespace Timtek.SemanticQuantities.Quantities.Domain;

internal static class AngleNorm
{
    // Wrap x to [lo, hi)
    public static double Wrap(double x, double lo, double hi)
    {
        var w = hi - lo;
        var y = (x - lo) % w;
        if (y < 0) y += w;
        return lo + y;
    }

    // Clamp x to [lo, hi]
    public static double Clamp(double x, double lo, double hi)
        => Math.Min(Math.Max(x, lo), hi);
}

/// <summary>
///     Right Ascension, normalized to [0, 24h) i.e., [0, 2π) radians.
/// </summary>
/// <remarks>
///     Right Ascension (RA) is typically expressed in hours-of-time. This type stores RA internally in SI radians
///     and provides factory methods and accessors to work in hours, degrees, or radians. Values are automatically
///     wrapped to the canonical range [0, 24h) on creation.
/// </remarks>
public readonly struct RightAscension
{
    private readonly double _rad; // SI radians, normalized to [0, 2π)

    private RightAscension(double radians) => _rad = AngleNorm.Wrap(radians, 0.0, 2.0 * Math.PI);

    /// <summary>
    ///     Creates a RightAscension from hours-of-time.
    /// </summary>
    /// <param name="hours">Right ascension in hours (will be wrapped to [0, 24)).</param>
    public static RightAscension FromHours(double hours) => new(new HourOfAngle().ToSI(hours));

    /// <summary>
    ///     Creates a RightAscension from degrees.
    /// </summary>
    /// <param name="degrees">Right ascension in degrees (will be wrapped to [0°, 360°)).</param>
    public static RightAscension FromDegrees(double degrees) => new(new Degree().ToSI(degrees));

    /// <summary>
    ///     Creates a RightAscension from radians.
    /// </summary>
    /// <param name="radians">Right ascension in radians (will be wrapped to [0, 2π)).</param>
    public static RightAscension FromRadians(double radians) => new(radians);

    /// <summary>
    ///     Gets the right ascension value in radians.
    /// </summary>
    public double AsRadians() => _rad;

    /// <summary>
    ///     Gets the right ascension value in degrees.
    /// </summary>
    public double AsDegrees() => new Degree().FromSI(_rad);

    /// <summary>
    ///     Gets the right ascension value in hours-of-time.
    /// </summary>
    public double AsHours() => new HourOfAngle().FromSI(_rad);
}

/// <summary>
///     Hour Angle quantity with normalization helpers. Canonical storage is [0, 24h).
/// </summary>
/// <remarks>
///     Provides both unsigned ([0, 24h)) and signed ([-12h, +12h)) hour-angle views.
/// </remarks>
public readonly struct HourAngle
{
    private readonly double _rad; // SI radians, wrapped to [0, 2π)

    private HourAngle(double radians) => _rad = AngleNorm.Wrap(radians, 0.0, 2.0 * Math.PI);

    /// <summary>
    ///     Creates an hour-angle quantity from hours-of-time.
    /// </summary>
    /// <param name="hours">Hour angle in hours (wrapped to [0, 24)).</param>
    public static HourAngle FromHours(double hours) => new(new HourOfAngle().ToSI(hours));

    /// <summary>
    ///     Creates an hour-angle quantity from degrees.
    /// </summary>
    /// <param name="degrees">Hour angle in degrees (wrapped to [0°, 360°)).</param>
    public static HourAngle FromDegrees(double degrees) => new(new Degree().ToSI(degrees));

    /// <summary>
    ///     Creates an hour-angle quantity from radians.
    /// </summary>
    /// <param name="radians">Hour angle in radians (wrapped to [0, 2π)).</param>
    public static HourAngle FromRadians(double radians) => new(radians);

    /// <summary>
    ///     Returns the hour angle in radians.
    /// </summary>
    public double AsRadians() => _rad;

    /// <summary>
    ///     Returns the hour angle in degrees.
    /// </summary>
    public double AsDegrees() => new Degree().FromSI(_rad);

    /// <summary>
    ///     Returns the hour angle in hours, normalized to [0, 24).
    /// </summary>
    public double AsHoursUnwrapped() => new HourOfAngle().FromSI(_rad); // in [0, 24)

    /// <summary>
    ///     Returns the hour angle in signed hours, normalized to [-12, +12).
    /// </summary>
    public double AsSignedHours()
    {
        var h = AsHoursUnwrapped();
        return h >= 12.0 ? h - 24.0 : h;
    }
}

/// <summary>
///     Declination, clamped to [-90°, +90°].
/// </summary>
/// <remarks>
///     Declination is analogous to latitude on the celestial sphere. This type stores values in SI radians,
///     clamping input to the physical bounds of [-90°, +90°].
/// </remarks>
public readonly struct Declination
{
    private readonly double _rad; // SI radians, clamped to [-π/2, +π/2]

    private Declination(double radians) => _rad = AngleNorm.Clamp(radians, -Math.PI / 2.0, Math.PI / 2.0);

    /// <summary>
    ///     Creates a declination from degrees. Values are clamped to [-90°, +90°].
    /// </summary>
    /// <param name="degrees">Declination in degrees.</param>
    public static Declination FromDegrees(double degrees) => new(new Degree().ToSI(degrees));

    /// <summary>
    ///     Creates a declination from radians. Values are clamped to [-π/2, +π/2].
    /// </summary>
    /// <param name="radians">Declination in radians.</param>
    public static Declination FromRadians(double radians) => new(radians);

    /// <summary>
    ///     Gets the declination in radians.
    /// </summary>
    public double AsRadians() => _rad;

    /// <summary>
    ///     Gets the declination in degrees.
    /// </summary>
    public double AsDegrees() => new Degree().FromSI(_rad);
}

/// <summary>
///     Geodetic latitude, clamped to [-90°, +90°].
/// </summary>
/// <remarks>
///     Latitude represents the north-south position on a reference ellipsoid/planet. This type stores values in SI
///     radians,
///     clamping input to [-90°, +90°].
/// </remarks>
public readonly struct Latitude
{
    private readonly double _rad; // SI radians, clamped to [-π/2, +π/2]

    private Latitude(double radians) => _rad = AngleNorm.Clamp(radians, -Math.PI / 2.0, Math.PI / 2.0);

    /// <summary>
    ///     Creates a latitude from degrees. Values are clamped to [-90°, +90°].
    /// </summary>
    /// <param name="degrees">Latitude in degrees.</param>
    public static Latitude FromDegrees(double degrees) => new(new Degree().ToSI(degrees));

    /// <summary>
    ///     Creates a latitude from radians. Values are clamped to [-π/2, +π/2].
    /// </summary>
    /// <param name="radians">Latitude in radians.</param>
    public static Latitude FromRadians(double radians) => new(radians);

    /// <summary>
    ///     Gets the latitude in radians.
    /// </summary>
    public double AsRadians() => _rad;

    /// <summary>
    ///     Gets the latitude in degrees.
    /// </summary>
    public double AsDegrees() => new Degree().FromSI(_rad);
}

/// <summary>
///     Longitude, normalized to [-180°, +180°).
/// </summary>
/// <remarks>
///     Longitude represents the east-west position. This type stores values in SI radians and wraps input to the canonical
///     range [-180°, +180°). A convenience accessor returns the alternate [0°, 360°) representation.
/// </remarks>
public readonly struct Longitude
{
    private readonly double _rad; // SI radians, wrapped to [-π, +π)

    private Longitude(double radians) => _rad = AngleNorm.Wrap(radians, -Math.PI, Math.PI);

    /// <summary>
    ///     Creates a longitude from degrees (wrapped to [-180°, +180°)).
    /// </summary>
    /// <param name="degrees">Longitude in degrees.</param>
    public static Longitude FromDegrees(double degrees) => new(new Degree().ToSI(degrees));

    /// <summary>
    ///     Creates a longitude from radians (wrapped to [-π, +π)).
    /// </summary>
    /// <param name="radians">Longitude in radians.</param>
    public static Longitude FromRadians(double radians) => new(radians);

    /// <summary>
    ///     Gets the longitude in radians.
    /// </summary>
    public double AsRadians() => _rad;

    /// <summary>
    ///     Gets the longitude in degrees in the canonical [-180°, +180°) range.
    /// </summary>
    public double AsDegrees() => new Degree().FromSI(_rad);

    /// <summary>
    ///     Gets the longitude in degrees normalized to [0°, 360°).
    /// </summary>
    public double AsDegreesZeroTo360()
    {
        var deg = AsDegrees();
        return deg < 0.0 ? deg + 360.0 : deg;
    }
}