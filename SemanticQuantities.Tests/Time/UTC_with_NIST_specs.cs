using Machine.Specifications;
using Timtek.SemanticQuantities.Time;

namespace Timtek.SemanticQuantities.Tests.Time;

[Subject("UTC scale integration with NIST modern provider")]
internal class When_converting_across_the_2016_2017_leap
{
    private static UTC                     Utc;
    private static ITimeScaleContext       Ctx;
    private static NistLeapSecondsProvider Leap;

    private Establish context = () =>
    {
        Utc = new UTC();
        Leap = new NistLeapSecondsProvider();
        Ctx = new BasicTimeScaleContext(Leap);
    };

    private It should_round_trip_on_both_sides_of_the_step = () =>
    {
        var t0   = new DateTime(2016, 12, 31, 12, 0, 0, DateTimeKind.Utc);
        var t1   = new DateTime(2017, 1, 1, 12, 0, 0, DateTimeKind.Utc);
        var u0   = EpochSeconds(t0);
        var u1   = EpochSeconds(t1);
        var tai0 = Utc.ToTaiSeconds(u0, Ctx);
        var tai1 = Utc.ToTaiSeconds(u1, Ctx);
        Utc.FromTaiSeconds(tai0, Ctx).ShouldEqual(u0);
        Utc.FromTaiSeconds(tai1, Ctx).ShouldEqual(u1);
    };

    private It tai_advances_by_two_seconds_while_utc_advances_by_one = () =>
    {
        var t0   = new DateTime(2016, 12, 31, 23, 59, 59, DateTimeKind.Utc);
        var t1   = new DateTime(2017, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var u0   = EpochSeconds(t0);
        var u1   = EpochSeconds(t1);
        var tai0 = Utc.ToTaiSeconds(u0, Ctx);
        var tai1 = Utc.ToTaiSeconds(u1, Ctx);
        (tai1 - tai0).ShouldEqual(2.0);
        (u1 - u0).ShouldEqual(1.0);
    };

    private static double EpochSeconds(DateTime utc) => (utc - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
}

[Subject("UTC scale integration with NIST historical provider (pre-1972)")]
internal class When_round_tripping_a_pre_1972_instant
{
    private static UTC                       Utc;
    private static ITimeScaleContext         Ctx;
    private static NistHistoricalUtcProvider Leap;

    private Establish context = () =>
    {
        Utc = new UTC();
        Leap = new NistHistoricalUtcProvider();
        Ctx = new BasicTimeScaleContext(Leap);
    };

    private It should_invert_to_and_from_tai_within_tolerance = () =>
    {
        var t    = new DateTime(1970, 6, 1, 0, 0, 0, DateTimeKind.Utc);
        var u    = EpochSeconds(t);
        var tai  = Utc.ToTaiSeconds(u, Ctx);
        var back = Utc.FromTaiSeconds(tai, Ctx);
        Math.Abs(back - u).ShouldBeLessThan(1e-9);
    };

    private static double EpochSeconds(DateTime utc) => (utc - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
}