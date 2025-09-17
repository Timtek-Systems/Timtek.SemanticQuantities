using Machine.Specifications;
using Timtek.SemanticQuantities.Dimensions;
using Timtek.SemanticQuantities.Quantities;
using Timtek.SemanticQuantities.Quantities.Domain;
using Timtek.SemanticQuantities.Units;

namespace Timtek.SemanticQuantities.Tests.DerivedUnits;

[Subject("Derived Units - energy and power")]
public class when_multiplying_force_by_distance
{
    private static Quantity force;
    private static Quantity dist;
    private static Quantity energy;

    private Establish context = () =>
    {
        force = Force.FromNewtons(2.0);
        dist = Distance.FromMeters(3.0);
    };

    private Because of = () => energy = force * dist; // expect Joule

    private It should_be_expressed_as_joules = () => energy.Unit.Symbol.ShouldEqual("J");
    private It should_equal_6_joules = () => energy.As(new Joule()).ShouldEqual(6.0);
}

[Subject("Derived Units - pressure and power")]
public class when_dividing_energy_by_time
{
    private static Quantity energy;
    private static Quantity time;
    private static Quantity power;

    private Because of = () =>
    {
        energy = new Quantity(10.0, new Joule());
        time = Duration.FromSeconds(2.0);
        power = energy / time; // 5 W
    };

    private It should_be_expressed_as_watts = () => power.Unit.Symbol.ShouldEqual("W");
    private It should_equal_5_watts = () => power.As(new Watt()).ShouldEqual(5.0);
}

[Subject("Derived Units - pascal from force per area")]
public class when_dividing_force_by_area
{
    private It should_return_pascal = () =>
    {
        var f = Force.FromNewtons(100.0);
        var area = new Quantity(10.0, new CompoundUnit("m^2", new DimensionSignature(2,0,0,0,0,0,0,0))); // simple area unit using canonical SI
        var p = f / area; // 10 Pa
        p.Unit.Symbol.ShouldEqual("Pa");
        p.As(new Pascal()).ShouldEqual(10.0);
    };
}
