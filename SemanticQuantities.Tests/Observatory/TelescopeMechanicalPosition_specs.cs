using System;
using System.Linq;
using Machine.Specifications;
using TA.ObjectOrientedAstronomy.Observatory;

namespace Timtek.SemanticQuantities.Tests.Observatory;

[Subject("TelescopeMechanicalPosition construction and comparison")]
class when_constructing_TelescopeMechanicalPosition
{
    static Exception Caught;

    It throws_when_hour_angle_is_out_of_range_low = () =>
        (Caught = Catch.Exception(() => new TelescopeMechanicalPosition(-0.1, 0.0)))
            .ShouldBeOfExactType<ArgumentOutOfRangeException>();

    It throws_when_hour_angle_is_out_of_range_high = () =>
        (Caught = Catch.Exception(() => new TelescopeMechanicalPosition(24.0, 0.0)))
            .ShouldBeOfExactType<ArgumentOutOfRangeException>();

    It throws_when_declination_is_out_of_range_low = () =>
        (Caught = Catch.Exception(() => new TelescopeMechanicalPosition(0.0, -90.1)))
            .ShouldBeOfExactType<ArgumentOutOfRangeException>();

    It throws_when_declination_is_out_of_range_high = () =>
        (Caught = Catch.Exception(() => new TelescopeMechanicalPosition(0.0, 90.1)))
            .ShouldBeOfExactType<ArgumentOutOfRangeException>();
}

[Subject("TelescopeMechanicalPosition ordering")]
class when_sorting_positions
{
    static TelescopeMechanicalPosition A;
    static TelescopeMechanicalPosition B;
    static TelescopeMechanicalPosition C;

    Establish context = () =>
    {
        A = new TelescopeMechanicalPosition(1.0, 0.0);   // HA 1h, Dec 0
        B = new TelescopeMechanicalPosition(2.0, -10.0); // HA 2h, Dec -10
        C = new TelescopeMechanicalPosition(2.0, +5.0);  // HA 2h, Dec +5
    };

    It sorts_by_hour_angle_then_declination = () =>
    {
        var sorted = new[] { C, B, A }.OrderBy(x => x).ToArray();
        sorted[0].ShouldEqual(A);
        sorted[1].ShouldEqual(B);
        sorted[2].ShouldEqual(C);
    };
}