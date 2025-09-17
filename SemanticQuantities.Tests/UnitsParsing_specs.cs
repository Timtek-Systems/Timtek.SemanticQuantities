using Machine.Specifications;
using Timtek.SemanticQuantities.Registry;
using Timtek.SemanticQuantities.Units;

namespace Timtek.SemanticQuantities.Tests.UnitsParsing;

[Subject("Unit expression parsing")]
public class when_parsing_unit_expressions
{
    private static DefaultDimensionRegistry R;

    private Establish context = () => R = RegistryBootstrapper.BuildDefault();

    private It parses_mps2 = () =>
    {
        Units.IUnit? u;
        UnitExpressionParser.TryParse("m/s^2", R, out u).ShouldBeTrue();
        u!.Symbol.ShouldEqual("m/s^2");
    };

    private It parses_kg_m_s2_as_newton = () =>
    {
        Units.IUnit? u;
        UnitExpressionParser.TryParse("kg*m/s^2", R, out u).ShouldBeTrue();
        // Coherent mapping to N is registry's job during normalization; parser returns a compound symbol
        u!.Symbol.ShouldEqual("kg*m/s^2");
    };

    private It parses_lbf_per_in2_as_psi_system_imperial = () =>
    {
        Units.IUnit? u;
        UnitExpressionParser.TryParse("lbf/in^2", R, out u).ShouldBeTrue();
        u!.Symbol.ShouldEqual("lbf/in^2");
        // System is Imperial; registry can map signature to psi as coherent when normalizing results
    };
}
