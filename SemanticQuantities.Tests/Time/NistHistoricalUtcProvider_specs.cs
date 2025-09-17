using Machine.Specifications;
using Timtek.SemanticQuantities.Time;

namespace Timtek.SemanticQuantities.Tests.Time;

[Subject("NIST Historical UTC Provider (pre-1972 piecewise linear)")]
internal class When_querying_pre_1972_intervals
{
    private static NistHistoricalUtcProvider Provider;

    private static double Tolerance = 1e-6;

    private Establish context = () => Provider = new NistHistoricalUtcProvider();

    private It interval_1961_02_01 = () =>
    {
        var utc      = new DateTime(1961, 2, 1, 0, 0, 0, DateTimeKind.Utc);
        var expected = 1.4228180 + 0.0012960 * (Mjd(utc) - 37300.0);
        Math.Abs(Provider.TaiMinusUtcSeconds(utc) - expected).ShouldBeLessThan(Tolerance);
    };

    private It interval_1964_05_01 = () =>
    {
        var utc      = new DateTime(1964, 5, 1, 12, 0, 0, DateTimeKind.Utc);
        var expected = 3.3401300 + 0.0012960 * (Mjd(utc) - 38165.0); // falls in 1964-04 to 1964-09 interval
        Math.Abs(Provider.TaiMinusUtcSeconds(utc) - expected).ShouldBeLessThan(Tolerance);
    };

    private It interval_1966_06_01 = () =>
    {
        var utc      = new DateTime(1966, 6, 1, 0, 0, 0, DateTimeKind.Utc);
        var expected = 4.3131700 + 0.0025920 * (Mjd(utc) - 39126.0);
        Math.Abs(Provider.TaiMinusUtcSeconds(utc) - expected).ShouldBeLessThan(Tolerance);
    };

    private It interval_1970_01_01 = () =>
    {
        var utc      = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var expected = 4.2131700 + 0.0025920 * (Mjd(utc) - 39126.0);
        Math.Abs(Provider.TaiMinusUtcSeconds(utc) - expected).ShouldBeLessThan(Tolerance);
    };

    private static double Mjd(DateTime utc)
    {
        var year  = utc.Year;
        var month = utc.Month;
        var day   = utc.Day + utc.TimeOfDay.TotalSeconds / 86400.0;
        if (month <= 2)
        {
            year -= 1;
            month += 12;
        }

        var A  = year / 100;
        var B  = 2 - A + A / 4;
        var jd = Math.Floor(365.25 * (year + 4716)) + Math.Floor(30.6001 * (month + 1)) + day + B - 1524.5;
        return jd - 2400000.5;
    }
}

[Subject("NIST Historical UTC Provider boundary at 1972-01-01")]
internal class When_crossing_the_1972_boundary
{
    private static NistHistoricalUtcProvider Provider;

    private It at_boundary_uses_step_table = () =>
        Provider.TaiMinusUtcSeconds(new DateTime(1972, 1, 1, 0, 0, 0, DateTimeKind.Utc)).ShouldEqual(10.0);

    private Establish context = () => Provider = new NistHistoricalUtcProvider();

    private It just_before_boundary_uses_linear = () =>
    {
        var t        = new DateTime(1971, 12, 31, 23, 59, 59, DateTimeKind.Utc);
        var expected = 4.2131700 + 0.0025920 * (Mjd(t) - 39126.0);
        Math.Abs(Provider.TaiMinusUtcSeconds(t) - expected).ShouldBeLessThan(1e-6);
    };

    private static double Mjd(DateTime utc)
    {
        var year  = utc.Year;
        var month = utc.Month;
        var day   = utc.Day + utc.TimeOfDay.TotalSeconds / 86400.0;
        if (month <= 2)
        {
            year -= 1;
            month += 12;
        }

        var A  = year / 100;
        var B  = 2 - A + A / 4;
        var jd = Math.Floor(365.25 * (year + 4716)) + Math.Floor(30.6001 * (month + 1)) + day + B - 1524.5;
        return jd - 2400000.5;
    }
}