using Machine.Specifications;
using Timtek.SemanticQuantities.Time;

namespace Timtek.SemanticQuantities.Tests.Time;

[Subject("NIST Leap Seconds Provider (1972+)")]
internal class When_querying_known_step_boundaries
{
    private static NistLeapSecondsProvider Provider;

    private Establish context = () => Provider = new NistLeapSecondsProvider();

    private It should_return_10s_before_1972_07_01 = () =>
        Provider.TaiMinusUtcSeconds(new DateTime(1972, 6, 30, 23, 59, 59, DateTimeKind.Utc))
            .ShouldEqual(10.0);

    private It should_return_11s_on_and_after_1972_07_01 = () =>
        Provider.TaiMinusUtcSeconds(new DateTime(1972, 7, 1, 0, 0, 0, DateTimeKind.Utc))
            .ShouldEqual(11.0);

    private It should_return_19s_on_1980_01_01 = () =>
        Provider.TaiMinusUtcSeconds(new DateTime(1980, 1, 1, 0, 0, 0, DateTimeKind.Utc))
            .ShouldEqual(19.0);

    private It should_return_36s_before_2017_01_01 = () =>
        Provider.TaiMinusUtcSeconds(new DateTime(2016, 12, 31, 23, 59, 59, DateTimeKind.Utc))
            .ShouldEqual(36.0);

    private It should_return_37s_on_2017_01_01 = () =>
        Provider.TaiMinusUtcSeconds(new DateTime(2017, 1, 1, 0, 0, 0, DateTimeKind.Utc))
            .ShouldEqual(37.0);
}

[Subject("NIST Leap Seconds Provider (extensions)")]
internal class When_adding_a_future_leap_second
{
    private static NistLeapSecondsProvider Provider;

    private Establish context = () => Provider = new NistLeapSecondsProvider();

    private Because of = () => Provider.AddLeapSecond(new DateTime(2030, 1, 1, 0, 0, 0, DateTimeKind.Utc), 38);

    private It should_return_37s_in_2025 = () =>
        Provider.TaiMinusUtcSeconds(new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc))
            .ShouldEqual(37.0);

    private It should_return_38s_in_2030 = () =>
        Provider.TaiMinusUtcSeconds(new DateTime(2030, 1, 1, 0, 0, 0, DateTimeKind.Utc))
            .ShouldEqual(38.0);
}

[Subject("NIST Leap Seconds Provider (pre-1972 unsupported)")]
internal class When_querying_before_1972_with_modern_provider
{
    private static NistLeapSecondsProvider Provider;
    private static Exception               Caught;

    private Establish context = () => Provider = new NistLeapSecondsProvider();

    private Because of = () =>
        Caught = Catch.Exception(() => Provider.TaiMinusUtcSeconds(new DateTime(1971, 12, 31, 23, 59, 59, DateTimeKind.Utc)));

    private It should_throw_not_supported = () => Caught.ShouldBeOfExactType<NotSupportedException>();
}