using Machine.Specifications;
using Timtek.SemanticQuantities.Quantities;
using Timtek.SemanticQuantities.Time;
using Timtek.SemanticQuantities.Units;

namespace Timtek.SemanticQuantities.Tests.Time;

[Subject("TimeInstant operations and cross-scale conversions")]
internal class When_creating_TimeInstant_from_scale_and_round_tripping
{
    private static ITimeScaleContext Ctx;
    private static TimeInstant<TAI>  TaiInstant;
    private static TimeInstant<UTC>  UtcInstant;
    private static double            UtcSeconds;
    private static double            TaiSeconds;

    private Establish context = () =>
    {
        Ctx = new BasicTimeScaleContext(); // BasicLeapSecondsProvider returns 0
        UtcSeconds = 123456.789;
    };

    private Because of = () =>
    {
        UtcInstant = TimeInstant<UTC>.FromSeconds(UtcSeconds, Ctx);
        TaiSeconds = UtcInstant.AsSeconds<TAI>(Ctx);
        TaiInstant = new TimeInstant<TAI>(TaiSeconds);
    };

    private It should_equal_between_utc_and_tai_when_no_leap_seconds = () => TaiSeconds.ShouldEqual(UtcSeconds);

    private It should_round_trip_between_scales = () =>
        TimeInstant<UTC>.FromSeconds(TaiInstant.AsSeconds<UTC>(Ctx), Ctx)
            .Subtract(UtcInstant)
            .As<Second>()
            .ShouldEqual(0.0);
}

[Subject("TimeInstant arithmetic in SI seconds")]
internal class When_adding_and_subtracting_TimeInstants
{
    private static TimeInstant<TAI> A;
    private static TimeInstant<TAI> B;
    private static Quantity<Second> Delta;

    private Establish context = () =>
    {
        A = new TimeInstant<TAI>(1000.0);
        B = new TimeInstant<TAI>(1500.0);
    };

    private Because of = () => Delta = B.Subtract(A);

    private It should_produce_positive_duration_in_seconds = () => Delta.ValueSI.ShouldEqual(500.0);

    private It should_support_addition_of_duration =
        () => A.Add(new Quantity<Second>(500.0, true)).Subtract(B).ValueSI.ShouldEqual(0.0);
}