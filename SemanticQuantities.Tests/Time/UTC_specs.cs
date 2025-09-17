using FakeItEasy;
using Machine.Specifications;
using Timtek.SemanticQuantities.Time;

namespace Timtek.SemanticQuantities.Tests.Time;

[Subject("Time scales - UTC with leap seconds")]
internal class When_converting_UTC_to_TAI_with_constant_offset
{
    private static UTC                  Utc;
    private static ITimeScaleContext    Ctx;
    private static ILeapSecondsProvider Leap;
    private static double               Input;
    private static double               ToTai;

    private Establish context = () =>
    {
        Utc = new UTC();
        Leap = A.Fake<ILeapSecondsProvider>();
        // Constant offset of 37 seconds for all dates
        A.CallTo(() => Leap.TaiMinusUtcSeconds(A<DateTime>.Ignored)).Returns(37.0);
        Ctx = new BasicTimeScaleContext(Leap);
        Input = 10_000.0;
    };

    private Because of = () => ToTai = Utc.ToTaiSeconds(Input, Ctx);

    private It should_add_the_leap_second_offset = () => ToTai.ShouldEqual(Input + 37.0);
}

[Subject("Time scales - UTC FromTaiSeconds invertibility with constant offset")]
internal class When_converting_TAI_to_UTC_with_constant_offset
{
    private static UTC                  Utc;
    private static ITimeScaleContext    Ctx;
    private static ILeapSecondsProvider Leap;
    private static double               Tai;
    private static double               UtcSeconds;

    private Establish context = () =>
    {
        Utc = new UTC();
        Leap = A.Fake<ILeapSecondsProvider>();
        A.CallTo(() => Leap.TaiMinusUtcSeconds(A<DateTime>.Ignored)).Returns(37.0);
        Ctx = new BasicTimeScaleContext(Leap);
        Tai = 50_000.0;
    };

    private Because of = () => UtcSeconds = Utc.FromTaiSeconds(Tai, Ctx);

    private It should_return_value_minus_offset = () => UtcSeconds.ShouldEqual(Tai - 37.0);
}

[Subject("Time scales - UTC with step change in leap seconds")]
internal class When_converting_near_a_leap_second_step
{
    private static UTC                  Utc;
    private static ITimeScaleContext    Ctx;
    private static ILeapSecondsProvider Leap;
    private static DateTime             StepUtc;
    private static double               BeforeTai;
    private static double               AfterTai;
    private static double               BeforeUtc;
    private static double               AfterUtc;

    private Establish context = () =>
    {
        Utc = new UTC();
        Leap = A.Fake<ILeapSecondsProvider>();
        // Define a step at 1970-01-02T00:00:00Z where TAI-UTC goes from 10s to 11s
        StepUtc = new DateTime(1970, 1, 2, 0, 0, 0, DateTimeKind.Utc);
        A.CallTo(() => Leap.TaiMinusUtcSeconds(A<DateTime>.Ignored))
            .ReturnsLazily((DateTime d) => d < StepUtc ? 10.0 : 11.0);
        Ctx = new BasicTimeScaleContext(Leap);
        // Choose two TAI times: one that maps before the step and one after
        // Before: pick UTC= 12 hours after epoch => tai = utc + 10
        BeforeUtc = TimeSpan.FromHours(12).TotalSeconds;
        BeforeTai = BeforeUtc + 10.0;
        // After: pick UTC= 1.5 days after epoch => tai = utc + 11
        AfterUtc = TimeSpan.FromDays(1.5).TotalSeconds;
        AfterTai = AfterUtc + 11.0;
    };

    private It should_map_after_step_correctly = () => Utc.FromTaiSeconds(AfterTai, Ctx).ShouldEqual(AfterUtc);

    private It should_map_before_step_correctly = () => Utc.FromTaiSeconds(BeforeTai, Ctx).ShouldEqual(BeforeUtc);
}