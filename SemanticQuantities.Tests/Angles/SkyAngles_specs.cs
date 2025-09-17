using Machine.Specifications;
using Timtek.SemanticQuantities.Quantities;
using Timtek.SemanticQuantities.Quantities.Domain;
using Timtek.SemanticQuantities.Units;
// Quantity<TUnit>

namespace Timtek.SemanticQuantities.Tests.Angles;

[Subject("HourAngle unit conversions")]
internal class when_converting_hour_angle_to_degrees_and_radians
{
    private It twelve_hours_should_be_180_degrees = () =>
    {
var deg = new Quantity(12.0, new HourOfAngle()).As(new Degree());
        Math.Abs(deg - 180.0).ShouldBeLessThan(1e-12);
    };

    private It twelve_hours_should_be_pi_radians = () =>
Math.Abs(new Quantity(12.0, new HourOfAngle()).ValueSI - Math.PI).ShouldBeLessThan(1e-12);
}

[Subject("Right Ascension normalization")]
internal class when_normalizing_right_ascension
{
    private It degrees_are_supported = () =>
    {
        var h = RightAscension.FromDegrees(180.0).AsHours();
        Math.Abs(h - 12.0).ShouldBeLessThan(1e-12);
    };

    private It hours_wrap_to_0_24 = () =>
    {
        var h = RightAscension.FromHours(25.0).AsHours();
        Math.Abs(h - 1.0).ShouldBeLessThan(1e-12);
    };
}

[Subject("HourAngle normalization")]
internal class when_checking_hour_angle_signed_and_unsigned
{
    private It signed_is_in_minus12_plus12 = () =>
    {
        var hs = HourAngle.FromHours(13.0).AsSignedHours();
        Math.Abs(hs - -11.0).ShouldBeLessThan(1e-12);
    };

    private It unsigned_is_in_0_24 = () =>
    {
        var h = HourAngle.FromDegrees(390.0).AsHoursUnwrapped(); // 390° = 26h -> 2h
        Math.Abs(h - 2.0).ShouldBeLessThan(1e-12);
    };
}

[Subject("Declination clamping")]
internal class when_clamping_declination
{
    private It clamps_above_90 = () =>
        Declination.FromDegrees(95.0).AsDegrees().ShouldEqual(90.0);

    private It clamps_below_minus_90 = () =>
        Declination.FromDegrees(-100.0).AsDegrees().ShouldEqual(-90.0);
}

[Subject("Latitude clamping")]
internal class when_clamping_latitude
{
    private It clamps_above_90 = () =>
        Latitude.FromDegrees(123.0).AsDegrees().ShouldEqual(90.0);

    private It clamps_below_minus_90 = () =>
        Latitude.FromDegrees(-123.0).AsDegrees().ShouldEqual(-90.0);
}

[Subject("Longitude normalization")]
internal class when_normalizing_longitude
{
    private It wraps_to_minus180_plus180 = () =>
    {
        var d = Longitude.FromDegrees(190.0).AsDegrees();
        Math.Abs(d - -170.0).ShouldBeLessThan(1e-12);
    };

    private It zero_to_360_view_is_available = () =>
    {
        var d = Longitude.FromDegrees(-190.0).AsDegreesZeroTo360();
        Math.Abs(d - 170.0).ShouldBeLessThan(1e-12);
    };
}