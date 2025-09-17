using Timtek.SemanticQuantities.Units;

namespace Timtek.SemanticQuantities.Registry;

public static class RegistryBootstrapper
{
    public static DefaultDimensionRegistry BuildDefault()
    {
        var r = new DefaultDimensionRegistry();
        // Register coherent SI base and common coherent derived units first
        // Base SI and coherent derived commonly used
        r.Register(new Meter());
        r.Register(new Kilogram());
        r.Register(new Second());
        r.Register(new Ampere());
        r.Register(new Kelvin());
        r.Register(new Mole());
        r.Register(new Candela());
        r.Register(new Radian());

        // Derived coherent
        r.Register(new MeterPerSecond());
        r.Register(new MeterPerSecondSquared());
        r.Register(new Newton());
        r.Register(new Pascal());
        r.Register(new Joule());
        r.Register(new Watt());
        r.Register(new Hertz());
        r.Register(new Coulomb());
        r.Register(new Volt());
        r.Register(new Ohm());
        r.Register(new Siemens());
        r.Register(new Farad());
        r.Register(new Weber());
        r.Register(new Tesla());
        r.Register(new Henry());
        r.Register(new Steradian());
        r.Register(new Lumen());
        r.Register(new Lux());

        // Accepted/non-SI convenience
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
        r.Register(new PoundForce());
        r.Register(new Psi());

        return r;
    }
}
