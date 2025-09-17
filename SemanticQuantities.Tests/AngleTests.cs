
using System;
using Machine.Specifications;

[Subject("Angle conversion")]
public class when_converting_degrees_to_radians
{
    static double result;

    Because of = () =>
        result = Angle.FromDegrees(180).As<Radian>();

    It should_be_pi = () =>
    {
        if (Math.Abs(result - Math.PI) > 1e-6)
            throw new Exception($"Expected PI within 1e-6, but was {result}");
    };
}
