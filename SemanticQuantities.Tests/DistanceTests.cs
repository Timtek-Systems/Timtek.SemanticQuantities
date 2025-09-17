using Machine.Specifications;
using Timtek.SemanticQuantities.Quantities;
using Timtek.SemanticQuantities.Quantities.Domain;
using Timtek.SemanticQuantities.Units;

namespace Timtek.SemanticQuantities.Tests;

[Subject("Distance - SI meters construction")]
public class when_constructing_distance_from_meters
{
private static Quantity distance;

    private Because of = () =>
        distance = Distance.FromMeters(12.0);

    private It value_si_should_equal_input = () =>
    {
        if (Math.Abs(distance.ValueSI - 12.0) > 1e-12)
            throw new Exception($"Expected 12.0 SI meters within 1e-12, but was {distance.ValueSI}");
    };

    private It as_meter_should_equal_input = () =>
    {
var m = distance.As(new Meter());
        if (Math.Abs(m - 12.0) > 1e-12)
            throw new Exception($"Expected 12.0 m within 1e-12, but was {m}");
    };
}

[Subject("Distance - kilometer construction")]
public class when_constructing_distance_from_kilometers
{
private static Quantity distance;

    private Because of = () =>
        distance = Distance.FromKilometers(2.5);

    private It value_si_should_be_2500_meters = () =>
    {
        if (Math.Abs(distance.ValueSI - 2500.0) > 1e-9)
            throw new Exception($"Expected 2500.0 SI meters within 1e-9, but was {distance.ValueSI}");
    };

    private It as_kilometers_should_be_2_point_5 = () =>
    {
var km = distance.As(new Kilometer());
        if (Math.Abs(km - 2.5) > 1e-12)
            throw new Exception($"Expected 2.5 km within 1e-12, but was {km}");
    };
}

[Subject("Distance - conversions")]
public class when_converting_between_meters_and_kilometers
{
    private It meters_to_kilometers_round_trip = () =>
    {
        var dist = Distance.FromMeters(1500.0);
var km = dist.As(new Kilometer());
        if (Math.Abs(km - 1.5) > 1e-12)
            throw new Exception($"Expected 1.5 km within 1e-12, but was {km}");

var backToMeters = new Quantity(km, new Kilometer()).As(new Meter());
        if (Math.Abs(backToMeters - 1500.0) > 1e-12)
            throw new Exception($"Expected 1500.0 m within 1e-12, but was {backToMeters}");
    };
}

[Subject("Distance - direct kilometer quantity behavior")]
public class when_constructing_quantity_of_kilometer_directly
{
private static Quantity qkm;

    private Because of = () =>
qkm = new Quantity(2.0, new Kilometer());

    private It value_si_should_be_2000_meters = () =>
    {
        if (Math.Abs(qkm.ValueSI - 2000.0) > 1e-12)
            throw new Exception($"Expected 2000.0 SI meters within 1e-12, but was {qkm.ValueSI}");
    };
}

[Subject("Distance - edge cases")]
public class when_using_edge_values_for_distance
{
    private It zero_value_is_supported = () =>
    {
        var zero = Distance.FromMeters(0.0);
        if (Math.Abs(zero.ValueSI - 0.0) > 1e-12)
            throw new Exception($"Expected 0.0 SI meters within 1e-12, but was {zero.ValueSI}");
    };

    private It fractional_values_are_supported = () =>
    {
        var frac = Distance.FromKilometers(0.001);
        if (Math.Abs(frac.ValueSI - 1.0) > 1e-12)
            throw new Exception($"Expected 1.0 SI meters within 1e-12, but was {frac.ValueSI}");
    };

    private It large_values_are_supported = () =>
    {
        var large = Distance.FromKilometers(1e6);
        if (Math.Abs(large.ValueSI - 1e9) > 1e-3)
            throw new Exception($"Expected 1e9 SI meters within 1e-3, but was {large.ValueSI}");
    };
}
