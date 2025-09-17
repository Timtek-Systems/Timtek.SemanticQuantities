using Machine.Specifications;
using TA.ObjectOrientedAstronomy.Observatory;
using Timtek.SemanticQuantities.Quantities;
using Timtek.SemanticQuantities.Quantities.Domain;
using Timtek.SemanticQuantities.Units;

namespace Timtek.SemanticQuantities.Tests.Observatory;

[Subject("WallaceDomeSync basic geometry at equator")]
class when_computing_dome_position_with_trivial_geometry
{
    static ObservatoryGeometry Geo;
    static WallaceDomeSync Sync;

    Establish context = () =>
    {
Geo = new ObservatoryGeometry(
            mountOffsetEast: 0.0,
            mountOffsetNorth: 0.0,
            mountOffsetUp: 0.0,
            domeRadius: 5.0,
            observatoryLatitude: Latitude.FromDegrees(0.0),
            polarDeclinationAxisDistance: 0.0,
            polarOpticalAxisDistance: 0.0,
            declinationOpticalAxisDistance: 0.0
        );
        Sync = new WallaceDomeSync(Geo);
    };

    It returns_zenith_for_HA0_Dec0 = () =>
    {
        var dome = Sync.FromTelescopeMechanicalPosition(HourAngle.FromHours(0.0), Declination.FromDegrees(0.0));
        var az = dome.Azimuth.As<Degree>();
        var el = dome.Elevation.As<Degree>();
        Math.Abs(az - 0.0).ShouldBeLessThan(1e-12);
        Math.Abs(el - 90.0).ShouldBeLessThan(1e-12);
    };
}
