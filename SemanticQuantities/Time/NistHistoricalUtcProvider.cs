namespace Timtek.SemanticQuantities.Time;

/// <summary>
///     Leap seconds provider implementing full UTC semantics using NIST reference data:
///     - For dates on/after 1972-01-01T00:00:00Z, uses the stepwise leap-second table (via NistLeapSecondsProvider).
///     - For dates from 1961-01-01 up to (but not including) 1972-01-01, applies the NIST piecewise-linear formulas.
/// </summary>
/// <remarks>
///     For dates prior to 1961-01-01, UTC definitions were not standardized in this form and are not supported by this
///     provider.
/// </remarks>
public sealed class NistHistoricalUtcProvider : ILeapSecondsProvider
{
    private static readonly DateTime ModernEpoch       = new(1972, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    private static readonly DateTime EarliestSupported = new(1961, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    private readonly NistLeapSecondsProvider _modern = new();

    public double TaiMinusUtcSeconds(DateTime utc)
    {
        if (utc.Kind != DateTimeKind.Utc)
            throw new ArgumentException("utc must be specified in UTC", nameof(utc));

        if (utc >= ModernEpoch)
            return _modern.TaiMinusUtcSeconds(utc);

        if (utc < EarliestSupported)
            throw new NotSupportedException("NistHistoricalUtcProvider supports 1961-01-01 and later.");

        // Compute MJD for the UTC instant
        var mjd = ToModifiedJulianDate(utc);

        // Apply NIST piecewise linear definitions (TAI - UTC in seconds)
        if (utc < new DateTime(1961, 8, 1, 0, 0, 0, DateTimeKind.Utc))
            return 1.4228180 + 0.0012960 * (mjd - 37300.0);
        if (utc < new DateTime(1962, 1, 1, 0, 0, 0, DateTimeKind.Utc))
            return 1.3728180 + 0.0012960 * (mjd - 37300.0);
        if (utc < new DateTime(1963, 11, 1, 0, 0, 0, DateTimeKind.Utc))
            return 1.8458580 + 0.0011232 * (mjd - 37665.0);
        if (utc < new DateTime(1964, 1, 1, 0, 0, 0, DateTimeKind.Utc))
            return 1.9458580 + 0.0011232 * (mjd - 37665.0);
        if (utc < new DateTime(1964, 4, 1, 0, 0, 0, DateTimeKind.Utc))
            return 3.2401300 + 0.0012960 * (mjd - 38165.0);
        if (utc < new DateTime(1964, 9, 1, 0, 0, 0, DateTimeKind.Utc))
            return 3.3401300 + 0.0012960 * (mjd - 38165.0);
        if (utc < new DateTime(1965, 1, 1, 0, 0, 0, DateTimeKind.Utc))
            return 3.4401300 + 0.0012960 * (mjd - 38165.0);
        if (utc < new DateTime(1965, 3, 1, 0, 0, 0, DateTimeKind.Utc))
            return 3.5401300 + 0.0012960 * (mjd - 38165.0);
        if (utc < new DateTime(1965, 7, 1, 0, 0, 0, DateTimeKind.Utc))
            return 3.6401300 + 0.0012960 * (mjd - 38165.0);
        if (utc < new DateTime(1965, 9, 1, 0, 0, 0, DateTimeKind.Utc))
            return 3.7401300 + 0.0012960 * (mjd - 38165.0);
        if (utc < new DateTime(1966, 1, 1, 0, 0, 0, DateTimeKind.Utc))
            return 3.8401300 + 0.0012960 * (mjd - 38165.0);
        if (utc < new DateTime(1968, 2, 1, 0, 0, 0, DateTimeKind.Utc))
            return 4.3131700 + 0.0025920 * (mjd - 39126.0);
        // 1968-02-01 to 1972-01-01
        return 4.2131700 + 0.0025920 * (mjd - 39126.0);
    }

    private static double ToModifiedJulianDate(DateTime utc)
    {
        // Algorithm for Gregorian calendar to Julian Day, then MJD = JD - 2400000.5
        var year  = utc.Year;
        var month = utc.Month;
        var day   = utc.Day + utc.TimeOfDay.TotalSeconds / 86400.0; // fractional day

        if (month <= 2)
        {
            year -= 1;
            month += 12;
        }

        var A = year / 100;
        var B = 2 - A + A / 4;
        var jd = Math.Floor(365.25 * (year + 4716))
            + Math.Floor(30.6001 * (month + 1))
            + day + B - 1524.5;
        var mjd = jd - 2400000.5;
        return mjd;
    }
}