using Machine.Specifications;
using TA.ObjectOrientedAstronomy.Observatory;
using Timtek.SemanticQuantities.Quantities.Domain;
using Timtek.SemanticQuantities.Quantities;
using Timtek.SemanticQuantities.Units;

namespace Timtek.SemanticQuantities.Tests.Observatory;

[Subject("Wallace worked example - east of pier")]
public class when_using_wallace_section5_example_east_of_pier
{
    static ObservatoryGeometry Geo;
    static WallaceDomeSync Sync;
    static DomePosition Result;

    Establish context = () =>
    {
        Geo = new ObservatoryGeometry(
            mountOffsetEast: Distance.FromMeters(-0.035), // -35 mm
            mountOffsetNorth: Distance.FromMeters(+0.370), // +370 mm
            mountOffsetUp: Distance.FromMeters(+1.250),    // +1250 mm
            domeRadius: Distance.FromMeters(1.900),        // 1900 mm radius
            observatoryLatitude: Latitude.FromRadians(+0.6315),
            polarDeclinationAxisDistance: Distance.FromMeters(0.0), // p = 0
            polarOpticalAxisDistance: Distance.FromMeters(0.505),   // q = 505 mm
            declinationOpticalAxisDistance: Distance.FromMeters(0.0) // r = 0
        );
        Sync = new WallaceDomeSync(Geo);
        Result = Sync.FromTelescopeMechanicalPositionRadians(
            mechanicalHourAngleRadians: +0.0436,
            mechanicalDeclinationRadians: +0.6615);
    };

    It azimuth_matches_wallace = () =>
    {
        var az = Result.Azimuth.As<Degree>();
        // Expected 50.369411°
        if (System.Math.Abs(az - 50.369411) > 1e-6)
            throw new System.Exception($"Az mismatch: expected 50.369411°, got {az}");
    };

    It elevation_matches_wallace = () =>
    {
        var el = Result.Elevation.As<Degree>();
        // Expected 72.051742°
        if (System.Math.Abs(el - 72.051742) > 1e-6)
            throw new System.Exception($"El mismatch: expected 72.051742°, got {el}");
    };
}

[Subject("Wallace worked example - west of pier")]
public class when_using_wallace_section5_example_west_of_pier
{
    static ObservatoryGeometry Geo;
    static WallaceDomeSync Sync;
    static DomePosition Result;

    Establish context = () =>
    {
        Geo = new ObservatoryGeometry(
            mountOffsetEast: Distance.FromMeters(-0.035), // -35 mm
            mountOffsetNorth: Distance.FromMeters(+0.370), // +370 mm
            mountOffsetUp: Distance.FromMeters(+1.250),    // +1250 mm
            domeRadius: Distance.FromMeters(1.900),        // 1900 mm radius
            observatoryLatitude: Latitude.FromRadians(+0.6315),
            polarDeclinationAxisDistance: Distance.FromMeters(0.0), // p = 0
            polarOpticalAxisDistance: Distance.FromMeters(0.505),   // q = 505 mm
            declinationOpticalAxisDistance: Distance.FromMeters(0.0) // r = 0
        );
        Sync = new WallaceDomeSync(Geo);
        Result = Sync.FromTelescopeMechanicalPositionRadians(
            mechanicalHourAngleRadians: -3.098,
            mechanicalDeclinationRadians: +2.480);
    };

    It azimuth_matches_wallace = () =>
    {
        var az = Result.Azimuth.As<Degree>();
        // Expected 305.595067°
        if (System.Math.Abs(az - 305.595067) > 1e-6)
            throw new System.Exception($"Az mismatch: expected 305.595067°, got {az}");
    };

    It elevation_matches_wallace = () =>
    {
        var el = Result.Elevation.As<Degree>();
        // Expected 68.824495°
        if (System.Math.Abs(el - 68.824495) > 1e-6)
            throw new System.Exception($"El mismatch: expected 68.824495°, got {el}");
    };
}
