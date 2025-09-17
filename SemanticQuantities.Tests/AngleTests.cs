using Machine.Specifications;
using Timtek.SemanticQuantities.Quantities.Domain;
using Timtek.SemanticQuantities.Units;

namespace Timtek.SemanticQuantities.Tests;

[Subject("Angle conversion")]
public class when_converting_degrees_to_radians
{
    private static double result;

    private Because of = () =>
result = Angle.FromDegrees(180).As(new Radian());

    private It should_be_pi = () =>
    {
        if (Math.Abs(result - Math.PI) > 1e-6)
            throw new Exception($"Expected PI within 1e-6, but was {result}");
    };
}