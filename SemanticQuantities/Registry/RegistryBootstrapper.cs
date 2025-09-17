using Timtek.SemanticQuantities.Units;

namespace Timtek.SemanticQuantities.Registry;

public static class RegistryBootstrapper
{
    public static DefaultDimensionRegistry BuildDefault()
    {
        var r = new DefaultDimensionRegistry();
        // Register coherent SI base and common coherent derived units first
        r.Register(new Meter());
        r.Register(new Kilogram());
        r.Register(new Second());
        r.Register(new Radian());
        r.Register(new MeterPerSecond());
        r.Register(new MeterPerSecondSquared());
        r.Register(new Newton());

        // Non-coherent but useful units (will not override coherent picks since they are already set)
        r.Register(new Kilometer());
        r.Register(new Degree());
        r.Register(new HourOfAngle());
        r.Register(new Minute());
        r.Register(new Hour());
        r.Register(new Day());
        r.Register(new JulianDay());

        // Imperial/customary convenience units (length, mass)
        r.Register(new Foot());
        r.Register(new Inch());
        r.Register(new Pound());
        r.Register(new Ounce());

        return r;
    }
}
