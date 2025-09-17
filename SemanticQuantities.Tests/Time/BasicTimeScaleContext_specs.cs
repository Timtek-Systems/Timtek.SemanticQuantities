using FakeItEasy;
using Machine.Specifications;
using Timtek.SemanticQuantities.Time;

namespace Timtek.SemanticQuantities.Tests.Time;

[Subject("Time scale context defaults")]
class When_creating_a_basic_time_scale_context
{
    static BasicTimeScaleContext Ctx;

    Because of = () => Ctx = new BasicTimeScaleContext();

    It         should_provide_a_leap_seconds_provider_by_default = () => Ctx.LeapSeconds.ShouldNotBeNull();
    private It should_allow_null_eop                             = () => Ctx.Eop.ShouldBeNull();
    private It should_allow_null_ephemeris                       = () => Ctx.Ephemeris.ShouldBeNull();
}

[Subject("Time scale context with injected providers")]
class When_injecting_providers
{
    static         BasicTimeScaleContext Ctx;
    private static ILeapSecondsProvider  Leap;
    private static IEopProvider          Eop;
    private static IEphemerisProvider    Eph;

    Establish context = () =>
    {
        Leap = A.Fake<ILeapSecondsProvider>();
        Eop = A.Fake<IEopProvider>();
        Eph = A.Fake<IEphemerisProvider>();
    };

    Because of = () => Ctx = new BasicTimeScaleContext(Leap, Eop, Eph);

    It         should_use_the_injected_leap_seconds_provider = () => Ctx.LeapSeconds.ShouldEqual(Leap);
    private It should_use_the_injected_eop_provider          = () => Ctx.Eop.ShouldEqual(Eop);
    private It should_use_the_injected_ephemeris_provider    = () => Ctx.Ephemeris.ShouldEqual(Eph);
}