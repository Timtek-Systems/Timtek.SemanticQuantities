using Machine.Specifications;
using Timtek.SemanticQuantities.Quantities;
using Timtek.SemanticQuantities.Quantities.Domain;
using Timtek.SemanticQuantities.Units;

namespace Timtek.SemanticQuantities.Tests.NonSI;

[Subject("Non-SI units - lbf and psi conversions")] 
public class when_converting_between_lbf_psi_and_SI
{
    private It lbf_to_newton_and_back = () =>
    {
        var f_lbf = new Quantity(1.0, new PoundForce());
        var n = f_lbf.As(new Newton());
        System.Math.Abs(n - 4.4482216152605).ShouldBeLessThan(1e-12);
        var roundTrip = new Quantity(n, new Newton()).As(new PoundForce());
        System.Math.Abs(roundTrip - 1.0).ShouldBeLessThan(1e-12);
    };

    private It psi_to_pascal_and_back = () =>
    {
        var p_psi = new Quantity(1.0, new Psi());
        var pa = p_psi.As(new Pascal());
        System.Math.Abs(pa - 6894.757293168361).ShouldBeLessThan(1e-9);
        var roundTrip = new Quantity(pa, new Pascal()).As(new Psi());
        System.Math.Abs(roundTrip - 1.0).ShouldBeLessThan(1e-12);
    };

    private It combining_lbf_over_inch_squared_yields_psi = () =>
    {
        var f = new Quantity(1.0, new PoundForce());
        var inch = new Quantity(1.0, new Inch());
        var area = inch * inch; // 1 in^2 with imperial system inferred
        var p = f / area; // 1 lbf / in^2 = 1 psi
        p.Unit.Symbol.ShouldEqual("psi");
        p.As(new Psi()).ShouldBeCloseTo(1.0, 1e-12);
    };
}
