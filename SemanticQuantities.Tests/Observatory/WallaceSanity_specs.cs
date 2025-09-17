using Machine.Specifications;
using TA.ObjectOrientedAstronomy.Observatory;
using Timtek.SemanticQuantities.Quantities.Domain;
using Timtek.SemanticQuantities.Quantities;
using Timtek.SemanticQuantities.Units;

namespace Timtek.SemanticQuantities.Tests.Observatory;

[Subject("Wallace sanity - no solution")]
public class when_geometry_cannot_intersect_dome
{
    static ObservatoryGeometry Geo;
    static WallaceDomeSync Sync;
    static System.Exception? Caught;

    Establish context = () =>
    {
        Geo = new ObservatoryGeometry(
            mountOffsetEast: Distance.FromMeters(-0.035),
            mountOffsetNorth: Distance.FromMeters(+0.370),
            mountOffsetUp: Distance.FromMeters(+1.250),
            domeRadius: Distance.FromMeters(0.01), // tiny dome, impossible to intersect
            observatoryLatitude: Latitude.FromRadians(+0.6315),
            polarDeclinationAxisDistance: Distance.FromMeters(0.0),
            polarOpticalAxisDistance: Distance.FromMeters(0.505),
            declinationOpticalAxisDistance: Distance.FromMeters(0.0)
        );
        Sync = new WallaceDomeSync(Geo);
        Caught = Catch.Exception(() => Sync.FromTelescopeMechanicalPositionRadians(0.0436, 0.6615));
    };

    It throws_invalid_operation_due_to_no_solution = () =>
        Caught.ShouldBeOfExactType<System.InvalidOperationException>();
}

[Subject("Wallace sanity - scaling invariance")]
public class when_scaling_all_distances
{
    static DomePosition resultMeters, resultScaled;

    Establish context = () =>
    {
        var geoM = new ObservatoryGeometry(
            Distance.FromMeters(-0.035),
            Distance.FromMeters(+0.370),
            Distance.FromMeters(+1.250),
            Distance.FromMeters(1.900),
            Latitude.FromRadians(+0.6315),
            Distance.FromMeters(0.0),
            Distance.FromMeters(0.505),
            Distance.FromMeters(0.0)
        );
        // Scale all distances by 1000: still meters, but 1000x the original values
        var s = 1000.0;
        var geoS = new ObservatoryGeometry(
            Distance.FromMeters(-0.035 * s),
            Distance.FromMeters(+0.370 * s),
            Distance.FromMeters(+1.250 * s),
            Distance.FromMeters(1.900 * s),
            Latitude.FromRadians(+0.6315),
            Distance.FromMeters(0.0 * s),
            Distance.FromMeters(0.505 * s),
            Distance.FromMeters(0.0 * s)
        );
        var syncM = new WallaceDomeSync(geoM);
        var syncS = new WallaceDomeSync(geoS);
        resultMeters = syncM.FromTelescopeMechanicalPositionRadians(+0.0436, +0.6615);
        resultScaled = syncS.FromTelescopeMechanicalPositionRadians(+0.0436, +0.6615);
    };

    It azimuth_is_invariant = () =>
        System.Math.Abs(resultMeters.Azimuth.As<Degree>() - resultScaled.Azimuth.As<Degree>()).ShouldBeLessThan(1e-9);

    It elevation_is_invariant = () =>
        System.Math.Abs(resultMeters.Elevation.As<Degree>() - resultScaled.Elevation.As<Degree>()).ShouldBeLessThan(1e-9);
}
