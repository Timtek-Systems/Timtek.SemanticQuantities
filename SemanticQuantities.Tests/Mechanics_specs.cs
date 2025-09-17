using Machine.Specifications;
using Timtek.SemanticQuantities.Dimensions;
using Timtek.SemanticQuantities.Quantities;
using Timtek.SemanticQuantities.Quantities.Domain;
using Timtek.SemanticQuantities.Units;

namespace Timtek.SemanticQuantities.Tests.Mechanics;

[Subject("Mechanics - Mass x Acceleration = Force")]
public class when_multiplying_mass_by_acceleration
{
    private static Quantity mass;
    private static Quantity accel;
    private static Quantity force;

    private Establish context = () =>
    {
        mass = Mass.FromKilograms(2.0);           // 2 kg
        accel = Acceleration.FromMetersPerSecondSquared(9.81); // 9.81 m/s^2
    };

    private Because of = () => force = mass * accel;

    private It should_have_newton_signature = () => force.Unit.Signature.ShouldEqual(new DimensionSignature(1,1,-2,0,0,0,0,0));

    private It should_equal_19_point_62_newtons_in_si = () =>
        force.As(new Newton()).ShouldBeCloseTo(19.62, 1e-12);
}

[Subject("Mechanics - Speed and Distance/Duration arithmetic")]
public class when_computing_speed_and_distance
{
    private static Quantity distance;
    private static Quantity duration;

    private Because of = () =>
    {
        distance = Distance.FromMeters(100.0);   // 100 m
        duration = Duration.FromSeconds(20.0);   // 20 s
    };

    private It division_gives_speed_in_m_per_s = () =>
    {
        var speed = distance / duration;        // 5 m/s
        speed.Unit.Symbol.ShouldEqual("m/s");
        speed.As(new MeterPerSecond()).ShouldBeCloseTo(5.0, 1e-12);
    };

    private It speed_times_time_returns_distance = () =>
    {
        var speed = distance / duration;        // 5 m/s
        var back = speed * Duration.FromSeconds(20.0);
        back.Unit.Symbol.ShouldEqual("m");
        back.As(new Meter()).ShouldBeCloseTo(100.0, 1e-12);
    };
}

[Subject("Cross-unit addition and normalization")]
public class when_adding_mixed_length_units
{
    private It meters_plus_feet_returns_coherent_si_meters = () =>
    {
        var a = Distance.FromMeters(1.0);       // 1 m
        var b = new Quantity(3.280839895, new Foot()); // approximately 1 m in feet
        var sum = a + b;                         // expect 2 m, in meters
        sum.Unit.Symbol.ShouldEqual("m");
        sum.As(new Meter()).ShouldBeCloseTo(2.0, 1e-9);
    };

    private It inches_plus_centimeters_returns_meters = () =>
    {
        var inch = new Quantity(10.0, new Inch());     // 10 in = 0.254 m
        // centimeters not implemented yet; use 0.30 m via meters for this test
        var meters = Distance.FromMeters(0.30);
        var sum = inch + meters;                 // expect 0.554 m
        sum.Unit.Symbol.ShouldEqual("m");
        sum.As(new Meter()).ShouldBeCloseTo(0.254 + 0.30, 1e-12);
    };
}
