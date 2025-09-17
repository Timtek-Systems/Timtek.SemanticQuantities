using Machine.Specifications;
using Timtek.SemanticQuantities.Quantities.Domain;

namespace Timtek.SemanticQuantities.Tests;

[Subject("Speed conversion")]
public class when_converting_kmh_to_mps
{
    private static double value;

    private Because of = () =>
        value = Speed.FromKilometersPerHour(36).ValueSI;

    private It should_be_10_mps = () =>
    {
        if (Math.Abs(value - 10.0) > 1e-6)
            throw new Exception($"Expected 10 m/s within 1e-6, but was {value}");
    };
}