
using System;
using Machine.Specifications;

[Subject("Speed conversion")]
public class when_converting_kmh_to_mps
{
    static double value;

    Because of = () =>
        value = Speed.FromKilometersPerHour(36).ValueSI;

    It should_be_10_mps = () =>
    {
        if (Math.Abs(value - 10.0) > 1e-6)
            throw new Exception($"Expected 10 m/s within 1e-6, but was {value}");
    };
}
